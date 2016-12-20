using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoanBroker.RuleBase;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using RabbitMQ.Client;

namespace LoanBrokerTests
{
    [TestClass]
    public class LoanBrokerTests
    {
        [TestMethod]
        public void TestBanks()
        {

            var banks = new List<RuleBase.Bank>();
            banks.Add(new RuleBase.Bank("CphXMLBank", 500, 999999999, new DateTime(3000, 01, 01), false, "datdb.cphbusiness.dk", "cphbusiness.bankXML"));
            banks.Add(new RuleBase.Bank("CphJSONBank", 500, 999999999, new DateTime(3000, 01, 01), false, "datdb.cphbusiness.dk", "cphbusiness.bankJSON"));

            // Test loanrequest, as we don't know if banks will respond to all kind of data sent.
            var request = new LoanBroker.LoanRequest("0101011122", 400, 10000, new DateTime(1970, 05, 05));



            foreach (var bank in banks)
            {
                string queueForBank = "HUNDIGETESTQUEUE" + bank.Name;
                // Make sure our queue exists.
                LoanBroker.MessageSender.DeclareQueue(bank.Host, queueForBank);

                if (bank.Exchange.Contains("XML"))
                { // XML Translator is used
                    var encodedMessage = UTF8Encoding.UTF8.GetBytes(LoanBroker.XMLConverter.GetXMLFromLoanRequest(request));
                    LoanBroker.MessageSender.SendMessage(bank.Host, bank.Exchange, queueForBank, encodedMessage);
                }
                else if (bank.Exchange.Contains("JSON"))
                { // JSON Translator is used
                    string json = LoanBroker.JSONConverter.GetJSONFromRequest(request);
                    var encodedMessage = UTF8Encoding.UTF8.GetBytes(json);
                    LoanBroker.MessageSender.SendMessage(bank.Host, bank.Exchange, queueForBank, encodedMessage);
                }
            }

            List<string> responses = new List<string>();
            foreach (var bank in banks)
            {
                string queueForBank = "HUNDIGETESTQUEUE" + bank.Name;
                responses.Add(LoanBroker.MessageReciever.RecieveSpecifiedTimeout(bank.Host, queueForBank, 10000)); // We allow each bank 10 seconds to respond.
            }

            // Remove all Null from responses
            responses = responses.Where(r => r != null).ToList();

            Assert.AreEqual(banks.Count, responses.Count); // If number of banks equals to number of responses recieved, every bank has answered and all banks are online.
        }

        [TestMethod]
        public void TestMessageBroker()
        {
            var factory = new ConnectionFactory() { HostName = "datdb.cphbusiness.dk" };
            var connection = factory.CreateConnection();
        }
    }
}
