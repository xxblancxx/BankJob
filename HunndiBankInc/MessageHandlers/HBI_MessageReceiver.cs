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
        public static void GetMessage(string _host, string _exchange, string _que)
        {
           
                var factory = new ConnectionFactory() { HostName = _host };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {

                    channel.QueueBind(queue: _que, exchange: _exchange, routingKey: "");

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) => // ea = BasicDeliverEventArgs
                    {

                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);

                        Console.WriteLine("Message contain:\n{0}", message);
                        Console.WriteLine();

                        LoanRequest receviedRequest = HBI_XMLConverter.GetRequestFromXML(message);

                        Console.WriteLine("Message Data: ssn: {0}, creditScore: {1}, loanAmount:{2}, loanDuration: {2}",
                            receviedRequest.ssn, receviedRequest.creditScore, receviedRequest.loanAmount, receviedRequest.loanDuration);

                        Console.WriteLine("Request send to LoanMaker");

                         new HBI_MakeLoan(receviedRequest.ssn, receviedRequest.creditScore, receviedRequest.loanAmount,receviedRequest.loanDuration);

                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine();
                    };

                    channel.BasicConsume(queue: _que, noAck: true, consumer: consumer);
                    // noack = no acknowledgement ---- consumer:consumer = name of consumer

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }


            }
        
    }
}
