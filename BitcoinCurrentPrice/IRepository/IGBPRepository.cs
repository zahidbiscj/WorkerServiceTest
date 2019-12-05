using BitcoinCurrentPrice.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice.Repository
{
    public interface IGBPRepository
    {
        void Add(GBP gbp);
    }
}
