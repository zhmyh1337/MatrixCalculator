using System;
using System.Collections.Generic;

namespace UI
{
    class Operation
    {
        public Operation(string name, Action callback)
        {
            _id = _idCounter++;
            _name = name;
            _callback = callback;

            _operationById[_id] = this;
        }

        public void Print()
        {
            Console.WriteLine($"{_id}. {_name}.");
        }

        public static bool TryToExecuteOperationWithId(int id)
        {
            if (!_operationById.TryGetValue(id, out Operation op))
            {
                return false;
            }
            op._callback.Invoke();
            return true;
        }

        public static void PrintAll()
        {
            foreach (var op in _operationById)
            {
                op.Value.Print();
            }
        }

        private int _id;
        private string _name;
        private Action _callback;

        private static int _idCounter = 1;
        private static Dictionary<int, Operation> _operationById = new Dictionary<int, Operation>();
    }
}
