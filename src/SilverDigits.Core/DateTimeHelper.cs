using System;
using System.Collections.Generic;

namespace SilverDigits.Core
{
    /// <summary>
    /// Provides helper methods for working with <see cref="DateTime"/> objects, including week and month calculations.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// Returns the start date of the week for the specified <see cref="DateTime"/> and starting day of the week.
        /// </summary>
        /// <param name="dt">The date to calculate from.</param>
        /// <param name="startOfWeek">The day considered as the start of the week (default is Sunday).</param>
        /// <returns>The date representing the start of the week.</returns>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        /// <summary>
        /// Returns the end date of the week for the specified <see cref="DateTime"/> and starting day of the week.
        /// </summary>
        /// <param name="dt">The date to calculate from.</param>
        /// <param name="startOfWeek">The day considered as the start of the week (default is Sunday).</param>
        /// <returns>The date representing the end of the week.</returns>
        public static DateTime EndOfWeek(this DateTime dt, DayOfWeek startOfWeek = DayOfWeek.Sunday)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(6 - diff).Date;
        }

        /// <summary>
        /// Finds the date of the nth occurrence of a specified weekday in a given month and year.
        /// </summary>
        /// <param name="year">The year to search in.</param>
        /// <param name="month">The month to search in (1-12).</param>
        /// <param name="weekday">The day of the week to find.</param>
        /// <param name="occurrence">The occurrence number (1 for first, 2 for second, etc.; must be 1-5).</param>
        /// <returns>The date of the nth occurrence, or null if it does not exist in the month.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if occurrence is not between 1 and 5.</exception>
        public static DateTime? NthWeekdayOfMonth(int year, int month, DayOfWeek weekday, int occurrence)
        {
            if (occurrence < 1 || occurrence > 5)
                throw new ArgumentOutOfRangeException(nameof(occurrence), "Occurrence must be between 1 and 5.");

            var firstDay = new DateTime(year, month, 1);
            int daysOffset = ((int)weekday - (int)firstDay.DayOfWeek + 7) % 7;
            var firstWeekday = firstDay.AddDays(daysOffset);
            var nthWeekday = firstWeekday.AddDays(7 * (occurrence - 1));
            if (nthWeekday.Month != month)
                return null;
            return nthWeekday;
        }

        /// <summary>
        /// Finds the date of the nth occurrence of a specified weekday in the month and year of the given <see cref="DateTime"/>.
        /// </summary>
        /// <param name="date">The date whose month and year will be used.</param>
        /// <param name="weekday">The day of the week to find.</param>
        /// <param name="occurrence">The occurrence number (1 for first, 2 for second, etc.; must be 1-5).</param>
        /// <returns>The date of the nth occurrence, or null if it does not exist in the month.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if occurrence is not between 1 and 5.</exception>
        public static DateTime? NthWeekdayOfMonth(this DateTime date, DayOfWeek weekday, int occurrence)
        {
            return NthWeekdayOfMonth(date.Year, date.Month, weekday, occurrence);
        }

        /// <summary>
        /// Returns an enumerable of dates from this <see cref="DateTime"/> to <paramref name="to"/> (inclusive).
        /// If <paramref name="to"/> is less than this date, the range is descending.
        /// </summary>
        /// <param name="date">The start date (extension method).</param>
        /// <param name="to">The end date.</param>
        /// <returns>An enumerable of dates in the specified range.</returns>
        public static IEnumerable<DateTime> GetDateRange(this DateTime date, DateTime to)
        {
            int step = date <= to ? 1 : -1;
            for (var d = date; step > 0 ? d <= to : d >= to; d = d.AddDays(step))
            {
                yield return d;
            }
        }

        /// <summary>
        /// Returns an enumerable of dates starting at this <see cref="DateTime"/> for <paramref name="numberOfDays"/> days.
        /// If <paramref name="numberOfDays"/> is negative, dates are subtracted from this date.
        /// </summary>
        /// <param name="date">The start date (extension method).</param>
        /// <param name="numberOfDays">The number of days to include. Negative values enumerate backwards.</param>
        /// <returns>An enumerable of dates starting at <paramref name="date"/>.</returns>
        public static IEnumerable<DateTime> GetDateRange(this DateTime date, int numberOfDays)
        {
            int step = numberOfDays >= 0 ? 1 : -1;
            int count = Math.Abs(numberOfDays);
            for (int i = 0; i < count; i++)
            {
                yield return date.AddDays(i * step);
            }
        }
    }
}
