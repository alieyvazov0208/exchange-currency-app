using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CurrencyExchangeApp.Services;

namespace CurrencyExchangeApp.Pages
{
    public class FetchRateModel : PageModel
    {
        private readonly NbpApiService _nbpApi;

        public FetchRateModel(NbpApiService nbpApi)
        {
            _nbpApi = nbpApi;
        }

        [BindProperty]
        public string CurrencyCode { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            CurrencyCode = CurrencyCode?.Trim().ToUpper();

            if (string.IsNullOrEmpty(CurrencyCode))
            {
                Message = "❌ Please enter a currency code (e.g., USD, EUR).";
                return Page();
            }

            if (CurrencyCode.Length != 3 || !CurrencyCode.All(char.IsLetter))
            {
                Message = "❌ Currency code must be exactly 3 letters (A–Z).";
                return Page();
            }

            var rate = await _nbpApi.GetRateAsync(CurrencyCode);
            if (rate == null)
            {
                Message = $"❌ Could not fetch rate for '{CurrencyCode}'.";
                return Page();
            }

            Message = $"✅ Rate for {CurrencyCode}: {rate.Rate}";
            return Page();
        }
    }
}
