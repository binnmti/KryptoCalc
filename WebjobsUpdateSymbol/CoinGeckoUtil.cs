using System.Text.Json;
using KryptoCalc.Shared;

namespace WebjobsUpdateSymbol
{
    internal static class CoinGeckoUtil
    {
        private static readonly string GeckoUrl = "https://api.coingecko.com/api/v3";
        public class GeckoCoin
        {
            public string id { get; set; } = "";
            public string symbol { get; set; } = "";
            public string name { get; set; } = "";
        }

        public class GeckoCoinMarkets
        {
            public string id { get; set; }
            public string symbol { get; set; }
            public string name { get; set; }
            public string image { get; set; }
            public float current_price { get; set; }
            public long market_cap { get; set; }
            public int market_cap_rank { get; set; }
            public long? fully_diluted_valuation { get; set; }
            public long total_volume { get; set; }
            public float high_24h { get; set; }
            public float low_24h { get; set; }
            public float price_change_24h { get; set; }
            public float price_change_percentage_24h { get; set; }
            public float market_cap_change_24h { get; set; }
            public float market_cap_change_percentage_24h { get; set; }
            public float circulating_supply { get; set; }
            public float? total_supply { get; set; }
            public float? max_supply { get; set; }
            public float ath { get; set; }
            public float ath_change_percentage { get; set; }
            public DateTime ath_date { get; set; }
            public float atl { get; set; }
            public float atl_change_percentage { get; set; }
            public DateTime atl_date { get; set; }
            public DateTime last_updated { get; set; }
        }

        internal static async Task<List<CoinMarkets>> GetCoinMarketListAsync(HttpClient httpClient)
        {
            var response = await httpClient.GetAsync($"{GeckoUrl}/coins/markets?vs_currency=jpy&order=market_cap_desc&per_page=100&page=1&sparkline=false&locale=en");
            var content = await response.Content.ReadAsStringAsync() ?? "";
            var coins = JsonSerializer.Deserialize<List<GeckoCoinMarkets>>(content) ?? new List<GeckoCoinMarkets>();
            return coins.Select(x => new CoinMarkets() {
                Id = x.id,
                Name = x.name,
                Symbol = x.symbol,
                Ath = x.ath,
                AthChangePercentage = x.ath_change_percentage,
                AthDate = x.ath_date,
                Atl = x.atl,
                Image = x.image,
                AtlChangePercentage = x.atl_change_percentage,
                AtlDate = x.atl_date,
                CirculatingSupply = x.circulating_supply,
                CurrentPrice = x.current_price,
                FullyDilutedValuation = x.fully_diluted_valuation ?? default,
                High24h = x.high_24h,
                LastUpdated = x.last_updated,
                Low24h = x.low_24h,
                MarketCap = x.market_cap,
                MarketCapChange24h = x.market_cap_change_24h,
                MarketCapChangePercentage24h = x.market_cap_change_percentage_24h,
                MarketCapRank = x.market_cap_rank,
                MaxSupply = x.max_supply ?? default,
                PriceChange24h = x.price_change_24h,
                PriceChangePercentage24h = x.price_change_percentage_24h,
                TotalSupply = x.total_supply ?? default,
                TotalVolume = x.total_volume,
            }).ToList();
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
