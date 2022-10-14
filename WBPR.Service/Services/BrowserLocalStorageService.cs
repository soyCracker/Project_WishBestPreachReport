using Blazored.LocalStorage;
using WBPR.Service.Interfaces;
using WBPR.Service.Models.Response;

namespace WBPR.Service.Services
{
    public class BrowserLocalStorageService : IBrowserLocalStorageService
    {
        private readonly ILocalStorageService localStorage;

        public BrowserLocalStorageService(ILocalStorageService localStorage)
        {
            this.localStorage=localStorage;
        }

        public async Task<MessageModel<bool>> DelKey(string key)
        {
            await localStorage.RemoveItemAsync(key);
            return new MessageModel<bool>
            {
                Data = true
            };
        }

        public async Task<MessageModel<T>> Get<T>(string key)
        {
            bool isExist = await localStorage.ContainKeyAsync(key);
            if (isExist)
            {
                T data = await localStorage.GetItemAsync<T>(key);
                return new MessageModel<T>
                {
                    Success = true,
                    Data = data
                };
            }
            return new MessageModel<T>
            {
                Success = false,
                Msg = string.Format("No data: {0}", key),
                Data = default
            };
        }

        public Task<MessageModel<bool>> GetBoolData(string key)
        {
            throw new NotImplementedException();
        }

        public Task<MessageModel<string>> GetStrData(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<MessageModel<bool>> Save<T>(string key, T value)
        {
            await localStorage.SetItemAsync(key, value);
            return new MessageModel<bool>
            {
                Data = true
            };
        }
    }
}
