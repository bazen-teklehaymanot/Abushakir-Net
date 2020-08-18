using Abushakir_Net;
using System;
using Xunit;

namespace Abushekir_Net.Tests
{
    public class EtDateTimeTests
    {
        private readonly EthiopianDateTime testData = new EthiopianDateTime(2012, 7, 7, 0, 0, 0, 0);

        [Fact]
        public void TestYear()
        {
            var expectedYear = 2012;
            Assert.Equal(testData.Year, expectedYear);
        }
        [Fact]
        public void TestMonth()
        {
            var expectedMonth = 7;
            Assert.Equal(testData.Month, expectedMonth);
        }

        [Fact]
        public void TestDay()
        {
            var expectedDay = 7;
            Assert.Equal(testData.Day, expectedDay);
        }

        [Fact]
        public void TestGeezDate()
        {
            var expectedValue = "፯";
            Assert.Equal(testData.DayGeez, expectedValue);
        }
        [Fact]
        public void TestParameterLessEtDateTime()
        {
            EthiopianDateTime paramLessData = new EthiopianDateTime();
            EthiopianDateTime currentDateTime = new EthiopianDateTime((long)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds);
            Assert.Equal(paramLessData.Year, currentDateTime.Year);
            Assert.Equal(paramLessData.Month, currentDateTime.Month);
            Assert.Equal(paramLessData.Day, currentDateTime.Day);
            Assert.Equal(paramLessData.DayGeez, currentDateTime.DayGeez);
            Assert.Equal(paramLessData.Hour, currentDateTime.Hour);
            Assert.Equal(paramLessData.Minute, currentDateTime.Minute);
            Assert.Equal(paramLessData.MonthGeezName, currentDateTime.MonthGeezName);
        }

        [Fact]
        public void TestUnixParamEtDateTime()
        {
            var dateTime = new EthiopianDateTime(1585731446021);

            var expectedDateString = "2012-07-23 08:57:26.021";
            var expectedYear = 2012;
            var expectedMonth = 7;
            var expectedDay = 23;
            var expectedDayGeez = "፳፫";
            var expectedHour = 8;
            var expectedMinute = 57;
            var expectedSecond = 26;

            Assert.Equal(dateTime.Year, expectedYear);
            Assert.Equal(dateTime.ToString(), expectedDateString);
            Assert.Equal(dateTime.Month, expectedMonth);
            Assert.Equal(dateTime.Day, expectedDay);
            Assert.Equal(dateTime.DayGeez, expectedDayGeez);
            Assert.Equal(dateTime.Hour, expectedHour);
            Assert.Equal(dateTime.Minute, expectedMinute);
            Assert.Equal(dateTime.Second, expectedSecond);
        }

        [Fact]
        public void TestDateTimeParam()
        {
            var etDateTime = new EthiopianDateTime(DateTime.Now);
            Assert.True(etDateTime.Year.Equals(2012));
        }

        [Fact]
        public void TestToDateTimeConverter()
        {
            var etDateTime = new EthiopianDateTime(1585731446021); // 2012-07-23 08:57:26.021
            var expectedYear = 2020;
            var expectedMonth = 4;
            var expectedDay = 1;

            var convertedDate = etDateTime.ToDateTime();
            Assert.Equal(convertedDate.Year, expectedYear);
            Assert.Equal(convertedDate.Month, expectedMonth);
            Assert.Equal(convertedDate.Day, expectedDay);

        }
    }
}