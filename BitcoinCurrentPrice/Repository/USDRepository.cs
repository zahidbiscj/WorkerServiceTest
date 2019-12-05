using BitcoinCurrentPrice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice.Repository
{
    public class USDRepository : IUSDRepository
    {
        public BitcoinContext _context;
        public USDRepository(BitcoinContext context)
        {
            _context = context;
        }

        public void Add(USD usd)
        {
            _context.USDs.Add(usd);
        }
    }
}
