using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FIAP.Repositories.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime FromUnixTimeStamp(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp);
            return dateTime;
        }

        public static double ToUnixTimeStamp(this DateTime dateTime)
        {
            DateTime baseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan ts = (dateTime.ToLocalTime() - baseDateTime.ToLocalTime());
            return Math.Round(ts.TotalSeconds);
        }
    }
}
