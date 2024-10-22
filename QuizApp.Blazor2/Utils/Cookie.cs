using Microsoft.JSInterop;

namespace QuizApp.Blazor2.Utils;

public interface ICookie
{
    public Task SetValue(string key, string value, int? days = null);
    public Task<string> GetValue(string key, string def = "");
}

public class Cookie : ICookie
{
    private readonly IJSRuntime _jsRuntime;
    private string _expires = "";

    public Cookie(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        ExpireDays = 300;
    }

    public async Task SetValue(string key, string value, int? days = null)
    {
        var curExp = (days != null) ? (days > 0 ? DateToUtc(days.Value) : "") : _expires;
        await SetCookie($"{key}={value}; expires={curExp}; path=/");
    }

    public async Task<string> GetValue(string key, string def = "")
    {
        var cValue = await GetCookie();
        if (string.IsNullOrEmpty(cValue)) return def;                

        var vals = cValue.Split(';');
        foreach (var val in vals)
            if(!string.IsNullOrEmpty(val) && val.IndexOf('=') > 0)
                if(val.Substring(0, val.IndexOf('=')).Trim().Equals(key, StringComparison.OrdinalIgnoreCase))
                    return val.Substring(val.IndexOf('=') + 1);
        return def;
    }

    private async Task SetCookie(string value)
    {
        await _jsRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{value}\"");
    }

    private async Task<string> GetCookie()
    {
        return await _jsRuntime.InvokeAsync<string>("eval", $"document.cookie");
    }

    public int ExpireDays
    {
        set => _expires = DateToUtc(value);
    }

    private static string DateToUtc(int days) => DateTime.Now.AddDays(days).ToUniversalTime().ToString("R");
}
