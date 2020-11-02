using MatrixCalculator;
using System;
using Utilities;

namespace UI
{
    /// <summary>
    /// This class provides user interface to matrix operations.
    /// </summary>
    class OperationHandlers<T> where T : struct
    {
        public void Trace()
        {
            var matrix = _matrixReader.ReadMatrix(null, x => x.HasTrace(), "Trace is not defined for this matrix.");
            if (matrix == null)
            {
                return;
            }
            ConsoleExtensions.PrintLineColor(UI<T>.EmphasizeColor, $"Matrix trace: {matrix.Trace()}.");
        }

        public void Transpose()
        {
            var matrix = _matrixReader.ReadMatrix();
            if (matrix == null)
            {
                return;
            }

            var result = matrix.Transpose();
            ConsoleExtensions.PrintLineColor(UI<T>.EmphasizeColor, "Transposed matrix:");
            result.Print(UI<T>.EmphasizeColor);
        }

        public void AddMatrices()
        {
            var matrixA = _matrixReader.ReadMatrix("First matrix:");
            if (matrixA == null)
            {
                return;
            }
            var matrixB = _matrixReader.ReadMatrix("Second matrix:");
            if (matrixB == null)
            {
                return;
            }

            if (!matrixA.IsSummableBy(matrixB))
            {
                ConsoleExtensions.PrintLineColor(UI<T>.EmphasizeColor, "These matrices are not summable.");
                return;
            }

            var result = matrixA.Add(matrixB);
            ConsoleExtensions.PrintLineColor(UI<T>.EmphasizeColor, "Result matrix:");
            result.Print(UI<T>.EmphasizeColor);
        }

        public void SubtractMatrices()
        {
            var matrixA = _matrixReader.ReadMatrix("First matrix:");
            if (matrixA == null)
            {
                return;
            }
            var matrixB = _matrixReader.ReadMatrix("Second matrix:");
            if (matrixB == null)
            {
                return;
            }

            if (!matrixA.IsSubtractableBy(matrixB))
            {
                ConsoleExtensions.PrintLineColor(UI<T>.EmphasizeColor, "These matrices are not subtractable.");
                return;
            }

            var result = matrixA.Substract(matrixB);
            ConsoleExtensions.PrintLineColor(UI<T>.EmphasizeColor, "Result matrix:");
            result.Print(UI<T>.EmphasizeColor);
        }

        public void MultiplyMatrices()
        {
            var matrixA = _matrixReader.ReadMatrix("First matrix:");
            if (matrixA == null)
            {
                return;
            }
            var matrixB = _matrixReader.ReadMatrix("Second matrix:");
            if (matrixB == null)
            {
                return;
            }

            if (!matrixA.IsMultipliableBy(matrixB))
            {
                ConsoleExtensions.PrintLineColor(UI<T>.EmphasizeColor, "These matrices are not multipliable.");
                return;
            }

            var result = matrixA.Multiply(matrixB);
            ConsoleExtensions.PrintLineColor(UI<T>.EmphasizeColor, "Result matrix:");
            result.Print(UI<T>.EmphasizeColor);
        }

        public void MultiplyMatrixByNumber()
        {
            var matrix = _matrixReader.ReadMatrix();
            if (matrix == null)
            {
                return;
            }

            var factor = ConsoleExtensions.ForceSafeRead<T?>("Multiplication factor");
            if (factor == null)
            {
                return;
            }

            if (!matrix.IsMultipliableBy(factor.Value))
            {
                // Should never happen.
                Console.WriteLine("An unknown error occurred.");
            }

            var result = matrix.Multiply(factor.Value);
            ConsoleExtensions.PrintLineColor(UI<T>.EmphasizeColor, "Result matrix:");
            result.Print(UI<T>.EmphasizeColor);
        }

        public void Determinant()
        {
            var matrix = _matrixReader.ReadMatrix(null, x => x.HasDeterminant(), "Determinant is not defined for this matrix.");
            if (matrix == null)
            {
                return;
            }
            ConsoleExtensions.PrintLineColor(UI<T>.EmphasizeColor, $"Matrix determinant: {matrix.Determinant()}.");
        }

        /// <summary>
        /// System of linear algebraic equation.
        /// </summary>
        public void Slae()
        {
            var slae = _matrixReader.ReadMatrix("todo", x => x.CorrectSlae(), "This matrix is not a SLAE.");
            if (slae == null)
            {
                return;
            }

            var slaeSolution = new Matrix<T>.SlaeSolution(slae);
            slaeSolution.PrintSolution(
                x => ConsoleExtensions.PrintColor(UI<T>.EmphasizeColor, x),
                x => ConsoleExtensions.PrintLineColor(UI<T>.EmphasizeColor, x),
                Matrix<T>.PrintFormat
            );
        }

        private static readonly MatrixReader<T> _matrixReader = new MatrixReader<T>();
    }
}
