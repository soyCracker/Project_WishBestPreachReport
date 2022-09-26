using Microsoft.JSInterop;

namespace WBPR.Wasm.Utils
{
    public class BlazorTimeUtil
    {
        public static async Task<TimeSpan> GetClientTimezoneOffset(IJSRuntime jsRuntime)
        {
            int offsetInMinutes = await jsRuntime.InvokeAsync<int>("GetClientTimezoneOffset");
            return TimeSpan.FromMinutes(-offsetInMinutes);
        }

        public static async Task<DateTimeOffset> GetClientDate(IJSRuntime jsRuntime)
        {
            DateTimeOffset dateTimeOffset = await jsRuntime.InvokeAsync<DateTimeOffset>("GetClientDate");
            return dateTimeOffset;
        }
    }
}
