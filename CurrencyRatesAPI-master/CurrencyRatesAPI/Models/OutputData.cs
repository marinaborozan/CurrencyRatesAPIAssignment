using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyRatesAPI.Models
{
    public class OutputData
    {
        public decimal MinRate { get; set; }
        public decimal MaxRate { get; set; }
        public decimal AverageRate { get; set; }
        public string MaxDate { get; set; }
        public string MinDate { get; set; }
    }
}
