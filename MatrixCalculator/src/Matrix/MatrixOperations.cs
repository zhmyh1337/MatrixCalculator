using System;
using System.Linq;

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
            var det = Utilities.Misc.ChangeType<T>(1);
            var minusOne = Utilities.Misc.ChangeType<T>(-1);

            var canonical = GaussianMethod(
                swapRowsCallback:    (_1, _2) => det = _mathProvider.Multiply(det, minusOne),
                swapColumnsCallback: (_1, _2) => det = _mathProvider.Multiply(det, minusOne),
                divideRowByValueCallback:   x => det = _mathProvider.Multiply(det, x)
            );
            for (int i = 0; i < _rows; i++)
            {
                det = _mathProvider.Multiply(det, canonical._data[i, i]);
            }
            return det;
        }

        public class SlaeSolution
        {
            public SlaeSolution(Matrix<T> slae)
            {
                VariableCount = slae._columns - 1;
                // x1, x2, ..., xn.
                VariableNames = Enumerable.Range(1, VariableCount).Select(x => $"x{x}").ToArray();

                // When we swap two columns, the variables representing them are swapped either.
                slae = slae.GaussianMethod(
                    swapColumnsCallback: (i, j) => (VariableNames[i], VariableNames[j]) = (VariableNames[j], VariableNames[i]),
                    allowColumnSwapWithLast: false
                );

                for (_mainVariables = 0; _mainVariables < slae._rows; _mainVariables++)
                {
                    bool allZero = true;
                    for (int j = 0; j < VariableCount; j++)
                    {
                        allZero &= _mathProvider.IsZero(slae._data[_mainVariables, j]);
                    }
                    if (allZero)
                    {
                        break;
                    }
                }
                _oneSolution = _mainVariables == VariableCount;
                _freeVariables = VariableCount - _mainVariables;

                _hasSolution = true;
                for (int i = _mainVariables; i < slae._rows; i++)
                {
                    if (!_mathProvider.IsZero(slae._data[i, VariableCount]))
                    {
                        _hasSolution = false;
                        return;
                    }
                }

                _particularSolution = new Utilities.Vector<T>(Enumerable.Range(0, VariableCount).
                    Select(x => x >= slae._rows ? Utilities.Misc.ChangeType<T>(0) : slae._data[x, VariableCount])
                );

                _solution = new (Utilities.Vector<T>, string)[_freeVariables];
                for (int i = 0; i < _freeVariables; i++)
                {
                    _solution[i].Item2 = VariableNames[i + _mainVariables];
                    _solution[i].Item1 = new Utilities.Vector<T>(VariableCount);
                    for (int j = 0; j < _mainVariables; j++)
                    {
                        _solution[i].Item1.Data[j] = slae._data[j, i + _mainVariables];
                        _solution[i].Item1.Data[i + _mainVariables] = Utilities.Misc.ChangeType<T>(1);
                    }
                }

                SortSolution();
            }

            private void SortSolution()
            {
                foreach (var item in _solution)
                {
                    var oldVariableNames = VariableNames.Clone() as string[];
                    Array.Sort(oldVariableNames, item.Item1.Data);
                }
                Array.Sort(VariableNames, _particularSolution.Value.Data);
                Array.Sort(_solution, (x, y) => x.Item2.CompareTo(y.Item2));
            }

            public void PrintSolution(Action<string> writer, Action<string> writeLiner, string valueFormat)
            {
                if (!_hasSolution)
                {
                    writeLiner("This SLAE has no solutions.");
                    return;
                }
                if (_oneSolution)
                {
                    writeLiner("This SLAE has one solution:");
                    for (int i = 0; i < VariableCount; i++)
                    {
                        writeLiner($"{VariableNames[i]} = {_particularSolution.Value.Data[i]}");
                    }
                    return;
                }
                writeLiner("Solution for this SLAE in vector form:");

                var maxNameLength = VariableNames.Max(x => x.Length);
                var namesVector = new Utilities.Vector<string>(VariableNames);

                var centerLine = (VariableCount - 1) / 2;
                Action<int, string> printSeparator = (line, separator) =>
                    writer(line == centerLine ? separator : new string(' ', separator.Length));

                for (int i = 0; i < VariableCount; i++)
                {
                    namesVector.PrintByRow(i, $",{-maxNameLength}", writer);
                    printSeparator(i, "=");
                    _particularSolution.Value.PrintByRow(i, valueFormat, writer, true);

                    foreach (var item in _solution)
                    {
                        printSeparator(i, $"-{item.Item2}");
                        item.Item1.PrintByRow(i, valueFormat, writer, true);
                    }
                    writeLiner("");
                }

                writeLiner($"Where {string.Join(", ", _solution.Select(x => x.Item2))} are any numbers.");
            }

            public int VariableCount { get; }
            public string[] VariableNames { get; set; }

            private readonly bool _hasSolution;
            private readonly bool _oneSolution;
            private readonly int _mainVariables;
            private readonly int _freeVariables;
            private readonly (Utilities.Vector<T>, string)[] _solution = null;
            private readonly Utilities.Vector<T>? _particularSolution = null;
        }
    }
}
