using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CurrencyRatesAPI.Models;
using CurrencyRatesAPI.Services;
using System.Net.Http;

namespace CurrencyRatesAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly IRateCalculationService _rateCalculation;
        public RateController(IRateCalculationService rateCalculation)
        {
            _rateCalculation = rateCalculation;
        }

        //This GET method is called upon application run for default route and it shows latest currency rate 
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            IActionResult result;
            try
            {
                string path = "latest";
                var currencyRate = await _rateCalculation.GetRateAsync(path);
                result = Ok(currencyRate);
            }
            catch (Exception e) 
            {
                result = BadRequest(e.Message);
            }
            return result;
        }

        //This GET method is called with /output?dateslist=dateslist&basecurrency=basecurrency&targetcurrency=targetcurrency
        [HttpGet]
        [Route("output")]
        public async Task<IActionResult> GetOutputAsync(string dateslist, string basecurrency, string targetcurrency)
        {
            IActionResult result;
            if (dateslist == null || basecurrency == null || targetcurrency == null)
            {
                result = BadRequest("Invalid URI format: dateslist, basecurrency and targetcurrency parameters cannot be undefined.");
            }
            else
            { 
                try
                {
                    var outputData = await _rateCalculation.GetOutputDataAsync(dateslist, basecurrency, targetcurrency);
                    result = Ok(outputData);
                }
                catch (Exception e)
                {
                    result = BadRequest(e.Message);
                }
            }
            return result;
        }
    }
}
