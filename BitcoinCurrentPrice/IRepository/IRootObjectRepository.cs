using BitcoinCurrentPrice.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice.Repository
{
    public interface IRootObjectRepository
    {
        void Add(RootObject rootObject);
    }
}
