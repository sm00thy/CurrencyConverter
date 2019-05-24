using System;

namespace CurrencyConverter.Core.Entities
{
    public class Rate
    {
        public string No { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal Mid { get; set; }
        public CurrencyTable CurrencyTable { get; set; }
        public int RateId { get; set; }
    }
}