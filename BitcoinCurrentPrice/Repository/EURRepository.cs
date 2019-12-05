using BitcoinCurrentPrice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice.Repository
{
    public class EURRepository : IEURRepository
    {
        public BitcoinContext _context;
        public EURRepository(BitcoinContext context)
        {
            _context = context;
        }

        public void Add(EUR eur)
        {
            _context.EURs.Add(eur);
        }
    }
}
