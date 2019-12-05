using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice.Models
{
    public class RootObject
    {
        public int Id { get; set; }
        public Time Time { get; set; }
        public string disclaimer { get; set; }
        public string chartName { get; set; }
        public Bpi Bpi { get; set; }
    }
}
