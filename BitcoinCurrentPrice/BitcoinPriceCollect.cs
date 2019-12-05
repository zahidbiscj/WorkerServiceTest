using BitcoinCurrentPrice.Models;
using BitcoinCurrentPrice.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BitcoinCurrentPrice
{
    public class BitcoinPriceCollect : IBitcoinPriceCollect
    {
        private IBitcoinPriceCheckService _bitcoinPriceCheckService;

        public BitcoinPriceCollect(IBitcoinPriceCheckService bitcoinPrice)
        {
            _bitcoinPriceCheckService = bitcoinPrice;
        }

        public void GetItem()
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
