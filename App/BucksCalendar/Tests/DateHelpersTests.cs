using System;
using BucksCalendar.Utilities;
using Xunit;

namespace Tests
{
    public class DateHelpersTests
    {
        [Fact]
        public void GetNumericYear_NullParam()
        {
            Assert.Equal(DateTime.Today.Year, DateHelpers.CalcNumericYear(null));
        }
        
        [Fact]
        public void GetNumericYear_YearParam()
        {
            Assert.Equal(2020, DateHelpers.CalcNumericYear(2020));
            Assert.Equal(2021, DateHelpers.CalcNumericYear(2021));
        }
        
        [Fact]
        public void GetNumericMonth_NullParam()
        {
            Assert.Equal(DateTime.Today.Month, DateHelpers.CalcNumericMonth(null));
        }
        
        [Fact]
        public void GetNumericMonth_MonthParam()
        {
            Assert.Equal(1, DateHelpers.CalcNumericMonth(0));
            Assert.Equal(3, DateHelpers.CalcNumericMonth(2));
        }
        
        [Fact]
        public void GetFirstDayOfMonth()
        {
            Assert.Equal(new DateTime(2020, 5, 1), DateHelpers.FirstDayOfMonth(2020, 5));
        }
        
        [Fact]
        public void GetLastDayOfMonth()
        {
            Assert.Equal(new DateTime(2021, 3, 31, 23, 59, 59), DateHelpers.LastDayOfMonth(2021, 3));
            Assert.Equal(new DateTime(2021, 11, 30, 23, 59, 59), DateHelpers.LastDayOfMonth(2021, 11));
            Assert.Equal(new DateTime(2021, 2, 28, 23, 59, 59), DateHelpers.LastDayOfMonth(2021, 2));
            Assert.Equal(new DateTime(2020, 2, 29, 23, 59, 59), DateHelpers.LastDayOfMonth(2020, 2));
        }
    }
}