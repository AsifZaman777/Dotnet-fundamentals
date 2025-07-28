using System;

/*

 ==============================
 📌 Boxing and Unboxing in C#
 ==============================

Boxing - Converting a value type to an object type (heap allocation).
Unboxing - Converting an object type back to a value type (stack allocation).

BEFORE BOXING:
Stack Memory          Heap Memory
┌─────────────┐      ┌─────────────┐
│ int x = 42  │      │   (empty)   │
│             │      │             │
└─────────────┘      └─────────────┘

AFTER BOXING (object obj = x):
Stack Memory          Heap Memory
┌─────────────┐      ┌─────────────┐
│ int x = 42  │      │ ┌─────────┐ │
│ obj ────────┼──────┼→│ int: 42 │ │
│             │      │ └─────────┘ │
└─────────────┘      └─────────────┘

UNBOXING (int y = (int)obj):
Stack Memory          Heap Memory
┌─────────────┐      ┌─────────────┐
│ int x = 42  │      │ ┌─────────┐ │
│ obj ────────┼──────┼→│ int: 42 │ │
│ int y = 42  │      │ └─────────┘ │
└─────────────┘      └─────────────┘



// ==============================
// 📊 When Boxing/Unboxing Occurs
// ==============================
/*
 BOXING OCCURS WHEN:
 1. Assigning value type to object
 2. Assigning value type to interface
 3. Adding value types to non-generic collections
 4. Calling virtual methods on value types
 5. Using value types with string concatenation (ToString())

 UNBOXING OCCURS WHEN:
 1. Explicitly casting boxed object back to value type
 2. Retrieving value types from non-generic collections

 PERFORMANCE IMPLICATIONS:
 1. Boxing: Heap allocation + copying data
 2. Unboxing: Type checking + copying data
 3. Garbage collection pressure from boxed objects
 4. Cache misses due to heap indirection

 AVOIDING BOXING/UNBOXING:
 1. Use generics instead of object-based collections
 2. Use generic methods instead of object parameters
 3. Implement IEquatable<T> to avoid boxing in comparisons
 4. Use nullable value types instead of boxing for null representation
*/


class BoxingUnboxingDemo
{
    static void Main()
    {
        Console.WriteLine("=== Boxing and Unboxing Examples ===\n");

        BasicBoxingUnboxing();
        BoxingWithCollections();
        InterfaceBoxing();
        CommonPitfalls();
    }

    // ==============================
    // 🔍 Example 1: Basic Boxing/Unboxing
    // ==============================
    static void BasicBoxingUnboxing()
    {
        Console.WriteLine("1. Basic Boxing and Unboxing:");

        // Value type on stack
        int valueType = 42;
        Console.WriteLine($"Original value: {valueType} (Stack)");

        // Boxing: int → object (moves to heap)
        object boxedValue = valueType;  // Implicit boxing
        Console.WriteLine($"Boxed value: {boxedValue} (Heap)");
        Console.WriteLine($"Are they same reference? {ReferenceEquals(valueType, boxedValue)}");

        // Unboxing: object → int (back to stack)
        int unboxedValue = (int)boxedValue;  // Explicit unboxing
        Console.WriteLine($"Unboxed value: {unboxedValue} (Stack)");

        // Values are equal but not same reference
        Console.WriteLine($"Values equal? {valueType == unboxedValue}");
        Console.WriteLine($"References equal? {ReferenceEquals(valueType, unboxedValue)}");
        Console.WriteLine();
    }

    // ==============================
    // 🔍 Example 2: Boxing with Collections (Pre-Generics Era)
    // ==============================
    static void BoxingWithCollections()
    {
        Console.WriteLine("2. Boxing with Non-Generic Collections:");

        // ArrayList stores objects, so value types get boxed
        System.Collections.ArrayList arrayList = new();

        // Each Add() causes boxing
        arrayList.Add(10);    // int boxed to object
        arrayList.Add(20.5);  // double boxed to object
        arrayList.Add('A');   // char boxed to object
        arrayList.Add(true);  // bool boxed to object

        Console.WriteLine("Values added to ArrayList (all boxed):");
        foreach (object item in arrayList)
        {
            Console.WriteLine($"  {item} (Type: {item.GetType()})");
        }

        // Retrieving requires unboxing
        int firstNumber = (int)arrayList[0];  // Unboxing required
        Console.WriteLine($"First number unboxed: {firstNumber}");
        Console.WriteLine();
    }

    // ==============================
    // 🔍 Example 3: Interface Boxing
    // ==============================
    static void InterfaceBoxing()
    {
        Console.WriteLine("3. Interface Boxing:");

        int number = 123;

        // Boxing occurs when casting value type to interface
        IComparable comparable = number;  // Boxing happens here
        Console.WriteLine($"Boxed as IComparable: {comparable}");

        // This also causes boxing
        IFormattable formattable = number;  // Another boxing
        Console.WriteLine($"Boxed as IFormattable: {formattable.ToString("X", null)}");
        Console.WriteLine();
    }

    // ==============================
    // 🔍 Example 4: Common Pitfalls
    // ==============================
    static void CommonPitfalls()
    {
        Console.WriteLine("5. Common Pitfalls:");

        // Pitfall 1: Wrong unboxing type
        try
        {
            object boxedInt = 42;
            //long wrongUnbox = (long)boxedInt;  // This will throw InvalidCastException
        }
        catch (InvalidCastException ex)
        {
            Console.WriteLine($"❌ Invalid cast: {ex.Message}");
        }

        // Pitfall 2: Unboxing null
        try
        {
            object nullObject = null;
            //int nullUnbox = (int)nullObject;  // This will throw NullReferenceException
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine($"❌ Null unboxing: {ex.Message}");
        }

        // Pitfall 3: Multiple boxing of same value creates different objects
        int value = 100;
        object box1 = value;
        object box2 = value;
        Console.WriteLine($"Same value, different boxed objects: {!ReferenceEquals(box1, box2)}");

        // Pitfall 4: Modifying boxed value types (not possible)
        Console.WriteLine("Note: Boxed value types are immutable - you can't modify them directly");
        Console.WriteLine();
    }

}

