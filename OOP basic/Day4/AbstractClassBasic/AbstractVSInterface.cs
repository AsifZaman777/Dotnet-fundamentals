// =============================================================================
// ABSTRACT CLASS vs INTERFACE: Comprehensive Comparison
// =============================================================================

/*
┌───────────────────────────────────────────────────────────────────────────────┐
│                        ABSTRACT CLASS vs INTERFACE                            │
├───────────────────────────────────────────────────────────────────────────────┤
│                                                                               │
│  ABSTRACT CLASS                    │  INTERFACE                               │
│  ════════════════                  │  ══════════                              │
│                                    │                                          │
│  • Can have implementation         │  • Pure contract (no implementation)     │
│  • Single inheritance only         │  • Multiple inheritance supported        │
│  • Can have constructors           │  • No constructors allowed               │
│  • Can have fields & properties    │  • Only method signatures & properties   │
│  • Access modifiers allowed        │  • All members implicitly public         │
│  • Can have static members         │  • Can have static members (C# 8+)       │
│  • Partial implementation          │  • Complete contract definition          │
│                                    │                                          │
└───────────────────────────────────────────────────────────────────────────────┘
*/

// =============================================================================
// 1. ABSTRACT CLASS EXAMPLE - IS-A Relationship
// =============================================================================

public abstract class Vehicle
{
    // Fields and properties
    protected string make;
    protected string model;
    protected DateTime manufactureDate;

    // Constructor
    protected Vehicle(string make, string model, DateTime manufactureDate)
    {
        this.make = make;
        this.model = model;
        this.manufactureDate = manufactureDate;
    }

    // Concrete method with implementation
    public virtual void StartEngine()
    {
        Console.WriteLine($"Starting {make} {model} engine...");
    }

    // Abstract method - must be implemented by derived classes
    public abstract void DisplaySpecifications();

    // Common behavior
    public void ShowManufactureInfo()
    {
        Console.WriteLine($"Manufactured: {manufactureDate.ToShortDateString()}");
    }
}

// =============================================================================
// 2. INTERFACE EXAMPLES - CAN-DO Relationships
// =============================================================================

// Interface for flying capability
public interface IFlyable
{
    void TakeOff();
    void Land();
    int MaxAltitude { get; }
}

// Interface for swimming capability
public interface ISwimmable
{
    void Dive();
    void Surface();
    int MaxDepth { get; }
}

// Interface for maintenance operations
public interface IMaintainable
{
    void PerformMaintenance();
    DateTime LastMaintenanceDate { get; set; }
}

// =============================================================================
// 3. IMPLEMENTATION EXAMPLES
// =============================================================================

// Concrete class extending abstract class
public class Car : Vehicle, IMaintainable
{
    private int numberOfDoors;

    public Car(string make, string model, DateTime manufactureDate, int doors)
        : base(make, model, manufactureDate)
    {
        numberOfDoors = doors;
        LastMaintenanceDate = DateTime.Now.AddMonths(-6);
    }

    // Must implement abstract method
    public override void DisplaySpecifications()
    {
        Console.WriteLine($"Car: {make} {model}, Doors: {numberOfDoors}");
    }

    // Interface implementation
    public void PerformMaintenance()
    {
        Console.WriteLine($"Performing car maintenance on {make} {model}");
        LastMaintenanceDate = DateTime.Now;
    }

    public DateTime LastMaintenanceDate { get; set; }
}

// Class implementing multiple interfaces
public class Airplane : Vehicle, IFlyable, IMaintainable
{
    public Airplane(string make, string model, DateTime manufactureDate)
        : base(make, model, manufactureDate)
    {
        LastMaintenanceDate = DateTime.Now.AddDays(-30);
    }

    public override void DisplaySpecifications()
    {
        Console.WriteLine($"Airplane: {make} {model}, Max Altitude: {MaxAltitude} ft");
    }

    // IFlyable implementation
    public void TakeOff()
    {
        Console.WriteLine($"{make} {model} is taking off!");
    }

    public void Land()
    {
        Console.WriteLine($"{make} {model} is landing safely.");
    }

    public int MaxAltitude => 35000;

    // IMaintainable implementation
    public void PerformMaintenance()
    {
        Console.WriteLine($"Performing aircraft maintenance on {make} {model}");
        LastMaintenanceDate = DateTime.Now;
    }

    public DateTime LastMaintenanceDate { get; set; }
}

// Amphibious vehicle - multiple capabilities
public class Seaplane : Vehicle, IFlyable, ISwimmable, IMaintainable
{
    public Seaplane(string make, string model, DateTime manufactureDate)
        : base(make, model, manufactureDate)
    {
        LastMaintenanceDate = DateTime.Now.AddDays(-15);
    }

    public override void DisplaySpecifications()
    {
        Console.WriteLine($"Seaplane: {make} {model}, Air/Water capable");
    }

    // IFlyable
    public void TakeOff() => Console.WriteLine("Seaplane taking off from water!");
    public void Land() => Console.WriteLine("Seaplane landing on water!");
    public int MaxAltitude => 15000;

    // ISwimmable
    public void Dive() => Console.WriteLine("Seaplane diving underwater!");
    public void Surface() => Console.WriteLine("Seaplane surfacing!");
    public int MaxDepth => 50;

    // IMaintainable
    public void PerformMaintenance()
    {
        Console.WriteLine("Performing seaplane maintenance (air & water systems)");
        LastMaintenanceDate = DateTime.Now;
    }

    public DateTime LastMaintenanceDate { get; set; }
}

// =============================================================================
// 4. TEXTUAL DIAGRAMS
// =============================================================================

/*
INHERITANCE HIERARCHY DIAGRAM:
═══════════════════════════════

                    Vehicle (Abstract Class)
                    ┌────────────────────────┐
                    │ - make: string         │
                    │ - model: string        │
                    │ + StartEngine()        │
                    │ + DisplaySpecs()* abs  │
                    └───────────┬────────────┘
                                │ inherits
              ┌─────────────────┼─────────────────┐
              │                 │                 │
           Car                Airplane        Seaplane
        ┌──────────┐       ┌──────────┐     ┌──────────┐
        │ + doors  │       │ + altitude│     │ + depth  │
        └──────────┘       └──────────┘     └──────────┘
              │                 │                 │
              │                 │                 │
              ▼                 ▼                 ▼
       IMaintainable    IFlyable + IMaint.  All Interfaces


INTERFACE IMPLEMENTATION DIAGRAM:
════════════════════════════════

      IFlyable           ISwimmable        IMaintainable
   ┌─────────────┐    ┌─────────────┐    ┌─────────────┐
   │ TakeOff()   │    │ Dive()      │    │ Maintain()  │
   │ Land()      │    │ Surface()   │    │ LastDate    │
   │ MaxAltitude │    │ MaxDepth    │    └─────────────┘
   └─────────────┘    └─────────────┘           │
           │                  │                 │
           └──────────┬───────┼─────────────────┘
                      │       │
                   Seaplane ──┘
                (implements all three)


CAPABILITY MATRIX:
═══════════════

Class      │ IS-A Vehicle │ CAN Fly │ CAN Swim │ CAN Maintain
═══════════┼══════════════┼═════════┼══════════┼═════════════
Car        │      ✓       │    ✗   │    ✗     │      ✓
Airplane   │      ✓       │    ✓    │    ✗     │      ✓
Seaplane   │      ✓       │    ✓    │    ✓     │      ✓
*/

// =============================================================================
// 5. WHEN TO USE WHAT - DECISION GUIDE
// =============================================================================

/*
USE ABSTRACT CLASS WHEN:
══════════════════════

✓ IS-A relationship (Car IS-A Vehicle)
✓ You need to share code among related classes
✓ You want to provide default implementations
✓ You need constructors to initialize common state
✓ Classes are closely related in hierarchy
✓ You want to enforce a template method pattern

Example: Animal → Dog, Cat, Bird (shared biology)


USE INTERFACE WHEN:
═════════════════

✓ CAN-DO relationship (Car CAN-BE maintained)
✓ You need multiple inheritance of type
✓ Unrelated classes need same capabilities
✓ You want to define pure contracts
✓ You're implementing Strategy or Adapter patterns
✓ You need maximum flexibility and testability

Example: IComparable, IEnumerable, IDisposable


HYBRID APPROACH:
══════════════

✓ Abstract class for core functionality (IS-A)
✓ Interfaces for additional capabilities (CAN-DO)
✓ Best of both worlds

Example: Vehicle (abstract) + IFlyable, ISwimmable (interfaces)
*/

// =============================================================================
// 6. PRACTICAL USAGE EXAMPLE
// =============================================================================

public class VehicleManager
{
    public void DemonstrateUsage()
    {
        // Abstract class provides common behavior
        var vehicles = new List<Vehicle>
        {
            new Car("Toyota", "Prius", new DateTime(2023, 1, 1), 4),
            new Airplane("Boeing", "747", new DateTime(2022, 6, 15)),
            new Seaplane("Cessna", "208 Caravan", new DateTime(2021, 3, 10))
        };

        // Polymorphism through abstract class
        foreach (var vehicle in vehicles)
        {
            vehicle.StartEngine();           // Common behavior
            vehicle.DisplaySpecifications(); // Specific implementation
            vehicle.ShowManufactureInfo();   // Shared concrete method
        }

        // Interface-based capabilities
        ProcessFlyableVehicles(vehicles.OfType<IFlyable>());
        ProcessMaintainableVehicles(vehicles.OfType<IMaintainable>());
    }

    private void ProcessFlyableVehicles(IEnumerable<IFlyable> flyables)
    {
        foreach (var flyable in flyables)
        {
            flyable.TakeOff();
            Console.WriteLine($"Max altitude: {flyable.MaxAltitude} ft");
            flyable.Land();
        }
    }

    private void ProcessMaintainableVehicles(IEnumerable<IMaintainable> maintainables)
    {
        foreach (var maintainable in maintainables)
        {
            if (maintainable.LastMaintenanceDate < DateTime.Now.AddDays(-30))
            {
                maintainable.PerformMaintenance();
            }
        }
    }
}

/*
KEY TAKEAWAYS:
═════════════

1. Abstract classes model INHERITANCE ("IS-A")
2. Interfaces model COMPOSITION ("CAN-DO") 
3. Use abstract classes for shared implementation
4. Use interfaces for contracts and multiple capabilities
5. Combine both for maximum flexibility
6. Think about relationships: inheritance vs capabilities

MODERN C# FEATURES:
═════════════════

• Default interface methods (C# 8.0+)
• Static members in interfaces (C# 8.0+)
• Generic constraints with interfaces
• Pattern matching with types and interfaces
*/