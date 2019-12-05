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
        private IBitcoinPriceCheckService bitcoinPriceCheckService;
        public IConfiguration Configuration { get; }
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        public void ConfigureServices()
        {
            /*var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BitcoinContext>(x => x.UseSqlServer(connectionString));*/
            var services = new ServiceCollection();
            services.AddScoped<BitcoinUnitOfWork>(x => new BitcoinUnitOfWork());

            services.AddScoped<IBitcoinUnitOfWork, BitcoinUnitOfWork>();
            services.AddScoped<IBitcoinPriceCheckService, BitcoinPriceCheckService>();
            services
            .AddScoped<IBpiRepository, BpiRepository>()
            .AddScoped<IEURRepository, EURRepository>()
            .AddScoped<IGBPRepository, GBPRepository>()
            .AddScoped<IRootObjectRepository, RootObjectRepository>()
            .AddScoped<ITimeRepository, TimeRepository>()
            .AddScoped<IUSDRepository, USDRepository>();

            _serviceProvider = services.BuildServiceProvider();

            bitcoinPriceCheckService = _serviceProvider.GetRequiredService<IBitcoinPriceCheckService>();
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
                    GetItem(bitcoinPriceCheckService);
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
