using System.Collections.Generic;
using CurrencyConverter.Core.Entities;

namespace CurrencyConverter.Infrastructure.Dto
{
    public class CurrencyConverterDto
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal BaseCurrencyValue { get; set; }
        public decimal ConvertedCurrencyValue { get; set; }
    }
}