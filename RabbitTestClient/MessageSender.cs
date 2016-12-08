using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using RabbitMQ;
using RabbitMQ.Client;
using System.Xml;
using System.IO;

namespace RabbitTestClient
{
    public class MessageSender
    {
        public static void SendMessage(string hostName, string exchange, Byte[] encodedMessageBody)
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            factory.UserName = "guest";
            factory.Password = "guest";

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var props = channel.CreateBasicProperties();
                props.ReplyTo = "Hundige_Queue";
                channel.BasicPublish(exchange: exchange,
                    routingKey: "",
                    basicProperties: props,
                    body: encodedMessageBody);
            }
        }
    }
}