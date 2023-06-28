namespace KryptoCalc.Shared;

//TODO:入力上限桁がいるかも*で上限簡単にこえるので。。。
//TODO:割り算の誤差が気になる

public class Calc
{
    private const string PlusSymbol = "+";
    private const string MinusSymbol = "-";
    private const string MultiplySymbol = "×";
    private const string DivideSymbol = "÷";
    private const string EqualSymbol = "=";
    private const string PlusMinusSymbol = "±";
    private const string ClearSymbol = "C";
    private const string DeleteSymbol = "☒";

    private List<string> InputValueList { get; set; } = new();

    /// <summary>
    /// 現在入力中の数字
    /// </summary>
    public decimal CurrentInputNumber { get; private set; }

    /// <summary>
    /// 入力した計算式
    /// </summary>
    public string InputedNumberAndSymbol { get; private set; } = string.Empty;

    public void Change(string value)
    {
        InputValueList.Clear();
        InputValueList.Add(value);
        CurrentInputNumber = ToCurrentInputNumber(InputValueList);
    }

    /// <summary>
    /// 入力
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string Input(string value)
    {
        //未入力か確定の状態
        if (InputValueList.Count == 0)
        {
            if (IsOperator(value) || value == PlusMinusSymbol)
            {
                //現在の数字をそのまま使用
                foreach (var n in CurrentInputNumber.ToString())
                {
                    InputValueList.Add(n.ToString());
                }
            }
        }
        //valueに演算子が2回連続
        else if (InputValueList.Count > 1 && IsOperator(InputValueList[^1]) && IsOperator(value))
        {
            //1+=という入力は有効
            if (value != EqualSymbol)
            {
                InputValueList.RemoveAt(InputValueList.Count - 1);
            }
        }

        if (IsEqualIgnoreCase(value, ClearSymbol))
        {
            InputValueList.Clear();
        }
        else if (IsEqualIgnoreCase(value, DeleteSymbol))
        {
            if (InputValueList.Count != 0)
            {
                InputValueList.RemoveAt(InputValueList.Count - 1);
            }
            else
            {
                return CurrentInputNumber.ToString();
            }
        }
        else
        {
            InputValueList.Add(value);
        }

        CurrentInputNumber = ToCurrentInputNumber(InputValueList);
        if(value == EqualSymbol)
        {
            InputValueList.Clear();
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
        => decimal.Parse(process.Replace(PlusMinusSymbol, "")) * PlusMinus(process, PlusMinusSymbol);

    private static int PlusMinus(string s, string c)
        => (s.Length - s.Replace(c.ToString(), "").Length) % 2 == 0 ? 1 : -1;

    private static decimal Calculator(List<Tuple<decimal, string>> calcItem)
    {
        string preOperator = "";
        decimal result = 0;
        foreach (var (num, ope) in calcItem)
        {
            result = preOperator switch
            {
                PlusSymbol => result + num,
                MinusSymbol => result - num,
                MultiplySymbol => result * num,
                DivideSymbol => result / num,
                _ => result + num,
            };
            preOperator = ope;
        }
        return result;
    }

    private static bool IsEqualIgnoreCase(string src, string dst)
        => string.Compare(src, dst, true) == 0;

    private static bool IsOperator(string value)
        => value == PlusSymbol
        || value == MinusSymbol
        || value == MultiplySymbol
        || value == DivideSymbol
        || value == EqualSymbol;
}
