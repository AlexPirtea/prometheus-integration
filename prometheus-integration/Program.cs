using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Prometheus.DotNetRuntime;

namespace prometheus_integration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotNetRuntimeStatsBuilder.Customize()
                   .WithThreadPoolSchedulingStats()
                   .WithContentionStats()
                   .WithGcStats()
                   .WithJitStats()
                   .WithThreadPoolStats()
                   .WithErrorHandler(ex => Console.WriteLine("ERROR: " + ex.ToString()))
                   //.WithDebuggingMetrics(true);
                   .StartCollecting();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel()
                              .UseUrls("http://*:5090")  ;
                    webBuilder.UseStartup<Startup>();
                });
    }
}
