namespace CurrencyConverter.Infrastructure.Commands
{
    public class CurrencyToExchange
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Amount { get; set; }
    }
}