using BitcoinCurrentPrice.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice
{
    public class BitcoinUnitOfWork : IBitcoinUnitOfWork
    {
        protected readonly BitcoinContext _context;
        public IBpiRepository BpiRepository { get; set; }
        public IEURRepository EURRepository { get; set; }
        public IGBPRepository GBPRepository { get; set; }
        public IRootObjectRepository RootObjectRepository { get; set; }
        public ITimeRepository TimeRepository { get; set; }
        public IUSDRepository USDRepository { get; set; }

        public BitcoinUnitOfWork(string connectionString)
        {
            _context = new BitcoinContext(connectionString);

            BpiRepository = new BpiRepository(_context);
            EURRepository = new EURRepository(_context);
            GBPRepository = new GBPRepository(_context);
            RootObjectRepository = new RootObjectRepository(_context);
            TimeRepository = new TimeRepository(_context);
            USDRepository = new USDRepository(_context);
        }

        public void Save() => _context.SaveChanges();
    }
}
