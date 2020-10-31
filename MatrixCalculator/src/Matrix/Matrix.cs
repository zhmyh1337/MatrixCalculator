using Utilities;

namespace MatrixCalculator
{
    partial class Matrix<T>
    {
        private Matrix(int rows, int columns, T[,] data)
        {
            _rows = rows;
            _columns = columns;
            _data = data;

            var x = _mathProvider.Add(_data[0, 0], _data[0, 1]);
        }

        private readonly int _rows;
        private readonly int _columns;
        private T[,] _data;

        private static MathProvider.IMathProvider<T> _mathProvider = MathProvider.Create<T>();
    }
}
