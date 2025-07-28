/* \
 Example: Basics of Garbage Collection (GC) in .NET 8
 
 GARBAGE COLLECTION BASICS:
 - GC is .NET's automatic memory management system
 - It runs on a separate thread and frees memory from unreferenced objects
 - Uses generational collection: Gen 0 (new objects), Gen 1 (survived once), Gen 2 (long-lived)
 - Objects ≥85KB go directly to Large Object Heap (LOH) in Gen 2
 - GC is triggered by memory pressure, allocation thresholds, or manually
*/


using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Simple GC Example ===\n");

        // BASIC GC CONCEPT:
        // GC automatically cleans up objects you don't use anymore

        // 1. Creating objects (GC manages these automatically)
        string name = "John";
        int[] numbers = new int[100];

        Console.WriteLine($"Memory used: {GC.GetTotalMemory(false)} bytes");

        // 2. LEGACY WAY - Manual cleanup (old, error-prone way)
        FileStream file = null;
        try
        {
            file = new FileStream("test.txt", FileMode.Create);
            // do work with file
        }
        finally
        {
            file?.Dispose(); // Must remember to clean up!
        }

        // 3. MODERN WAY - Automatic cleanup (recommended)
        using (var file2 = new FileStream("test2.txt", FileMode.Create))
        {
            // do work with file
        } // Automatically cleaned up here!

        // 4. Check what GC is doing
        Console.WriteLine($"Gen 0 collections: {GC.CollectionCount(0)}");
        Console.WriteLine($"Gen 1 collections: {GC.CollectionCount(1)}");
        Console.WriteLine($"Gen 2 collections: {GC.CollectionCount(2)}");
    }
}

/*
Note: GC.collect() is not recommended in production code as it can lead to performance issues.

keypoints:
- GC is automatic memory management in .NET
- It runs on a separate thread to free memory from unreferenced objects
- Uses generational collection: Gen 0 (new), Gen 1 (survived once), Gen 2 (long-lived)
- Objects ≥85KB go to Large Object Heap (LOH) in Gen 2
- GC is triggered by memory pressure, allocation thresholds, or manually
- Use 'using' statement for automatic cleanup of IDisposable objects
- Avoid manual GC.collect() in production code as it can hurt performance
- Use GC.CollectionCount to check how many collections have occurred
- Use GC.GetTotalMemory to check memory usage
- Use 'using' statement for automatic cleanup of IDisposable objects

Idisposable object: FileStream, MemoryStream, StreamReader, StreamWriter, etc.
Idisposable: An interface that provides a mechanism for releasing unmanaged resources.


 */