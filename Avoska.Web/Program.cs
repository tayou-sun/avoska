using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;

namespace Avoska.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
             CreateWebHostBuilder(args).Build().Run();

    /*         using MeterProvider meterProvider = Sdk.CreateMeterProviderBuilder()
                .AddMeter("HatCo.HatStore")
                .AddPrometheusExporter(opt =>
                {
                    opt.StartHttpListener = true;
                    opt.HttpListenerPrefixes = new string[] { $"http://localhost:9184/" };
                })
                .Build();

            CreateWebHostBuilder(args).Build().Run(); */

            // Console.WriteLine("Press any key to exit");
            /*    while (!Console.KeyAvailable)
               {
                   // Pretend our store has a transaction each second that sells 4 hats
                   Thread.Sleep(1000);
                   s_hatsSold.Add(4);
               } */
        }

        static Meter s_meter = new Meter("HatCo.HatStore", "1.0.0");
        static Counter<int> s_hatsSold = s_meter.CreateCounter<int>(name: "hats-sold",
                                                                    unit: "Hats",
                                                                    description: "The number of hats sold in our store");

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseUrls("https://*:51820", "http://*:51821");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
