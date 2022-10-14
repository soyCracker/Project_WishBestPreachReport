using WBPR.Base.Config;
using WBPR.Service.Interfaces;
using WBPR.Service.Models.Response;

namespace WBPR.Service.Services
{
    public class SettingService : ISettingService
    {
        private readonly IBrowserLocalStorageService browserLocalStorageService;
        public SettingService(IBrowserLocalStorageService browserLocalStorageService)
        {
            this.browserLocalStorageService = browserLocalStorageService;
        }

        public async Task<MessageModel<string>> GetLang()
        {
            var res = await browserLocalStorageService.Get<string>(LocalStorageKey.LANG);
            return res;
        }

        public async Task<MessageModel<bool>> SetLang(string lang)
        {
            var res = await browserLocalStorageService.Save(LocalStorageKey.LANG, lang);
            return res;
        }

        public async Task<MessageModel<bool>> IsDarkTheme()
        {
            var res = await browserLocalStorageService.Get<bool>(LocalStorageKey.IS_DARK_THEME);
            return res;
        }

        public async Task<MessageModel<bool>> SetDarkTheme(bool isDark)
        {
            var res = await browserLocalStorageService.Save(LocalStorageKey.IS_DARK_THEME, isDark);
            return res;
        }
    }
}
