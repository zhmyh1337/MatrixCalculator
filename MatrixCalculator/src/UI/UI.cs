using System;
using Utilities;

namespace UI
{
    static class UI
    {
        public static void Launch()
        {
            new Operation("Find matrix trace", () => OperationHandlers.Trace());
            new Operation("Transpose matrix", () => OperationHandlers.Transpose());
            new Operation("Add two matrices", () => OperationHandlers.AddMatrices());
            new Operation("Subtract two matrices", () => OperationHandlers.SubtractMatrices());
            new Operation("Multiply two matrices", () => OperationHandlers.MultiplyMatrices());
            new Operation("Multiply matrix by number", () => OperationHandlers.MultiplyMatrixByNumber());
            new Operation("Calculate determinant", () => OperationHandlers.Determinant());
            new Operation("Solve a system of linear algebraic equation", () => OperationHandlers.Slae());
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
            }
        }
    }
}
