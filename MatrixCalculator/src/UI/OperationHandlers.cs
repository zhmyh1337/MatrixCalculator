using System;
using System.Collections.Generic;
using System.Text;
using MatrixCalculator;

namespace UI
{
    class OperationHandlers<T>
    {
        public void Trace()
        {
            var matrix = _matrixReader.ReadMatrix();
            if (matrix == null)
            {
                return;
            }
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
