namespace MatrixCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // This generic type defines the type we will work with.
            // E. g., the type of matrix elements or the type of multiplication factor.
            UI.UI<decimal>.Launch();
        }
    }
}
