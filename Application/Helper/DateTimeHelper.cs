using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helper
{
    public class DateTimeHelper
    {

        public static int GetMinutes(string HoursAndMin)
        {
            if (HoursAndMin.Contains(":"))
            {
                int hours = 0;
                int minutes = 0;

                string[] timestamps = HoursAndMin.Split(":");

                hours = timestamps.Count() > 0 ? Convert.ToInt32(timestamps[0]) : 0;

                minutes = timestamps.Count() > 1 ? Convert.ToInt32(timestamps[1]) : 0;

                return minutes + (hours * 60);
            }
            return 0;
        }

        public static string GetTimeSpan(int? minutes)
        {
            if (minutes != null)
            {
                if (minutes > 0)
                {
                    int hours = minutes.Value / 60;
                    minutes = minutes % 60;
                    return $"{hours:D2}:{minutes:D2}";
                }
                else
                {
                    return $"{0:D2}:{0:D2}";

                }

            }
            else
            {
                return $"{0:D2}:{0:D2}";
            }
        }
        public static DateTime GetStartOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
        }

        public static DateTime GetEndOfDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month,
                                 date.Day, 23, 59, 59, 0);
        }

        public static DateTime GetCurrentTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month,
                                 date.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
        }

        public static DateTime GetDateTimeConverted(DateTime dateTime, string NewGMTTimeZone)
        {

            if (dateTime.Kind != DateTimeKind.Utc)
            {
                var newdateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime,
               TimeZoneInfo.Local.Id, NewGMTTimeZone);
                return newdateTime;
            }
            else
            {
                var newdateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime,
               TimeZoneInfo.Utc.Id, NewGMTTimeZone);
                return newdateTime;
            }

        }

        public static DateTime GetEndOfMonth(int year, int month)
        {
            DateTime lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            return GetEndOfDay(lastDayOfMonth);
        }


        public static DateTime GetStartOfMonth(int year, int month)
        {
            DateTime lastDayOfMonth = new DateTime(year, month, 1);

            return GetStartOfDay(lastDayOfMonth);
        }

    }
}