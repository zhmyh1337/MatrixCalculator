namespace MatrixCalculator
{
    partial class Matrix<T>
    {
        public T Trace()
        {
            T sum = default;
            for (int i = 0; i < _rows; i++)
            {
                sum = _mathProvider.Add(sum, _data[i, i]);
            }
            return sum;
        }

        public Matrix<T> Transpose()
        {
            var newData = new T[_columns, _rows];
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    newData[j, i] = _data[i, j];
                }
            }
            return new Matrix<T>(_columns, _rows, newData);
        }

        public Matrix<T> Add(Matrix<T> other)
        {
            var result = new Matrix<T>(_rows, _columns);
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    result._data[i, j] = _mathProvider.Add(_data[i, j], other._data[i, j]);
                }
            }
            return result;
        }

        public Matrix<T> Substract(Matrix<T> other)
        {
            var result = new Matrix<T>(_rows, _columns);
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    result._data[i, j] = _mathProvider.Subtract(_data[i, j], other._data[i, j]);
                }
            }
            return result;
        }
    }
}
