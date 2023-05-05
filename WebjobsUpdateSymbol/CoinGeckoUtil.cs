using System.Text.Json;
using KryptoCalc.Shared;

namespace WebjobsUpdateSymbol
{
    internal static class CoinGeckoUtil
    {
        private static readonly string GeckoUrl = "https://api.coingecko.com/api/v3";
        public class GeckoCoin
        {
            public string id { get; set; }
            public string symbol { get; set; }
            public string name { get; set; }
        }

        internal static async Task<List<Coin>> GetCoinListAsync(HttpClient httpClient)
        {
            var response = await httpClient.GetAsync($"{GeckoUrl}/coins/list?include_platform=false");
            var content = await response.Content.ReadAsStringAsync() ?? "";
            var coins = JsonSerializer.Deserialize<List<GeckoCoin>>(content) ?? new List<GeckoCoin>();
            return coins.Select(x => new Coin() { Id = x.id, Name = x.name, Symbol = x.symbol }).ToList();
        }

        internal static async Task SetCoinListPriceAsync(HttpClient httpClient, IEnumerable<Coin> coinList)
        {
            var ids = string.Join("%2C", coinList.Select(x => x.Id));
            var response = await httpClient.GetAsync($"{GeckoUrl}/simple/price?ids={ids}&vs_currencies=jpy");
            var content = await response.Content.ReadAsStringAsync() ?? "";
            var jsonLines = content[1..].Split(",");
            for (int i = 0; i < jsonLines.Length; i++)
            {
                coinList.ElementAt(i).Price = GetPrice(jsonLines[i]);
            }
        }

        internal static decimal GetPrice(string line)
             => decimal.TryParse(line[(line.LastIndexOf(":") + 1)..^1], out var price) ? price : 0m;
    }
}
