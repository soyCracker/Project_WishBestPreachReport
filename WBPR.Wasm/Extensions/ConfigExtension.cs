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
                //options.ProviderOptions.Authentication.Authority =
                //    string.Format("https://login.microsoftonline.com/{0}", Constant.MS_AUTH_TENANT_ID);
                //Microsoft identity platform v2.0才支援多租用戶，但目前不支援&prompt=select_account選擇帳號
                //Azure AD 資訊清單 - allowPublicClient:true, signInAudience: AzureADandPersonalMicrosoftAccount
                options.ProviderOptions.Authentication.Authority = "https://login.microsoftonline.com/common";
                options.ProviderOptions.Authentication.ValidateAuthority = true;
                options.ProviderOptions.LoginMode = "redirect";
                options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/User.Read");
                options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/Files.Read");
                options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/Files.Read.All");
                options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/Files.ReadWrite");
                options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/Files.ReadWrite.All");
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
