namespace KryptoCalc.Shared;

public static class LegalCurrency
{
    //Idをそのまま使うと、仮想通貨と法定通貨のIDが同じものが(USD)あるので変える
    public static readonly string PrefixId = "legal"; 

    public static IEnumerable<CoinMarketView> ToCoinMarketViews(string language)
        => Currencys.Select(x => ToCoinMarketView(language, x));

    public static CoinMarketView GetCoinMarketView(string id)
        => ToCoinMarketView("", Currencys.Single(x => string.Compare(PrefixId + x.Id, id, true) == 0));

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
        new Currency("JPY", "Japanese Yen", "Japan","日本円", "円"),
        new Currency("USD", "US Dollar", "United States", "アメリカドル", "ドル"),
        new Currency("AED", "UAE Dirham", "United Arab Emirates", "UAEディルハム", "ディルハム"),
        new Currency("ARS", "Argentine Peso", "Argentina", "アルゼンチン・ペソ", "ペソ"),
        new Currency("AUD", "Australian Dollar", "Australia", "オーストラリ・アドル", "ドル"),
        new Currency("BDT", "BDT Bangladeshi taka", "Banglades", "バングラディッシュ・タカ", "タカ"),
        new Currency("BHD", "Bahraini Dinar", "Bahrain", "バーレーン・ディナール", "ディナール"),
        new Currency("BMD", "BMD Bermudian dollar", "Bermuda", "バーミューダ・ドル", "ドル"),
         new Currency("BRL", "Real", "Brazil", "ブラジル・レアル", "レアル"),
         new Currency("CAD", "Canadian Dollar", "Canada", "カナダ・ドル", "ドル"),
         new Currency("CHF", "Swiss Franc", "Switzerland", "スイス・フラン", "フラン"),
         new Currency("CLP", "Chilean Peso", "Chile", "チリ・ペソ", "ペソ"),
         new Currency("CNY", "PRC Renminbi", "People's Republic of China", "中国人民元", "人民元"),
         new Currency("CZK", "Czech Koruna", "Czech Republic", "チェコ・コルナ", "コルナ"),
         new Currency("DKK", "Danish Krone", "Denmark", "デンマーク・クローネ", "クローネ"),
         new Currency("GBP", "UK Pound Sterling", "United Kingdom", "英国ポンド", "ポンド"),
         new Currency("HKD", "Hong Kong Dollar", "Hong Kong SAR", "香港ドル", "ドル"),
         new Currency("HUF", "Hungarian Forint", "Hungary", "ハンガリー・フォリント", "フォリント"),
         new Currency("IDR", "Indonesian Rupiah", "Indonesia", "ンドネシア・ルピー", "ルピー"),
         new Currency("ILS", "Israeli New Shekel", "Israel", "イスラエル・シェケル", "シェケル"),
         new Currency("INR", "Indian Rupee", "India", "インド・ルピー", "ルピー"),
         new Currency("KRW", "Korean Won", "Korea", "韓国ウォン", "ウォン"),
         new Currency("KWD", "Kuwaiti Dinar", "Kuwait", "クウェート・ディナール", "ディナール"),
         new Currency("LKR", "LKR Sri Lankan rupee", "Sri Lanka", "スリランカ・ルピー", "ルピー"),
         new Currency("MMK", "MMK Myanma kyat", "Myanmar", "ミャンマー・チャット", "チャット"),
          new Currency("MXN", "Mexican Peso", "Mexico", "メキシコ・ペソ", "ペソ"),
          new Currency("MYR", "Malaysian Ringgit", "Malaysia", "マレーシア・リンギット", "リンギット"),
          new Currency("NGN", "Malaysian Ringgit", "Malaysia", "ナイジェリア・ナイラ", "ナイラ"),
          new Currency("NOK", "Norwegian Krone", "Norway", "ノルウェー・クローネ", "クローネ"),
          new Currency("NZD", "New Zealand Dollar", "New Zealand", "ニュージーランド・ドル", "ドル"),
          new Currency("PHP", "Philippine Peso", "Philippines", "フィリピン・ペソ", "ペソ"),
          new Currency("PKR", "Pakistan Rupee", "Islamic Republic of Pakistan", "パキスタン・ルピー", "ルピー"),
          new Currency("PLN", "Polish Zloty", "Poland", "ポーランド・ゾロチ", "ゾロチ"),
          new Currency("RUB", "Russian Ruble", "Russia", "ロシア・ルーブル", "ルーブル"),
          new Currency("SAR", "Saudi Riyal", "Saudi Arabia", "サウジアラビア・リヤル", "リヤル"),
          new Currency("SEK", "Swedish Krona", "Sweden", "スウェーデン・クローナ", "クローナ"),
          new Currency("SGD", "Singapore Dollar", "Singapore", "シンガポール・ドル", "ドル"),
          new Currency("THB", "Thai Baht", "Thailand", "タイ・バーツ", "バーツ"),
          new Currency("TRY", "Turkish Lira", "Turkey", "新トルコ・リラ", "リラ"),
          new Currency("TWD", "New Taiwan Dollar", "Taiwan", "新台湾ドル", "ドル"),
          new Currency("UAH", "Ukrainian Hryvnia", "Ukraine", "ウクライナ・フリヴニャ", "フリヴニャ"),
          new Currency("VEF", "Venezuelan Bolivar", "Bolivarian Republic of Venezuela", "ベネズエラボリバル", "ボリバル"),
          new Currency("VND", "Vietnamese Dong", "Vietnam", "ベトナム・ドン", "ドン"),
          new Currency("ZAR", "South African Rand", "South Africa", " 南アフリカ・ランド", "ランド"),
         //new RegionCurrency("EUR", "Euro", "", "", "ユーロ"),
    };

    //TODO:languageは外から引数でわたってくる。この管理では多分NG。リソース化する必要がある。
    private static CoinMarketView ToCoinMarketView(string language, Currency currency)
        => new(PrefixId + currency.Id,
                language == "" ? currency.JapanSymbol : currency.Symbol,
                language == "" ? currency.JapanCountryName : currency.CountryName,
                $"img/{currency.Id}.png",
                1, 0, true);
}
