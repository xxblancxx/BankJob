using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LoanBroker
{
    public class MessageReciever
    {
        public static void Recieve(List<string> recievedMessages,string host, string queue)
        {
            var factory = new ConnectionFactory() { HostName = host };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = UTF8Encoding.UTF8.GetString(body);
                    recievedMessages.Add(message);
                };
                channel.BasicConsume(queue: queue,
                                     noAck: true,
                                     consumer: consumer);
            
            }
        }
    }
}