using WBPR.Service.Models.Response;

namespace WBPR.Service.Interfaces
{
    public interface ISettingService
    {
        Task<MessageModel<bool>> IsDarkTheme();
        Task<MessageModel<bool>> SetDarkTheme(bool isDark);
        Task<MessageModel<string>> GetLang();
        Task<MessageModel<bool>> SetLang(string lang);
    }
}
