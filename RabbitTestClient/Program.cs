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
            //var encodedMessage = UTF8Encoding.UTF8.GetBytes(XMLConverter.GetXMLFromLoanRequest(new LoanRequest(12121992, 800, 10000, new DateTime(1973, 01, 01))));
            Console.WriteLine("Click to send message");
            //Console.ReadKey();

            //var encodedMessage = UTF8Encoding.UTF8.GetBytes(XMLConverter.GetXMLFromLoanRequest(new LoanRequest(12121992, 800, 10000, DateTime.Now)));
            //MessageSender.SendMessage("datdb.cphbusiness.dk", "HBITestExchange", encodedMessage);
            //Console.WriteLine("Message sent succesfully, press to send second");

            while (true)
            {

                if (Console.ReadKey(true).Key != ConsoleKey.Escape)
                {
                    var encodedMessage = UTF8Encoding.UTF8.GetBytes(XMLConverter.GetXMLFromLoanRequest(new LoanRequest(12121992, 800, 10000, DateTime.Now)));
                    MessageSender.SendMessage("datdb.cphbusiness.dk", "HBITestExchange", encodedMessage);
                    Console.WriteLine("Message sent succesfully, press to send second");
                }

            }


            //Console.ReadKey();

            //encodedMessage = UTF8Encoding.UTF8.GetBytes(XMLConverter.GetXMLFromLoanRequest(new LoanRequest(22587946, 800, 92546, DateTime.Now)));

            //MessageSender.SendMessage("datdb.cphbusiness.dk", "HBITestExchange", encodedMessage);

            //Console.WriteLine("Message sent succesfully");
            //Console.ReadKey();

        }
    }
}
