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
            List<string> recievedResponses = new List<string>();
            
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
                
                if (bank.UsesMessaging)
                { // Use AMQP Messaging protocol (RabbitMQ broker)
                    MessageSender.DeclareQueue(bank.Host, ssn);
                    if (bank.Exchange.Contains("XML"))
                    { // XML Translator is used
                        var encodedMessage = UTF8Encoding.UTF8.GetBytes(XMLConverter.GetXMLFromLoanRequest(request));
                        MessageSender.SendMessage(bank.Host, bank.Exchange, ssn, encodedMessage);
                    }
                    else if (bank.Exchange.Contains("JSON"))
                    { // JSON Translator is used
                        string json = JSONConverter.GetJSONFromRequest(request);
                        var encodedMessage = UTF8Encoding.UTF8.GetBytes(json);
                        MessageSender.SendMessage(bank.Host, bank.Exchange, ssn.ToString(), encodedMessage);
                    }

                   MessageReciever.Recieve(recievedResponses, bank.Host, ssn);
                }
                else
                { // Use soap
                    var myResponse = DynamicSoapRequestHandler.SendSoapMessage(bank.Host, "RequestLoan", request).Result;
                    recievedResponses.Add(myResponse);
                }

            }
            if (recipientList[0].Exchange != null)
            {
                return recipientList[0].Exchange;
            }
            else
            {
                return recipientList[0].Host;
            }

        }
    }
}
