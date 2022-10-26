using WBPR.Service.Models.Common;
using WBPR.Service.Models.Response;

namespace WBPR.Service.Interfaces
{
    public interface IPreachReportService
    {
        Task<MessageModel<bool>> Update(PreachReportMonthData monthData);
        Task<MessageModel<PreachReportMonthData>> Get(DateTimeOffset dateTimeOffset);
    }
}
