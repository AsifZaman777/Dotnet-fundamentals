
/*
╔══════════════════════════════════════════════════════════════════════════════════════╗
║                                INHERITANCE TYPES IN C#                               ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

1.	Single Inheritance: One class inherits from exactly one base class
2.	Multilevel Inheritance: A chain where class C inherits from B, which inherits from A
3.	Hierarchical Inheritance: Multiple classes inherit from the same base class
4.	Interface Inheritance: A class can implement multiple interfaces
5.	Hybrid Inheritance: Combination using interfaces (since C# doesn't allow multiple class inheritance)


Note: C# does not support multiple class inheritance, but allows multiple interface inheritance.


inheritance flow diagram:
┌────────────────────────────────────────────────────────────────────────────────────┐
│                                   Animal (Base)                                    │
│                              ┌─────────────────────┐                               │
│                              │   name: string      │                               │
│                              │   MakeSound()       │                               │
│                              │   Sleep()           │                               │
│                              └─────────────────────┘                               │
│                                         │                                          │
│                    ┌────────────────────┼────────────────────┐                     │
│                    │                    │                    │                     │
│                ┌───▼───┐            ┌───▼───┐            ┌───▼───┐                 │
│                │  Dog  │            │  Cat  │            │ Bird  │                 │
│                └───┬───┘            └───────┘            └───────┘                 │
│                    │                                                               │
│                ┌───▼───┐                                                           │
│                │ Puppy │                                                           │
│                └───────┘                                                           │
└────────────────────────────────────────────────────────────────────────────────────┘

TYPES OF INHERITANCE:
1. Single       : Dog → Animal
2. Multilevel   : Puppy → Dog → Animal  
3. Hierarchical : Dog, Cat, Bird → Animal
4. Interface    : Duck implements ISwimmable, IFlyable
5. Hybrid       : Horse extends Animal + implements IRunnable


*/


// base class
class Animal
{
    protected string name;
   
    public Animal(string name) => this.name = name;
    
    /*
    VIRTUAL METHOD PATTERN:
    Base implementation → Can be overridden in derived classes → Polymorphism
    */
    public virtual void MakeSound() => Console.WriteLine($"{name} makes a sound");
    public void Sleep() => Console.WriteLine($"{name} is sleeping");
}

/*
╔══════════════════════════════════════════════════════════════════════════════════════╗
║                              1. SINGLE INHERITANCE                                  ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

STRUCTURE:
    Animal (Base)
      ↓
    Dog (Derived)

FLOW:
Dog constructor → calls base(name) → Animal constructor → field initialization
Dog.MakeSound() → overrides Animal.MakeSound() → polymorphic behavior
*/
class Dog : Animal
{
    public Dog(string name) : base(name) { }
    
    // Method override - runtime polymorphism
    public override void MakeSound() => Console.WriteLine($"{name} barks");
    
    // New behavior specific to Dog
    public void Fetch() => Console.WriteLine($"{name} fetches the ball");
}

/*
╔══════════════════════════════════════════════════════════════════════════════════════╗
║                             2. MULTILEVEL INHERITANCE                                ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

INHERITANCE CHAIN:
    Animal (Grandparent)
      ↓
    Dog (Parent)  
      ↓
    Puppy (Child)

CONSTRUCTOR CHAINING FLOW:
Puppy("Buddy") → Dog("Buddy") → Animal("Buddy")
   ↓              ↓               ↓
  this.name    base(name)     field assignment

METHOD RESOLUTION ORDER:
Puppy.MakeSound() → Dog.MakeSound() → overridden behavior
Puppy.Sleep() → Animal.Sleep() → inherited behavior
Puppy.Play() → unique to Puppy class
*/
class Puppy : Dog
{
    public Puppy(string name) : base(name) { }
    
    public void Play() => Console.WriteLine($"{name} plays with toys");
}

/*
╔══════════════════════════════════════════════════════════════════════════════════════╗
║                            3. HIERARCHICAL INHERITANCE                               ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

INHERITANCE TREE:
                    Animal
                      │
        ┌─────────────┼─────────────┐
        │             │             │
      Dog           Cat           Bird
        │
      Puppy

POLYMORPHISM DEMONSTRATION:
Animal[] animals = {new Dog("Rex"), new Cat("Whiskers"), new Bird("Tweety")};
foreach(Animal animal in animals) 
    animal.MakeSound(); // Different behavior for each type
*/
class Cat : Animal
{
    public Cat(string name) : base(name) { }
    
    public override void MakeSound() => Console.WriteLine($"{name} meows");
}

class Bird : Animal
{
    public Bird(string name) : base(name) { }
    
    public override void MakeSound() => Console.WriteLine($"{name} chirps");
    public void Fly() => Console.WriteLine($"{name} flies high");
}

/*
╔══════════════════════════════════════════════════════════════════════════════════════╗
║                             4. INTERFACE INHERITANCE                                 ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

INTERFACE CONTRACT PATTERN:
┌─────────────────┐    ┌─────────────────┐
│  ISwimmable     │    │   IFlyable      │
│  + Swim()       │    │   + Fly()       │
└─────────────────┘    └─────────────────┘
         │                       │
         └───────┬───────────────┘
                 │
         ┌─────────────────┐
         │     Duck        │
         │ + Swim()        │
         │ + Fly()         │
         │ + MakeSound()   │
         └─────────────────┘

IMPLEMENTATION FLOW:
Duck class → implements both interfaces → must provide concrete implementations
*/
interface ISwimmable
{
    void Swim();
}

interface IFlyable
{
    void Fly();
}

class Duck : Animal, ISwimmable, IFlyable
{
    public Duck(string name) : base(name) { }
    
    public override void MakeSound() => Console.WriteLine($"{name} quacks");
    
    // Interface implementations
    public void Swim() => Console.WriteLine($"{name} swims in water");
    public void Fly() => Console.WriteLine($"{name} flies in sky");
}

/*
╔══════════════════════════════════════════════════════════════════════════════════════╗
║                              5. HYBRID INHERITANCE                                   ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

HYBRID PATTERN (Class + Interface):
    Animal (Base Class)
      ↓
    Horse (Derived Class) + IRunnable (Interface)

C# LIMITATION WORKAROUND:
Since C# doesn't allow multiple class inheritance, we use:
- Single class inheritance (Horse : Animal)  
- Multiple interface implementation (Horse : IRunnable)

COMPOSITION OVER INHERITANCE PRINCIPLE:
Horse "IS-A" Animal (inheritance) and "CAN-DO" Running (interface capability)
*/
interface IRunnable
{
    void Run();
}

class Horse : Animal, IRunnable
{
    public Horse(string name) : base(name) { }
    
    public override void MakeSound() => Console.WriteLine($"{name} neighs");
    public void Run() => Console.WriteLine($"{name} gallops fast");
}

/*
╔══════════════════════════════════════════════════════════════════════════════════════╗
║                                EXECUTION FLOW                                        ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

PROGRAM EXECUTION SEQUENCE:
1. Object Creation → Constructor chaining → Field initialization
2. Method Calls → Virtual dispatch → Polymorphic behavior
3. Interface Methods → Contract fulfillment → Multiple capabilities

MEMORY LAYOUT:
┌─────────────────┐
│   Stack Frame   │
│   ┌─────────────┤
│   │ dog (ref)   │ ──┐
│   │ puppy (ref) │   │    ┌─────────────────┐
│   │ cat (ref)   │   └────→ Dog object      │
│   └─────────────┤        │ name: "Rex"     │
└─────────────────┘        │ vtable ptr      │
                           └─────────────────┘
*/
class Program
{
    static void Main(string[] args)
    {
        /*
        DEMONSTRATION FLOW:
        Each section shows a different inheritance type in action
        */
        
        Console.WriteLine("=== SINGLE INHERITANCE ===");
        /*
        OBJECT CREATION FLOW:
        new Dog("Rex") → Dog constructor → base("Rex") → Animal constructor
        */
        Dog dog = new Dog("Rex");
        dog.MakeSound(); // Polymorphic call - Dog's override
        dog.Fetch();     // Dog-specific method
        
        Console.WriteLine("\n=== MULTILEVEL INHERITANCE ===");
        /*
        INHERITANCE CHAIN ACCESS:
        Puppy has access to: Animal methods, Dog methods, Puppy methods
        */
        Puppy puppy = new Puppy("Buddy");
        puppy.MakeSound(); // From Dog override (inherited)
        puppy.Sleep();     // From Animal (inherited through Dog)
        puppy.Play();      // Puppy-specific method
        
        Console.WriteLine("\n=== HIERARCHICAL INHERITANCE ===");
        /*
        SIBLING CLASSES:
        Cat and Bird are siblings - both inherit from Animal independently
        */
        Cat cat = new Cat("Whiskers");
        Bird bird = new Bird("Tweety");
        cat.MakeSound();  // Cat's override
        bird.MakeSound(); // Bird's override
        bird.Fly();       // Bird-specific method
        
        Console.WriteLine("\n=== INTERFACE INHERITANCE ===");
        /*
        MULTIPLE CAPABILITY PATTERN:
        Duck can do everything Animal can do + Swimming + Flying
        */
        Duck duck = new Duck("Donald");
        duck.MakeSound(); // Overridden from Animal
        duck.Swim();      // From ISwimmable
        duck.Fly();       // From IFlyable
        
        Console.WriteLine("\n=== HYBRID INHERITANCE ===");
        /*
        CLASS + INTERFACE COMBINATION:
        Horse extends Animal functionality with IRunnable capability
        */
        Horse horse = new Horse("Thunder");
        horse.MakeSound(); // Overridden Animal method
        horse.Run();       // Interface implementation
        
        /*
        POLYMORPHISM DEMONSTRATION:
        All objects can be treated as Animal due to inheritance hierarchy
        */
        Console.WriteLine("\n=== POLYMORPHISM IN ACTION ===");
        Animal[] animals = { dog, puppy, cat, bird, duck, horse };
        foreach (Animal animal in animals)
        {
            animal.MakeSound(); // Runtime polymorphism - each calls its own override
        }
    }
}