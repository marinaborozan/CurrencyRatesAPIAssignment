using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CurrencyRatesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyRatesAPI.Services
{
    public interface IRateCalculationService
    {
        Task<CurrencyRate> GetRateAsync(string path);
        Task<OutputData> GetOutputDataAsync(string dateslist, string basecurrency, string targetcurrency);
    }
}
