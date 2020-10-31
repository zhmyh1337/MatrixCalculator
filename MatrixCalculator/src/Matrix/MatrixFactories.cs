using System;
using System.IO;
using System.Linq;
using Utilities;

namespace MatrixCalculator
{
    partial class Matrix<T>
    {
        /// <summary>
        /// This factory method creates a matrix,
        /// each element of which is generated using <paramref name="generator"/> parameter.
        /// If an error occurs, returns null.
        /// </summary>
        public static Matrix<T> CreateGenerate(int rows, int columns, Func<T> generator)
        {
            try
            {
                var data = new T[rows, columns];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        data[i, j] = generator();
                    }
                }

                return new Matrix<T>(rows, columns, data);
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Not enough memory to store the matrix.");
            }
            catch
            {
                Console.WriteLine("An unknown error occurred.");
            }

            return null;
        }

        /// <summary>
        /// This factory method creates a matrix,
        /// each element of which is generated randomly as a double
        /// from <paramref name="min"/> to <paramref name="max"/>.
        /// </summary>
        public static Matrix<T> CreateRandomDouble(int rows, int columns, double min = 0, double max = 1)
        {
            return CreateGenerate(rows, columns, 
                () => Misc.ChangeType<T>(Randomizer.Get().NextDouble() * (max - min) + min));
        }

        /// <summary>
        /// This factory method creates a matrix,
        /// each element of which is generated randomly as an integer
        /// from <paramref name="min"/> to <paramref name="max"/> (not included).
        /// </summary>
        public static Matrix<T> CreateRandomInteger(int rows, int columns, int min = 0, int max = 100)
        {
            return CreateGenerate(rows, columns,
                () => Misc.ChangeType<T>(Randomizer.Get().Next(min, max)));
        }

        /// <summary>
        /// This factory method creates a matrix,
        /// which is loaded from file <paramref name="filePath"/>.
        /// If an error occurs, returns null.
        /// </summary>
        /// <remarks>
        /// File format:
        /// rowsCount and columnsCount on the first row,
        /// elements on other.
        /// </remarks>
        public static Matrix<T> CreateFromFile(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);
                if (lines.Length == 0)
                {
                    throw new FormatException();
                }

                var parsedFirstLine = lines.First().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parsedFirstLine.Length != 2)
                {
                    throw new FormatException();
                }

                var rows = int.Parse(parsedFirstLine.First());
                var columns = int.Parse(parsedFirstLine.Last());
                if (rows < 1 || columns < 1)
                {
                    throw new FormatException();
                }

                lines = lines.Skip(1).ToArray();
                if (lines.Length != rows)
                {
                    throw new FormatException();
                }

                var data = new T[rows, columns];
                for (int i = 0; i < rows; i++)
                {
                    var parsedLine = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parsedLine.Length != columns)
                    {
                        throw new FormatException();
                    }

                    for (int j = 0; j < columns; j++)
                    {
                        data[i, j] = Misc.ChangeType<T>(parsedLine[j]);
                    }
                }

                return new Matrix<T>(rows, columns, data);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The file does not exist.");
            }
            catch (Exception e) when (
                e is FormatException ||
                e is InvalidCastException ||
                e is OverflowException
            )
            {
                Console.WriteLine("The file was not in the correct format.");
            }
            catch
            {
                Console.WriteLine("An error occurred while reading from the file.");
            }

            return null;
        }

        public static Matrix<T> CreateFromConsoleInput()
        {
            try
            {
                var rows = ConsoleExtensions.ForceSafeRead<int>("rows", x => x > 0);
                var columns = ConsoleExtensions.ForceSafeRead<int>("columns", x => x > 0);

                var data = new T[rows, columns];
                for (int i = 0; i < rows; i++)
                {
                    var line = Console.ReadLine();
                    if (line == null)
                    {
                        throw new FormatException();
                    }

                    var parsedLine = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parsedLine.Length != columns)
                    {
                        throw new FormatException();
                    }

                    for (int j = 0; j < columns; j++)
                    {
                        data[i, j] = Misc.ChangeType<T>(parsedLine[j]);
                    }
                }

                return new Matrix<T>(rows, columns, data);
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Not enough memory to store the matrix.");
            }
            catch (Exception e) when (
                e is FormatException ||
                e is InvalidCastException ||
                e is OverflowException
            )
            {
                Console.WriteLine("At least one element was not in the correct format.");
            }
            catch
            {
                Console.WriteLine("An unknown error occurred.");
            }

            return null;
        }
    }
}
