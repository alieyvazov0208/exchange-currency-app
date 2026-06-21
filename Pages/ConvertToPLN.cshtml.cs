using Microsoft.AspNetCore.Mvc.RazorPages;
using CurrencyExchangeApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyExchangeApp.Models;

namespace CurrencyExchangeApp.Pages
{
    public class ConvertToPLNModel : PageModel
    {
        private readonly NbpApiService _nbpApi;

        public ConvertToPLNModel(NbpApiService nbpApi)
        {
            _nbpApi = nbpApi;
        }

        public Dictionary<string, decimal> ExchangeRates { get; set; } = new();

        public async Task OnGetAsync()
        {
            var currencies = new[] { "PLN", "USD", "EUR", "GBP", "CHF", "JPY" };

            ExchangeRates["PLN"] = 1.0m; // base

            foreach (var code in currencies)
            {
                if (code == "PLN") continue;

                var rate = await _nbpApi.GetRateAsync(code);
                if (rate != null)
                {
                    ExchangeRates[code] = rate.Rate;
                }
            }
        }
    }
}
