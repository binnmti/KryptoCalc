namespace KryptoCalc.Client.Select;

public class DisplayNumber : IViewSelect
{
    public string Id => "DisplayNumber";
    public string Value { get; set; } = "";
    public string Default => "10";
    public string Title => "表示件数";
    public Dictionary<string, string> Items => new Dictionary<string, string>()
    {
        {"15","15件"},
        {"30","30件"},
        {"45","45件"},
    };
}
