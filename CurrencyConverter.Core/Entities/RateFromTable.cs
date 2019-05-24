using System;

namespace CurrencyConverter.Core.Entities
{
    public class RateFromTable
    {
        public string Currency { get; set; }
        public string Code { get; set; }
        private DateTime CreatedAt { get; } = DateTime.UtcNow;
    }
}