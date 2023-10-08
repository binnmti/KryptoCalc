namespace KryptoCalc.Shared;

public record CoinMarketView
{
    public CoinMarketView()
    {
    }

    public CoinMarketView(string id, string symbol, string name, string image, decimal currentPrice, decimal inputPrice, bool isLegal)
    {
        Id = id;
        Symbol = symbol;
        Name = name;
        Image = image;
        CurrentPrice = currentPrice;
        InputPrice = inputPrice;
        IsLegal = isLegal;
    }

    public CoinMarketView(string id,
                          string symbol,
                          string name,
                          string image,
                          decimal currentPrice,
                          decimal inputPrice,
                          bool isLegal,
                          decimal usd,
                          decimal aed,
                          decimal ars,
                          decimal aud,
                          decimal bdt,
                          decimal bhd,
                          decimal bmd,
                          decimal brl,
                          decimal cad,
                          decimal chf,
                          decimal clp,
                          decimal cny,
                          decimal czk,
                          decimal dkk,
                          decimal eur,
                          decimal gbp,
                          decimal hkd,
                          decimal huf,
                          decimal idr,
                          decimal ils,
                          decimal inr,
                          decimal jpy,
                          decimal krw,
                          decimal kwd,
                          decimal lkr,
                          decimal mmk,
                          decimal mxn,
                          decimal myr,
                          decimal ngn,
                          decimal nok,
                          decimal nzd,
                          decimal php,
                          decimal pkr,
                          decimal pln,
                          decimal rub,
                          decimal sar,
                          decimal sek,
                          decimal sgd,
                          decimal thb,
                          decimal @try,
                          decimal twd,
                          decimal uah,
                          decimal vef,
                          decimal vnd,
                          decimal zar,
                          decimal xdr,
                          decimal xag,
                          decimal xau,
                          decimal bits,
                          decimal sat)
        : this(id, symbol, name, image, currentPrice, inputPrice, isLegal)
    {
        Usd = usd;
        Aed = aed;
        Ars = ars;
        Aud = aud;
        Bdt = bdt;
        Bhd = bhd;
        Bmd = bmd;
        Brl = brl;
        Cad = cad;
        Chf = chf;
        Clp = clp;
        Cny = cny;
        Czk = czk;
        Dkk = dkk;
        Eur = eur;
        Gbp = gbp;
        Hkd = hkd;
        Huf = huf;
        Idr = idr;
        Ils = ils;
        Inr = inr;
        Jpy = jpy;
        Krw = krw;
        Kwd = kwd;
        Lkr = lkr;
        Mmk = mmk;
        Mxn = mxn;
        Myr = myr;
        Ngn = ngn;
        Nok = nok;
        Nzd = nzd;
        Php = php;
        Pkr = pkr;
        Pln = pln;
        Rub = rub;
        Sar = sar;
        Sek = sek;
        Sgd = sgd;
        Thb = thb;
        Try = @try;
        Twd = twd;
        Uah = uah;
        Vef = vef;
        Vnd = vnd;
        Zar = zar;
        Xdr = xdr;
        Xag = xag;
        Xau = xau;
        Bits = bits;
        Sat = sat;
    }

    public string Id { get; set; } = "";
    public string Symbol { get; set; } = "";
    public string Name { get; set; } = "";
    public string Image { get; set; } = "";
    public decimal CurrentPrice { get; set; }
    public decimal InputPrice { get; set; }
    public bool IsLegal { get; set; }
    public decimal Usd { get; set; }
    public decimal Aed { get; set; }
    public decimal Ars { get; set; }
    public decimal Aud { get; set; }
    public decimal Bdt { get; set; }
    public decimal Bhd { get; set; }
    public decimal Bmd { get; set; }
    public decimal Brl { get; set; }
    public decimal Cad { get; set; }
    public decimal Chf { get; set; }
    public decimal Clp { get; set; }
    public decimal Cny { get; set; }
    public decimal Czk { get; set; }
    public decimal Dkk { get; set; }
    public decimal Eur { get; set; }
    public decimal Gbp { get; set; }
    public decimal Hkd { get; set; }
    public decimal Huf { get; set; }
    public decimal Idr { get; set; }
    public decimal Ils { get; set; }
    public decimal Inr { get; set; }
    public decimal Jpy { get; set; }
    public decimal Krw { get; set; }
    public decimal Kwd { get; set; }
    public decimal Lkr { get; set; }
    public decimal Mmk { get; set; }
    public decimal Mxn { get; set; }
    public decimal Myr { get; set; }
    public decimal Ngn { get; set; }
    public decimal Nok { get; set; }
    public decimal Nzd { get; set; }
    public decimal Php { get; set; }
    public decimal Pkr { get; set; }
    public decimal Pln { get; set; }
    public decimal Rub { get; set; }
    public decimal Sar { get; set; }
    public decimal Sek { get; set; }
    public decimal Sgd { get; set; }
    public decimal Thb { get; set; }
    public decimal Try { get; set; }
    public decimal Twd { get; set; }
    public decimal Uah { get; set; }
    public decimal Vef { get; set; }
    public decimal Vnd { get; set; }
    public decimal Zar { get; set; }
    public decimal Xdr { get; set; }
    public decimal Xag { get; set; }
    public decimal Xau { get; set; }
    public decimal Bits { get; set; }
    public decimal Sat { get; set; }
}

public static class CoinMarketViewExtention
{
    public static bool CompareId(this CoinMarketView coinMarkets, string dst)
        => string.Compare(coinMarkets.Id, dst, true) == 0;

    public static IEnumerable<CoinMarketView> UpdateCoinMarketViews(this List<CoinMarketView> coinMarkets, string id)
        => coinMarkets.Select(x => x with { CurrentPrice = GetIdValue(x, id) });

    public static decimal ExChange(this CoinMarketView coinMarket, string srcCurrency, string dstCurrency)
        => GetIdValue(coinMarket, dstCurrency) / GetIdValue(coinMarket, srcCurrency);

    //TODO:UnitTest
    public static IEnumerable<CoinMarketView> UpdateCoinMarketViews(this List<CoinMarketView> coinMarkets, CoinMarketView current, decimal inputNumber)
        => coinMarkets.Select(x => x with { InputPrice = GetInputPrice(x, current, inputNumber) });

    private static decimal GetIdValue(CoinMarketView x, string id)
    {
        //先頭を大文字、それ以降を小文字にする
        var idc = string.Concat(id.Select((x, idx) => idx == 0 ? char.ToUpper(x) : char.ToLower(x)));
        var value = (decimal?)(typeof(CoinMarketView).GetProperty(idc)?.GetValue(x)) ?? 1m;
        return value == 0 ? 1 : value;
    }

    private static decimal GetInputPrice(CoinMarketView coinMarketView, CoinMarketView current, decimal inputNumber)
        => current.IsLegal
            ? Round(inputNumber / coinMarketView.CurrentPrice)
            : coinMarketView.IsLegal
                ? Round(inputNumber * current.CurrentPrice)
                : Round(inputNumber * current.CurrentPrice / coinMarketView.CurrentPrice);

    private static decimal Round(decimal price)
        => Math.Round(price, 8);
}
