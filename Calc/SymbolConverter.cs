namespace Calc;

public static class SymbolConverter
{
    public static decimal Convert(this Dictionary<string, decimal> symbolItems, string nowSymbol, string newSymbol, decimal price)
    {
        if (nowSymbol == "yen")
        {
            symbolItems.TryGetValue(newSymbol, out decimal newPrice);
            return price / newPrice;
        }
        else
        {
            symbolItems.TryGetValue(nowSymbol, out decimal nowPrice);
            if (newSymbol == "yen")
            {
                return price * nowPrice;
            }
            else
            {
                symbolItems.TryGetValue(newSymbol, out decimal newePrice);
                return price * nowPrice / newePrice;
            }
        }
    }
}
