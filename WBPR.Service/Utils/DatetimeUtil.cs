using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPR.Service.Utils
{
    public class DatetimeUtil
    {
        public DateTimeOffset LocalToUtc(DateTime localTime)
        {
            //var dt = new DateTime(2021, 4, 20);
            //var ti = new TimeSpan(21, 0, 0);
            //var nd = (dt + ti);
            //TimeZone localZone = TimeZone.CurrentTimeZone;
            return new DateTimeOffset(localTime, 
                TimeZoneInfo.FindSystemTimeZoneById("Taipei Standard Time").BaseUtcOffset).ToUniversalTime();
        }
    }
}
