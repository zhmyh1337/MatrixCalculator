using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    struct Vector<T>
    {
        public Vector(T[] data)
        {
            Data = data;
        }

        public Vector(int size)
        {
            Data = new T[size];
        }

        public Vector(IEnumerable<T> data)
        {
            Data = data.ToArray();
        }

        public void PrintByRow(int row, string format, Action<string> writer)
        {
            if (row == 0)
            {
                writer("┌");
            }
            else if (row == Size - 1)
            {
                writer("└");
            }
            else
            {
                writer("│");
            }

            writer(string.Format($"{{0{format}}}", Data[row]));

            if (row == 0)
            {
                writer("┐");
            }
            else if (row == Size - 1)
            {
                writer("┘");
            }
            else
            {
                writer("│");
            }
        }

        public int Size
        {
            get { return Data.Length; }
        }

        public T[] Data { get; set; }
    }
}
