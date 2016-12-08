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
            Console.WriteLine("Click to send message");

            while (true)
            {
                var pressedKey = Console.ReadKey(true);

                if (pressedKey.Key != ConsoleKey.Escape)
                {
                    var encodedMessage = UTF8Encoding.UTF8.GetBytes(XMLConverter.GetXMLFromLoanRequest(new LoanRequest(12121992, 257, 10000, new DateTime(1970, 05, 05))));
                    MessageSender.SendMessage("datdb.cphbusiness.dk", "HBITestExchange", encodedMessage);
                    Console.WriteLine("Message sent succesfully, press to send second");
                }

            }




        }
    }
}
