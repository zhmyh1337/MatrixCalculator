using MatrixCalculator;
using System;
using System.Linq;
using Utilities;

namespace UI
{
    /// <summary>
    /// This class handles user-entered matrices.
    /// </summary>
    class MatrixReader<T>
    {
        private enum InputTypes
        {
            file = 0,
            input,
            random
        }

        /// <summary>
        /// This method prompts the user to enter the matrix in one of the ways.
        /// </summary>
        /// <param name="message">Hint before entering.</param>
        /// <param name="predicate">Predicate for the entered matrix.</param>
        /// <param name="predicateHint">Hint when predicate fails.</param>
        /// <returns>Read matrix (or null if canceled).</returns>
        public Matrix<T> ReadMatrix(string message = null, Predicate<Matrix<T>> predicate = null, string predicateHint = null)
        {
            if (message != null)
            {
                Console.WriteLine(message);
            }

            while (true)
            {
                var inputType = ConsoleExtensions.UserPromptNullable<InputTypes>("Choose how to read the matrix");

                var matrix = inputType switch
                {
                    InputTypes.file => ReadFromFile(),
                    InputTypes.input => ReadFromConsoleInput(),
                    InputTypes.random => GenerateRandom(),
                    _ => null,
                };

                // Cancel.
                if (matrix == null)
                {
                    return null;
                }
                // Ok.
                if (predicate == null || predicate.Invoke(matrix))
                {
                    return matrix;
                }
                // Predicate failed.
                if (predicateHint != null)
                {
                    Console.WriteLine(predicateHint);
                }
            }
        }

        private static Matrix<T> ReadFromFile()
        {
            Matrix<T> matrix = null;
            while (matrix == null)
            {
                Console.Write("file path: ");
                var filePath = Console.ReadLine();
                if (filePath == null)
                {
                    return null;
                }
                matrix = Matrix<T>.CreateFromFile(filePath);
            }

            var prompt = ConsoleExtensions.UserPromptNullable<ConsoleExtensions.PromptYesNo>("Print read matrix?");
            if (prompt == null)
            {
                return null;
            }
            if (prompt == ConsoleExtensions.PromptYesNo.Yes)
            {
                matrix.Print();
            }

            return matrix;
        }

        private static void ReadRowsAndColumnsFromConsole(out int? rows, out int? columns)
        {
            rows = ConsoleExtensions.ForceSafeRead<int?>("rows", x => x == null || x > 0);
            if (rows == null)
            {
                columns = null;
                return;
            }
            columns = ConsoleExtensions.ForceSafeRead<int?>("columns", x => x == null || x > 0);
        }

        private static Matrix<T> ReadFromConsoleInput()
        {
            Matrix<T> matrix = null;

            while (matrix == null)
            {
                ReadRowsAndColumnsFromConsole(out int? rows, out int? columns);
                if (rows == null || columns == null)
                {
                    return null;
                }

                matrix = Matrix<T>.CreateFromConsoleInput(rows.Value, columns.Value);
            }

            return matrix;
        }

        private static Matrix<T> GenerateRandom()
        {
            Matrix<T> matrix = null;

            while (matrix == null)
            {
                ReadRowsAndColumnsFromConsole(out int? rows, out int? columns);
                if (rows == null || columns == null)
                {
                    return null;
                }

                matrix = Matrix<T>.CreateRandomInteger(rows.Value, columns.Value);
            }

            Console.WriteLine("Randomized matrix:");
            matrix.Print();

            return matrix;
        }
    }
}
