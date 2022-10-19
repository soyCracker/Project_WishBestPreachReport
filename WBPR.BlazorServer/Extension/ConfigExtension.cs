namespace WBPR.BlazorServer.Extension
{
    public static class ConfigExtension
    {
        public static void SetMsAuth(this IServiceCollection serviceCollection)
        {

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
