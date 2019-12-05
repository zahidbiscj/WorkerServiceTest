using BitcoinCurrentPrice.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice.Repository
{
    public interface IBpiRepository
    {
        void Add(Bpi bpi);
    }
}
