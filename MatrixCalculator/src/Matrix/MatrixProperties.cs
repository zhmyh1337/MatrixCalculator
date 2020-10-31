using System;

namespace MatrixCalculator
{
    partial class Matrix<T>
    {
        public bool IsSquare()
        {
            return _rows == _columns;
        }

        public bool HasTrace() => IsSquare();

        public bool HasDeterminant() => IsSquare();

        public bool IsSummableBy<T1>(Matrix<T1> other)
        {
            return typeof(T) == typeof(T1) && _rows == other._rows && _columns == other._columns;
        }

        public bool IsSubtractableBy<T1>(Matrix<T1> other)
        {
            return typeof(T) == typeof(T1) && _rows == other._rows && _columns == other._columns;
        }

        public bool IsMultipliableBy<T1>(Matrix<T1> other)
        {
            return typeof(T) == typeof(T1) && _columns == other._rows;
        }

        public bool IsMultipliableBy<T1>(T1 value)
        {
            return typeof(T) == typeof(T1);
        }
    }
}
