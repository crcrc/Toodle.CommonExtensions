

using System;
using System.Collections.Generic;
using System.Linq;

namespace Toodle.CommonExtensions
{
    /// <summary>
    /// Provides extension methods for working with enums.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the values of the specified enum type, optionally excluding specified items.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="itemsToIgnore">The items to ignore.</param>
        /// <returns>A list of enum values, optionally excluding specified items, ordered by their string representation.</returns>
        public static List<T> GetEnumValues<T>(IEnumerable<T>? itemsToIgnore = null) where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>();

            if (itemsToIgnore != null)
            {
                values = values.Except(itemsToIgnore);
            }

            return values.OrderBy(x => x.ToString()).ToList();
        }
    }
}
