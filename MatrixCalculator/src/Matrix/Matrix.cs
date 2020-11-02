using Utilities;

namespace MatrixCalculator
{
    partial class Matrix<T>
    {
        private Matrix(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            _data = new T[rows, columns];
        }

        private Matrix(int rows, int columns, T[,] data)
        {
            _rows = rows;
            _columns = columns;
            _data = data;
        }

        private Matrix(Matrix<T> other)
            : this(other._rows, other._columns, other._data.Clone() as T[,])
        {
        }

        public static MathProvider.IMathProvider<T> MathProvider { get => _mathProvider; }

        private readonly int _rows;
        private readonly int _columns;
        private T[,] _data;
        private static readonly MathProvider.IMathProvider<T> _mathProvider = Utilities.MathProvider.Create<T>();
    }
}
