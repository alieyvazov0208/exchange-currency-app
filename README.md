# CurrencyExchangeApp

CurrencyExchangeApp is an ASP.NET Core Razor Pages web application for viewing live currency exchange rates, converting currencies and simulating buy/sell transactions through a user wallet.

The project uses ASP.NET Core Identity for authentication, Entity Framework Core for database access and SQLite as the local database. Exchange rates are fetched from the public NBP API.

## Student

- Student: Ali Eyvazov
- Student ID: 72842
- Course: Team Application Project

## Main Features

- User registration, login and logout
- Dashboard with total PLN balance and wallet overview
- Live exchange rates for USD, EUR, GBP, CHF and JPY
- Currency converter between PLN and supported currencies
- Buy and sell currency using wallet balances
- Transaction history for each logged-in user
- SQLite database persistence
- Dark blue responsive user interface

## Technology Stack

| Area | Technology |
| --- | --- |
| Web Framework | ASP.NET Core Razor Pages |
| Language | C# |
| Database | SQLite |
| ORM | Entity Framework Core |
| Authentication | ASP.NET Core Identity |
| Frontend | Razor Pages, HTML, CSS, Bootstrap |
| External API | NBP Exchange Rates API |

## Project Structure

```text
CurrencyExchangeApp/
├── Data/
│   └── AppDbContext.cs
├── Models/
│   ├── CurrencyRate.cs
│   ├── Transaction.cs
│   └── UserWallet.cs
├── Pages/
│   ├── HomeDashboard.cshtml
│   ├── LiveRates.cshtml
│   ├── ConvertToPLN.cshtml
│   └── BuySellCurrency.cshtml
├── Services/
│   └── NbpApiService.cs
├── wwwroot/
├── Program.cs
├── currency.db
└── CurrencyExchangeApp.csproj
```

## Database

The application uses SQLite. The database file is:

```text
currency.db
```

The database is configured in `Program.cs`:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=currency.db"));
```

The main application tables are:

- `Wallets` - stores user wallet balances
- `Transactions` - stores buy/sell transaction history
- `CurrencyRates` - stores fetched exchange rate data
- ASP.NET Identity tables - store users and authentication data

## Requirements

Install the .NET 9 SDK before running the project:

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

After installation, check that .NET is available:

```powershell
dotnet --version
```

## How To Run

Open PowerShell in the project folder:

```powershell
cd "C:\Users\USER\Desktop\CurrencyExchangeApp"
```

Restore packages:

```powershell
dotnet restore
```

Build the project:

```powershell
dotnet build
```

Run the application:

```powershell
dotnet run
```

Open the application in a browser:

```text
http://localhost:5253
```

If the terminal shows a different localhost URL, open the URL shown in the terminal.

## How To Use

1. Register or log in with a user account.
2. Open the Home dashboard to view wallet balance.
3. Open Live Exchange Rates to see current rates.
4. Open Convertor to calculate currency conversions.
5. Open Trade to buy or sell currency.
6. Review wallet balances and transaction history after trading.

## Important Pages

| Page | Purpose |
| --- | --- |
| Home | Shows dashboard, PLN balance, wallet and exchange trend |
| Live Exchange Rates | Shows current rates from the NBP API |
| Convertor | Converts entered amounts between currencies |
| Trade | Allows users to buy or sell currency |

## External API

The project uses the NBP API to fetch exchange rates:

```text
http://api.nbp.pl/api/exchangerates/rates/a/{currencyCode}/?format=json
```

Example supported currencies:

- USD
- EUR
- GBP
- CHF
- JPY

## Notes

- This project is intended for academic/demo use.
- Trading is simulated and does not connect to a real banking system.
- The local SQLite database is suitable for development and testing.
- Internet connection is required when fetching live exchange rates.
