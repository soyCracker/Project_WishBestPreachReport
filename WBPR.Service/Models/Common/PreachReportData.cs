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
        public DateTimeOffset UpdateTime { get; set; }
    }

    public class PreachReportMonthlyData
    {
        public List<PreachReportData> PreachReportDatas { get; set; }
    }

    public class PreachReportTimePeriod
    {
        public bool PreachTimeRadio { get; set; } = true;
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset EndTime { get; set; }
        public double PreachMinute { get; set; } = 0;
    }
}
