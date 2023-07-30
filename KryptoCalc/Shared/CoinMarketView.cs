namespace KryptoCalc.Shared;

public record CoinMarketView(
    string Id,
    string Symbol,
    string Name,
    string Image,
    decimal CurrentPrice,
    decimal InputPrice
    );

public static class CoinMarketViewExtention
{
    public static bool CompareId(this CoinMarketView coinMarkets, string dst)
        => string.Compare(coinMarkets.Id, dst, true) == 0;
}
