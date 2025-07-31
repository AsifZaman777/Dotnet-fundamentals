
using System;

class DelegatesBasic
{
    //delegate declaration
    public delegate int MathOperation(int x, int y); //delegate creates a new type to hold methods reference

    static int Add(int a, int b)
    {
        Console.WriteLine($"Adding {a} and {b}");
        return a + b;
    }

    static int Subtract(int a, int b)
    {
        Console.WriteLine($"Subtracting {b} from {a}");
        return a - b;
    }

    static void Main()
    {
        //single-cast delegate usage
        Console.WriteLine("Single-cast Delegate Example:");
        MathOperation operation; //instantiate the delegate
        operation = Add; //assign Add method to delegate

        Console.WriteLine($"Result: {operation(5, 3)}"); //invoke delegate
        operation = Subtract;
        Console.WriteLine($"Result: {operation(5, 3)}"); //invoke delegate with new method


        //multi-cast delegate usage
        Console.WriteLine("\nMulti-cast Delegate Example:");
        MathOperation multiCastOperation = null; //initialize to null

        multiCastOperation+= Add; //add Add method
        multiCastOperation+= Subtract;

        //multicasting - invoking all methods in the delegate chain
        var result = multiCastOperation?.Invoke(10, 4); //safe invoke, will call all the subscribed methods
        Console.WriteLine($"Result: {result}");

        //unsubscribe a method
        Console.WriteLine("\nUnsubscribing Subtract method from multi-cast delegate:");
        multiCastOperation -= Subtract; //remove the method from the delegate chain
        result = multiCastOperation?.Invoke(10, 4);
        Console.WriteLine($"Result:{ result}");
    }
}



/*
QUICK REFERENCE:

1. DECLARE A DELEGATE:
   public delegate ReturnType DelegateName(Parameters);

2. USE A DELEGATE:
   DelegateName myDelegate = MethodName;
   myDelegate(parameters);

3. BUILT-IN DELEGATES:
   Action<T>        - void method with parameters
   Func<T,TResult>  - method that returns something
   Predicate<T>     - method that returns bool

4. LAMBDA EXPRESSIONS:
   x => x * 2           (simple expression)
   (x, y) => x + y      (multiple parameters)
   x => { return x * 2; } (statement lambda)

5. COMMON PATTERNS:
   += to add methods
   -= to remove methods
   ?. to safely invoke (null check)
*/



/*
 use case: Event-driven architecture with delegates

DECISION MATRIX:
┌─────────────────┬────────────┬──────────────┬─────────────┐
│ Approach        │ Complexity │ Type Safety  │ Performance │
├─────────────────┼────────────┼──────────────┼─────────────┤
│ Custom Delegate │    Low     │    High      │    High     │
│ Interface       │   Medium   │    High      │   Medium    │
│ EventHandler<T> │    Low     │    High      │    High     │
│ Command Pattern │    High    │    Medium    │   Medium    │
│ Callbacks       │   Medium   │    Low       │    High     │
└─────────────────┴────────────┴──────────────┴─────────────┘

//Event -driven architecture with delegates can be implemented using various patterns. Here are some common approaches and their pros/cons:
1. INTERFACE PATTERN:
   ✅ Type safety through interfaces
   ✅ Clear contracts for handlers
   ✅ Easy to test and mock
   ❌ More boilerplate code
   ❌ Manual subscriber management

2. EVENTHANDLER<T> PATTERN:
   ✅ Built-in .NET pattern
   ✅ Standardized event args
   ✅ Less custom code
   ❌ Still uses delegates internally

3. COMMAND PATTERN:
   ✅ Queuing and delayed execution
   ✅ Undo/Redo capabilities
   ✅ Decoupled execution
   ❌ More complex implementation

4. CALLBACK FUNCTIONS:
   ✅ Functional programming style
   ✅ Flexible method signatures
   ❌ Less type safety
   ❌ Harder to debug


 */