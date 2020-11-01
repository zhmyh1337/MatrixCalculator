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
                // If it's nullable type with null value
                // (Convert.ChangeType works incorrect in this case).
                if (Nullable.GetUnderlyingType(typeof(T)) != null && input == null)
                {
                    return (T)(object)null;
                }
                else if (typeof(T).IsEnum)
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
                    return (T)Convert.ChangeType(input, Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T));
                }
            }
            catch
            {
                throw new InvalidCastException();
            }
        }
    }
}
