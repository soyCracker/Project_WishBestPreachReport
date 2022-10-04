using WBPR.Service.Models.Common;
using WBPR.Service.Models.Response;

namespace WBPR.Service.Interfaces
{
    public interface IPreachReportService
    {
        Task<MessageModel<bool>> Update(PreachReportData data);
        Task<MessageModel<PreachReportData>> Get(DateTimeOffset dateTimeOffset);
    }
}
