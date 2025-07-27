/* 
 ==============================
 📌 Stack vs Heap in C#
 ==============================

 Stack:
 - Stores value types and method call info.
 - Memory is managed using LIFO (Last In First Out).
 - Very fast, automatically cleaned up after method returns.

 Heap:
 - Stores reference type objects.
 - Memory is managed by Garbage Collector (GC).
 - Slightly slower than stack due to indirect access.

 ------------------------------
 Stack vs Heap - Textual Table
 ------------------------------

| Feature              | Stack                       | Heap                          |
|----------------------|-----------------------------|-------------------------------|
| Memory Type          | Value types                 | Reference types               |
| Access Speed         | Fast                        | Slower                        |
| Memory Allocation    | Static (at compile time)    | Dynamic (at runtime)          |
| Lifetime             | Ends with method execution  | Until GC collects it          |
| Cleanup              | Automatic                   | GC needed                     |
| Thread-Specific      | Yes                         | No (shared between threads)   |


 ==============================
 📌 Value Type vs Reference Type
 ==============================

 Value Type:
 - Holds actual value.
 - Stored in stack.
 - Copies create independent instances. Means the assigned value is copied. Not the reference.
 - Examples: int, float, bool, struct, enum

 Reference Type:
 - Holds reference to memory address.
 - Stored in heap.
 - Copies share the same object. Means the assigned value is a reference to the same object in memory.
 - Examples: class, object, string, array

 ------------------------------
 Value Type vs Reference Type - Textual Table
 ------------------------------

| Feature             | Value Type                   | Reference Type                       |
|---------------------|------------------------------|--------------------------------------|
| Stores              | Actual value                 | Reference to object                  |
| Memory              | Stack                        | Heap                                 |
| Copy Behavior       | Copies value                 | Copies reference                     |
| Performance         | Faster                       | Slightly slower                      |
| Garbage Collection  | Not needed                   | Needed (by GC)                       |
| Nullability         | Only with nullable types     | Can be null                          |


 ==============================
 🔍 Example 1: Value Type - stored in Stack
 ==============================

void ValueTypeExample()
{
    int a = 5;
    int b = a;    // b gets a copy of a
    b = 10; // b changed, but a remains unchanged cause they dont share the same memory

    Console.WriteLine($"Value of a: {a}"); // Output: 5
    Console.WriteLine($"Value of b: {b}"); // Output: 10
}

 ==============================
 🔍 Example 2: Reference Type - stored in Heap
 ==============================

*/

/*class Person
{
    public string Name;
}
void ReferenceTypeExample()
{
    Person p1 = new Person();
    p1.Name = "Alice";

    Person p2 = p1;  // p2 references same object as p1
    p2.Name = "Bob"; // p2 changes the Name property of the object that p1 references

    Console.WriteLine($"p1.Name: {p1.Name}"); // Output: Bob
    Console.WriteLine($"p2.Name: {p2.Name}"); // Output: Bob
}*/

