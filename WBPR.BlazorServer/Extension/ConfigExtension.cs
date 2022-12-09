using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using System.Text.RegularExpressions;
using WBPR.Base.Config;

namespace WBPR.BlazorServer.Extension
{
    public static class ConfigExtension
    {
        public static void SetAuthMSIdentity(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "Microsoft";
            })
            //網站本身的Cookie - based Authentication
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.Redirect(new PathString(Constant.MS_LOGIN_URL));
                    return Task.CompletedTask;
                };
                // ExpireTimeSpan與Cookie.MaxAge都要設定
                options.ExpireTimeSpan = TimeSpan.FromHours(6);
                options.Cookie.MaxAge = options.ExpireTimeSpan;
                //登入後過期時間內没有進行操作就會過期;false有操作還是會過期
                options.SlidingExpiration = false;
            })
            .AddMicrosoftAccount("Microsoft", options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.ClientId = Constant.MS_AUTH_CLIENT_ID;
                options.ClientSecret = Constant.MS_AUTH_CLIENT_SECRET;
                options.Events.OnRedirectToAuthorizationEndpoint = context =>
                {
                    context.HttpContext.Response.Redirect(context.RedirectUri + "&prompt=select_account");
                    return Task.CompletedTask;
                };
            });
        }

        public static void AddCultureResource(this IServiceCollection serviceCollection)
        {
            //語系
            serviceCollection.AddLocalization(option =>
            {
                option.ResourcesPath = "Resources";
            });
        }

        public static void SetCulture(this WebApplication webApplication)
        {
            //語系 **取得的瀏覽器語系不是預期的zh-hant而是zh-tw，所以決定只設定語系'-'前字元
            var supportedCultures = new[] { "en", "zh" };
            webApplication.UseRequestLocalization(new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures));
        }
    }
}
