using WBPR.Service.Constants;

namespace WBPR.Service.Models.Common
{
    public class PreachReportData
    {
        public DateTimeOffset PreachDate { get; set; }
        public List<PreachReportTimePeriod> PreachReportTimes { get; set; }
        public int ReviewNum { get; set; } = 0;
        public int BibleStudyNum { get; set; } = 0;
        public int MediaPlayNum { get; set; } = 0;
        public int DistributeNum { get; set; } = 0;
        public int PreachTimeRadio { get; set; } = (int)PreachRadioEnum.Hours;
        public DateTimeOffset UpdateTime { get; set; }
        public double PreachMinute { get; set; } = 0;
    }

    public class PreachReportMonthData
    {
        public List<PreachReportData> MonthData { get; set; }
    }

    public class PreachReportTimePeriod
    {
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
    }
}
