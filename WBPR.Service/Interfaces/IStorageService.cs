using WBPR.Service.Models.Response;

namespace WBPR.Service.Interfaces
{
    public interface IStorageService
    {
        MessageModel<bool> Save(string fileName, string filepath, byte[] bytes);
        MessageModel<StorageGetRes> Get(string fileName, string filepath);
        MessageModel<bool> Delete(string fileName, string filepath);
    }
}
