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
        public EventingBasicConsumer Consumer { get; set; }

        public static void Recieve(List<string> recievedMessages, string host, string queue)
        {
            var factory = new ConnectionFactory() { HostName = host };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                using (IModel model = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                    //var result = channel.BasicGet(queue: queue, noAck: true);

                    var subscription = new Subscription(model, queue, true);
                    var result = new BasicDeliverEventArgs();
                    subscription.Next(2000,out result);
                    var body = result.Body;
                    var message = UTF8Encoding.UTF8.GetString(body);

                    recievedMessages.Add(message);
                    //Consumer.Received += (model, ea) =>
                    //{
                    //    var body = ea.Body;
                    //    var message = UTF8Encoding.UTF8.GetString(body);
                    //    recievedMessages.Add(message);
                    //};
                    //string myConsumed =  channel.BasicConsume(queue: queue,
                    //                     noAck: true,
                    //                     consumer: Consumer);
                }
            }
        }
    }
}