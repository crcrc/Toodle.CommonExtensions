using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toodle.CommonExtensions.Date
{
    /// <summary>
    /// Extension methods for DateTime formatting
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Creates a fluent date formatter for this DateTime
        /// </summary>
        /// <param name="date">The DateTime to format.</param>
        /// <returns>A DateFormatter instance for the specified DateTime.</returns>
        public static DateFormatter Format(this DateTime date) => new DateFormatter(date);

        /// <summary>
        /// Fluent builder for formatting dates
        /// </summary>
        public class DateFormatter
        {
            private readonly DateTime _date;
            private bool _useShortDayName;
            private bool _useFullDayName;
            private bool _useShortMonthName;
            private bool _useShortYear;
            private bool _useOrdinalSuffix = true;

            /// <summary>
            /// Initializes a new instance of the DateFormatter class.
            /// </summary>
            /// <param name="date">The DateTime to format.</param>
            internal DateFormatter(DateTime date) => _date = date;

            /// <summary>
            /// Adds abbreviated day name (e.g., "Mon")
            /// </summary>
            /// <returns>The current DateFormatter instance.</returns>
            public DateFormatter WithShortDayName() { _useShortDayName = true; return this; }

            /// <summary>
            /// Adds full day name (e.g., "Monday")
            /// </summary>
            /// <returns>The current DateFormatter instance.</returns>
            public DateFormatter WithFullDayName() { _useFullDayName = true; return this; }

            /// <summary>
            /// Uses abbreviated month (e.g., "Jan")
            /// </summary>
            /// <returns>The current DateFormatter instance.</returns>
            public DateFormatter WithShortMonthName() { _useShortMonthName = true; return this; }

            /// <summary>
            /// Uses 2-digit year (e.g., "24")
            /// </summary>
            /// <returns>The current DateFormatter instance.</returns>
            public DateFormatter WithShortYear() { _useShortYear = true; return this; }

            /// <summary>
            /// Removes ordinal suffix
            /// </summary>
            /// <returns>The current DateFormatter instance.</returns>
            public DateFormatter WithoutOrdinalSuffix() { _useOrdinalSuffix = false; return this; }

            /// <summary>
            /// Returns a string that represents the formatted date.
            /// </summary>
            /// <returns>A string that represents the formatted date.</returns>
            /// <example>
            /// For DateTime(2025, 2, 25):
            /// - Default: "25th February 2025"
            /// - WithShortDayName(): "Tue 25th February 2025"
            /// - WithShortMonthName(): "25th Feb 2025"
            /// - WithShortYear(): "25th February 25"
            /// - WithoutOrdinalSuffix(): "25 February 2025"
            /// - Combined: DateTime.Now.Format().WithShortDayName().WithShortMonthName().WithShortYear() 
            ///   returns "Tue 25th Feb 25"
            /// </example>
            public override string ToString()
            {
                string dayPrefix = _useShortDayName ? "ddd " : _useFullDayName ? "dddd " : "";
                string dayFormat = _useOrdinalSuffix ? $"d'{GetOrdinalSuffix(_date.Day)}'" : "d";
                string monthFormat = _useShortMonthName ? "MMM" : "MMMM";
                string yearFormat = _useShortYear ? "yy" : "yyyy";

                return _date.ToString($"{dayPrefix}{dayFormat} {monthFormat} {yearFormat}", CultureInfo.CurrentCulture);
            }

            /// <summary>
            /// Gets the ordinal suffix for a given day.
            /// </summary>
            /// <param name="day">The day of the month.</param>
            /// <returns>The ordinal suffix for the specified day.</returns>
            private static string GetOrdinalSuffix(int day) =>
               (day % 100) switch
               {
                   11 => "th",
                   12 => "th",
                   13 => "th",
                   _ => (day % 10) switch
                   {
                       1 => "st",
                       2 => "nd",
                       3 => "rd",
                       _ => "th"
                   }
               };
        }
    }
}
