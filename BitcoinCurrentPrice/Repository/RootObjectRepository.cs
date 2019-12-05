using BitcoinCurrentPrice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice.Repository
{
    public class RootObjectRepository : IRootObjectRepository
    {
        public DbContext _context;
        public RootObjectRepository(DbContext context)
        {
            _context = context;
        }

        public void Add(RootObject rootObject)
        {
            throw new NotImplementedException();
        }
    }
}
