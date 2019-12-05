using BitcoinCurrentPrice.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice
{
    public interface IBitcoinUnitOfWork
    {
        public IBpiRepository BpiRepository { get; set; }
        public IEURRepository EURRepository { get; set; }
        public IGBPRepository GBPRepository { get; set; }
        public IRootObjectRepository RootObjectRepository { get; set; }
        public ITimeRepository TimeRepository { get; set; }
        public IUSDRepository USDRepository { get; set; }
        void Save();
    }
}
