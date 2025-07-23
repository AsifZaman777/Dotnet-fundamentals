using System;

/*
╔══════════════════════════════════════════════════════════════════════════════════════╗
║                            OVERLOADING VS OVERRIDING IN C#                           ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

OVERLOADING vs OVERRIDING COMPARISON:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│                    OVERLOADING                  │           OVERRIDING              │
│ ┌─────────────────────────────────────────────┐ │ ┌─────────────────────────────────┐ 
│ │ • Same method name, different parameters    │ │ │ • Same signature in hierarchy   │ 
│ │ • Compile-time polymorphism (Early Binding)│ │ │ • Runtime polymorphism (Late)    │  
│ │ • Within same class or inheritance          │ │ │ • Across inheritance hierarchy  │ 
│ │ • Method signature must differ              │ │ │ • virtual/override keywords     │ 
│ │ • No special keywords required             │ │ │ • Same return type required      │  
│ └─────────────────────────────────────────────┘ │ └─────────────────────────────────┘ 
└─────────────────────────────────────────────────────────────────────────────────────┘

DECISION FLOW:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│ Same Method Name?                                                                   │
│         │                                                                           │
│         ▼                                                                           │
│ ┌─────────────────┐    ┌─────────────────┐                                          │
│ │Different Params?│ -> │   OVERLOADING   │                                          │
│ └─────────────────┘    └─────────────────┘                                          │
│         │                                                                           │
│         ▼                                                                           │
│ ┌─────────────────┐    ┌─────────────────┐                                          │
│ │Same Signature + │ -> │   OVERRIDING    │                                          │
│ │Inheritance?     │    └─────────────────┘                                          │
│ └─────────────────┘                                                                 │
└─────────────────────────────────────────────────────────────────────────────────────┘
*/

/*
╔══════════════════════════════════════════════════════════════════════════════════════╗
║                                METHOD OVERLOADING                                    ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

OVERLOADING RULES:
1. Same method name
2. Different parameter list (number, type, or order)
3. Return type can be different (but doesn't count for overloading)
4. Access modifiers can be different

OVERLOADING WAYS:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│ 1. Different Number of Parameters:                                                 │
│    Calculate(int a)                                                                │
│    Calculate(int a, int b)                                                         │
│                                                                                    │
│ 2. Different Parameter Types:                                                      │
│    Calculate(int a)                                                                │
│    Calculate(double a)                                                             │
│                                                                                    │
│ 3. Different Parameter Order:                                                      │
│    Process(string name, int age)                                                   │
│    Process(int age, string name)                                                   │
└────────────────────────────────────────────────────────────────────────────────────┘
*/

class Calculator
{
    /*
    OVERLOADING EXAMPLE 1: Different Number of Parameters
    ┌─────────────────────────────────────────────────────────────┐
    │ Method Name: Add                                            │
    │ Variations: 2 params, 3 params, 4 params                    │
    │ Compile-time resolution based on arguments passed           │
    └─────────────────────────────────────────────────────────────┘
    */
    public int Add(int a, int b)
    {
        Console.WriteLine("Add(int, int) called");
        return a + b;
    }

    public int Add(int a, int b, int c)
    {
        Console.WriteLine("Add(int, int, int) called");
        return a + b + c;
    }

    public int Add(int a, int b, int c, int d)
    {
        Console.WriteLine("Add(int, int, int, int) called");
        return a + b + c + d;
    }

    /*
    OVERLOADING EXAMPLE 2: Different Parameter Types
    ┌─────────────────────────────────────────────────────────────┐
    │ Method Name: Multiply                                       │
    │ Variations: int, double, decimal types                      │
    │ Compiler chooses best match based on argument types         │
    └─────────────────────────────────────────────────────────────┘
    */
    public int Multiply(int a, int b)
    {
        Console.WriteLine("Multiply(int, int) called");
        return a * b;
    }

    public double Multiply(double a, double b)
    {
        Console.WriteLine("Multiply(double, double) called");
        return a * b;
    }

    public decimal Multiply(decimal a, decimal b)
    {
        Console.WriteLine("Multiply(decimal, decimal) called");
        return a * b;
    }

    /*
    OVERLOADING EXAMPLE 3: Different Parameter Order
    ┌─────────────────────────────────────────────────────────────┐
    │ Method Name: Display                                        │
    │ Variations: (string, int) vs (int, string)                  │
    │ Order matters for overload resolution                       │
    └─────────────────────────────────────────────────────────────┘
    */
    public void Display(string message, int number)
    {
        Console.WriteLine($"Display(string, int): {message} - {number}");
    }

    public void Display(int number, string message)
    {
        Console.WriteLine($"Display(int, string): {number} - {message}");
    }

    /*
    OVERLOADING EXAMPLE 4: Optional Parameters vs Overloading
    ┌─────────────────────────────────────────────────────────────┐
    │ Alternative to multiple overloads using default parameters  │
    │ Modern C# approach for similar functionality                │
    └─────────────────────────────────────────────────────────────┘
    */
    public int Power(int baseNum, int exponent = 2)
    {
        Console.WriteLine($"Power({baseNum}, {exponent}) called");
        return (int)Math.Pow(baseNum, exponent);
    }

    //virtual method for overriding
    public virtual double Calculate(double value)
    {
        return value;
    }
}

/*
CONSTRUCTOR OVERLOADING:
Multiple constructors with different parameter lists
*/
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }

    // Constructor overloading
    public Person()
    {
        Console.WriteLine("Person() - Default constructor");
        Name = "Unknown";
        Age = 0;
        Email = "no-email";
    }

    public Person(string name)
    {
        Console.WriteLine("Person(string) constructor");
        Name = name;
        Age = 0;
        Email = "no-email";
    }

    public Person(string name, int age)
    {
        Console.WriteLine("Person(string, int) constructor");
        Name = name;
        Age = age;
        Email = "no-email";
    }

    public Person(string name, int age, string email)
    {
        Console.WriteLine("Person(string, int, string) constructor");
        Name = name;
        Age = age;
        Email = email;
    }

    public override string ToString()
    {
        return $"Person: {Name}, Age: {Age}, Email: {Email}";
    }
}

// ═══════════════════════════════════════════════════════════════════════════════════
//                                METHOD OVERRIDING
// ═══════════════════════════════════════════════════════════════════════════════════

/*
╔══════════════════════════════════════════════════════════════════════════════════════╗
║                                METHOD OVERRIDING                                     ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

OVERRIDING RULES:
1. Inheritance relationship required
2. Base method must be virtual, abstract, or override
3. Derived method must use override keyword
4. Exact same signature (name, parameters, return type)
5. Access modifier must be same or more accessible

OVERRIDING FLOW:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│ Base Class: virtual Method()    →    Derived Class: override Method()               │
│      ↓                                        ↓                                     │
│ Provides default implementation     Provides specific implementation                │
│      ↓                                        ↓                                     │
│ Can be called via base.Method()     Runtime polymorphism applies                    │
└─────────────────────────────────────────────────────────────────────────────────────┘
*/

// Base class with virtual methods
abstract class Vehicle
{
    protected string brand;
    protected int year;

    public Vehicle(string brand, int year)
    {
        this.brand = brand;
        this.year = year;
    }

    /*
    VIRTUAL METHOD - Can be overridden - Provides default implementation
    */
    public virtual void Start()
    {
        Console.WriteLine($"Vehicle {brand} ({year}) is starting...");
    }

    public virtual void Stop()
    {
        Console.WriteLine($"Vehicle {brand} ({year}) is stopping...");
    }

    /*
    ABSTRACT METHOD - Must be overridden - No implementation in base class
    */
    public abstract void Move();

    /*
    VIRTUAL METHOD with implementation - Can call base implementation in override
    */
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Vehicle Info: {brand} {year}");
    }
}

/*
OVERRIDING EXAMPLE 1: Car class
Different implementations for each virtual method
*/
class Car : Vehicle
{
    private int doors;

    public Car(string brand, int year, int doors) : base(brand, year)
    {
        this.doors = doors;
    }

    /*
    OVERRIDE - Completely replace base implementation
    */
    public override void Start()
    {
        Console.WriteLine($"Car {brand} engine starting with key ignition...");
    }

    public override void Stop()
    {
        Console.WriteLine($"Car {brand} stopping and parking...");
    }

    /*
    OVERRIDE - Required implementation of abstract method
    */
    public override void Move()
    {
        Console.WriteLine($"Car {brand} driving on roads with {doors} doors");
    }

    /*
    OVERRIDE - Extending base implementation
    Calls base method then adds more functionality
    */
    public override void DisplayInfo()
    {
        base.DisplayInfo(); // Call base implementation
        Console.WriteLine($"Car specific: {doors} doors");
    }
}

/*
OVERRIDING EXAMPLE 2: Motorcycle class
Different behavior for same methods
*/
class Motorcycle : Vehicle
{
    private bool hasSidecar;

    public Motorcycle(string brand, int year, bool hasSidecar) : base(brand, year)
    {
        this.hasSidecar = hasSidecar;
    }

    public override void Start()
    {
        Console.WriteLine($"Motorcycle {brand} starting with kick/electric start...");
    }

    public override void Stop()
    {
        Console.WriteLine($"Motorcycle {brand} stopping and balancing...");
    }

    public override void Move()
    {
        string sidecarInfo = hasSidecar ? "with sidecar" : "solo";
        Console.WriteLine($"Motorcycle {brand} riding {sidecarInfo}");
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Motorcycle specific: Sidecar: {hasSidecar}");
    }
}

// ═══════════════════════════════════════════════════════════════════════════════════
//                        OVERLOADING + OVERRIDING COMBINATION
// ═══════════════════════════════════════════════════════════════════════════════════

/*
COMPLEX EXAMPLE: Both overloading and overriding in same hierarchy
*/
class Shape
{
    protected string name;

    public Shape(string name) => this.name = name;

    /*
    VIRTUAL METHOD - Can be overridden
    */
    public virtual double CalculateArea()
    {
        Console.WriteLine("Base Shape: Cannot calculate area");
        return 0.0;
    }

    /*
    OVERLOADED METHODS - Different parameters
    */
    public virtual void Draw()
    {
        Console.WriteLine($"Drawing {name} with default settings");
    }

    public virtual void Draw(string color)
    {
        Console.WriteLine($"Drawing {name} with color: {color}");
    }

    public virtual void Draw(string color, int thickness)
    {
        Console.WriteLine($"Drawing {name} with color: {color}, thickness: {thickness}");
    }
}

class Circle : Shape
{
    private double radius;

    public Circle(string name, double radius) : base(name)
    {
        this.radius = radius;
    }

    /*
    OVERRIDE - Different implementation
    */
    public override double CalculateArea()
    {
        Console.WriteLine($"Circle: Calculating area for radius {radius}");
        return Math.PI * radius * radius;
    }

    /*
    OVERRIDE + OVERLOAD - Override base method and add new overload
    */
    public override void Draw()
    {
        Console.WriteLine($"Drawing circle {name} with radius {radius}");
    }

    public override void Draw(string color)
    {
        Console.WriteLine($"Drawing circle {name} (radius: {radius}) with color: {color}");
    }

    public override void Draw(string color, int thickness)
    {
        Console.WriteLine($"Drawing circle {name} (radius: {radius}) with color: {color}, border thickness: {thickness}");
    }

    /*
    NEW OVERLOAD - Additional method specific to Circle
    */
    public void Draw(string color, int thickness, bool filled)
    {
        string fillStatus = filled ? "filled" : "outline";
        Console.WriteLine($"Drawing {fillStatus} circle {name} (radius: {radius}) with color: {color}, thickness: {thickness}");
    }
}

// ═══════════════════════════════════════════════════════════════════════════════════
//                                DEMONSTRATION
// ═══════════════════════════════════════════════════════════════════════════════════



