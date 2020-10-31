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

        public Matrix<T> Multiply(Matrix<T> other)
        {
            var result = new Matrix<T>(_rows, other._columns);
            for (int i = 0; i < result._rows; i++)
            {
                for (int j = 0; j < result._columns; j++)
                {
                    T sum = default;
                    for (int k = 0; k < _columns; k++)
                    {
                        sum = _mathProvider.Add(sum, _mathProvider.Multiply(_data[i, k], other._data[k, j]));
                    }
                    result._data[i, j] = sum;
                }
            }
            return result;
        }

        public Matrix<T> Multiply(T value)
        {
            var result = new Matrix<T>(_rows, _columns);
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    result._data[i, j] = _mathProvider.Multiply(_data[i, j], value);
                }
            }
            return result;
        }

        public T Determinant()
        {
            var canonical = GaussianMethod();
            var det = canonical._data[0, 0];
            for (int i = 1; i < _rows; i++)
            {
                det = _mathProvider.Multiply(det, canonical._data[i, i]);
            }
            return det;
        }
    }
}
