using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Serilog;

namespace ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/log")
                .ConfigureLogging(config =>
                {
                    config.AddSerilog();
                })
                .Build();

            connection.On("LogEvent", (string message) =>
            {
                System.Console.WriteLine($"Message: {message}");
            });

            connection.Closed += async (ex) =>
            {
                System.Console.WriteLine("Reconnecting...");

                await Task.Delay(TimeSpan.FromSeconds(2).Milliseconds);

                await connection.StartAsync();
            };

            await connection.StartAsync();

            System.Console.WriteLine("Press return to exit");
            Console.ReadLine();
        }
    }
}
