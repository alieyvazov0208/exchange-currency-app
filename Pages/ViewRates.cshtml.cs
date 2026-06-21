using Microsoft.AspNetCore.Mvc.RazorPages;
using CurrencyExchangeApp.Data;
using CurrencyExchangeApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeApp.Pages
{
    public class ViewRatesModel : PageModel
    {
        private readonly AppDbContext _context;

        public ViewRatesModel(AppDbContext context)
        {
            _context = context;
        }

        public List<CurrencyRate> Rates { get; set; }

        public async Task OnGetAsync()
        {
            Rates = await _context.CurrencyRates
                .OrderByDescending(r => r.DateFetched)
                .ToListAsync();
        }
    }
}
