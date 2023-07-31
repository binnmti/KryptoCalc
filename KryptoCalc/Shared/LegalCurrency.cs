namespace KryptoCalc.Shared;

public static class LegalCurrency
{
    public static IEnumerable<CoinMarketView> GetCoinMarketViews(string country)
    => Currencys.Select(x => ToCoinMarketView(country, x));

    public static CoinMarketView GetCoinMarketView(string id)
        => ToCoinMarketView("", Currencys.Single(x => string.Compare(x.Id, id, true) == 0));

    private record Currency(
        string Id,
        string Symbol,
        string CountryName,
        string JapanCountryName,
        string JapanSymbol);

    private static readonly List<Currency> Currencys = new()
    {
        //Currency
        //https://learn.microsoft.com/en-us/dotnet/api/system.globalization.regioninfo.isocurrencysymbol?view=net-7.0
        //Flag
        //https://flagdownload.com/
        //日本語
        //https://fxtop.com/jp/countries-currencies.php
        new Currency("JPY", "Japanese Yen", "Japan","日本", "円"),
        new Currency("USD", "US Dollar", "United States", "アメリカ", "ドル"),
        new Currency("AED", "UAE Dirham", "United Arab Emirates", "アラブ首長国連邦", "ディルハム"),
        new Currency("ARS", "Argentine Peso", "Argentina", "アルゼンチン", "ペソ"),
        new Currency("AUD", "Australian Dollar", "Australia", "オーストラリア", "ドル"),
        new Currency("BDT", "BDT Bangladeshi taka", "Banglades", "バングラディッシュ", "タカ"),
        new Currency("BHD", "Bahraini Dinar", "Bahrain", "バーレーン", "ディナール"),
        new Currency("BMD", "BMD Bermudian dollar", "Bermuda", "バーミューダ", "ドル"),
         new Currency("BRL", "Real", "Brazil", "ブラジル", "レアル"),
         new Currency("CAD", "Canadian Dollar", "Canada", "カナダ", "ドル"),
         new Currency("CHF", "Swiss Franc", "Switzerland", "スイス", "フラン"),
         new Currency("CLP", "Chilean Peso", "Chile", "チリ", "ペソ"),
         new Currency("CNY", "PRC Renminbi", "People's Republic of China", "中国", "人民元"),
         new Currency("CZK", "Czech Koruna", "Czech Republic", "チェコ", "コルナ"),
         new Currency("DKK", "Danish Krone", "Denmark", "デンマーク", "クローネ"),
         new Currency("GBP", "UK Pound Sterling", "United Kingdom", "英国", "ポンド"),
         new Currency("HKD", "Hong Kong Dollar", "Hong Kong SAR", "香港", "香港ドル"),
         new Currency("HUF", "Hungarian Forint", "Hungary", "ハンガリー", "フォリント"),
         new Currency("IDR", "Indonesian Rupiah", "Indonesia", "インドネシア", "ルピー"),
         new Currency("ILS", "Israeli New Shekel", "Israel", "イスラエル", "シェケル"),
         new Currency("INR", "Indian Rupee", "India", "インド", "ルピー"),
         new Currency("KRW", "Korean Won", "Korea", "韓国", "ウォン"),
         new Currency("KWD", "Kuwaiti Dinar", "Kuwait", "クウェート", "ディナール"),
         new Currency("LKR", "LKR Sri Lankan rupee", "Sri Lanka", "スリランカ", "ルピー"),
         new Currency("MMK", "MMK Myanma kyat", "Myanmar", "ミャンマー", "チャット"),
          new Currency("MXN", "Mexican Peso", "Mexico", "メキシコ", "ペソ"),
          new Currency("MYR", "Malaysian Ringgit", "Malaysia", "マレーシア", "リンギット"),
          new Currency("NGN", "Malaysian Ringgit", "Malaysia", "ナイジェリア", "ナイラ"),
          new Currency("NOK", "Norwegian Krone", "Norway", "ノルウェー", "クローネ"),
          new Currency("NZD", "New Zealand Dollar", "New Zealand", "ニュージーランド", "ドル"),
          new Currency("PHP", "Philippine Peso", "Philippines", "フィリピン", "ペソ"),
          new Currency("PKR", "Pakistan Rupee", "Islamic Republic of Pakistan", "パキスタン", "ルピー"),
          new Currency("PLN", "Polish Zloty", "Poland", "ポーランド", "ゾロチ"),
          new Currency("RUB", "Russian Ruble", "Russia", "ロシア", "ルーブル"),
          new Currency("SAR", "Saudi Riyal", "Saudi Arabia", "サウジアラビア", "リヤル"),
          new Currency("SEK", "Swedish Krona", "Sweden", "スウェーデン", "クローナ"),
          new Currency("SGD", "Singapore Dollar", "Singapore", "シンガポール", "シンガポール・ドル"),
          new Currency("THB", "Thai Baht", "Thailand", "タイ", "バーツ"),
          new Currency("TRY", "Turkish Lira", "Turkey", "トルコ", "リラ"),
          new Currency("TWD", "New Taiwan Dollar", "Taiwan", "台湾", "ドル"),
          new Currency("UAH", "Ukrainian Hryvnia", "Ukraine", "ウクライナ", "フリヴニャ"),
          new Currency("VEF", "Venezuelan Bolivar", "Bolivarian Republic of Venezuela", "ベネズエラ", "ボリバル"),
          new Currency("VND", "Vietnamese Dong", "Vietnam", "ベトナム", "ドン"),
          new Currency("ZAR", "South African Rand", "South Africa", "南アフリカ", "ランド"),
         //new RegionCurrency("EUR", "Euro", "", "", "ユーロ"),
    };

    private static CoinMarketView ToCoinMarketView(string country, Currency currency)
        => new(currency.Id,
                country == "" ? currency.JapanSymbol : currency.Symbol,
                country == "" ? currency.JapanCountryName : currency.CountryName,
                $"img/{currency.Id}.png",
                1, 1, true);
}
