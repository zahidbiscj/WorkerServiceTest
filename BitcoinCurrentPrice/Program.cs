using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BitcoinCurrentPrice.Repository;
using BitcoinCurrentPrice.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace BitcoinCurrentPrice
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(@"D:\temp\workerService\logfile.txt")
                .CreateLogger();

            try
            {
                Log.Information("Starting the service");
                CreateHostBuilder(args).Build().Run();
                return;
            }
            catch (Exception e)
            {
                Log.Fatal(e, "There was a problem in Starting the service");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseWindowsService()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHostedService<Worker>();
               })
           .UseSerilog();
        }
    }
}













/*services.AddTransient<IBitcoinUnitOfWork, BitcoinUnitOfWork>();
                    services.AddTransient<IBitcoinPriceCheckService, BitcoinPriceCheckService>();
                    services
                    .AddTransient<IBpiRepository, BpiRepository>()
                    .AddTransient<IEURRepository, EURRepository>()
                    .AddTransient<IGBPRepository, GBPRepository>()
                    .AddTransient<IRootObjectRepository, RootObjectRepository>()
                    .AddTransient<ITimeRepository, TimeRepository>()
                    .AddTransient<IUSDRepository, USDRepository>();*/
