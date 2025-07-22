/*
╔══════════════════════════════════════════════════════════════════════════════════════╗
║                              VIRTUAL KEYWORD IN C#                                  ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

THE VIRTUAL KEYWORD:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│ DEFINITION: The 'virtual' keyword allows a method to be overridden in derived       │
│ classes, enabling runtime polymorphism through method overriding.                   │
│                                                                                     │
│ PURPOSE: Establishes a contract that says "this method can be customized by        │
│ derived classes while maintaining the same signature"                               │
└─────────────────────────────────────────────────────────────────────────────────────┘

VIRTUAL METHOD DISPATCH FLOW:
┌───────────────────────────────────────────────────────────────────────────────────┐
│    Compile Time                          Runtime                                  │
│ ┌─────────────────┐                 ┌─────────────────┐                           │
│ │  animal.Method()│   ----------->  │ Check object's  │                           │
│ │                 │                 │ actual type     │                           │
│ └─────────────────┘                 └─────────────────┘                           │
│                                             │                                     │
│                                             ▼                                     │
│                                    ┌─────────────────┐                            │
│                                    │  Call correct   │                            │
│                                    │  override       │                            │
│                                    └─────────────────┘                            │
│                                                                                   │
│ This is called "Late Binding" or "Dynamic Dispatch"                               │
└───────────────────────────────────────────────────────────────────────────────────┘

VIRTUAL vs NON-VIRTUAL COMPARISON:
┌────────────────────────────────────────────────────────────────────────────────────┐
│                    NON-VIRTUAL (Static Binding)                                    │
│ ┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐                  │
│ │  Base Class     │    │ Derived Class   │    │   Call Result   │                  │
│ │ void Method()   │ -> │ void Method()   │ -> │ Base.Method()   │                  │
│ │ { ... }         │    │ { ... }         │    │ (HIDING)        │                  │
│ └─────────────────┘    └─────────────────┘    └─────────────────┘                  │
│                                                                                    │
│                      VIRTUAL (Dynamic Binding)                                     │
│ ┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐                  │
│ │  Base Class     │    │ Derived Class   │    │   Call Result   │                  │
│ │virtual Method() │ -> │override Method()│ -> │Derived.Method() │                  │
│ │ { ... }         │    │ { ... }         │    │ (OVERRIDING)    │                  │
│ └─────────────────┘    └─────────────────┘    └─────────────────┘                  │
└────────────────────────────────────────────────────────────────────────────────────┘

static binding occurs at compile time, where the method to call is determined
dynamic binding occurs at runtime, where the actual object type determines which method is called

*/



// ═══════════════════════════════════════════════════════════════════════════════════
//                           VIRTUAL METHOD EXAMPLES
// ═══════════════════════════════════════════════════════════════════════════════════

class Shape
{
    protected string name;

    public Shape(string name) => this.name = name;

    /*
    VIRTUAL METHOD DECLARATION:
    ┌─────────────────────────────────────────────────────────────┐
    │ public virtual double CalculateArea()                       │
    │   │      │       │                                         │
    │   │      │       └── Return type                           │
    │   │      └────────── VIRTUAL keyword                       │
    │   └─────────────────── Access modifier                     │
    └─────────────────────────────────────────────────────────────┘
    
    WHAT VIRTUAL ENABLES:
    1. Method can be overridden in derived classes
    2. Runtime polymorphism (late binding)
    3. Different behavior for different object types
    4. Maintains same method signature across hierarchy
    */
    public virtual double CalculateArea()
    {
        Console.WriteLine($"Calculating area for generic shape: {name}");
        return 0.0; // Default implementation
    }

    /*
    NON-VIRTUAL METHOD:
    Cannot be overridden - uses static binding
    */
    public void Display()
    {
        Console.WriteLine($"Shape: {name}, Area: {CalculateArea()}");
    }
}

class Rectangle : Shape
{
    private double length, width;

    public Rectangle(string name, double length, double width) : base(name)
    {
        this.length = length;
        this.width = width;
    }

    /*
    METHOD OVERRIDE:
    ┌─────────────────────────────────────────────────────────────┐
    │ public override double CalculateArea()                      │
    │   │      │        │                                        │
    │   │      │        └── Same signature as virtual method     │
    │   │      └─────────── OVERRIDE keyword (required)          │
    │   └────────────────── Access modifier (same or less)      │
    └─────────────────────────────────────────────────────────────┘
    
    OVERRIDE REQUIREMENTS:
    1. Must use 'override' keyword
    2. Same signature as virtual method
    3. Same or more accessible than base method
    4. Can call base implementation using base.Method()
    */
    public override double CalculateArea()
    {
        Console.WriteLine($"Calculating rectangle area for: {name}");
        return length * width;
    }
}

class Circle : Shape
{
    private double radius;

    public Circle(string name, double radius) : base(name)
    {
        this.radius = radius;
    }

    public override double CalculateArea()
    {
        Console.WriteLine($"Calculating circle area for: {name}");
        return Math.PI * radius * radius;
    }
}

/*


/*
COMPARISON TABLE:
┌─────────────────┬─────────────────┬─────────────────┬─────────────────┐
│     FEATURE     │    VIRTUAL      │    ABSTRACT     │   INTERFACE     │
├─────────────────┼─────────────────┼─────────────────┼─────────────────┤
│ Implementation  │ Can provide     │ No impl.        │ Default (C# 8+) │
│ Override Req.   │ Optional        │ Required        │ Optional        │
│ Class Type      │ Any class       │ Abstract only   │ Interface only  │
│ Instantiation   │ Allowed         │ Not allowed     │ Not allowed     │
│ Multiple        │ No (single)     │ No (single)     │ Yes (multiple)  │
└─────────────────┴─────────────────┴─────────────────┴─────────────────┘
*/


// ═══════════════════════════════════════════════════════════════════════════════════
//                              DEMONSTRATION
// ═══════════════════════════════════════════════════════════════════════════════════

class VirtualDemo
{
    static void DemonstrateVirtual()
    {
        /*
        POLYMORPHISM IN ACTION: All objects stored as base type (Shape) But each calls its own overridden method
        */
        Console.WriteLine("=== RUNTIME POLYMORPHISM ===");
        Shape[] shapes = {
            new Rectangle("Rect1", 5, 3),
            new Circle("Circle1", 2.5),
            new Rectangle("Rect2", 4, 4)
        };

        /*
        VIRTUAL METHOD DISPATCH:
        Even though 'shape' is declared as Shape,
        the actual object's CalculateArea() method is called
        */
        foreach (Shape shape in shapes)
        {
            // Runtime determines which CalculateArea() to call
            shape.Display(); // Calls virtual CalculateArea() polymorphically
        }

    }
}