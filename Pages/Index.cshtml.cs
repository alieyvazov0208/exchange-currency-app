using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using CurrencyExchangeApp.Services;
using CurrencyExchangeApp.Models;

namespace CurrencyExchangeApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly NbpApiService _nbp;

        public decimal EurRate { get; set; }

        public IndexModel(ILogger<IndexModel> logger, NbpApiService nbp)
        {
            _logger = logger;
            _nbp = nbp;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/HomeDashboard");
            }

            var rate = await _nbp.GetRateAsync("EUR");
            EurRate = rate?.Rate ?? 0;

            return Page();
        }
    }
}
