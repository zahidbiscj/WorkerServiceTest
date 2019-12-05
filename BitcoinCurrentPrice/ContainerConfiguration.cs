using Autofac;
using Autofac.Extensions.DependencyInjection;
using BitcoinCurrentPrice.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice
{
    public static class ContainerConfiguration
    {
        public static IServiceProvider Configure()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(x => x.AddConsole())
                .Configure<LoggerFilterOptions>(c => c.MinLevel = LogLevel.Trace);

            var builder = new ContainerBuilder();
            builder.Populate(serviceCollection);

            builder.RegisterType<BitcoinUnitOfWork>().As<IBitcoinUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<BitcoinPriceCheckService>().As<IBitcoinPriceCheckService>().SingleInstance();
            builder.RegisterType<BitcoinPriceCollect>().As<IBitcoinPriceCollect>().InstancePerLifetimeScope();

            var container = builder.Build();

            return new AutofacServiceProvider(container);
        }

    }
}
