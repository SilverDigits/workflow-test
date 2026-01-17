using System;
using System.Collections.Generic;
using System.Linq;
using SilverDigits.Core;
using Xunit;

namespace SilverDigits.Core.Tests
{
    public class DateTimeHelperTests
    {
        [Theory]
        [InlineData("2024-06-12", DayOfWeek.Sunday, "2024-06-09")]
        [InlineData("2024-06-12", DayOfWeek.Monday, "2024-06-10")]
        [InlineData("2024-06-09", DayOfWeek.Sunday, "2024-06-09")]
        public void StartOfWeek_ReturnsCorrectDate(string date, DayOfWeek startOfWeek, string expected)
        {
            var dt = DateTime.Parse(date);
            var expectedDate = DateTime.Parse(expected);
            var result = dt.StartOfWeek(startOfWeek);
            Assert.Equal(expectedDate, result);
        }

        [Theory]
        [InlineData("2024-06-12", DayOfWeek.Sunday, "2024-06-15")]
        [InlineData("2024-06-12", DayOfWeek.Monday, "2024-06-16")]
        [InlineData("2024-06-09", DayOfWeek.Sunday, "2024-06-15")]
        public void EndOfWeek_ReturnsCorrectDate(string date, DayOfWeek startOfWeek, string expected)
        {
            var dt = DateTime.Parse(date);
            var expectedDate = DateTime.Parse(expected);
            var result = dt.EndOfWeek(startOfWeek);
            Assert.Equal(expectedDate, result);
        }

        [Theory]
        [InlineData(2026, 3, DayOfWeek.Sunday, 3, "2026-03-15")]
        [InlineData(2026, 2, DayOfWeek.Monday, 1, "2026-02-02")]
        [InlineData(2026, 2, DayOfWeek.Monday, 5, null)]
        public void NthWeekdayOfMonth_ReturnsCorrectDate(int year, int month, DayOfWeek weekday, int occurrence, string expected)
        {
            var result = DateTimeHelper.NthWeekdayOfMonth(year, month, weekday, occurrence);
            if (expected == null)
                Assert.Null(result);
            else
                Assert.Equal(DateTime.Parse(expected), result);
        }

        [Theory]
        [InlineData("2026-03-01", DayOfWeek.Sunday, 3, "2026-03-15")]
        [InlineData("2026-02-01", DayOfWeek.Monday, 1, "2026-02-02")]
        [InlineData("2026-02-01", DayOfWeek.Monday, 5, null)]
        public void NthWeekdayOfMonth_Overload_ReturnsCorrectDate(string date, DayOfWeek weekday, int occurrence, string expected)
        {
            var dt = DateTime.Parse(date);
            var result = dt.NthWeekdayOfMonth(weekday, occurrence);
            if (expected == null)
                Assert.Null(result);
            else
                Assert.Equal(DateTime.Parse(expected), result);
        }

        [Fact]
        public void NthWeekdayOfMonth_ThrowsOnInvalidOccurrence()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => DateTimeHelper.NthWeekdayOfMonth(2024, 6, DayOfWeek.Sunday, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => DateTimeHelper.NthWeekdayOfMonth(2024, 6, DayOfWeek.Sunday, 6));
            Assert.Throws<ArgumentOutOfRangeException>(() => DateTimeHelper.NthWeekdayOfMonth(DateTime.Now, DayOfWeek.Sunday, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => DateTimeHelper.NthWeekdayOfMonth(DateTime.Now, DayOfWeek.Sunday, 6));
        }

        [Theory]
        [InlineData("2024-06-01", "2024-06-05", new[] { "2024-06-01", "2024-06-02", "2024-06-03", "2024-06-04", "2024-06-05" })]
        [InlineData("2024-06-05", "2024-06-01", new[] { "2024-06-05", "2024-06-04", "2024-06-03", "2024-06-02", "2024-06-01" })]
        [InlineData("2024-06-01", "2024-06-01", new[] { "2024-06-01" })]
        public void GetDateRange_FromTo_ReturnsCorrectDates(string from, string to, string[] expected)
        {
            var fromDate = DateTime.Parse(from);
            var toDate = DateTime.Parse(to);
            var result = fromDate.GetDateRange(toDate).ToArray();
            var expectedDates = expected.Select(DateTime.Parse).ToArray();
            Assert.Equal(expectedDates, result);
        }

        [Theory]
        [InlineData("2024-06-01", 5, new[] { "2024-06-01", "2024-06-02", "2024-06-03", "2024-06-04", "2024-06-05" })]
        [InlineData("2024-06-05", -5, new[] { "2024-06-05", "2024-06-04", "2024-06-03", "2024-06-02", "2024-06-01" })]
        [InlineData("2024-06-01", 1, new[] { "2024-06-01" })]
        public void GetDateRange_NumberOfDays_ReturnsCorrectDates(string start, int numberOfDays, string[] expected)
        {
            var startDate = DateTime.Parse(start);
            var result = startDate.GetDateRange(numberOfDays).ToArray();
            var expectedDates = expected.Select(DateTime.Parse).ToArray();
            Assert.Equal(expectedDates, result);
        }
    }
}
