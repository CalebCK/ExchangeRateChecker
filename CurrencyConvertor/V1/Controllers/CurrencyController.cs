using System;
using System.Threading.Tasks;
using CurrencyConvertor.Extensions;
using CurrencyConvertor.Extensions.Exceptions;
using CurrencyConvertor.V1.DataTransferObjects;
using CurrencyConvertor.V1.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConvertor.V1.Controllers
{
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        /// <summary>
        /// Get all currency codes and names, codes that'll aid in checking rates
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Get()
        {
            var results = _currencyService.GetCurrencies();

            return Ok(results);
        }

        /// <summary>
        /// Provide comma separated currency symbols as parameter
        /// </summary>
        /// <param name="currencies"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("eurorates")]
        public async Task<ActionResult> ExchangeApiRates(string currencies)
        {
            var results = await _currencyService.GetRatesAsync(currencies, User.Identity.Name);

            if (results.success)
                return Ok(results.response);

            return BadRequest(results.response);
        }
    }
}
