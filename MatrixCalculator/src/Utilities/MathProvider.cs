using System;
using System.Numerics;

namespace Utilities
{
    /// Implementation of generic arithmetics.
    /// <see href="https://stackoverflow.com/questions/63694/creating-a-math-library-using-generics-in-c-sharp"/>
    /// Btw, c++ can do this easily.

    static class MathProvider
    {
        /// <summary>
        /// Factory method to create an instance which is IMathProvider&lt;<typeparamref name="T"/>&gt;.
        /// </summary>
        public static IMathProvider<T> Create<T>()
        {
            if (typeof(T) == typeof(int)) 
            {
                return new IntMathProvider() as IMathProvider<T>;
            }
            else if (typeof(T) == typeof(long))
            {
                return new LongMathProvider() as IMathProvider<T>;
            }
            else if (typeof(T) == typeof(BigInteger))
            {
                return new BigIntegerMathProvider() as IMathProvider<T>;
            }
            else if (typeof(T) == typeof(float))
            {
                return new FloatMathProvider() as IMathProvider<T>;
            }
            else if (typeof(T) == typeof(double))
            {
                return new DoubleMathProvider() as IMathProvider<T>;
            }
            else if (typeof(T) == typeof(decimal))
            {
                return new DecimalMathProvider() as IMathProvider<T>;
            }
            else
            {
                throw new NotImplementedException("The math provider wasn't found.");
            }
        }

        public interface IMathProvider<T>
        {
            T Add(T a, T b);
            T Subtract(T a, T b);
            T Multiply(T a, T b);
            T Divide(T a, T b);
        }

        class IntMathProvider : IMathProvider<int>
        {
            public int Add(int a, int b)
            {
                return a + b;
            }

            public int Subtract(int a, int b)
            {
                return a - b;
            }

            public int Multiply(int a, int b)
            {
                return a * b;
            }

            public int Divide(int a, int b)
            {
                return a / b;
            }
        }

        class LongMathProvider : IMathProvider<long>
        {
            public long Add(long a, long b)
            {
                return a + b;
            }

            public long Subtract(long a, long b)
            {
                return a - b;
            }

            public long Multiply(long a, long b)
            {
                return a * b;
            }

            public long Divide(long a, long b)
            {
                return a / b;
            }
        }

        class BigIntegerMathProvider : IMathProvider<BigInteger>
        {
            public BigInteger Add(BigInteger a, BigInteger b)
            {
                return a + b;
            }

            public BigInteger Subtract(BigInteger a, BigInteger b)
            {
                return a - b;
            }

            public BigInteger Multiply(BigInteger a, BigInteger b)
            {
                return a * b;
            }

            public BigInteger Divide(BigInteger a, BigInteger b)
            {
                return a / b;
            }
        }

        class FloatMathProvider : IMathProvider<float>
        {
            public float Add(float a, float b)
            {
                return a + b;
            }

            public float Subtract(float a, float b)
            {
                return a - b;
            }

            public float Multiply(float a, float b)
            {
                return a * b;
            }

            public float Divide(float a, float b)
            {
                return a / b;
            }
        }

        class DoubleMathProvider : IMathProvider<double>
        {
            public double Add(double a, double b)
            {
                return a + b;
            }

            public double Subtract(double a, double b)
            {
                return a - b;
            }

            public double Multiply(double a, double b)
            {
                return a * b;
            }

            public double Divide(double a, double b)
            {
                return a / b;
            }
        }

        class DecimalMathProvider : IMathProvider<decimal>
        {
            public decimal Add(decimal a, decimal b)
            {
                return a + b;
            }

            public decimal Subtract(decimal a, decimal b)
            {
                return a - b;
            }

            public decimal Multiply(decimal a, decimal b)
            {
                return a * b;
            }

            public decimal Divide(decimal a, decimal b)
            {
                return a / b;
            }
        }
    }
}
