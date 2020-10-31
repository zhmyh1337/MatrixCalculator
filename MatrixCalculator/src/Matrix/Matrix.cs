namespace MatrixCalculator
{
    partial class Matrix<T>
    {
        private Matrix(int rows, int columns, T[,] data)
        {
            _rows = rows;
            _columns = columns;
            _data = data;
        }

        private readonly int _rows;
        private readonly int _columns;
        private T[,] _data;
    }
}
