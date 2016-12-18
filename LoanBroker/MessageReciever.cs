using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.MessagePatterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LoanBroker
{
    public class MessageReciever
    {

        public static string Recieve(string host, string queue)
        {
            var factory = new ConnectionFactory() { HostName = host };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                using (IModel model = connection.CreateModel())
                {
                    // Create or use existing queue
                    channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: true,
                                     arguments: null);

                    // Create subscription that can look for message in queue.
                    var subscription = new Subscription(model, queue, true);
                    var result = new BasicDeliverEventArgs(); // Output is held in this variable.

                    // Subscription waits for next incoming message, timeout is set to 2000ms
                    subscription.Next(2000, out result);
                    if (result != null)
                    {
                        var body = result.Body;
                        var message = UTF8Encoding.UTF8.GetString(body);
                        return message;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}