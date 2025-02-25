using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toodle.CommonExtensions.String
{
    public static class DateStringExtensions
    {
        /// <summary>
        /// Converts the Month Name to Number, eg. January -> 1
        /// </summary>
        /// <param name="month">The name of the month</param>
        /// <returns>The number of the month, or 0 if the month name is invalid</returns>
        public static int MonthNameToNumber(this string month)
        {
            if (string.IsNullOrWhiteSpace(month)) return 0;

            return DateTime.TryParseExact(month, "MMMM", CultureInfo.CurrentCulture,
                DateTimeStyles.None, out DateTime date) ? date.Month : 0;
        }

        /// <summary>
        /// Checks if the given month name is the current month.
        /// </summary>
        /// <param name="monthName">The name of the month</param>
        /// <returns>True if the given month name is the current month, otherwise false</returns>
        public static bool IsCurrentMonth(this string monthName)
        {
            return DateTime.Now.Month == MonthNameToNumber(monthName);
        }
    }
}
