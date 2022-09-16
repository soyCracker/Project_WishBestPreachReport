namespace WBPR.Service.Models.Common
{
    public class PreachReportData
    {
        public DateTimeOffset PreachDate { get; set; }
        public List<PreachReportTimePeriod> PreachReportTimes { get; set; }
        public int ReviewNum { get; set; }
        public int BibleStudyNum { get; set; }
        public int MediaPlayNum { get; set; } 
        public int DistributeNum { get; set; }
        public DateTimeOffset UpdateTime { get; set; }
    }

    public class PreachReportMonthlyData
    {
        public List<PreachReportData> PreachReportDatas { get; set; }
    }

    public class PreachReportTimePeriod
    {
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public double PreachMinute { get; set; }
    }
}
