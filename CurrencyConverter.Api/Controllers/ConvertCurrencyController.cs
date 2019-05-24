using System.Threading.Tasks;
using CurrencyConverter.Infrastructure.Commands;
using CurrencyConverter.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConvertCurrencyController : Controller
    {
        private readonly ICurrencyConverterService _converterService;
        public ConvertCurrencyController(ICurrencyConverterService converterService)
        {
            _converterService = converterService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAvailableCurrencies()
        {
            var data = await _converterService.GetCurrencyListAsync();
            return Ok(data);
        }
        
        [HttpPost]
        public async Task<IActionResult> ConvertCurrency(CurrencyToExchange toExchange)
        {
            var data = await _converterService.ConvertCurrency(toExchange);
            return Ok(data);
        }
    }
}