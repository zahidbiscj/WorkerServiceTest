using BitcoinCurrentPrice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice.Repository
{
    public class GBPRepository : IGBPRepository
    {
        public BitcoinContext _context;
        public GBPRepository(BitcoinContext context)
        {
            _context = context;
        }

        public void Add(GBP gbp)
        {
            _context.GBPs.Add(gbp);
        }
    }
}
