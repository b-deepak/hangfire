using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQReceiver
{
    public class TopicReceiver
    {
        public TopicReceiver()
        { }

        //public async Task<string> SubscribeAsync()
        public async Task SubscribeAsync()
        {
            //return await Task.Run(() =>
            await Task.Run(() =>
            {
                try
                {
                    var factory = new ConnectionFactory();
                    factory.UserName = "guest";
                    factory.Password = "guest";
                    factory.VirtualHost = "/";
                    factory.HostName = "52.175.209.90";
                    factory.Port = 5672;

                    using (var connection = factory.CreateConnection())
                    {
                        Debug.WriteLine("RabbitMQ Broker Connection Established.");
                        using (var channel = connection.CreateModel())
                        {
                            channel.ExchangeDeclare(
                                exchange: "orders-exchange",
                                type: "topic",
                                durable: true,
                                autoDelete: false,
                                arguments: null);

                            var queueName = "order-created";//channel.QueueDeclare().QueueName;
                            var routingKey = "orders.created";

                            Debug.WriteLine("Queue Name:" + queueName);

                            channel.QueueBind(
                                exchange: "orders-exchange",
                                queue: queueName,
                                routingKey: routingKey);

                            Debug.WriteLine("Queue Bound:" + queueName);

                            var message = string.Empty;
                            var consumer = new EventingBasicConsumer(channel);

                            consumer.Received += (model, ea) =>
                            {
                                var body = ea.Body;
                                message = Encoding.UTF8.GetString(body);
                                Debug.WriteLine("Message Received:" + message);
                            };

                            channel.BasicConsume(
                                queue: queueName,
                                noAck: true,
                                consumer: consumer);

                            //run forever
                            Debug.WriteLine("Listening for messages...");
                            while (true)
                            { }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("Exception Occurred:" + ex.ToString());
                }
            }).ConfigureAwait(false);
        }

    }
}
