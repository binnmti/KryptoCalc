using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using KryptoCalc.Shared;

namespace WebjobsUpdateSymbol;

internal static class CoinGeckoUtil
{
    private static readonly string GeckoUrl = "https://api.coingecko.com/api/v3";

    public class GeckoCoinMarkets
    {
        public string id { get; set; } = "";
        public string symbol { get; set; } = "";
        public string name { get; set; } = "";
        public string image { get; set; } = "";
        public float? current_price { get; set; }
        public float? market_cap { get; set; }
        public int? market_cap_rank { get; set; }
        public float? fully_diluted_valuation { get; set; }
        public float? total_volume { get; set; }
        public float? high_24h { get; set; }
        public float? low_24h { get; set; }
        public float? price_change_24h { get; set; }
        public float? price_change_percentage_24h { get; set; }
        public float? market_cap_change_24h { get; set; }
        public float? market_cap_change_percentage_24h { get; set; }
        public float? circulating_supply { get; set; }
        public float? total_supply { get; set; }
        public float? max_supply { get; set; }
        public float? ath { get; set; }
        public float? ath_change_percentage { get; set; }
        public DateTime? ath_date { get; set; }
        public float? atl { get; set; }
        public float? atl_change_percentage { get; set; }
        public DateTime? atl_date { get; set; }
        public DateTime? last_updated { get; set; }
    }

    private static readonly List<string> CurrencyList = new List<string>()
    {
      "usd",
      "aed",
      "ars",
      "aud",
      "bdt",
      "bhd",
      "bmd",
      "brl",
      "cad",
      "chf",
      "clp",
      "cny",
      "czk",
      "dkk",
      "eur",
      "gbp",
      "hkd",
      "huf",
      "idr",
      "ils",
      "inr",
      "jpy",
      "krw",
      "kwd",
      "lkr",
      "mmk",
      "mxn",
      "myr",
      "ngn",
      "nok",
      "nzd",
      "php",
      "pkr",
      "pln",
      "rub",
      "sar",
      "sek",
      "sgd",
      "thb",
      "try",
      "twd",
      "uah",
      "vef",
      "vnd",
      "zar",
      "xdr",
      "xag",
      "xau",
      "bits",
      "sats"
    };
    private static decimal ToDecimal(JsonNode? jsonNode, string id, string name) => decimal.Parse(jsonNode?[id]?[name]?.ToString() ?? "0", NumberStyles.Float);
    private static Price GetPrice(JsonNode? jsonNode, string id)
        => new()
        {
            Id = 0,
            CoinMarketsId = id,
            Aed = ToDecimal(jsonNode, id, "aed"),
            Ars = ToDecimal(jsonNode, id, "ars"),
            Aud = ToDecimal(jsonNode, id, "ars"),
            Bdt = ToDecimal(jsonNode, id, "bdt"),
            Bhd = ToDecimal(jsonNode, id, "bhd"),
            Bits = ToDecimal(jsonNode, id, "bits"),
            Bmd = ToDecimal(jsonNode, id, "bmd"),
            Brl = ToDecimal(jsonNode, id, "brl"),
            Cad = ToDecimal(jsonNode, id, "cad"),
            Chf = ToDecimal(jsonNode, id, "chf"),
            Clp = ToDecimal(jsonNode, id, "clp"),
            Cny = ToDecimal(jsonNode, id, "cny"),
            Czk = ToDecimal(jsonNode, id, "czk"),
            Dkk = ToDecimal(jsonNode, id, "dkk"),
            Eur = ToDecimal(jsonNode, id, "eur"),
            Gbp = ToDecimal(jsonNode, id, "gbp"),
            Hkd = ToDecimal(jsonNode, id, "hkd"),
            Huf = ToDecimal(jsonNode, id, "huf"),
            Idr = ToDecimal(jsonNode, id, "idr"),
            Ils = ToDecimal(jsonNode, id, "ils"),
            Inr = ToDecimal(jsonNode, id, "inr"),
            Jpy = ToDecimal(jsonNode, id, "jpy"),
            Krw = ToDecimal(jsonNode, id, "krw"),
            Kwd = ToDecimal(jsonNode, id, "kwd"),
            Lkr = ToDecimal(jsonNode, id, "lkr"),
            Mmk = ToDecimal(jsonNode, id, "mmk"),
            Mxn = ToDecimal(jsonNode, id, "mxn"),
            Myr = ToDecimal(jsonNode, id, "myr"),
            Ngn = ToDecimal(jsonNode, id, "ngn"),
            Nok = ToDecimal(jsonNode, id, "nok"),
            Nzd = ToDecimal(jsonNode, id, "nzd"),
            Php = ToDecimal(jsonNode, id, "php"),
            Pkr = ToDecimal(jsonNode, id, "pkr"),
            Pln = ToDecimal(jsonNode, id, "pln"),
            Rub = ToDecimal(jsonNode, id, "rub"),
            Sar = ToDecimal(jsonNode, id, "sar"),
            Sat = ToDecimal(jsonNode, id, "sat"),
            Sek = ToDecimal(jsonNode, id, "sek"),
            Sgd = ToDecimal(jsonNode, id, "sgd"),
            Thb = ToDecimal(jsonNode, id, "thb"),
            Try = ToDecimal(jsonNode, id, "try"),
            Twd = ToDecimal(jsonNode, id, "twd"),
            Uah = ToDecimal(jsonNode, id, "uah"),
            Usd = ToDecimal(jsonNode, id, "usd"),
            Vef = ToDecimal(jsonNode, id, "vef"),
            Vnd = ToDecimal(jsonNode, id, "vnd"),
            Xag = ToDecimal(jsonNode, id, "xag"),
            Xau = ToDecimal(jsonNode, id, "xau"),
            Xdr = ToDecimal(jsonNode, id, "xdr"),
            Zar = ToDecimal(jsonNode, id, "zar"),
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now,
        };

    internal static async Task<List<Price>> GetPriceListAsync(HttpClient httpClient, List<string> ids)
    {
        int page = 1;
        var currencyList = string.Join("%2C", CurrencyList);
        var priceList = new List<Price>();
        foreach (var idChunk in ids.Chunk(250))
        {
            var response = await httpClient.GetAsync($"{GeckoUrl}/simple/price?ids={string.Join("%2C", idChunk)}&vs_currencies={currencyList}");
            var content = await response.Content.ReadAsStringAsync() ?? "";
            priceList.AddRange(idChunk.Select(id => GetPrice(JsonNode.Parse(content), id)));

            Console.WriteLine($"simple/price/page={page++}&count={priceList.Count}");
            Thread.Sleep(1000 * 60);
        }
        return priceList;
    }

    //Must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM.
    private static DateTime MinDateTime = new DateTime(1800, 1, 1, 0, 0, 0);
    private static DateTime? GetMinDateTime(DateTime? src)
        => src?.Ticks > MinDateTime.Ticks ? src : MinDateTime;

    internal static async Task<List<CoinMarkets>> GetCoinMarketListAsync(HttpClient httpClient)
    {
        var coinMarkets = new List<CoinMarkets>();
        int page = 1;
        while (true)
        {
            var response = await httpClient.GetAsync($"{GeckoUrl}/coins/markets?vs_currency=jpy&order=market_cap_desc&per_page=250&page={page}&sparkline=false&locale=en");
            var content = await response.Content.ReadAsStringAsync() ?? "";
            var coins = JsonSerializer.Deserialize<List<GeckoCoinMarkets>>(content) ?? new List<GeckoCoinMarkets>();
            if (coins.Count == 0) break;

            coinMarkets.AddRange(coins.Select(x => new CoinMarkets()
            {
                Id = x.id,
                Name = x.name,
                Symbol = x.symbol,
                Ath = x.ath ?? default,
                AthChangePercentage = x.ath_change_percentage ?? default,
                AthDate = GetMinDateTime(x.ath_date) ?? default,
                Atl = x.atl ?? default,
                Image = x.image,
                AtlChangePercentage = x.atl_change_percentage == float.PositiveInfinity ? 0 : x.atl_change_percentage ?? default,
                AtlDate = GetMinDateTime(x.atl_date) ?? default,
                CirculatingSupply = x.circulating_supply ?? default,
                CurrentPrice = x.current_price ?? default,
                FullyDilutedValuation = x.fully_diluted_valuation ?? default,
                High24h = x.high_24h ?? default,
                LastUpdated = GetMinDateTime(x.last_updated) ?? default,
                Low24h = x.low_24h ?? default,
                MarketCap = x.market_cap ?? default,
                MarketCapChange24h = x.market_cap_change_24h ?? default,
                MarketCapChangePercentage24h = x.market_cap_change_percentage_24h ?? default,
                MarketCapRank = x.market_cap_rank ?? default,
                MaxSupply = x.max_supply ?? default,
                PriceChange24h = x.price_change_24h ?? default,
                PriceChangePercentage24h = x.price_change_percentage_24h ?? default, 
                TotalSupply = x.total_supply ?? default,
                TotalVolume = x.total_volume ?? default,
            }));
            Console.WriteLine($"coins/markets/page={page}&count={coinMarkets.Count}");
            page++;
            Thread.Sleep(1000 * 60);
        }
        return coinMarkets;
    }
}
