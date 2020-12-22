using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CurrencyRatesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CurrencyRatesAPI.Services
{
    public class RateCalculationService : IRateCalculationService
    {
        private static HttpClient client;  
        
        static RateCalculationService() 
        {
            ConfigureHttpClient();
        }

        static void ConfigureHttpClient() 
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://api.exchangeratesapi.io/")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
       
        public async Task<CurrencyRate> GetRateAsync(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var rate = JsonConvert.DeserializeObject<CurrencyRate>(jsonString);
                return rate;
            }
            else
            {
                throw new Exception("Invalid URI format.");
            }
        }
   
        public async Task<OutputData> GetOutputDataAsync(string dateslist, string basecurrency, string targetcurrency)
        {
            RateDetails rateDetails = new RateDetails();
            List<string> dates = dateslist.Split(',').ToList();

            foreach (string date in dates)
            {
                var currencyRate = await GetRate(date, basecurrency, targetcurrency);
                CalculateCurrency(date, rateDetails, currencyRate);
            }
            if (dates.Count() != 0)
            {
                rateDetails.AverageRate /= dates.Count();
            }

            OutputData outputData = new OutputData
            {
                MaxRate = rateDetails.MaxRate,
                MaxDate = rateDetails.MaxDate,
                MinRate = rateDetails.MinRate,
                MinDate = rateDetails.MinDate,
                AverageRate = decimal.Round(rateDetails.AverageRate, 12)
            };
            return outputData;
        }
        
        private Task<CurrencyRate> GetRate(string date, string basecurrency, string targetcurrency)
        {
            string path = string.Format("{0}?base={1}&symbols={2}", date, basecurrency, targetcurrency);
            return GetRateAsync(path);
        }

        private void CalculateCurrency(string date, RateDetails details, CurrencyRate currencyRate)
        {
            decimal currencyOnDate = currencyRate.Rates.Values.FirstOrDefault();
            if (currencyOnDate > details.MaxRate)
            {
                details.MaxRate = currencyOnDate;
                details.MaxDate = date;
            }
            if (currencyOnDate < details.MinRate)
            {
                details.MinRate = currencyOnDate;
                details.MinDate = date;
            }
            details.AverageRate += currencyOnDate;
        }

        public class RateDetails
        {
            public RateDetails()
            {
                MaxRate = decimal.MinValue;
                MinRate = decimal.MaxValue;
                AverageRate = 0;
                MaxDate = null;
                MinDate = null;
            }
            public decimal MaxRate { get; set; }
            public decimal MinRate { get; set; }
            public decimal AverageRate { get; set; }
            public string MaxDate { get; set; }
            public string MinDate { get; set; }
        }
    }
}
