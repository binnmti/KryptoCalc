namespace KryptoCalc.Shared;

//TODO:入力上限桁がいるかも*で上限簡単にこえるので。。。
//TODO:割り算の誤差が気になる

public class Calc
{
    private List<string> InputList { get; set; } = new();

    /// <summary>
    /// 現在入力中の数字
    /// </summary>
    public decimal CurrentInputNumber { get; private set; }

    /// <summary>
    /// 入力した計算式
    /// </summary>
    public string InputedNumberAndSymbol { get; private set; } = string.Empty;

    /// <summary>
    /// 入力
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string Input(string value)
    {
        //未入力か確定の状態で演算子
        if (InputList.Count == 0)
        {
            if (IsOperator(value) || value == "±")
            {
                //現在の数字をそのまま使用
                foreach (var n in CurrentInputNumber.ToString())
                {
                    InputList.Add(n.ToString());
                }
            }
        }
        //連続演算子
        if (InputList.Count > 1 && IsOperator(InputList[^1]) && IsOperator(value))
        {
            //3+=は有効
            if (value != "=")
            {
                InputList.RemoveAt(InputList.Count - 1);
            }
        }
        if (string.Compare(value, "C", true) == 0)
        {
            InputList.Clear();
        }
        else if (string.Compare(value, "☒", true) == 0)
        {
            if (InputList.Count != 0)
            {
                InputList.RemoveAt(InputList.Count - 1);
            }
            else
            {
                return CurrentInputNumber.ToString();
            }
        }
        else
        {
            InputList.Add(value);
        }

        CurrentInputNumber = ToCurrentInputNumber(InputList);
        if(value == "=")
        {
            InputList.Clear();
        }
        return CurrentInputNumber.ToString();
    }

    private decimal ToCurrentInputNumber(IEnumerable<string> inputList)
    {
        var calcItem = new List<Tuple<decimal, string>>();
        var inputProcess = "";
        var numberPreOperator = 0m;
        foreach (var input in inputList)
        {
            if (IsOperator(input))
            {
                var ope = input;
                //演算子の前に入力された数字
                if (inputProcess != "")
                {
                    numberPreOperator = ToNumber(inputProcess);
                }
                calcItem.Add(Tuple.Create(numberPreOperator, ope));
                inputProcess = "";
                continue;
            }
            inputProcess += input;
        }
        //processが""なら演算子なので計算結果、そうでなければ数字を返す
        InputedNumberAndSymbol = string.Concat(calcItem.Select(x => x.Item1 + x.Item2)) + (inputProcess == "" ? "" : ToNumber(inputProcess));
        return inputProcess == "" ? Calculator(calcItem) : ToNumber(inputProcess);
    }

    private static decimal ToNumber(string process)
        => decimal.Parse(process.Replace("±", "")) * PlusMinus(process, '±');

    private static int PlusMinus(string s, char c)
        => (s.Length - s.Replace(c.ToString(), "").Length) % 2 == 0 ? 1 : -1;

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
