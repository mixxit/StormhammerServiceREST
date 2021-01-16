using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace StormhammerServiceREST
{
    public class Program
    {
        public static void Main(string[] args) => BuildWebHost(args).Run();

        public static IWebHost BuildWebHost(string[] args)
        {
            var Configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
               .AddCommandLine(args)
               .AddEnvironmentVariables()
               .Build();

            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    IWebHostEnvironment env = builderContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                    logging.AddSentry(
                        o =>
                        {
                            o.Dsn = Configuration["Sentry:Dsn"];
                            o.MinimumBreadcrumbLevel = Enum.Parse<LogLevel>(Configuration["Sentry:MinimumBreadcrumbLevel"]);
                            o.MinimumEventLevel = Enum.Parse<LogLevel>(Configuration["Sentry:MinimumEventLevel"]);
                            o.MaxBreadcrumbs = int.Parse(Configuration["Sentry:MaxBreadcrumbs"]);
                            o.Environment = Configuration["Sentry:Environment"];
                        });
                }
                )
                /*.UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 80);
                })*/
                .Build();
        }
    }
}
