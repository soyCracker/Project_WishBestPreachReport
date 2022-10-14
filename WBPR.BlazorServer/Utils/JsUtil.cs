using Microsoft.JSInterop;

namespace WBPR.BlazorServer.Utils
{
    public class JsUtil
    {
        public static async Task<string> GetBrowserLang(IJSRuntime jsRuntime)
        {
            return await GetData<string>(jsRuntime, "GetBrowserLang");
        }

        private static async Task<T> GetData<T>(IJSRuntime jsRuntime, string funKey)
        {
            T res = await jsRuntime.InvokeAsync<T>(funKey);
            return res;
        }
    }
}
