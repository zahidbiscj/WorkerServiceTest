using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BitcoinCurrentPrice.Models
{
    public class GBP
    {
        public int Id { get; set; }
        public string code { get; set; }
        public string symbol { get; set; }
        public string rate { get; set; }
        public string description { get; set; }
        public double rate_float { get; set; }
        public IList<Bpi> Bpis { get; set; }
    }

}
