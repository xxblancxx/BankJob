using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using RabbitMQ;
using RabbitMQ.Client;
using System.Xml;
using System.IO;

namespace LoanBroker
{
    public class MessageSender
    {


        public static void SendMessage(string hostName, string exchange, string replyto, Byte[] encodedMessageBody)
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            factory.UserName = "guest";
            factory.Password = "guest";

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var props = channel.CreateBasicProperties();
                props.ReplyTo = replyto;
                channel.BasicPublish(exchange: exchange,
                    routingKey: "",
                    basicProperties: props,
                    body: encodedMessageBody);
            }
        }

        public static void DeclareQueue(string hostName, string replyto)
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            factory.UserName = "guest";
            factory.Password = "guest";
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(replyto, false, false, true, null);
            }
        }
    }
}