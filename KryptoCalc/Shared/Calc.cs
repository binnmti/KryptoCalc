namespace KryptoCalc.Shared;

//TODO:入力上限桁がいるかも*で上限簡単にこえるので。。。
//TODO:割り算の誤差が気になる

public class Calc
{
    //value     入力した文字     数字と記号とそれ以外  1-0+-×÷=.C
    //number    数字            0-9
    //symbol    記号            +-×÷=±C
    //operator  演算子          記号の一部(+-×÷=)
    //formula   計算式          1+2×
    private const string PlusSymbol = "+";
    private const string MinusSymbol = "-";
    private const string MultiplySymbol = "×";
    private const string DivideSymbol = "÷";
    private const string EqualSymbol = "=";
    private const string ClearSymbol = "C";
    private const string DeleteSymbol = "☒";
    private const string PramaiSymbol = "±"; //何処に挿入しても有効なので特殊処理が必要

    //入力文字リスト
    private List<string> InputValueList { get; set; } = new();

    /// <summary>
    /// 現在表示している数字（記号は含まない）
    /// </summary>
    public decimal CurrentNumber { get; private set; }

    /// <summary>
    /// 入力した計算式
    /// </summary>
    public string InputedFormula { get; private set; } = string.Empty;

    /// <summary>
    /// 変更
    /// </summary>
    /// <param name="value"></param>
    public void Change(decimal value)
    {
        var number = Math.Round(value, 6).ToString();
        InputValueList.Clear();
        InputValueList.Add(number);
        CurrentNumber = ToCurrentNumber(InputValueList);
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
            //valueが演算子
            if (IsOperator(value) || value == PramaiSymbol)
            {
                //現在の数字をそのまま入力文字リストに追加
                InputValueList.AddRange(ToStrings(CurrentNumber));
            }
        }
        //入力中
        else
        {
            //valueが2回連続演算子
            if (IsOperator(InputValueList[^1]) && IsOperator(value))
            {
                //1+=という入力は有効
                if (value != EqualSymbol)
                {
                    InputValueList.RemoveAt(InputValueList.Count - 1);
                }
            }
        }

        if (IsEqualIgnoreCase(value, ClearSymbol))
        {
            InputValueList.Clear();
        }
        else if (IsEqualIgnoreCase(value, DeleteSymbol))
        {
            InputValueList.RemoveAt(InputValueList.Count - 1);
        }
        else
        {
            InputValueList.Add(value);
        }

        CurrentNumber = ToCurrentNumber(InputValueList);
        if(value == EqualSymbol)
        {
            InputValueList.Clear();
        }
        return CurrentNumber.ToString();
    }

    private decimal ToCurrentNumber(IEnumerable<string> inputList)
    {
        var calcItem = new List<(decimal, string)>();
        var numberAndPramai = "";
        var numberPreOperator = 0m;
        foreach (var input in inputList)
        {
            if (IsOperator(input))
            {
                //演算子の前に入力された数字
                if (numberAndPramai != "")
                {
                    numberPreOperator = ToNumber(numberAndPramai);
                }
                calcItem.Add((numberPreOperator, input));
                numberAndPramai = "";
                continue;
            }
            numberAndPramai += input;
        }
        InputedFormula = ToString(calcItem, numberAndPramai);
        return IsEmpty(numberAndPramai) ? Calculator(calcItem) : ToNumber(numberAndPramai);
    }

    private static string ToString(List<(decimal, string)> calcItem, string numberAndPramai)
    {
        var numberAndSymbol = string.Concat(calcItem.Select(x => x.Item1 + x.Item2));
        var lastNumber = IsEmpty(numberAndPramai) ? "" : ToNumber(numberAndPramai).ToString();
        return numberAndSymbol + lastNumber;
    }

    private static decimal Calculator(List<(decimal, string)> calcItem)
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

    //数字を一文字ずつのstringにして返す
    private static IEnumerable<string> ToStrings(decimal currentNumber)
        => currentNumber.ToString().Select(x => x.ToString());

    private static bool IsEmpty(string numberAndPramai) => numberAndPramai == "";

    //入力文字の±から1か-1をかけて±を消す
    private static decimal ToNumber(string numberAndPramai)
        => decimal.Parse(numberAndPramai.Replace(PramaiSymbol, "")) * Pramai(numberAndPramai);

    //入力文字の±の数を数えて偶数なら1奇数なら-1を返す
    private static int Pramai(string numberAndPramai)
        => numberAndPramai.Count(s => s.ToString() == PramaiSymbol) % 2 == 0 ? 1 : -1;

    private static bool IsEqualIgnoreCase(string src, string dst)
        => string.Compare(src, dst, true) == 0;

    private static bool IsOperator(string value)
        => value == PlusSymbol
        || value == MinusSymbol
        || value == MultiplySymbol
        || value == DivideSymbol
        || value == EqualSymbol;
}
