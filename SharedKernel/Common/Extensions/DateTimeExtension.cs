using System;

namespace Common.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime UltimoDiaDoMes(this DateTime date)
        {
            var dia = DateTime.DaysInMonth(date.Year, date.Month);

            return new DateTime(date.Year, date.Month, dia);
        }
    }
}
