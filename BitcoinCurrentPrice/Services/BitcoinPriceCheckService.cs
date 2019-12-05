using System;
using System.Collections.Generic;
using System.Text;
using BitcoinCurrentPrice.Models;

namespace BitcoinCurrentPrice.Services
{
    public class BitcoinPriceCheckService : IBitcoinPriceCheckService
    {
        private IBitcoinUnitOfWork _bitcoinUnitOfWork;
        public BitcoinPriceCheckService(IBitcoinUnitOfWork bitcoinUnitOfWork)
        {
            _bitcoinUnitOfWork = bitcoinUnitOfWork;
        }

        public void AddBitCoinPrice(Time time, Bpi bpi)
        {
            _bitcoinUnitOfWork.TimeRepository.Add(time);
            _bitcoinUnitOfWork.USDRepository.Add(bpi.USD);
            _bitcoinUnitOfWork.GBPRepository.Add(bpi.GBP);
            _bitcoinUnitOfWork.EURRepository.Add(bpi.EUR);
            _bitcoinUnitOfWork.Save();
        }

    }
}
