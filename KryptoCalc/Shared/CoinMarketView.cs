namespace KryptoCalc.Shared;

public record CoinMarketView(
    string Id,
    string Symbol,
    string Name,
    string Image,
    decimal CurrentPrice,
    decimal InputPrice
    );
