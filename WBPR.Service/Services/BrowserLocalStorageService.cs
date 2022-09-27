using Blazored.LocalStorage;
using WBPR.Service.Interfaces;
using WBPR.Service.Models.Response;

namespace WBPR.Service.Services
{
    public class BrowserLocalStorageService : IStorageService
    {
        private readonly ILocalStorageService localStorage;

        public BrowserLocalStorageService(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task<MessageModel<bool>> Delete(string fileName, string filepath)
        {
            await localStorage.RemoveItemAsync(filepath + "/" + fileName);
            return new MessageModel<bool>
            {
                Data = true
            };
        }

        public async Task<MessageModel<StorageGetRes>> Get(string fileName, string filepath)
        {
            bool isExist = await localStorage.ContainKeyAsync(filepath + "/" + fileName);
            if (isExist)
            {
                byte[] data = await localStorage.GetItemAsync<byte[]>(filepath + "/" + fileName);
                return new MessageModel<StorageGetRes>
                {
                    Success = true,
                    Data = new StorageGetRes
                    {
                        Name = fileName,
                        Data = data
                    }
                };
            }
            return new MessageModel<StorageGetRes>
            {
                Success = false,
                Msg = string.Format("No data: {0}", fileName),
                Data = new StorageGetRes
                {
                    Name = "",
                    Data = new byte[0]
                }
            };
        }

        public async Task<MessageModel<bool>> Save(string fileName, string filepath, byte[] bytes)
        {
            await localStorage.SetItemAsync(filepath + "/" + fileName, bytes);
            return new MessageModel<bool>
            {
                Data = true
            };
        }
    }
}
