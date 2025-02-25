using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toodle.CommonExtensions.String
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the string to initials by taking the first letter of each word, converted to uppercase.
        /// </summary>
        /// <param name="str">The input string.</param>
        /// <returns>A string containing the uppercase first letter of each word. Returns empty string if input is null or whitespace.</returns>
        /// <remarks>
        /// - Handles multiple consecutive spaces
        /// - Trims leading and trailing spaces
        /// - Converts all first letters to uppercase
        /// </remarks>
        /// <example>
        /// <code>
        /// string result = "hello world".ToInitials(); // Returns "HW"
        /// string result2 = "United States of America".ToInitials(); // Returns "USOA"
        /// string result3 = "  multiple   spaces   ".ToInitials(); // Returns "MS"
        /// string result4 = "".ToInitials(); // Returns ""
        /// </code>
        /// </example>
        public static string ToInitials(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;

            return string.Join(string.Empty,
                str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(word => word.Trim())
                   .Where(word => word.Length > 0)
                   .Select(word => word[0].ToString().ToUpperInvariant()));
        }

        /// <summary>
        /// Removes all non-alphabetic characters from a string.
        /// </summary>
        /// <param name="input">The string to process.</param>
        /// <returns>A string containing only letters from the alphabet. Returns empty string if input is null or empty.</returns>
        /// <example>
        /// <code>
        /// string result = "Hello123World!".RemoveNonAlphabeticCharacters(); // Returns "HelloWorld"
        /// string result2 = "12345".RemoveNonAlphabeticCharacters(); // Returns ""
        /// string result3 = "Hello World!".RemoveNonAlphabeticCharacters(); // Returns "HelloWorld"
        /// string result4 = null.RemoveNonAlphabeticCharacters(); // Returns ""
        /// </code>
        /// </example>
        public static string RemoveNonAlphabeticCharacters(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return new string(input.Where(char.IsLetter).ToArray());
        }


        /// <summary>
        /// Converts a string to title case
        /// </summary>
        /// <param name="input">The string to convert</param>
        /// <returns>The string in title case</returns>
        public static string ToTitleCase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLower());
        }

    }
}
