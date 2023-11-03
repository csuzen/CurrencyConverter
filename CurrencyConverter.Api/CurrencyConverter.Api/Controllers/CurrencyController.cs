using CurrencyConverter.Core.Domain;
using CurrencyConverter.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("{currencyText}", Name = "GetWord")]
        public IActionResult Get(string currencyText)
        {
            return new ObjectResult(_currencyService.GetWord(new Currency(currencyText)));
        }
    }
}
