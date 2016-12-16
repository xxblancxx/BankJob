using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace HunndiBankInc
{
    class HBI_Banking
    {
        static void Main(string[] args)
        {

            HBI_MessageReceiver.GetMessages("datdb.cphbusiness.dk", "Hunndi.Bankqueue");
           
        }
    }
}
