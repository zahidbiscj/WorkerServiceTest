using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BitcoinCurrentPrice.Models
{
    public class Bpi
    {
        public int Id { get; set; }
        public USD USD { get; set; }
        public GBP GBP { get; set; }
        public EUR EUR { get; set; }
        public IList<RootObject> RootObjects { get; set; }
    }
}
