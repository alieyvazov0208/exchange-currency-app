using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyExchangeApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWalletToPLN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Currency",
                value: "PLN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: 1,
                column: "Currency",
                value: "USD");
        }
    }
}
