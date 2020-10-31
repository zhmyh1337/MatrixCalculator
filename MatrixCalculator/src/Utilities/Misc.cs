using System;

namespace Utilities
{
    static class Misc
    {
        /// <summary>
        /// This method casts <paramref name="input"/> to <typeparamref name="T"/>.
        /// If an error occurs while casting, throws <see cref="InvalidCastException"/>.
        /// </summary>
        /// <exception cref="InvalidCastException"/>
        public static T ChangeType<T>(object input)
        {
            try
            {
                if (typeof(T).IsEnum)
                {
                    return (T)Enum.ToObject(typeof(T), input);
                }
                else if (typeof(T) == typeof(System.Numerics.BigInteger))
                {
                    // This works only with integers.
                    return (T)(object)System.Numerics.BigInteger.Parse(input.ToString());
                }
                else
                {
                    return (T)Convert.ChangeType(input, typeof(T));
                }
            }
            catch
            {
                throw new InvalidCastException();
            }
        }
    }
}
