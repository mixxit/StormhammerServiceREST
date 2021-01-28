using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using StormhammerAPIClient;

namespace ZoneClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Missing host:port");
                return;
            }

            //var app = new App(args[0]);
            //app.Run();
        }
    }
}
