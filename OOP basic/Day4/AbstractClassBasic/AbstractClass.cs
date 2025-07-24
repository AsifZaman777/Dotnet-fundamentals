
//namespace AbstractClassNamespace
//    {
//    //concrete class - fully implemented class
//    class Vehicle
//    {
//        public string Make { get; set; }
//        public string Model { get; set; }
//        public Vehicle(string make, string model)
//        {
//            Make = make;
//            Model = model;
//        }
//        public virtual void DisplayInfo()
//        {
//            Console.WriteLine($"Vehicle Make: {Make}, Model: {Model}");
//        }
//    }


//    //abstract class - The abstract method defines a contract - the signature that all derived classes must follow

//    /*
//     An abstract class is used when you want to define a common base with shared code and a
//    contract for derived classes, but you do not want the base class itself to be instantiated. 
//    It helps enforce structure and code reuse in object-oriented design


//    Example: 
//    AbstractVehicle is an abstract class that defines a contract for all vehicles. It helps to ensure that
//    all derived classes (like Car and Truck) implement the DisplayInfo method, while also allowing them to share common functionality through the CommonMethod.

//    Benefits of using an abstract class:
//    1. **Code Reusability**: Common functionality can be implemented once in the abstract class and reused in derived classes.
//    2. **Enforcement of Structure**: It enforces a structure that derived classes must follow, ensuring they implement certain methods.


//               AbstractVehicle (abstract)
//               ┌─────────────────────────────┐
//               │ + make: string              │
//               │ + model: string             │
//               │ + CommonMethod()            │
//               │ + DisplayInfo() [abstract]  │
//               └─────────────┬───────────────┘
//                             │
//             ┌───────────────┴───────────────┐
//             │                               │
//          Car (concrete)                Truck (concrete)
//    ┌─────────────────────┐      ┌────────────────────────┐
//    │ + numberOfDoors     │      │ + payloadCapacity      │
//    │ + DisplayInfo()     │      │ + DisplayInfo()        │
//    └─────────────────────┘      └────────────────────────┘


//     */

//    abstract class AbstractVehicle
//    {
//        public string make { get; set; }
//        public string model { get; set; }
//        protected AbstractVehicle(string make, string model)
//        {
//            this.make = make;
//            this.model = model;
//        }
//        public abstract void DisplayInfo(); // abstract method - to be implemented by derived classes

//        //concrete method - implemented method
//        public void CommonMethod()
//        {
//            Console.WriteLine("This is a common method in the abstract class.");
//        }
//    }

//    // car class implementing the abstract class
//    class Car : AbstractVehicle
//    {
//        public int numberOfDoors { get; set; }
//        public Car(string make, string model, int numberOfDoors) : base(make, model)
//        {
//            this.numberOfDoors = numberOfDoors;
//        }
//        public override void DisplayInfo()
//        {
//            Console.WriteLine($"Car Make: {make}, Model: {model}, Number of Doors: {numberOfDoors}");
//        }

//    }

//    // truck class implementing the abstract class
//    class Truck : AbstractVehicle
//    {
//        public int payloadCapacity { get; set; }
//        public Truck(string make, string model, int payloadCapacity) : base(make, model)
//        {
//            this.payloadCapacity = payloadCapacity;
//        }
//        public override void DisplayInfo()
//        {
//            Console.WriteLine($"Truck Make: {make}, Model: {model}, Payload Capacity: {payloadCapacity} kg");
//        }
//    }


//    // a class that breaks the contract
//    //class MotorCycle: AbstractVehicle
//    //{
//    //    public bool hasSideCar { get; set; }
//    //    public MotorCycle(string make, string model, bool hasSideCar) : base(make, model)
//    //    {
//    //        this.hasSideCar = hasSideCar;
//    //    }

//    //}
//    // This class will not compile because it does not implement the abstract method DisplayInfo



//    // Example usage
//    class AbstractClass
//    {
//        static void Main(string[] args)
//        {
//            AbstractVehicle myCar = new Car("Toyota", "Camry", 4);
//            myCar.DisplayInfo(); // Calls the overridden method in Car
//            myCar.CommonMethod(); // Calls the concrete method in AbstractVehicle

//            AbstractVehicle myTruck = new Truck("Ford", "F-150", 1000);
//            myTruck.DisplayInfo(); // Calls the overridden method in Truck
//            myTruck.CommonMethod(); // Calls the concrete method in AbstractVehicle
//        }
//    }
//}


////AbstractVehicle is like a contract that says:
////•	"Any vehicle must have a make and model"
////•	"Any vehicle must be able to display its info (but each type displays it differently)"
////•	"All vehicles share some common functionality"

