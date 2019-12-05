using BitcoinCurrentPrice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice.Repository
{
    public class BpiRepository : IBpiRepository
    {
        public BitcoinContext _context;
        public BpiRepository(BitcoinContext context)
        {
            _context = context;
        }

        public void Add(Bpi bpi)
        {

        }
    }
}
