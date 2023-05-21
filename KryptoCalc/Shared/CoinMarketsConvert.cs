namespace KryptoCalc.Shared;

public static class CoinMarketConvert
{
    public static IEnumerable<CoinMarketView> ToCoinMarketView(this List<CoinMarkets> coinMarkets, string newSymbol, decimal currentPrice, decimal price)
        => coinMarkets
            .Select(x => new CoinMarketView(
                x.Id,
                x.Name,
                x.Symbol,
                x.Image,
                GetCoinPrice(price, newSymbol,currentPrice, x.Id, (decimal)x.CurrentPrice)));

    private static decimal GetCoinPrice(decimal price, string newSymbol, decimal newPrice, string currentSymbol, decimal currentPrice)
        => (newSymbol == "yen")
            ? Round(price / currentPrice)
            : (currentSymbol == "yen")
                ? Round(price * newPrice)
                : Round(price * newPrice / currentPrice);

    private static decimal Round(decimal price)
        => Math.Round(price, 8);
}
