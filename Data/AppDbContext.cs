using CurrencyExchangeApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeApp.Data
{
    // Inherit from IdentityDbContext instead of DbContext
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<UserWallet> Wallets { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed starting balance
            modelBuilder.Entity<UserWallet>().HasData(
                new UserWallet { Id = 1, Currency = "PLN", Balance = 100 }
            );
        }
    }
}
