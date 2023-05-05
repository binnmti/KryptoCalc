using System.Text.Json;
using KryptoCalc.Shared;

namespace WebjobsUpdateSymbol
{
    internal static class CoinGeckoUtil
    {
        public class GeckoCoin
        {
            public string id { get; set; }
            public string symbol { get; set; }
            public string name { get; set; }
        }

        internal static async Task<IReadOnlyList<Coin>> GetCoinList(HttpClient httpClient)
        {
            var response = await httpClient.GetAsync("https://api.coingecko.com/api/v3/coins/list?include_platform=false");
            var content = await response.Content.ReadAsStringAsync() ?? "";
            var coins = JsonSerializer.Deserialize<List<GeckoCoin>>(content);
            return coins.Select(x => new Coin() { Id = x.id, Name = x.name, Symbol = x.symbol }).ToList();
        }

        //internal static async Task<List<Coin>> GetCoinList2(HttpClient httpClient)
        //{
        //    //var coins = CoinName.CoinNames.Skip(skip).Take(take);
        //    //if (coins.Count() == 0) break;

        //    //var ids = string.Join("%2C", coins);
        //    //var response = await httpClient.GetAsync($"https://api.coingecko.com/api/v3/simple/price?ids={ids}&vs_currencies=jpy");
        //    //var content = await response.Content.ReadAsStringAsync() ?? "";
        //    //skip += take;

        //}
    }
}
