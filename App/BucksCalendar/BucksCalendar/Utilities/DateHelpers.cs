using System;

namespace BucksCalendar.Utilities
{
    public class DateHelpers
    {
        public static int CalcNumericYear(int? year)
        {
            return year ?? DateTime.Today.Year;
        }
        
        public static int CalcNumericMonth(int? month)
        {
            var transformMonth = month != null ? month + 1 : null;
            return transformMonth ?? DateTime.Today.Month;
        }

        public static DateTime FirstDayOfMonth(int year, int month)
        {
            return new DateTime(year, month, 1);
        }
        
        public static DateTime LastDayOfMonth(int year, int month)
        {
            return new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59);
        }
    }
}