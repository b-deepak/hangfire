using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

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

            //Process Job
            using (var server = new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
