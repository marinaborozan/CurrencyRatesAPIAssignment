using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyRatesAPI.Models
{
    public class CurrencyRate
    {
        public Dictionary<string, decimal> Rates { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
    }
}
