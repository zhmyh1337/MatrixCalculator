using System;
using Utilities;

namespace UI
{
    static class UI<T>
    {
        public static void Launch()
        {
            Console.WriteLine("Input Ctrl+Z at any time while inputting to cancel.");
            Console.WriteLine();

            new Operation("Find matrix trace", _operationHandlers.Trace);
            new Operation("Transpose matrix", _operationHandlers.Transpose);
            new Operation("Add two matrices", _operationHandlers.AddMatrices);
            new Operation("Subtract two matrices", _operationHandlers.SubtractMatrices);
            new Operation("Multiply two matrices", _operationHandlers.MultiplyMatrices);
            new Operation("Multiply matrix by number", _operationHandlers.MultiplyMatrixByNumber);
            new Operation("Calculate determinant", _operationHandlers.Determinant);
            new Operation("Solve a system of linear algebraic equation", _operationHandlers.Slae);
            new Operation("Clear console", () => Console.Clear());
            new Operation(0, "Exit", () => Environment.Exit(0));

            Loop();
        }

        private static void Loop()
        {
            while (true)
            {
                Operation.PrintAll();
                ConsoleExtensions.ForceSafeRead<int>("Enter operation number to proceed",
                    x => Operation.TryToExecuteOperationWithId(x));
                Console.WriteLine();
            }
        }

        private static readonly OperationHandlers<T> _operationHandlers = new OperationHandlers<T>();
    }
}
