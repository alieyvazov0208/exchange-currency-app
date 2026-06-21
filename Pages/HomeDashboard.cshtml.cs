using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CurrencyExchangeApp.Data;
using CurrencyExchangeApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using CurrencyExchangeApp.Services;

namespace CurrencyExchangeApp.Pages
{
    [Authorize]
    public class HomeDashboardModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _db;
        private readonly NbpApiService _nbp;

        public HomeDashboardModel(UserManager<IdentityUser> userManager, AppDbContext db, NbpApiService nbp)
        {
            _userManager = userManager;
            _db = db;
            _nbp = nbp;
        }

        public string? UserName { get; set; }
        public decimal TotalBalancePLN { get; set; }
        public List<UserWallet> Wallets { get; set; } = new();

        public string RateLabelsJson { get; set; } = "";
        public string RateValuesJson { get; set; } = "";

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            UserName = user?.UserName;

            var userId = _userManager.GetUserId(User);
            Wallets = await _db.Wallets.Where(w => w.UserId == userId).ToListAsync();

            TotalBalancePLN = Wallets.Sum(w => w.Currency == "PLN" ? w.Balance : 0);

            // 📊 Mock chart data (replace with real API data later)
            var sampleRates = new List<decimal> { 4.32m, 4.34m, 4.30m, 4.28m, 4.35m };
            var sampleDays = new List<string> { "Mon", "Tue", "Wed", "Thu", "Fri" };

            RateLabelsJson = JsonSerializer.Serialize(sampleDays);
            RateValuesJson = JsonSerializer.Serialize(sampleRates);
        }
    }
}
