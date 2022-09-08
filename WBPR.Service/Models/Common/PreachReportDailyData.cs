using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPR.Service.Models.Common
{
    public class PreachReportDailyData
    {
        public DateTime PreachDate { get; set; }
        public double PreachMinute { get; set; }
        public int ReviewNum { get; set; }
        public int BibleStudy { get; set; }
        public int MediaPlayNum { get; set; }
        public int DistributeNum { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
