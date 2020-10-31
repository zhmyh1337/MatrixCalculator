using System;

namespace MatrixCalculator
{
    partial class Matrix<T>
    {
        public bool IsSquare()
        {
            return _rows == _columns;
        }

        public bool IsMultipliableBy<T1>(Matrix<T1> other)
        {
            return typeof(T) == typeof(T1) && _columns == other._rows;
        }
    }
}
