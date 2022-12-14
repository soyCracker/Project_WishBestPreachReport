using WBPR.Service.Interfaces;
using WBPR.Service.Models.Response;

namespace WBPR.Service.Services
{
    public class TestLocalStorageService : IStorageService
    {
        private readonly string folder = "WBPR_TEST";

        public async Task<MessageModel<bool>> Delete(string filepath, string fileName)
        {
            string fullFileName = Path.Combine(filepath, fileName);
            if (File.Exists(fullFileName))
            {
                File.Delete(fullFileName);
                return new MessageModel<bool>
                {
                    Data = true
                };
            }
            return new MessageModel<bool>
            {
                Msg = string.Format("No data: {0}", fileName),
                Data = false
            };
        }

        public async Task<MessageModel<StorageGetRes>> Get(string filepath, string fileName)
        {
            string fullFileName = Path.Combine(filepath, fileName);
            if (File.Exists(fullFileName))
            {
                using FileStream fs = new FileStream(fullFileName, FileMode.Open, FileAccess.Read);
                using MemoryStream ms = new MemoryStream();
                fs.CopyTo(ms);
                return new MessageModel<StorageGetRes>
                {
                    Success = true,
                    Data = new StorageGetRes
                    {
                        Name = fileName,
                        Data = ms.ToArray()
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

        public async Task<MessageModel<bool>> Save(string filepath, string fileName, byte[] data)
        {
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            string fullpath = Path.Combine(filepath, fileName);
            using FileStream fs = new FileStream(fullpath, FileMode.Create);
            fs.Write(data, 0, data.Length);
            return new MessageModel<bool>
            {
                Data = true
            };
        }
    }
}
