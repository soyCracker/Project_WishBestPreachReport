using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WBPR.Service.Utils
{
    public static class DatetimeUtil
    {
        public static DateTimeOffset LocalToUtc(this DateTime localTime, TimeZoneInfo clientTimeZoneInfo)
        {
            //var dt = new DateTime(2021, 4, 20);
            //var ti = new TimeSpan(21, 0, 0);
            //var nd = (dt + ti);
            //TimeZone localZone = TimeZone.CurrentTimeZone;
            return new DateTimeOffset(localTime,
                clientTimeZoneInfo.BaseUtcOffset).ToUniversalTime();
        }

        public static DateTime ToServerLocalTime(this DateTime clientTime)
        {
            DateTime serverTime = TimeZoneInfo.ConvertTime(clientTime, TimeZoneInfo.Local);
            return serverTime;
        }

        public static DateTimeOffset GetServerTime()
        {
            return DateTimeOffset.UtcNow;
        }
    }
}
