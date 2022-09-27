using WBPR.Service.Models.Response;

namespace WBPR.Service.Interfaces
{
    public interface IStorageService
    {
        Task<MessageModel<bool>> Save(string fileName, string filepath, byte[] bytes);
        Task<MessageModel<StorageGetRes>> Get(string fileName, string filepath);
        Task<MessageModel<bool>> Delete(string fileName, string filepath);
    }
}
