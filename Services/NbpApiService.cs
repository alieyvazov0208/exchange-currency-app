using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CurrencyExchangeApp.Models;

namespace CurrencyExchangeApp.Services
{
    public class NbpApiService
    {
        private readonly HttpClient _httpClient;

        public NbpApiService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<CurrencyRate?> GetRateAsync(string currencyCode)
        {
            var url = $"http://api.nbp.pl/api/exchangerates/rates/a/{currencyCode}/?format=json";

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var json = JsonDocument.Parse(response);

                var rate = json.RootElement
                    .GetProperty("rates")[0]
                    .GetProperty("mid")
                    .GetDecimal();

                var date = json.RootElement
                    .GetProperty("rates")[0]
                    .GetProperty("effectiveDate")
                    .GetDateTime();

                return new CurrencyRate
                {
                    CurrencyCode = currencyCode.ToUpper(),
                    Rate = rate,
                    DateFetched = date
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
