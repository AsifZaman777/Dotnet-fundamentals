/*
╔══════════════════════════════════════════════════════════════════════════════════════╗
║                                ABSTRACT CLASS EXAMPLE                               ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

ABSTRACT CLASS DIAGRAM:
┌───────────────┐
│  Animal       │  ← abstract class
│───────────────│
│+ MakeSound()  │  ← abstract method
│+ Sleep()      │  ← concrete method
└─────┬─────────┘
      │
      ▼
┌───────────────┐
│  Dog          │  ← derived class
│───────────────│
│+ MakeSound()  │  ← must implement
└───────────────┘
*/

namespace AbstractClassDemo
{

    abstract class Animal
    {
        // Abstract method: must be implemented by derived classes
        public abstract void MakeSound();

        // Concrete method: can be used as-is or overridden
        public void Sleep()
        {
            Console.WriteLine("Animal is sleeping");
        }
    }

    class Dog : Animal
    {
        // Must implement abstract method
        public override void MakeSound()
        {
            Console.WriteLine("Dog barks");
        }
    }

    // Usage
    class Demo
    {
        static void Main()
        {
            // Animal a = new Animal(); // ❌ Not allowed
            Animal a = new Dog();       // ✅ Allowed
            a.MakeSound();              // Output: Dog barks
            a.Sleep();                  // Output: Animal is sleeping
        }
    }

}