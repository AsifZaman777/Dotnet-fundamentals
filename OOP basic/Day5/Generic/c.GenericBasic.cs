/*Boxing is the process of converting a value type to a reference type (object).
The value type is wrapped in an object and stored on the managed heap.

Unboxing is the reverse process - extracting the value type from the boxed object back to its original form.

 

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
 
 
 
 
 
 */
