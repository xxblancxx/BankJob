using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace HunndiBankInc
{
    public class HBI_MessageSender
    {
        public static void SendMessage(string replyTo, string _host, Byte[] encodedMessageBody)
        {
            var factory = new ConnectionFactory() { HostName = _host };
            factory.UserName = "guest";
            factory.Password = "guest";

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(replyTo, false, false,true, null);

                channel.BasicPublish(exchange: "",
                                routingKey: replyTo, 
                                basicProperties: null,
                                body: encodedMessageBody);
            }
        }

    }
}
