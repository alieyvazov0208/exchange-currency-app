using Microsoft.AspNetCore.Mvc.RazorPages;
using CurrencyExchangeApp.Services;
using CurrencyExchangeApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyExchangeApp.Pages
{
    public class LiveRatesModel : PageModel
    {
        private readonly NbpApiService _nbpApi;

        public LiveRatesModel(NbpApiService nbpApi)
        {
            _nbpApi = nbpApi;
        }

        public List<CurrencyRate> LiveCurrencyRates { get; set; } = new();

        public async Task OnGetAsync()
        {
            var currencies = new[] { "USD", "EUR", "GBP", "CHF", "JPY" };

            foreach (var code in currencies)
            {
                var rate = await _nbpApi.GetRateAsync(code);
                if (rate != null)
                {
                    LiveCurrencyRates.Add(rate);
                }
            }
        }
    }
}
