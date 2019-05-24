using System.Collections.Generic;

namespace CurrencyConverter.Core.Entities
{
    public class CurrencyTable : BaseEntity
    {
        public string Table { get; set; }
        public string Currency { get; set; }
        public string Code { get; set; }
        public List<Rate> Rates { get; set; }
    }
}