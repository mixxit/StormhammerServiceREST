using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using StormhammerAPIClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZoneClientTest
{
    public class WorldConnector
    {
        private string hostandport;
        private string token;

        public event Action OnMessage;
        public WorldConnector(string token, string hostandport)
        {
            this.hostandport = hostandport;
            this.token = token;
        }
        public void Listen()
        {
            var connection = new HubConnectionBuilder()
                        .WithUrl($"https://{hostandport}/ZoneHub", options =>
                        {
                            options.AccessTokenProvider = () => Task.FromResult(token);
                        })
                        .AddMessagePackProtocol()
                        .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {

            });

            connection.StartAsync().Wait();

            Console.WriteLine("Client connected!");

            string line = null;
            while ((line = System.Console.ReadLine()) != null)
            {
                connection.InvokeAsync("HelloMethod", line);
            }
        }

    }
}
