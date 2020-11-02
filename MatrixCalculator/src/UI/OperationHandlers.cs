using System;
using Utilities;

namespace UI
{
    class OperationHandlers<T> where T : struct
    {
        public void Trace()
        {
            var matrix = _matrixReader.ReadMatrix(null, x => x.HasTrace(), "Trace is not defined for this matrix.");
            if (matrix == null)
            {
                return;
            }
            ConsoleExtensions.PrintColor(UI<T>.EmphasizeColor, $"Matrix trace: {matrix.Trace()}.");
        }

        public void Transpose()
        {
            var matrix = _matrixReader.ReadMatrix();
            if (matrix == null)
            {
                return;
            }

            var result = matrix.Transpose();
            ConsoleExtensions.PrintColor(UI<T>.EmphasizeColor, "Transposed matrix:");
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
                ConsoleExtensions.PrintColor(UI<T>.EmphasizeColor, "These matrices are not summable.");
                return;
            }

            var result = matrixA.Add(matrixB);
            ConsoleExtensions.PrintColor(UI<T>.EmphasizeColor, "Result matrix:");
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
                ConsoleExtensions.PrintColor(UI<T>.EmphasizeColor, "These matrices are not subtractable.");
                return;
            }

            var result = matrixA.Substract(matrixB);
            ConsoleExtensions.PrintColor(UI<T>.EmphasizeColor, "Result matrix:");
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
                ConsoleExtensions.PrintColor(UI<T>.EmphasizeColor, "These matrices are not multipliable.");
                return;
            }

            var result = matrixA.Multiply(matrixB);
            ConsoleExtensions.PrintColor(UI<T>.EmphasizeColor, "Result matrix:");
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
            ConsoleExtensions.PrintColor(UI<T>.EmphasizeColor, "Result matrix:");
            result.Print(UI<T>.EmphasizeColor);
        }

        public void Determinant()
        {
            var matrix = _matrixReader.ReadMatrix(null, x => x.HasDeterminant(), "Determinant is not defined for this matrix.");
            if (matrix == null)
            {
                return;
            }
            ConsoleExtensions.PrintColor(UI<T>.EmphasizeColor, $"Matrix determinant: {matrix.Determinant()}.");
        }

        /// <summary>
        /// System of linear algebraic equation.
        /// </summary>
        public void Slae()
        {
            
        }

        private static readonly MatrixReader<T> _matrixReader = new MatrixReader<T>();
    }
}
