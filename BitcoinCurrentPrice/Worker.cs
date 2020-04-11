using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BitcoinCurrentPrice.Models;
using BitcoinCurrentPrice.Repository;
using BitcoinCurrentPrice.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BitcoinCurrentPrice 
{
    public class Worker : BackgroundService
    {
        private static IServiceProvider _serviceProvider;
        private readonly ILogger<Worker> _logger;
        private HttpClient _client;
        private IBitcoinPriceCheckService _bitcoinPriceCheckService;
        public IConfiguration Configuration { get; }
        public Worker(ILogger<Worker> logger,IConfiguration configuration,IServiceProvider serviceProvider)
        {
            _logger = logger;
            Configuration = configuration;
            _serviceProvider = serviceProvider;
        }
        public void ConfigureServices()
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");

            var services = new ServiceCollection();

            services.AddTransient<IBitcoinUnitOfWork>(x => new BitcoinUnitOfWork(connectionString));
            services.AddTransient<IBitcoinPriceCollect, BitcoinPriceCollect>();
            services.AddTransient<IBitcoinPriceCheckService,BitcoinPriceCheckService>();
            services
            .AddTransient<IBpiRepository, BpiRepository>()
            .AddTransient<IEURRepository, EURRepository>()
            .AddTransient<IGBPRepository, GBPRepository>()
            .AddTransient<IRootObjectRepository, RootObjectRepository>()
            .AddTransient<ITimeRepository, TimeRepository>()
            .AddTransient<IUSDRepository, USDRepository>();

            _serviceProvider = services.BuildServiceProvider();

            _bitcoinPriceCheckService = _serviceProvider.GetRequiredService<IBitcoinPriceCheckService>();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            ConfigureServices();
            _client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _client.Dispose();
            _logger.LogInformation("The service has stopped");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await _client.GetAsync("https://api.coindesk.com/v1/bpi/currentprice.json");
                if (result.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Server is up. status code = " + result.StatusCode);
                    GetItem(_bitcoinPriceCheckService);
                }
                else
                {
                    _logger.LogError("Stopped . Status code = "+result.StatusCode);
                }
                await Task.Delay(6000, stoppingToken);
            }
        }

       public void GetItem(IBitcoinPriceCheckService _bitcoinPriceCheckService)
        {
            const string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            var request = WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            using (var response = request.GetResponse())
            {
                using (var streamItem = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(streamItem))
                    {
                        var result = reader.ReadToEnd();
                        //dynamic s = JsonConvert.DeserializeObject(result);

                        JObject jsonFullObject = JObject.Parse(result);
                        var time = jsonFullObject["time"];
                        var Bpi = jsonFullObject["bpi"];

                        var timeObject = time.ToObject<Time>();
                        var BpiObject = Bpi.ToObject<Bpi>();
                        
                        _bitcoinPriceCheckService.AddBitCoinPrice(timeObject, BpiObject);
                    }
                }
            }
        }
    }
}
