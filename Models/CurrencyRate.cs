using System;

namespace CurrencyExchangeApp.Models
{
    public class CurrencyRate
    {
        public int Id { get; set; } // Unique ID
        public string CurrencyCode { get; set; } // Example: "USD"
        public decimal Rate { get; set; } // Example: 4.12
        public DateTime DateFetched { get; set; } // When it was fetched
    }
}
