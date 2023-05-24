namespace KryptoCalc.Shared;

public record CoinMarketView(
    string Id,
    string Name,
    string Symbol,
    string Image,
    decimal CurrentPrice,
    decimal InputPrice
    );
