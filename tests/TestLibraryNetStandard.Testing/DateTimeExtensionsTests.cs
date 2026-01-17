using System;
using Xunit;
using TestLibrary;

namespace TestLibraryNetStandard.Testing
{
    public class DateTimeExtensionsTests
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
    }
}
