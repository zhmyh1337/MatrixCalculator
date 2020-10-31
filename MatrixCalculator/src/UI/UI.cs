using System;

namespace UI
{
    class UI
    {
        public void Launch()
        {
            new Operation("Find matrix trace", () => throw new NotImplementedException());
            new Operation("Transpose matrix", () => throw new NotImplementedException());
            new Operation("Add two matrices", () => throw new NotImplementedException());
            new Operation("Subtract two matrices", () => throw new NotImplementedException());
            new Operation("Multiply two matrices", () => throw new NotImplementedException());
            new Operation("Multiply matrix by number", () => throw new NotImplementedException());
            new Operation("Calculate determinant", () => throw new NotImplementedException());
            new Operation("Solve a system of linear algebraic equation", () => throw new NotImplementedException());
            new Operation("Clear console", () => throw new NotImplementedException());
            new Operation("Exit", () => throw new NotImplementedException());

            Operation.PrintAll();
        }
    }
}
