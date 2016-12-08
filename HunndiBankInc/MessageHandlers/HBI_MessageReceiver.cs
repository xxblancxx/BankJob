using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using HunndiBankInc.MakeLoan;

namespace HunndiBankInc
{
    class HBI_MessageReceiver
    {
        public static void GetMessages(string _host, string _que)
        {
            LoanRequest receviedRequest = null;
            var factory = new ConnectionFactory() { HostName = _host };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _que,
                                   durable: true,
                                   exclusive: false,
                                   autoDelete: false,
                                   arguments: null);
                // channel.QueueBind(queue: _que, exchange: "HBITestExchange", routingKey: "");

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) => // ea = BasicDeliverEventArgs
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);

                    receviedRequest = HBI_XMLConverter.GetRequestFromXML(message);
                   
                    Console.WriteLine(message);
                    var encodedResponse = HBI_XMLConverter.GetXMLFromLoanResponse(new LoanResponse(receviedRequest.ssn, HBI_MakeLoan.CalculateCreditscore(receviedRequest)));
                    HBI_MessageSender.SendMessage(ea.BasicProperties.ReplyTo, _host, UTF8Encoding.UTF8.GetBytes(encodedResponse));
                };

                channel.BasicConsume(queue: _que, noAck: true, consumer: consumer);
                Console.ReadKey(true);
            }
        }
    }
}
