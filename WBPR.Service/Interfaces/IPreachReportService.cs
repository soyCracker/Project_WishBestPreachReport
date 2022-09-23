using WBPR.Service.Models.Common;
using WBPR.Service.Models.Response;

namespace WBPR.Service.Interfaces
{
    public interface IPreachReportService
    {
        MessageModel<bool> Update(PreachReportData data, string filename, string filepath);
        MessageModel<PreachReportData> Get(DateTimeOffset dateTimeOffset, string fileName, string filepath);
    }
}
