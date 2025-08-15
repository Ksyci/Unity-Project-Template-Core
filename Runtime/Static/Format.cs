using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectTemplate
{
    /// <summary>
    /// Provides utility methods and constants for formatting and validating strings according to predefined formats.
    /// </summary>
    public static class Format
    {
        #region Constantes

        public const string EDITABLES_HEADER = "Editables";
        public const string REFERENCES_HEADER = "References";

        private const string ANY_CHARACTERS = "\\s\\S";
        private const string DATE_TIME_FORMAT = "dd-MM-yyyy, HH:mm";
        private const string HEXADECIMAL_CHARACTERS = "a-fA-F0-9";
        private const string INTEGER_CHARACTERS = "0-9";
        private const string NAME_CHARACTERS = "a-zA-Z0-9 ";
        private const string WORD_CHARACTERS = "a-zA-Z";

        #endregion

        #region Enumerations

        /// <summary>
        /// Defines the set of supported format types for input validation and cleaning.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item><description><c>DEFAULT</c> = Allows any characters.</description></item>
        /// <item><description><c>WORD</c> = Allows only letters (a-z, A-Z).</description></item>
        /// <item><description><c>NAME</c> = Allows letters, digits and spaces.</description></item>
        /// <item><description><c>DECIMAL</c> = Allows only digits (0-9).</description></item>
        /// <item><description><c>HEXADECIMAL</c> = Allows digits and letters A-F (case insensitive).</description></item>
        /// </list>
        /// </remarks>
        public enum Type { DEFAULT, WORD, NAME, INTEGER, HEXADECIMAL }

        #endregion

        #region Properties

        private static readonly Dictionary<Type, string> _formatToPattern = new()
        {
            { Type.DEFAULT, ANY_CHARACTERS },
            { Type.WORD, WORD_CHARACTERS },
            { Type.NAME, NAME_CHARACTERS },
            { Type.INTEGER, INTEGER_CHARACTERS },
            { Type.HEXADECIMAL, HEXADECIMAL_CHARACTERS },
        };

        private static readonly Dictionary<Type, Regex> _validators = _formatToPattern
            .ToDictionary(kv => kv.Key, kv => new Regex($"^[{kv.Value}]*$", RegexOptions.Compiled));

        private static readonly Dictionary<Type, Regex> _cleaners = _formatToPattern
            .ToDictionary(kv => kv.Key, kv => new Regex($"[^{kv.Value}]", RegexOptions.Compiled));

        #endregion

        #region Methods

        /// <summary>
        /// Removes all characters from the input string that are not valid for the specified format type.
        /// </summary>
        /// <param name="input">The string to clean.</param>
        /// <param name="format">The format type to use for cleaning.</param>
        /// <returns>The cleaned string.</returns>
        public static string Clean(string input, Type format)
            => _cleaners[format].Replace(input, string.Empty);

        /// <summary>
        /// Checks if the input string only contains characters valid for the specified format type.
        /// </summary>
        /// <param name="input">The string to validate.</param>
        /// <param name="format">The format type to validate against.</param>
        /// <returns>True if the input is valid; otherwise, false.</returns>
        public static bool IsValid(string input, Type format)
            => _validators[format].IsMatch(input);

        /// <summary>
        /// Formats the input string by capitalizing the first letter and inserting spaces before uppercase letters.
        /// </summary>
        /// <param name="input">The string to polish.</param>
        /// <returns>The polished string.</returns>
        public static string Polish(string input)
        {
            input = IsValid(input, Type.WORD) ? input : Clean(input, Type.WORD);

            return string.Concat
            (
                input.Select
                (
                    (c, i) => i == 0
                    ? char.ToUpperInvariant(c).ToString()
                    : char.IsUpper(c) ? " " + c : c.ToString()
                )
            );
        }

        /// <summary>
        /// Formats a <see cref="DateTime"/> value as a string using a standard format (dd-MM-yyyy, HH:mm).
        /// </summary>
        /// <param name="time">The date and time to format.</param>
        /// <returns>The formatted date string.</returns>
        public static string ToDate(long time)
        {
            DateTime dateTime = new(time);
            return dateTime.ToString(DATE_TIME_FORMAT, CultureInfo.InvariantCulture);
        }

        #endregion
    }
}