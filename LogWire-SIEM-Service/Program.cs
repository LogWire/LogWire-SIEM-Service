using System;
using LogWire.Controller.Client.Configuration;
using LogWire.SIEM.Service.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace LogWire.SIEM.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    string endpoint = Environment.GetEnvironmentVariable("lw_controller_endpoint");
                    string token = Environment.GetEnvironmentVariable("lw_access_token");

                    ApiToken.Instance.Init(token);

                    webBuilder.ConfigureAppConfiguration(config =>
                    {
                        config.AddEnvironmentVariables("lw_");
                        config.AddControllerConfiguration(endpoint, "api", token);
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
