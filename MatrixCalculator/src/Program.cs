using System;

namespace MatrixCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = Matrix<string>.CreateRandomDouble(3, 3);
        }
    }
}
