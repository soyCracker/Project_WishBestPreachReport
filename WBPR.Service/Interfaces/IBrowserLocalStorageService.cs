using WBPR.Service.Models.Response;

namespace WBPR.Service.Interfaces
{
    public interface IBrowserLocalStorageService
    {
        Task<MessageModel<bool>> Save<T>(string key, T value);
        Task<MessageModel<bool>> DelKey(string key);
        Task<MessageModel<bool>> GetBoolData(string key);
        Task<MessageModel<string>> GetStrData(string key);
        Task<MessageModel<T>> Get<T>(string key);
    }
}
