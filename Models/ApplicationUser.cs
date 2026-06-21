using Microsoft.AspNetCore.Identity;

namespace CurrencyExchangeApp.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public decimal BalanceUSD { get; set; } = 100;
    }
}
