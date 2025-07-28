/*
 Genric - Generic is a powerful feature in C# that allows you to define classes, methods, and interfaces with a placeholder for the type. 
This enables type safety and performance benefits by avoiding boxing and unboxing of value types.

Non-Generic Collections: Before generics, collections like ArrayList stored elements as objects, leading to boxing for value types.


Real life usecases of Generics in C#:
 1. **Data Structures**: Implementing collections like List<T>, Dictionary<TKey, TValue> for type-safe storage and retrieval of data without boxing.
 2. **Algorithms**: Writing reusable algorithms that work with any data type, such as sorting or searching, without sacrificing performance.
 3. **Dependency Injection**: Using generics in DI frameworks to resolve services with specific types at runtime without boxing overhead.
 4. **Event Handling**: Defining events with generic parameters to allow type-safe event arguments without boxing.


*******Memory Allocation in C# Collections: Non-Generic vs Generic*******

Key Differences in Heap Usage: Non-Generic (ArrayList) vs Generic (List<T>)

1. Non-Generic (ArrayList) - Value Types
   Stack                    Heap
   +----------------+       +-------------------+
   | int value = 42 | ----> | ArrayList         |
   |                |       | [0] -> Object(42) | (Boxing: new object per value)
   +----------------+       | [1] -> Object(43) | (Each int boxed, heap allocation)
                            | ...               |
                            +-------------------+
   - Heap Usage: Internal array + separate object per value type (boxed).
   - Performance: Slower due to boxing/unboxing.
   - Memory: Higher usage (extra objects for boxing).

2. Generic (List<int>) - Value Types
   Stack                    Heap
   +----------------+       +-------------------+
   | int value = 42 | ----> | List<int>         |
   |                |       | [0] = 42          | (No boxing, direct storage)
   +----------------+       | [1] = 43          |
                            | ...               |
                            +-------------------+
   - Heap Usage: Only internal array on heap, values stored directly.
   - Performance: Faster, no boxing/unboxing.
   - Memory: Lower usage (no extra objects).

3. Non-Generic (ArrayList) - Reference Types
   Stack                    Heap
   +----------------+       +-------------------+
   | string s = "Hi"| ----> | ArrayList         |
   |                |       | [0] -> "Hi"       | (Reference to string object)
   +----------------+       | [1] -> "World"    |
                            | ...               |
                            +-------------------+
                            | "Hi" (string obj) |
                            | "World" (string)  |
                            +-------------------+
   - Heap Usage: Internal array + existing reference type objects.
   - Performance: No boxing/unboxing (same as generic).
   - Memory: Similar to generic for reference types.

4. Generic (List<string>) - Reference Types
   Stack                    Heap
   +----------------+       +-------------------+
   | string s = "Hi"| ----> | List<string>      |
   |                |       | [0] -> "Hi"       | (Reference to string object)
   +----------------+       | [1] -> "World"    |
                            | ...               |
                            +-------------------+
                            | "Hi" (string obj) |
                            | "World" (string)  |
                            +-------------------+
   - Heap Usage: Internal array + existing reference type objects.
   - Performance: No boxing/unboxing (same as non-generic).
   - Memory: Same as non-generic for reference types.

Summary:
- Non-Generic (ArrayList): Boxing for value types creates extra heap objects, reducing performance and memory efficiency.
- Generic (List<T>): No boxing for value types, only internal array on heap, better performance and memory efficiency.
- Reference Types: Both use similar heap structure (array + object references), no significant difference.

 */





//Non generic way

using System;
using System.Collections;
using System.Collections.Generic;

class NonGenericBoxingDemo
{
    static void Main(string[] args)
    {
        ArrayList list = new ArrayList();
        int number = 42; //value type
        list.Add(number); // Boxing occurs here, converting value type to reference type (object) -> stack to heap memory allocation
        Console.WriteLine($"Boxed value: {list[0]} (Heap)"); // Accessing boxed value

        try
        {
            int unboxedValue = (int)list[0]; //unboxing occurs here, converting reference type back to value type -> heap to stack memory allocation
            Console.WriteLine($"Unboxed value: {unboxedValue} (Stack)"); // Accessing unboxed value

            //try with non-value type
            string strValue = "Ola";
            list.Add(strValue); // boxing not needed, string is already a reference type
            string unboxedString = (string)list[1]; // No unboxing needed, as string is a reference type
        }
        catch (InvalidCastException ex)
        {
            Console.WriteLine($"Unboxing failed: {ex.Message}");
        }

        //demonstrate performance issue
        Console.WriteLine("Performance issue with non-generic collections:");
        for (int i = 0; i < 1000000; i++)
        {
            list.Add(i); // Boxing occurs for each value type added
        }
        Console.WriteLine($"Total items in list: {list.Count}"); // Accessing count of items in the list

    }

    /* Note: Non-generic collections like ArrayList are less efficient due to boxing and unboxing overhead.
      Garbage collection is also more frequent due to the creation of many boxed objects.*/
}


class GenericBoxingDemo
{
    static void Main()
    {
      List<int> list = new List<int>();
        int number = 42;
        list.Add(number); // No boxing occurs, as List<int> is a generic collection for value types. No heap allocation needed
        Console.WriteLine($"{number} is stored (Stack)");

        //retrieve value without unboxing. Directly access the value type from the generic collection
        int retrievedValue= list[0]; // No unboxing needed, no casting needed, as List<int> is type-safe
        Console.WriteLine($"Retrieved value: {retrievedValue} (Stack)"); // Accessing retrieved value



        //demonstrate performance improvement
        Console.WriteLine("Performance improvement with generic collections:");
        for (int i = 0; i < 1000000; i++)
        {
            list.Add(i); // No boxing occurs for each value type added
        }
        Console.WriteLine($"Total items in list: {list.Count}"); // Accessing count of items in the list
    }
}





/*
 ==============================
 📌 Memory Allocation Diagrams - Generic vs Non-Generic Collections
 ==============================

 Based on your code:
 - NonGenericBoxingDemo: ArrayList list = new ArrayList(); list.Add(42);
 - GenericBoxingDemo: List<int> list = new List<int>(); list.Add(42);

 ==============================
 🔍 NON-GENERIC COLLECTION (ArrayList) - WITH BOXING
 ==============================

 STEP 1: Initial State
 Stack Memory                    Heap Memory
 ┌─────────────────────────┐    ┌──────────────────────────┐
 │ ArrayList list ─────────┼────┼→ ArrayList Object        │
 │ int number = 42         │    │  - capacity: 4           │
 │                         │    │  - count: 0              │
 └─────────────────────────┘    │  - items: object[]       │
                                └──────────────────────────┘

 STEP 2: After list.Add(number) - BOXING OCCURS
 Stack Memory                    Heap Memory
 ┌─────────────────────────┐    ┌──────────────────────────┐
 │ ArrayList list ─────────┼────┼→ ArrayList Object        │
 │ int number = 42         │    │  - capacity: 4           │
 │                         │    │  - count: 1              │
 └─────────────────────────┘    │  - items[0] ─────────────┼─┐
                                └──────────────────────────┘ │
                                ┌──────────────────────────┐ │
                                │ ┌──────────────────────┐ │←┘
                                │ │ Boxed int: 42        │ │     
                                │ │ (object wrapper)     │ │                      
                                │ └──────────────────────┘ │
                                └──────────────────────────┘

 STEP 3: After int unboxedValue = (int)list[0] - UNBOXING OCCURS
 Stack Memory                    Heap Memory
 ┌─────────────────────────┐    ┌──────────────────────────┐
 │ ArrayList list ─────────┼────┼→ ArrayList Object        │
 │ int number = 42         │    │  - items[0] ─────────────┼─┐
 │ int unboxedValue = 42   │    └──────────────────────────┘ │
 └─────────────────────────┘    ┌──────────────────────────┐ │
                                │ ┌──────────────────────┐ │←┘
                                │ │ Boxed int: 42        │ │
                                │ │ (remains on heap)    │ │
                                │ └──────────────────────┘ │
                                └──────────────────────────┘

 ⚠️  MEMORY OVERHEAD: 
 - Original int: 4 bytes (Stack)
 - Boxed int: 24 bytes (Heap) = 8 bytes object header + 4 bytes int + 12 bytes padding
 - Total: 28 bytes for storing one int!

 ==============================
 🔍 GENERIC COLLECTION (List<int>) - NO BOXING
 ==============================

 STEP 1: Initial State
 Stack Memory                    Heap Memory
 ┌─────────────────────────┐    ┌──────────────────────────┐
 │ List<int> list ─────────┼────┼→ List<int> Object        │
 │ int number = 42         │    │  - capacity: 4           │
 │                         │    │  - count: 0              │
 └─────────────────────────┘    │  - items: int[]          │
                                └──────────────────────────┘

 STEP 2: After list.Add(number) - NO BOXING
 Stack Memory                    Heap Memory
 ┌─────────────────────────┐    ┌──────────────────────────┐
 │ List<int> list ─────────┼────┼→ List<int> Object        │
 │ int number = 42         │    │  - capacity: 4           │
 │                         │    │  - count: 1              │
 └─────────────────────────┘    │  - items[0]: 42 (int)    │
                                │    (stored directly)     │
                                └──────────────────────────┘

 STEP 3: After int retrievedValue = list[0] - NO UNBOXING
 Stack Memory                    Heap Memory
 ┌─────────────────────────┐    ┌──────────────────────────┐
 │ List<int> list ─────────┼────┼→ List<int> Object        │
 │ int number = 42         │    │  - items[0]: 42 (int)    │
 │ int retrievedValue = 42 │    │                          │
 └─────────────────────────┘    └──────────────────────────┘

 ✅ MEMORY EFFICIENCY:
 - Original int: 4 bytes (Stack)  
 - Stored int: 4 bytes (Heap array)
 - Retrieved int: 4 bytes (Stack)
 - Total: 12 bytes for three int variables - NO BOXING OVERHEAD!

 ==============================
 📊 Performance Comparison - 1,000,000 iterations
 ==============================

 NON-GENERIC (ArrayList):
 Stack Memory                    Heap Memory
 ┌─────────────────────────┐    ┌──────────────────────────┐
 │ ArrayList list          │    │ ArrayList Object         │
 │ for(int i=0; i<1M; i++) │    │ ┌──────────────────────┐ │
 │                         │    │ │ Boxed int: 0         │ │
 └─────────────────────────┘    │ │ Boxed int: 1         │ │
                                │ │ Boxed int: 2         │ │
                                │ │ ...                  │ │
                                │ │ Boxed int: 999,999   │ │
                                │ └──────────────────────┘ │
                                └──────────────────────────┘
 
 Memory Usage: ~24MB (24 bytes × 1,000,000 boxed objects)
 GC Pressure: HIGH (1M objects to collect)

 GENERIC (List<int>):
 Stack Memory                    Heap Memory
 ┌─────────────────────────┐    ┌──────────────────────────┐
 │ List<int> list          │    │ List<int> Object         │
 │ for(int i=0; i<1M; i++) │    │ int[]: [0,1,2,3...999999]│
 │                         │    │ (contiguous array)       │
 └─────────────────────────┘    └──────────────────────────┘

 Memory Usage: ~4MB (4 bytes × 1,000,000 ints)
 GC Pressure: LOW (only 1 array object + List object)




 ==============================
 📈 Memory Layout Summary
 ==============================

 NON-GENERIC (ArrayList):
 items[0]: Object(42) → Points to boxed object containing 42
 items[1]: Object(43) → Points to boxed object containing 43
 items[2]: Object(44) → Points to boxed object containing 44

 GENERIC (List<int>):
 items[0]: 42  (direct int value)
 items[1]: 43  (direct int value) 
 items[2]: 44  (direct int value)

 ==============================
 🔍 CORRECT VISUAL REPRESENTATION
 ==============================

 NON-GENERIC ArrayList Internal Structure:
 ┌────────────────────────────────────┐
 │ ArrayList Object                   │
 │ ┌────────────────────────────────┐ │
 │ │ object[] items array           │ │
 │ │ ┌─────┬─────┬─────┬─────┐      │ │
 │ │ │[0]  │[1]  │[2]  │[3]  │      │ │
 │ │ │  │  │  │  │  │  │ ... │      │ │
 │ │ └──┼──┴──┼──┴──┼──┴─────┘      │ │
 │ └─────┼─────┼─────┼──────────────┘ │
 └───────┼─────┼─────┼────────────────┘
         │     │     │
         ▼     ▼     ▼
    ┌─────────┐ ┌─────────┐ ┌─────────┐
    │Boxed 42 │ │Boxed 43 │ │Boxed 44 │
    │(24 bytes│ │(24 bytes│ │(24 bytes│
    └─────────┘ └─────────┘ └─────────┘

 GENERIC List<int> Internal Structure:
 ┌─────────────────────────────────────┐
 │ List<int> Object                    │
 │ ┌─────────────────────────────────┐ │
 │ │ int[] items array               │ │
 │ │ ┌─────┬─────┬─────┬─────┐       │ │
 │ │ │ 42  │ 43  │ 44  │ ... │       │ │
 │ │ │(4b) │(4b) │(4b) │     │       │ │
 │ │ └─────┴─────┴─────┴─────┘       │ │
 │ └─────────────────────────────────┘ │
 └─────────────────────────────────────┘
   ↑ All values stored directly in array



 ArrayList (Non-Generic):
 [Stack] → [Heap: ArrayList] → [Heap: object[]] → [Heap: Boxed Values]
                                      ↓
 Multiple heap allocations, scattered memory, boxing overhead

 List<int> (Generic):  
 [Stack] → [Heap: List<int>] → [Heap: int[]]
                                      ↓
 Single array allocation, contiguous memory, no boxing


*/





/*
 ==============================
 📌 Boxed Int Memory Breakdown (24 bytes total) (Deep concept)
 ==============================

 When you box an int (4 bytes), .NET creates an object wrapper on the heap.
 Here's the detailed memory layout:

 ┌─────────────────────────────────────────────────────────┐
 │                 BOXED INT OBJECT (24 bytes)             │
 ├─────────────────────────────────────────────────────────┤
 │ 1. Object Header (8 bytes)                              │
 │    ├─ Method Table Pointer: 4 bytes                     │
 │    └─ Sync Block Index: 4 bytes                         │
 ├─────────────────────────────────────────────────────────┤
 │ 2. Actual Int Value (4 bytes)                           │
 │    └─ The value 42 stored here                          │
 ├─────────────────────────────────────────────────────────┤
 │ 3. Memory Alignment Padding (12 bytes)                  │
 │    └─ Empty space for 16-byte boundary alignment        │
 └─────────────────────────────────────────────────────────┘

 DETAILED BREAKDOWN:

 1. OBJECT HEADER (8 bytes):
    - Method Table Pointer (4 bytes): Points to type information
    - Sync Block Index (4 bytes): Used for locking/synchronization

 2. VALUE DATA (4 bytes):
    - The actual int value (42 in your example)

 3. MEMORY ALIGNMENT PADDING (12 bytes):
    - .NET aligns objects to 8-byte or 16-byte boundaries for performance
    - Since we have 12 bytes (8 + 4), we need 12 more bytes to reach 24
    - This ensures optimal memory access patterns

 ==============================
 🔍 Why This Matters - Memory Comparison
 ==============================

 UNBOXED INT (Stack):
 ┌─────────────┐
 │ int x = 42  │ ← 4 bytes total
 └─────────────┘

 BOXED INT (Heap):
 ┌─────────────────────────────┐
 │ Object Header    (8 bytes)  │
 │ Int Value       (4 bytes)   │ ← Your actual data
 │ Padding        (12 bytes)   │
 └─────────────────────────────┘
   ↑ 24 bytes total (600% overhead!)

 MEMORY EFFICIENCY COMPARISON:
 - Stack int: 4 bytes
 - Boxed int: 24 bytes  
 - Overhead: 20 bytes (500% more memory!)
 - Efficiency loss: 83% wasted space

 ==============================
 📊 Real Example with Your Code
 ==============================

 Your ArrayList example:
 for (int i = 0; i < 1000000; i++)
 {
     list.Add(i); // Each i gets boxed to 24 bytes
 }

 MEMORY USAGE CALCULATION:
 - Unboxed: 1,000,000 × 4 bytes = 4 MB
 - Boxed: 1,000,000 × 24 bytes = 24 MB
 - Extra memory used: 20 MB (500% more!)
 - Plus ArrayList overhead, GC pressure, etc.

 ==============================
 🔧 Memory Alignment Explanation
 ==============================

 Why 12 bytes padding?
 
 Current size: 8 (header) + 4 (int) = 12 bytes
 Next alignment boundary: 16 bytes (common on 64-bit systems)
 Padding needed: 16 - 12 = 4 bytes
 
 BUT .NET often aligns to larger boundaries (24, 32 bytes) 
 for better cache performance, hence 12 bytes padding.

 This varies by:
 - .NET version
 - Target architecture (32-bit vs 64-bit)
 - Specific runtime optimizations
*/
