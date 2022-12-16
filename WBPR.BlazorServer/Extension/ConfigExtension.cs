﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using WBPR.Base.Config;

namespace WBPR.BlazorServer.Extension
{
    public static class ConfigExtension
    {
        private static readonly string[] scopes = new[] {
                            "User.Read",
                            "Files.ReadWrite"
                        };

        public static void SetAuth(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    //讓MVC及API驗證失敗時有不同的行為
                    context.Response.Redirect(new PathString(Constant.GOOGLE_LOGIN_URL));
                    return Task.CompletedTask;
                };
                // ExpireTimeSpan與Cookie.MaxAge都要設定
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.Cookie.MaxAge = options.ExpireTimeSpan;
                //登入後過期時間內没有進行操作就會過期;false有操作還是會過期
                options.SlidingExpiration = false;
            })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.Authority = configuration["GoogleOIDC:Authority"];
                options.ClientId = configuration["GoogleOIDC:ClientId"];
                options.ClientSecret = configuration["GoogleOIDC:ClientSecret"];
                options.ResponseType = "code";
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.SaveTokens = true;
                options.Prompt = "select_account";
            });
        }

        public static void SetAuthMSIdentity(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            string[] initialScopes = configuration.GetValue<string>("DownstreamApi:Scopes")?.Split(' ');
            serviceCollection.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(configuration.GetSection("AzureAd"))
                .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
                .AddMicrosoftGraph(configuration.GetSection("DownstreamApi"))
                .AddInMemoryTokenCaches();
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
