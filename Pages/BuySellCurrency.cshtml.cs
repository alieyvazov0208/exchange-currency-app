using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CurrencyExchangeApp.Models;
using CurrencyExchangeApp.Services;
using CurrencyExchangeApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CurrencyExchangeApp.Pages
{
    [Authorize]
    public class BuySellCurrencyModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly NbpApiService _nbp;
        private readonly UserManager<IdentityUser> _userManager;

        public BuySellCurrencyModel(AppDbContext db, NbpApiService nbp, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _nbp = nbp;
            _userManager = userManager;
        }

        [BindProperty]
        public Transaction Transaction { get; set; } = new();

        public List<UserWallet> Wallets { get; set; } = new();
        public List<Transaction> AllTransactions { get; set; } = new();
        public bool ShowResult { get; set; } = false;

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);

            // ✅ Seed PLN wallet if not exists
            if (!await _db.Wallets.AnyAsync(w => w.UserId == userId && w.Currency == "PLN"))
            {
                _db.Wallets.Add(new UserWallet
                {
                    Currency = "PLN",
                    Balance = 100,
                    UserId = userId
                });
                await _db.SaveChangesAsync();
            }

            Wallets = await _db.Wallets.Where(w => w.UserId == userId).ToListAsync();
            AllTransactions = await _db.Transactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Transaction.Amount <= 0)
            {
                await OnGetAsync();
                return Page();
            }

            var rate = await _nbp.GetRateAsync(Transaction.Currency);
            if (rate == null)
            {
                ModelState.AddModelError("", "Failed to fetch exchange rate.");
                await OnGetAsync();
                return Page();
            }

            decimal totalPLN = Transaction.Amount * rate.Rate;
            Transaction.Rate = rate.Rate;
            Transaction.Date = DateTime.Now;
            Transaction.TotalPLN = totalPLN;

            var userId = _userManager.GetUserId(User);

            var fromCurrency = Transaction.Type == "Buy" ? "PLN" : Transaction.Currency;
            var toCurrency = Transaction.Type == "Buy" ? Transaction.Currency : "PLN";

            var fromWallet = await _db.Wallets.FirstOrDefaultAsync(w => w.Currency == fromCurrency && w.UserId == userId);
            var toWallet = await _db.Wallets.FirstOrDefaultAsync(w => w.Currency == toCurrency && w.UserId == userId);

            if (Transaction.Type == "Buy" && fromWallet != null && fromWallet.Balance < totalPLN)
            {
                ModelState.AddModelError("", $"Not enough PLN to buy {Transaction.Currency}.");
                await OnGetAsync();
                return Page();
            }
            else if (Transaction.Type == "Sell" && fromWallet != null && fromWallet.Balance < Transaction.Amount)
            {
                ModelState.AddModelError("", $"Not enough {Transaction.Currency} to sell.");
                await OnGetAsync();
                return Page();
            }

            if (Transaction.Type == "Buy")
            {
                fromWallet.Balance -= totalPLN;
                if (toWallet == null)
                {
                    toWallet = new UserWallet { Currency = toCurrency, Balance = 0, UserId = userId };
                    _db.Wallets.Add(toWallet);
                }
                toWallet.Balance += Transaction.Amount;
            }
            else
            {
                fromWallet.Balance -= Transaction.Amount;
                if (toWallet == null)
                {
                    toWallet = new UserWallet { Currency = toCurrency, Balance = 0, UserId = userId };
                    _db.Wallets.Add(toWallet);
                }
                toWallet.Balance += totalPLN;
            }

            Transaction.UserId = userId;
            _db.Transactions.Add(Transaction);
            await _db.SaveChangesAsync();

            ShowResult = true;
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostClearHistoryAsync()
        {
            var userId = _userManager.GetUserId(User);

            var userTransactions = _db.Transactions.Where(t => t.UserId == userId);
            _db.Transactions.RemoveRange(userTransactions);

            var userWallets = _db.Wallets.Where(w => w.UserId == userId);
            foreach (var w in userWallets)
                w.Balance = w.Currency == "PLN" ? 100 : 0;

            await _db.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
