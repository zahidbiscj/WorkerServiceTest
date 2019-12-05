using BitcoinCurrentPrice.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice.Services
{
    public interface IBitcoinPriceCheckService
    {
        void AddBitCoinPrice(Time time, Bpi bpi);
    }
}
