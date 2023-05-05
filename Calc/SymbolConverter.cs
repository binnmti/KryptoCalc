namespace Calc;

public static class SymbolConverter
{
    public static decimal Convert(this Dictionary<string, decimal> symbolItems, string nowSymbol, string newSymbol, decimal price)
    {
        if (string.Compare(nowSymbol, "yen", true) == 0)
        {
            symbolItems.TryGetValue(newSymbol, out decimal newPrice);
            return price / newPrice;
        }
        else
        {
            symbolItems.TryGetValue(nowSymbol, out decimal nowPrice);
            if (string.Compare(newSymbol, "yen", true) == 0)
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
