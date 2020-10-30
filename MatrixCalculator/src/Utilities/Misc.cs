using System;
using System.ComponentModel;

namespace Utilities
{
    static class Misc
    {
        /// <summary>
        /// This method parses type <typeparamref name="T"/> from string <paramref name="input"/>.
        /// If an error occurs while parsing, throws <see cref="FormatException"/>.
        /// </summary>
        /// <returns>Parsed value.</returns>
        /// <exception cref="FormatException"/>
        /// <exception cref="NullReferenceException"/>
        public static T ParseGeneric<T>(this string input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)converter.ConvertFromString(input);
            }
            catch (NotSupportedException)
            {
                throw new FormatException();
            }
            catch
            {
                throw;
            }
        }
    }
}
