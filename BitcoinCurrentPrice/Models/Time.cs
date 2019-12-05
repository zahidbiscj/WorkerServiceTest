using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinCurrentPrice.Models
{
    public class Time
    {
        public int Id { get; set; }
        public string updated { get; set; }
        public string updatedISO { get; set; }
        public string updateduk { get; set; }
        public IList<RootObject> RootObjects { get; set; }
    }
}
