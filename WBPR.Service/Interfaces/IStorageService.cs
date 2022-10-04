using WBPR.Service.Models.Response;

namespace WBPR.Service.Interfaces
{
    public interface IStorageService
    {
        Task<MessageModel<bool>> Save(string filepath, string fileName, byte[] bytes);
        Task<MessageModel<StorageGetRes>> Get(string filepath, string fileName);
        Task<MessageModel<bool>> Delete(string filepath, string fileName);
    }
}
