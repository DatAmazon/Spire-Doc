using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extentions
{
    internal static class DateTimeExtensions
    {
        public static DateTime GetFirstDayOfMonth(this DateTime dateTime) => new DateTime(dateTime.Year, dateTime.Month, 1);
        public static DateTime GetLastDayOfMonth(this DateTime dateTime) => dateTime.AddMonths(1).AddDays(-1);
    }
}
