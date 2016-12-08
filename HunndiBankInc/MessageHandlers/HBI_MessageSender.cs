using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace HunndiBankInc
{
    class HBI_MessageSender
    {
        public static void SendMessage(string _host, string _exchange, Byte[] encodedMessageBody)
        {
            var factory = new ConnectionFactory() { HostName = _host };
            factory.UserName = "guest";
            factory.Password = "guest";

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var props = channel.CreateBasicProperties();
                props.ReplyTo = ""; 
                channel.BasicPublish(exchange: _exchange,
                    routingKey: "",
                    basicProperties: props,
                    body: encodedMessageBody);
            }
        }


    }
}
