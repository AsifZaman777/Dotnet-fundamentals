/*
Interface: An interface in C# is a contract that defines a set of method
signatures, properties, events, or indexers, but does not provide any implementation. 
Classes or structs that implement an interface must provide concrete implementations for all its members.



Use cases:
•	Decoupling: Interfaces allow code to depend on abstractions, not concrete implementations, making your code more flexible and easier to maintain.
•	Multiple Inheritance: A class can implement multiple interfaces, overcoming the single inheritance limitation of classes.
•	Polymorphism: You can write code that works with any class that implements a specific interface, regardless of the class hierarchy.
•	Testability: Interfaces make it easier to write unit tests by allowing you to mock dependencies.
 */

public interface IVehicle
{
    void Drive(int speed);
    void DisplayCarInfo();
}

public class Car : IVehicle
{
    public string model { get; set; }
    public DateTime manufactureDate { get; set; }

    public Car(string model, DateTime manufactureDate)
    {
        this.model = model;
        this.manufactureDate = manufactureDate;
    }

    public void Drive(int speed)
    {
        Console.WriteLine($"Driving {model} at {speed} km/h.");
    }

    public void DisplayCarInfo()
    {
        Console.WriteLine($"Car Model: {model}, Manufacture Date: {manufactureDate.ToShortDateString()}");
    }
}

public class Truck : IVehicle
{
    public string model { get; set; }
    public int payloadCapacity { get; set; }
    public Truck(string model, int payloadCapacity)
    {
        this.model = model;
        this.payloadCapacity = payloadCapacity;
    }
    public void Drive(int speed)
    {
        Console.WriteLine($"Driving {model} at {speed} km/h with a payload capacity of {payloadCapacity} kg.");
    }
    public void DisplayCarInfo()
    {
        Console.WriteLine($"Truck Model: {model}, Payload Capacity: {payloadCapacity} kg");
    }
}


class InterfaceBasic
{
    static void Main(string[] args)
    {
        IVehicle myCar = new Car("Toyota Camry", new DateTime(2020, 5, 1));
        myCar.Drive(80);
        myCar.DisplayCarInfo();

        IVehicle myTruck = new Truck("Ford F-150", 1000);
        myTruck.Drive(60);
        myTruck.DisplayCarInfo();

    }
}



/*how it decouples the code
         ┌──────────────┐
         │  IVehicle    │   <--- Interface (contract)
         │──────────────│
         │+ Drive()     │
         │+ DisplayInfo()│
         └─────┬────────┘
               │
   ┌───────────┴─────────────┐
   │                         │
┌───────┐               ┌────────┐
│  Car  │               │ Truck  │   <--- Concrete classes
│───────│               │────────│
│+Drive()│              │+Drive()│
│+DisplayInfo()│        │+DisplayInfo()│
└───────┘               └────────┘
   ▲                         ▲
   │                         │
   └─────────────┬───────────┘
                 │
         ┌───────────────┐
         │  Client Code  │   <--- Uses IVehicle, not Car/Truck directly
         └───────────────┘

•	The client code depends on the IVehicle interface, not on specific implementations like Car or Truck.
•	You can add new vehicle types (e.g., Bus : IVehicle) without changing the client code.
•	This makes your code flexible, maintainable, and easy to extend or test.


*/