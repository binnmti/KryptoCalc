using Microsoft.JSInterop;

namespace KryptoCalc.Client;

public interface ICookie
{
    public Task SetValue(string key, string value, int? days = null);
    public Task<string?> GetValue(string key);
}

public class Cookie : ICookie
{
    readonly IJSRuntime JSRuntime;
    string expires = "";

    public Cookie(IJSRuntime jsRuntime)
    {
        JSRuntime = jsRuntime;
        ExpireDays = 300;
    }

    public async Task SetValue(string key, string value, int? days = null)
    {
        var curExp = (days != null) ? (days > 0 ? DateToUTC(days.Value) : "") : expires;
        await SetCookie($"{key}={value}; expires={curExp}; path=/");
    }

    public async Task<string?> GetValue(string key)
    {
        var cValue = await GetCookie();
        if (string.IsNullOrEmpty(cValue)) return null;

        var vals = cValue.Split(';');
        foreach (var val in vals)
            if (!string.IsNullOrEmpty(val) && val.IndexOf('=') > 0)
                if (val[..val.IndexOf('=')].Trim().Equals(key, StringComparison.OrdinalIgnoreCase))
                    return val[(val.IndexOf('=') + 1)..];
        return null;
    }

    private async Task SetCookie(string value)
        => await JSRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{value}\"");

    private async Task<string> GetCookie()
        => await JSRuntime.InvokeAsync<string>("eval", $"document.cookie");

    public int ExpireDays
    {
        set => expires = DateToUTC(value);
    }

    private static string DateToUTC(int days) => DateTime.Now.AddDays(days).ToUniversalTime().ToString("R");
}
