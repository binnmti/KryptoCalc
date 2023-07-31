namespace KryptoCalc.Shared;

public record CoinMarketView(
    string Id,
    string Symbol,
    string Name,
    string Image,
    decimal CurrentPrice,
    decimal InputPrice,
    bool IsLegal
    );

public static class CoinMarketViewExtention
{
    public static bool CompareId(this CoinMarketView coinMarkets, string dst)
        => string.Compare(coinMarkets.Id, dst, true) == 0;

    //TODO:UnitTest
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
