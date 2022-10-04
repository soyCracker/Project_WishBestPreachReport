using Microsoft.Graph;
using WBPR.Service.Interfaces;
using WBPR.Service.Models.Response;

namespace WBPR.Service.Services
{
    public class OnedriveService : IStorageService
    {
        private readonly GraphServiceClient graphClient;

        public OnedriveService(GraphServiceClient graphClient)
        {
            this.graphClient = graphClient;
        }

        public async Task<MessageModel<bool>> Delete(string filepath, string fileName)
        {
            var req = graphClient.Me.Drive.Root.ItemWithPath(filepath + "/" + fileName).Request();
            await req.DeleteAsync();
            return new MessageModel<bool>
            {
                Data = true
            };
        }

        public async Task<MessageModel<StorageGetRes>> Get(string filepath, string fileName)
        {
            var item = await graphClient.Me.Drive.Root.ItemWithPath(filepath + "/" + fileName).Content.Request().GetAsync();
            if (item != null)
            {
                using MemoryStream ms = new MemoryStream();
                item.Seek(0, SeekOrigin.Begin);
                item.CopyTo(ms);
                byte[] data = ms.ToArray();
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

        public async Task<MessageModel<bool>> Save(string filepath, string fileName, byte[] bytes)
        {
            using Stream stream = new MemoryStream(bytes);
            var res = await graphClient.Me.Drive.Root.ItemWithPath(string.Format("{0}/{1}", filepath, fileName)).Content
                            .Request().PutAsync<DriveItem>(stream);
            return new MessageModel<bool>
            {
                Msg = "Save",
                Data = true
            };
        }
    }
}
