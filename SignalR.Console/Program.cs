using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace SignalR.Console
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                 .WithUrl("https://localhost:44328/streamHub")
                 .ConfigureLogging(logging =>
                 {
                     logging.SetMinimumLevel(LogLevel.Debug);
                 })
                 .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(1000);
                await connection.StartAsync();
            };

            await connection.StartAsync();

            var cancellationTokenSource = new CancellationTokenSource();

            var stream = connection.StreamAsync<string>("SendDataRow", cancellationTokenSource.Token);

            await foreach (var count in stream.WithCancellation(cancellationTokenSource.Token))
            {
                System.Console.WriteLine($"{count}");
            }

            System.Console.WriteLine("Streaming completed");
            System.Console.ReadLine();
        }
    }
}