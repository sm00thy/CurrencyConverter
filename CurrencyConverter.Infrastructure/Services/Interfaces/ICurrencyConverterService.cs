using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyConverter.Core.Entities;
using CurrencyConverter.Infrastructure.Commands;
using CurrencyConverter.Infrastructure.Dto;

namespace CurrencyConverter.Infrastructure.Services.Interfaces
{
    public interface ICurrencyConverterService
    {
        Task<IEnumerable<CurrenciesListDto>> GetCurrencyListAsync();
        Task<CurrencyConverterDto> ConvertCurrency(CurrencyToExchange toExchange);
    }
}