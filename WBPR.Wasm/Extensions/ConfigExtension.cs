using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;
using WBPR.Base.Config;

namespace WBPR.Wasm.Extensions
{
    public static class ConfigExtension
    {
        public static void SetMsAuth(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMsalAuthentication(options =>
            {
                options.ProviderOptions.Authentication.ClientId = Constant.MS_AUTH_CLIENT_ID;
                options.ProviderOptions.Authentication.Authority =
                    string.Format("https://login.microsoftonline.com/{0}", Constant.MS_AUTH_TENANT_ID);
                options.ProviderOptions.Authentication.ValidateAuthority = true;
                options.ProviderOptions.LoginMode = "redirect";
                options.ProviderOptions.Authentication.PostLogoutRedirectUri = "/";
            });
        }

        public static async Task SetCulture(WebAssemblyHost host)
        {
            CultureInfo culture;
            var js = host.Services.GetRequiredService<IJSRuntime>();
            var result = await js.InvokeAsync<string>("blazorCulture.get");

            culture = new CultureInfo("zh-Hant");
            await js.InvokeVoidAsync("blazorCulture.set", "zh-Hant");
            /*if (result != null)
            {
                culture = new CultureInfo(result);
            }
            else
            {
                culture = new CultureInfo("zh-Hant");
                await js.InvokeVoidAsync("blazorCulture.set", "zh-Hant");
            }*/

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}
