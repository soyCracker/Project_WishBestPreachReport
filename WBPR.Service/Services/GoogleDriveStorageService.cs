using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBPR.Service.Interfaces;
using WBPR.Service.Models.Response;

namespace WBPR.Service.Services
{
    public class GoogleDriveStorageService : IStorageService
    {
        public GoogleDriveStorageService()
        {

        }

        public Task<MessageModel<bool>> Delete(string filepath, string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<MessageModel<StorageGetRes>> Get(string filepath, string fileName)
        {
            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = cred
            });
            var files = await service.Files.List().ExecuteAsync();
            var fileNames = files.Files.Select(x => x.Name).ToList();

        }

        public Task<MessageModel<bool>> Save(string filepath, string fileName, byte[] bytes)
        {
            throw new NotImplementedException();
        }
    }
}
