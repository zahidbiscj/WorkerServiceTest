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

namespace BitcoinCurrentPrice
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                });
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
