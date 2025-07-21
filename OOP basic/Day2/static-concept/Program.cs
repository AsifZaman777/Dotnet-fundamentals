
/*
 use cases static?

- Utiity methods for common tasks
- Constant values or configuration settings
- Singletons for shared resources
- Extension methods for types
- Factories for creating instances
- Counter and global state management

*/

/*
 when we are writting static items then we are basically saying that all the instances 
must share the same memory location for that variable or method.

// This is useful for things like counters, where we want to keep track of how many instances have been created

Class Memory (Shared):
┌─────────────────┐
│ Counter Class   │
│ _totalInstance = 2 │  ← Single shared location
└─────────────────┘
        ↑
        │ (shared by all instances)
        │
┌─────────────────┐    ┌─────────────────┐
│ instance1       │    │ instance2       │
│ _instanceId = 1 │    │ _instanceId = 2 │
└─────────────────┘    └─────────────────┘

 */


class Counter
{
    private int _totalInstance = 0;
    private int _instanceId = 0;

    public Counter()
    {
        _instanceId = ++_totalInstance;
        Console.WriteLine($"Instance {_instanceId} created.");
    }

    public string GetTotalInstances()
    {
        return $"Total instances created: {_totalInstance}";
    }

}

class Program
{
    static void Main(string[] args)
    {

        var instance1 = new solvedCounter();
        var instance2 = new solvedCounter();
        Console.WriteLine(solvedCounter.GetTotalInstances());
        //look static constructor is called only once   
    }

}

/*
 output: 
Instance 1 created. Total instances: 1
Instance 1 created. Total instances: 1
 */

/* Problem: 1.Each instance is creating its own _totalInstance rather shared across instances.
 2. This is because _totalInstance is an instance variable, not a static variable.
 3. No class level access. Basically cant access _totalInstance without creating instance of the class.*/




//solution: make _totalInstance static so that it is shared across all instances of the class. 

class solvedCounter
{
    private static int _totalInstance = 0; // static variable shared across all instances
    private int _instanceId = 0;
    private static readonly DateTime _creationTime;
    public solvedCounter()
    {
        _instanceId = ++_totalInstance;
        Console.WriteLine($"Instance {_instanceId} created.");
    }

    static solvedCounter() 
    {
       _creationTime = DateTime.Now; 
        Console.WriteLine($"Counter class created at {_creationTime}");
    }
    /*
     * static constructor:
     1.	Called only once - No matter how many instances you create
     2.	No access modifiers - Cannot be public, private, etc.
     3.	No parameters - Cannot take any arguments
     4.	Automatic execution - You never call it directly
     5.	Thread-safe - .NET guarantees it runs safely
     */

    public static string GetTotalInstances() // static method to access the (static) shared variable
    {
        return $"Total instances created: {_totalInstance}";
    }
}









