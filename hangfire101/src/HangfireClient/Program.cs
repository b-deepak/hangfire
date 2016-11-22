using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RabbitMQReceiver;

namespace HangfireClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            GlobalConfiguration
                .Configuration
                //.UseSqlServerStorage(@"Server=.\sqlexpress;Database=Hangfire;Trusted_Connection=True;");                
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireDb"))
                .UseColouredConsoleLogProvider();


            //Queue Job
            //BackgroundJob.Enqueue(() => Console.WriteLine("Hello, world!"));
            BackgroundJob.Enqueue<TopicReceiver>(receiver => receiver.SubscribeAsync());
            //RecurringJob.AddOrUpdate<TopicReceiver>(receiver => receiver.SubscribeAsync(), Cron.MinuteInterval(1));

            //var client = new BackgroundJobClient();
            //client.Enqueue<TopicReceiver>(receiver => receiver.SubscribeAsync());

            Console.WriteLine("Job queued.\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
