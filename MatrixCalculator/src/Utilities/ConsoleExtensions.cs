using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Utilities
{
    static class ConsoleExtensions
    {
        /// <summary>
		/// This method forces the user to input a value of type <typeparamref name="T"/>
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
                    var type = typeof(T);
                    var userInput = Console.ReadLine();
                    T parsed = type.IsEnum ?
                        (T)Enum.Parse(type, userInput) :
                        Misc.ChangeType<T>(userInput);

                    if (type.IsEnum && Enum.IsDefined(type, parsed))
                    {
                        throw new Exception("Enum value is not defined.");
                    }

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

        public enum PromptYesNo
        {
            Yes = 0,
            No
        }

        /// <summary>
        /// This method forces the user to input a value of enum <typeparamref name="T"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// UserPrompt&lt;PromptYesNo&gt;("prompt");
        /// </code>
        /// </example>
        public static T UserPrompt<T>(string message, bool ignoreCase = true)
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("The generic type is not a enum.");
            }

            Func<string, string> transformation = x => ignoreCase ? x?.ToLower() : x;

            var names = Enum.GetNames(typeof(T)).Select(transformation).ToArray();

            var userInput = ForceSafeRead<string>(
                $"{message} ({string.Join(", ", names)})",
                x => Array.IndexOf(names, transformation(x)) != -1
            );

            return (T)Enum.Parse(typeof(T), userInput, ignoreCase);
        }

        /// <summary>
        /// Same as <see cref="UserPrompt{T}(string)"/>, but nullable.
        /// (User can use Ctrl+Z to pass null value).
        /// </summary>
        public static T? UserPromptNullable<T>(string message, bool ignoreCase = true) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("The generic type is not a enum.");
            }

            Func<string, string> transformation = x => ignoreCase ? x?.ToLower() : x;

            var names = Enumerable.Concat(
                new string[] { null }, 
                Enum.GetNames(typeof(T))
            ).Select(transformation).ToArray();
            var userInput = ForceSafeRead<string>(
                $"{message} ({string.Join(", ", names.Skip(1))})",
                x => Array.IndexOf(names, transformation(x)) != -1
            );

            if (userInput == null)
            {
                return null;
            }
            return (T?)Enum.Parse(typeof(T), userInput, ignoreCase);
        }

        public static void PrintColor(ConsoleColor color, string message)
        {
            var wasColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = wasColor;
        }

        public static void PrintColor(ConsoleColor color, string message, params string[] args)
        {
            PrintColor(color, string.Format(message, args));
        }
    }
}
