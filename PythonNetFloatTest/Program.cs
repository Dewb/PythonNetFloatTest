using Python.Runtime;

namespace PythonNetFloatTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string preamble =
                "import clr\n" +
                "clr.AddReference('MyModule')\n" +
                "from MyModule import MyClass\n" +
                "import sys\n" +
                "print(sys.version)";

            string callWithFloatArgs = "result = MyClass.Add(1.0, 2.0)";
            string callWithIntArgs = "result = MyClass.Add(1, 2)";
            string callWithMixedArgs = "result = MyClass.Add(1.0, 2)";

            PythonEngine.Initialize();
            using (Py.GIL())
            {
                using (PyScope scope = Py.CreateScope())
                {
                    scope.Exec(preamble);
                    
                    scope.Exec(callWithFloatArgs);
                    System.Console.WriteLine(scope.Get("result")); // prints "3.0"

                    scope.Exec(callWithIntArgs);
                    System.Console.WriteLine(scope.Get("result")); // prints "3"

                    scope.Exec(callWithMixedArgs); // Python.Runtime.PythonException: 'TypeError : No method matches given arguments for Add'
                    System.Console.WriteLine(scope.Get("result"));
                }
            }
        }
    }
}
