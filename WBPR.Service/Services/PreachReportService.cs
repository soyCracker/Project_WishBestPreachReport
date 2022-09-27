using System.Text;
using System.Text.Json;
using WBPR.Service.Constants;
using WBPR.Service.Interfaces;
using WBPR.Service.Models.Common;
using WBPR.Service.Models.Response;

namespace WBPR.Service.Services
{
    public class PreachReportService : IPreachReportService
    {
        private readonly IStorageService storageService;

        public PreachReportService(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        public async Task<MessageModel<PreachReportData>> Get(DateTimeOffset dateTimeOffset, string fileName, string filepath)
        {
            string dateFlag = dateTimeOffset.DateTime.ToString("yyyyMMdd");
            string filename = CommonConstant.DATA_FILE_NAME_PREFIX + dateFlag;
            var mm = await storageService.Get(filename, filepath);
            if (mm.Success)
            {
                using StreamReader sr = new StreamReader(new MemoryStream(mm.Data.Data));
                string content = sr.ReadToEnd();
                return new MessageModel<PreachReportData>
                {
                    Data = JsonSerializer.Deserialize<PreachReportData>(content)
                };
            }
            return new MessageModel<PreachReportData>
            {
                Success = false,
                Msg = string.Format("{0} no data", dateFlag),
                Data = null
            };
        }

        public async Task<MessageModel<bool>> Update(PreachReportData data, string filekey, string filepath)
        {
            DateTime utcTime = data.PreachDate.DateTime;
            string fileName = CommonConstant.DATA_FILE_NAME_PREFIX + utcTime.ToString("yyyyMMdd");
            var mm = await storageService.Get(fileName, filepath);
            if (mm.Success)
            {
                await storageService.Delete(fileName, filepath);
            }
            string resStr = JsonSerializer.Serialize(data);
            using MemoryStream ms = new MemoryStream();
            using StreamWriter sw = new StreamWriter(ms, Encoding.UTF8);
            sw.WriteLine(resStr);
            sw.Flush();
            var bytes = ms.ToArray();
            var saveRes = await storageService.Save(fileName, filepath, bytes);
            return new MessageModel<bool>
            {
                Success = saveRes.Data,
                Msg = "",
                Data = saveRes.Data
            };
        }
    }
}
