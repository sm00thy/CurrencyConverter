namespace CurrencyConverter.Core.Entities
{
    public class CurrencyConvert : BaseEntity
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal BaseCurrencyValue { get; set; }
        public decimal ConvertedCurrencyValue { get; set; }
    }
}