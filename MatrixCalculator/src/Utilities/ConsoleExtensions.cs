﻿using System;

namespace Utilities
{
    static class ConsoleExtensions
    {
        /// <summary>
		/// This method forces user to input a value of type <typeparamref name="T"/>
        /// while <paramref name="checker"/> predicate is not positive.
        /// Keep <paramref name="checker"/> null to disable predicate checking.
		/// </summary>
		/// <param name="message">Message before inputting (keep null to disable).</param>
		/// <returns>User input of type <typeparamref name="T"/>.</returns>
        public static T ForceSafeRead<T>(string message = null, Predicate<T> checker = null)
        {
            while (true)
            {
                if (message != null)
                {
                    Console.Write(message + ": ");
                }
                try
                {
                    var parsed = Misc.ParseGeneric<T>(Console.ReadLine());

                    if (checker != null && !checker(parsed))
                    {
                        throw new Exception("Predicate failed.");
                    }

                    return parsed;
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
