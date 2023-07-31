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
            0, false));

    public static IEnumerable<CoinMarketView> UpdateCoinMarketViews(this List<CoinMarketView> coinMarkets, CoinMarketView current, decimal inputNumber)
        => coinMarkets.Select(x => new CoinMarketView(
                x.Id,
                x.Symbol,
                x.Name,
                x.Image,
                x.CurrentPrice,
                GetInputPrice(x, current, inputNumber),
                x.IsLegal));

    private static decimal GetInputPrice(CoinMarketView coinMarketView, CoinMarketView current, decimal inputNumber)
        => current.IsLegal
            ? Round(inputNumber / coinMarketView.CurrentPrice)
            : coinMarketView.IsLegal
                ? Round(inputNumber * current.CurrentPrice)
                : Round(inputNumber * current.CurrentPrice / coinMarketView.CurrentPrice);

    private static decimal Round(decimal price)
        => Math.Round(price, 8);
}
