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


            DateTime loanDuration = new DateTime(1970, 01, 01);
            loanDuration = loanDuration.AddDays(loanDurationInDays);

            // Get Creditscore from webservice
            var creditBureau = new CreditBureau.CreditScoreService();
            int creditScore = creditBureau.creditScore(ssn);

            // Creditscore always returns -1, so we implemented the below piece for test purposes
            //__________Below Code is just for test purpose____________________________________
            Random r = new Random();
            creditScore = r.Next(1, 800);
            //__________Above Code is just for test purpose____________________________________

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
            // Uses translators to LoanResponse objects

            Dictionary<string, LoanResponse> responses = new Dictionary<string, LoanResponse>();

            foreach (var response in recievedResponses)
            {
                // Look for XML style closing tag
                if (response.Value.Contains("</"))
                { // its XML
                    var loanResponse = XMLConverter.GetResponseFromXML(response.Value);
                    responses.Add(response.Key, loanResponse);
                }
                // Look for JSON style closing tag
                else if (response.Value.Contains("}"))
                { // its JSON
                    var loanResponse = JSONConverter.GetResponseFromJSON(response.Value);
                    responses.Add(response.Key, loanResponse);
                }
            }

            //Aggregate to find best rate.
            LoanResponse bestResponse = null;
            string bankName = "";
            foreach (var response in responses)
            {
                if (bestResponse == null)
                {
                    bestResponse = response.Value;
                    bankName = response.Key;
                }
                else
                {
                    if (response.Value.interestRate < bestResponse.interestRate)
                    {
                        bestResponse = response.Value;
                        bankName = response.Key;
                    }
                }
            }

            string returnString = "The best option for a loan is offered by: " + bankName + ", who offer an interest rate of: " + bestResponse.interestRate + "%";
            return returnString;

        }
    }
}
