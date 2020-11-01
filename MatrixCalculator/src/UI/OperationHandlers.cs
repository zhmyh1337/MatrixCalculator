using System;
using Utilities;

namespace UI
{
    class OperationHandlers<T>
    {
        public void Trace()
        {
            var matrix = _matrixReader.ReadMatrix(null, x => x.IsSquare(), "Matrix must be square.");
            if (matrix == null)
            {
                return;
            }
            ConsoleExtensions.PrintColor(UI<T>.EmphasizeColor, $"Matrix trace: {matrix.Trace()}.");
        }

        public void Transpose()
        {
            
        }

        public void AddMatrices()
        {

        }

        public void SubtractMatrices()
        {

        }

        public void MultiplyMatrices()
        {

        }

        public void MultiplyMatrixByNumber()
        {

        }

        public void Determinant()
        {

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
