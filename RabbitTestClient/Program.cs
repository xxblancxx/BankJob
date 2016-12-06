using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var encodedMessage = UTF8Encoding.UTF8.GetBytes(XMLConverter.GetXMLFromLoanRequest(new LoanRequest(12121992, 800, 10000, new DateTime(1973, 01, 01))));
            MessageSender.SendMessage("datdb.cphbusiness.dk", "cphbusiness.bankXML", encodedMessage);
            Console.WriteLine("Message sent succesfully");
            Console.ReadKey();
        }
    }
}
