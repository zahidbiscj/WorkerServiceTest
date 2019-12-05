using BitcoinCurrentPrice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice.Repository
{
    public class TimeRepository : ITimeRepository
    {
        public BitcoinContext _context;
        public TimeRepository(BitcoinContext context)
        {
            _context = context;
        }

        public void Add(Time time)
        {
            _context.Times.Add(time);
        }
    }
}
