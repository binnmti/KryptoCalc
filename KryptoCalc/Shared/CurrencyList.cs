using System.Globalization;
using System.Text.RegularExpressions;

namespace KryptoCalc.Shared;

public static class CurrencyList
{
    public static string GetCurrentId()
    {
        var name = CultureInfo.CurrentCulture.IetfLanguageTag;
        var regionInfo = new RegionInfo(name);
        return regionInfo.ISOCurrencySymbol.ToLower();
    }

    public static List<CoinMarkets> GetCurrencyCoinMarkets()
    {
        var regionInfos = GetRegionInfo();
        var coinMarkets = new List<CoinMarkets>();
        foreach (var id in CurrencyIdList)
        {
            try
            {
                var regionInfo = regionInfos.First(x => string.Compare(x.ISOCurrencySymbol, id, true) == 0);
                var coinMarket = new CoinMarkets(id, regionInfo.ISOCurrencySymbol.ToLower(), regionInfo.CurrencyNativeName, IsoCountryCodeToFlagEmoji(regionInfo.TwoLetterISORegionName), 1);
                coinMarkets.Add(coinMarket);
            }
            catch(Exception ex)
            {
                Console.WriteLine(id + ":" + ex.ToString());
            }
        }
        return coinMarkets;
    }

    //Countryクラス的なのを作ってそこに項目を入れる
    public static CoinMarkets GetCoinMarkets(string id)
    {
        var regionInfo = GetRegionInfo().SingleOrDefault(x => string.Compare(x.ISOCurrencySymbol, id, true) == 0);
        return new CoinMarkets(id, regionInfo.ISOCurrencySymbol.ToLower(), regionInfo.CurrencyNativeName, IsoCountryCodeToFlagEmoji(regionInfo.TwoLetterISORegionName), 1);
    }


    private static List<RegionInfo> GetRegionInfo()
    {
        List<RegionInfo> countries = new List<RegionInfo>();

        //CultureInfo全部
        foreach (var culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
        {
            //CultureからRegin
            var regionInfo = new RegionInfo(culture.Name);
            //CultureとReginになる名前だけ
            if (countries.Any(x => x.EnglishName == regionInfo.EnglishName)) continue;

            countries.Add(regionInfo);
        }
        return countries;
    }

    private static string IsoCountryCodeToFlagEmoji(string country)
    {
        return string.Concat(country.ToUpper().Select(x => char.ConvertFromUtf32(x + 0x1F1A5)));
    }

    //CoinMarkets
    private static readonly List<string> CurrencyIdList = new List<string>()
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
      //"vef",
      "vnd",
      "zar",
      //"xdr",
      //"xag",
      //"xau",
      //"bits",
      //"sats"
    };
}
