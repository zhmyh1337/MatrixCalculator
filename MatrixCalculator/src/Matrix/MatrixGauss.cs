using System;

namespace MatrixCalculator
{
    partial class Matrix<T>
    {
        /// <summary>
        /// This method applies the Gaussian method on the matrix and returns the matrix in canonical form.
        /// </summary>
        public Matrix<T> GaussianMethod(Action<int, int> swapRowsCallback = null, 
            Action<int, int> swapColumnsCallback = null,
            Action<T> divideRowByValueCallback = null)
        {
            var result = new Matrix<T>(this);

            int i = 0, j = 0;
            while (i < result._rows && j < result._columns)
            {
                if (_mathProvider.IsZero(result._data[i, j]))
                {
                    int k;
                    for (k = i + 1; k < result._rows; k++)
                    {
                        if (!_mathProvider.IsZero(result._data[k, j]))
                        {
                            break;
                        }
                    }

                    // No such rows.
                    if (k == result._rows)
                    {
                        bool notNullAbove = false;
                        for (k = 0; k < i; k++)
                        {
                            notNullAbove |= !_mathProvider.IsZero(result._data[k, j]);
                        }

                        if (notNullAbove)
                        {
                            for (k = j + 1; k < result._columns; ++k)
                            {
                                if (!_mathProvider.IsZero(result._data[i, k]))
                                {
                                    break;
                                }
                            }

                            // No such columns.
                            if (k == result._columns)
                            {
//                                 PrintSteps($"Skip column {j + 1}.");
                                j++;
                            }
                            else
                            {
                                swapColumnsCallback?.Invoke(j, k);
//                                 PrintSteps($"Swap columns {j + 1} and {k + 1}.");
                                for (int l = 0; l < result._rows; l++)
                                {
                                    (result._data[l, j], result._data[l, k]) = (result._data[l, k], result._data[l, j]);
                                }
                            }
                        }
                        else
                        {
//                             PrintSteps($"Skip column {j + 1}.");
                            j++;
                        }
                    }
                    else
                    {
                        swapRowsCallback?.Invoke(i, k);
//                         PrintSteps($"Swap rows {i + 1} and {k + 1}.");
                        for (int l = j; l < result._columns; l++)
                        {
                            (result._data[i, l], result._data[k, l]) = (result._data[k, l], result._data[i, j]);
                        }
                    }
                }
                else
                {
                    T coeff = result._data[i, j];
                    divideRowByValueCallback?.Invoke(coeff);
//                     PrintSteps($"M[{i + 1}] /= {coeff}.");
                    for (int k = j; k < result._columns; ++k)
                    {
                        result._data[i, k] = _mathProvider.Divide(result._data[i, k], coeff);
                    }

                    for (int k = 0; k < result._rows; k++)
                    {
                        if (k == i)
                        {
                            continue;
                        }

                        coeff = _mathProvider.Divide(result._data[k, j], result._data[i, j]);
//                         PrintSteps($"M[{k + 1}] -= M[{i + 1}] * {coeff}.");
                        for (int l = j; l < result._columns; l++)
                        {
                            var tmp = _mathProvider.Multiply(coeff, result._data[i, l]);
                            result._data[k, l] = _mathProvider.Subtract(result._data[k, l], tmp);
                        }
                    }

                    i++; j++;
                }
            }

            return result;
        }
    }
}
