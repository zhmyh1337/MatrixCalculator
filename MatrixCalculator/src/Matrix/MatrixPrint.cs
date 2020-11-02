using System;
using Utilities;

namespace MatrixCalculator
{
    partial class Matrix<T>
    {
        /// <summary>
        /// Prints the matrix to console.
        /// </summary>
        /// <param name="color">Foreground color (leave null to keep the color).</param>
        public void Print(ConsoleColor? color = null)
        {
            if (_rows > MaxSizeForPrintingWithoutConfirm ||
                _columns > MaxSizeForPrintingWithoutConfirm)
            {
                var userConfirmation = ConsoleExtensions.UserPrompt<ConsoleExtensions.PromptYesNo>(
                    "The matrix is too large. Print anyway?"
                );
                if (userConfirmation == ConsoleExtensions.PromptYesNo.No)
                {
                    return;
                }
            }

            ConsoleColor wasColor = Console.ForegroundColor;
            if (color != null)
            {
                Console.ForegroundColor = color.Value;
            }
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    Console.Write($"{{0{PrintFormat}}} ", _data[i, j]);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = wasColor;
        }

        public static string PrintFormat { get; set; } = $",{PrintFormatLength}:{PrintFormatSpecifier}{PrintFormatFloatingPoint}";

        private const int PrintFormatLength = 8;
        private const char PrintFormatSpecifier = 'G';
        private const int PrintFormatFloatingPoint = 4;
        private const int MaxSizeForPrintingWithoutConfirm = 10;
    }
}
