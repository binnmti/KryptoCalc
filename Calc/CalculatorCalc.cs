namespace Calc;

//TODO:入力上限桁がいるかも*で上限簡単にこえるので。。。
//TODO:1+*=はどうなるんだっけ？

public class CalculatorCalc
{
    private readonly List<string> StrList = new();

    public decimal CurrentNumber { get; private set; }

    public string Add(string str)
    {
        //未入力か確定の状態で演算子
        if (StrList.Count == 0 && IsOperator(str))
        {
            //現在の数字をそのまま使用
            foreach (var n in CurrentNumber.ToString())
            {
                StrList.Add(n.ToString());
            }
        }
        if (string.Compare(str, "C", true) == 0)
        {
            StrList.Clear();
        }
        else if (string.Compare(str, "☒", true) == 0)
        {
            if (StrList.Count != 0)
            {
                StrList.RemoveAt(StrList.Count - 1);
            }
            else
            {
                return CurrentNumber.ToString();
            }
        }
        else
        {
            StrList.Add(str);
        }

        CurrentNumber = ToNumber(StrList);
        if(str == "=")
        {
            StrList.Clear();
        }
        return CurrentNumber.ToString();
    }

    private static decimal ToNumber(IEnumerable<string> strList)
    {
        var calcItem = new List<Tuple<decimal, string>>();
        var process = "";
        var numberPreOperator = 0m;
        foreach (var str in strList)
        {
            if (IsOperator(str))
            {
                var ope = str;
                //演算子の前に入力された数字
                if (process != "")
                {
                    numberPreOperator = decimal.Parse(process);
                }
                calcItem.Add(Tuple.Create(numberPreOperator, ope));
                process = "";
                continue;
            }
            process += str;
        }
        //processが""なら演算子なので計算結果、そうでなければ数字を返す
        return process == "" ? Calculator(calcItem) : decimal.Parse(process);
    }

    private static decimal Calculator(List<Tuple<decimal, string>> calcItem)
    {
        string preOpe = "";
        decimal result = 0;
        foreach (var (num, ope) in calcItem)
        {
            result = preOpe switch
            {
                "+" => result + num,
                "-" => result - num,
                "×" => result * num,
                "÷" => result / num,
                _ => result + num,
            };
            preOpe = ope;
        }
        return result;
    }

    private static bool IsOperator(string str)
        => str == "+" || str == "-" || str == "÷" || str == "×" || str == "=";
}
