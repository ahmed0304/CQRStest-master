using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            // BuildWebHost(args).Run();
        }

        //public static IWebHost BuildWebHost(string[] args)
        //{
        //    var builder = WebHost.CreateDefaultBuilder(args);

        //    builder.ConfigureServices(s => s.AddSingleton(builder))
        //            .ConfigureAppConfiguration(
        //                  ic => ic.AddJsonFile(Path.Combine("configuration.json")))
        //            .UseStartup<Startup>();
        //    var host = builder.Build();
        //    return host;
        //}
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                   webBuilder.UseStartup<Startup>();
                   webBuilder.ConfigureAppConfiguration(config =>
                       config.AddJsonFile($"ocelot.{"dev"}.json"));
               })
           .ConfigureLogging(logger => logger.AddConsole());
    }
}
