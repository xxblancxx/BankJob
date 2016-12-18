using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Services;

namespace LoanBroker
{
    /// <summary>
    /// Summary description for LoanBroker
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LoanBroker : System.Web.Services.WebService
    {

        [WebMethod]
        public string RequestLoan(string ssn, bool isInRKI, double loanAmount, int loanDurationInDays)
        {
            Dictionary<string, string> recievedResponses = new Dictionary<string, string>();
            if (ssn.Length != 11 && ssn[6]!='-')
            {
                return "ssn MUST be in the format XXXXXX-XXXX";
            }

            DateTime loanDuration = new DateTime(1970, 01, 01);
            loanDuration = loanDuration.AddDays(loanDurationInDays);

            // Get Creditscore from webservice
            var creditBureau = new CreditBureau.CreditScoreService();
            int creditScore = creditBureau.creditScore(ssn);
            ssn = ssn.Replace("-","");

            // Get Banks from rulebase
            var rulebase = new RuleBase.RuleBase();
            var recipientList = rulebase.GetBanks(creditScore, isInRKI, loanAmount, loanDuration).ToList();

            var request = new LoanRequest(ssn, creditScore, loanAmount, loanDuration);

            // Translate and send.

            foreach (var bank in recipientList)
            {
                string queueForBank = ssn + "." + bank.Name;

                if (bank.UsesMessaging)
                { // Use AMQP Messaging protocol (RabbitMQ broker)
                    MessageSender.DeclareQueue(bank.Host, queueForBank);

                    if (bank.Exchange.Contains("XML"))
                    { // XML Translator is used
                        var encodedMessage = UTF8Encoding.UTF8.GetBytes(XMLConverter.GetXMLFromLoanRequest(request));
                        MessageSender.SendMessage(bank.Host, bank.Exchange, queueForBank, encodedMessage);
                    }
                    else if (bank.Exchange.Contains("JSON"))
                    { // JSON Translator is used
                        string json = JSONConverter.GetJSONFromRequest(request);
                        var encodedMessage = UTF8Encoding.UTF8.GetBytes(json);
                        MessageSender.SendMessage(bank.Host, bank.Exchange, queueForBank, encodedMessage);
                    }
                }
                else
                { // Use http request to contact soap webservice.
                    var myResponse = DynamicSoapRequestHandler.SendSoapMessage(bank.Host, "RequestLoan", request).Result;
                    recievedResponses.Add(bank.Name, myResponse);
                }
            }

            // Recieve messages
            foreach (var bank in recipientList)
            {
                if (bank.UsesMessaging)
                {
                    string queueForBank = ssn + "." + bank.Name;
                    string response = MessageReciever.Recieve(bank.Host, queueForBank);
                    recievedResponses.Add(bank.Name, response);
                }
            }

            // Normalize
            Dictionary<string, LoanResponse> responses = Normalizer.Normalize(recievedResponses); // Uses translators to get LoanResponse objects

            //Aggregate to find best rate.
            string bestInterestInformation = Aggregator.FindBestInterestRate(responses);

            return bestInterestInformation;
        }
    }
}
