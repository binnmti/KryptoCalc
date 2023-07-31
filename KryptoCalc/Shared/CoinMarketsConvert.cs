namespace KryptoCalc.Shared;

public static class CoinMarketConvert
{
    public static IEnumerable<CoinMarketView> ToCoinMarketViews(this IEnumerable<CoinMarkets> coinMarketsList)
        => coinMarketsList.Select(x => new CoinMarketView(
            x.Id,
            x.Symbol,
            x.Name,
            x.Image,
            (decimal)x.CurrentPrice,
            0,
            false));
}
