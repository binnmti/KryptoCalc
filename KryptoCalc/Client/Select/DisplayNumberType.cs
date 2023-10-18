namespace KryptoCalc.Client.Select;

public class DisplayNumberType : IViewSelect
{
    public string Id => "DisplayType";
    public string Value { get; set; } = "";
    public string Default => "10";
    public string Title => "表示件数";
    public Dictionary<string, string> Items => new()
    {
        {"1","1行表示"},
        {"2","2行表示"},
    };
}
