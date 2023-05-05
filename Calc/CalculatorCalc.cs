namespace Calc;

//TODO:入力上限桁がいるかも*で上限簡単にこえるので。。。
//TODO:1+*=はどうなるんだっけ？

public class CalculatorCalc
{
    private readonly List<string> StrList = new();

    public decimal CurrentNumber { get; private set; }

    public string Add(string str)
    {
        if (StrList.Count == 0 && IsCalc(str))
        {
            foreach (var n in CurrentNumber.ToString())
            {
                StrList.Add(n.ToString());
            }
        }
        if (string.Compare(str, "C", true) == 0)
        {
            StrList.Clear();
        }
        else
        {
            StrList.Add(str);
        }
        CurrentNumber = ToDecimal(StrList);
        if(str == "=")
        {
            StrList.Clear();
        }
        return CurrentNumber.ToString();
    }

    private static decimal ToDecimal(IEnumerable<string> strList)
    {
        var calcItem = new List<Tuple<decimal, string>>();
        var result = "";
        var preNum = 0m;
        foreach (var str in strList)
        {
            if (IsCalc(str))
            {
                if (result != "")
                {
                    preNum = decimal.Parse(result);
                }
                calcItem.Add(Tuple.Create(preNum, str));
                result = "";
                continue;
            }
            result += str;
        }
        return result == "" ? ToDecimal(calcItem) : decimal.Parse(result);
    }

    private static decimal ToDecimal(List<Tuple<decimal, string>> calcItem)
    {
        string preSymbol = "";
        decimal result = 0;
        foreach (var (num, symbol) in calcItem)
        {
            result = preSymbol switch
            {
                "+" => result + num,
                "-" => result - num,
                "×" => result * num,
                "÷" => result / num,
                "%" => result % num,
                _ => result + num,
            };
            preSymbol = symbol;
        }
        return result;
    }

    private static bool IsCalc(string str)
        => str == "+" || str == "-" || str == "÷" || str == "×" || str == "=";
}
