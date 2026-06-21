namespace CurrencyExchangeApp.Models
{
    public class UserWallet
    {
        public int Id { get; set; }
        public string Currency { get; set; } = "";
        public decimal Balance { get; set; }

        public string UserId { get; set; } = ""; // ✅ Add this line
    }
}
