using MatrixCalculator;
using System;
using System.Linq;
using System.Numerics;
using Utilities;

namespace UI
{
    /// <summary>
    /// This class handles user-entered matrices.
    /// </summary>
    class MatrixReader<T>
    {
        public Matrix<T> ReadMatrix(string message = null, Predicate<Matrix<T>> predicate = null)
        {
            if (message != null)
            {
                Console.WriteLine(message);
            }

            var inputTypes = new string[] {
                null,
                "file",
                "input",
                "random"
            };
            var inputType = ConsoleExtensions.ForceSafeRead<string>(
                $"Choose how to read the matrix ({string.Join(", ", inputTypes.Skip(1))})",
                x => Array.IndexOf(inputTypes, x) != -1
            );

            return inputType switch
            {
                "file" => ReadFromFile(),
                "input" => ReadFromConsoleInput(),
                "random" => GenerateRandom(),
                _ => null,
            };
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
            return matrix;
        }

        private static Matrix<T> ReadFromConsoleInput()
        {
            Matrix<T> matrix = null;

            while (matrix == null)
            {
                matrix = Matrix<T>.CreateFromConsoleInput();
            }

            return matrix;
        }

        private static Matrix<T> GenerateRandom()
        {
            Matrix<T> matrix = null;

            while (matrix == null)
            {
                var rows = ConsoleExtensions.ForceSafeRead<int?>("rows", x => x == null || x > 0);
                if (rows == null)
                {
                    return null;
                }

                var columns = ConsoleExtensions.ForceSafeRead<int?>("columns", x => x == null || x > 0);
                if (columns == null)
                {
                    return null;
                }

                matrix = Matrix<T>.CreateRandomInteger(rows.Value, columns.Value);
            }

            return matrix;
        }
    }
}
