using System;

namespace CurrencyExchangeApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public string Type { get; set; } // "Buy" or "Sell"
        public string Currency { get; set; }
        public decimal Amount { get; set; } // Foreign currency amount
        public decimal Rate { get; set; }   // PLN exchange rate at time
        public decimal TotalPLN { get; set; } // Amount * Rate
        public DateTime Date { get; set; } = DateTime.Now;

        public string UserId { get; set; } = ""; // ✅ Link transaction to specific user
    }
}
