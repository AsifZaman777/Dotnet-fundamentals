/*
╔══════════════════════════════════════════════════════════════════════════════════════╗
║                                THE DIAMOND PROBLEM                                   ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

DEFINITION: The Diamond Problem occurs when a class inherits from two classes that 
both inherit from the same base class, creating an ambiguous inheritance diamond shape.

CORE ISSUES:
1. Method Resolution Ambiguity
2. Memory Layout Duplication  
3. Constructor Call Confusion
4. Virtual Function Table Conflicts

╔══════════════════════════════════════════════════════════════════════════════════════╗
║                              DIAMOND INHERITANCE PATTERN                             ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

THE DIAMOND SHAPE STRUCTURE:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│                                                                                     │
│                            ┌─────────────────┐                                     │
│                            │   BaseClass     │  ← Grandparent                      │
│                            │   + Method()    │                                     │
│                            │   + field       │                                     │
│                            └─────────┬───────┘                                     │
│                                      │                                             │
│                         ┌────────────┴────────────┐                               │
│                         │                         │                               │
│                         ▼                         ▼                               │
│                ┌─────────────────┐       ┌─────────────────┐                      │
│                │   LeftChild     │       │   RightChild    │  ← Parents           │
│                │   + Method()    │       │   + Method()    │                      │
│                │   + leftField   │       │   + rightField  │                      │
│                └─────────┬───────┘       └─────────┬───────┘                      │
│                          │                         │                              │
│                          └────────────┬────────────┘                              │
│                                       │                                           │
│                                       ▼                                           │
│                              ┌─────────────────┐                                  │
│                              │  DiamondClass   │  ← Child (PROBLEM!)             │
│                              │  Which Method()?│                                  │
│                              │  How many fields?│                                 │
│                              └─────────────────┘                                  │
│                                                                                    │
└─────────────────────────────────────────────────────────────────────────────────────┘

THE DIAMOND SHAPE: BaseClass → LeftChild + RightChild → DiamondClass

╔══════════════════════════════════════════════════════════════════════════════════════╗
║                              AMBIGUITY SCENARIOS                                     ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

1. METHOD RESOLUTION AMBIGUITY:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│ DiamondClass obj = new DiamondClass();                                              │
│ obj.Method(); // ❌ WHICH METHOD?                                                   │
│                                                                                     │
│ POSSIBLE CANDIDATES:                                                                │
│ • BaseClass.Method()     - Original implementation                                 │
│ • LeftChild.Method()     - Left override                                           │
│ • RightChild.Method()    - Right override                                          │
│                                                                                     │
│ COMPILER CONFUSION: Cannot determine which version to call!                        │
└─────────────────────────────────────────────────────────────────────────────────────┘

2. MEMORY LAYOUT DUPLICATION:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│ MEMORY STRUCTURE QUESTION:                                                          │
│                                                                                     │
│ Option A: TWO COPIES                    Option B: ONE COPY                         │
│ ┌─────────────────┐                     ┌─────────────────┐                       │
│ │ DiamondClass    │                     │ DiamondClass    │                       │
│ │ ├─LeftChild     │                     │ ├─LeftChild     │                       │
│ │ │  └─BaseClass   │                     │ │  └─┐           │                       │
│ │ ├─RightChild    │                     │ ├─RightChild   │                       │
│ │ │  └─BaseClass   │                     │ │    └─BaseClass │                       │
│ │ │     (COPY!)    │                     │ │      (SHARED)  │                       │
│ └─────────────────┘                     └─────────────────┘                       │
│                                                                                     │
│ PROBLEMS: Wastes memory,                 PROBLEMS: Complex sharing,                 │
│          field conflicts                         inheritance conflicts              │
│                                                                                     │
│ REAL WORLD ANALOGY:                                                                 │
│ Does a StudentEmployee have TWO Person identities or ONE shared identity?          │
│ • Two copies: Different ages, names in Student vs Employee sides (confusing)      │
│ • One copy: Shared identity but which role takes precedence? (complex)            │
└─────────────────────────────────────────────────────────────────────────────────────┘

3. CONSTRUCTOR CHAINING CONFUSION:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│ CONSTRUCTOR CALL ORDER AMBIGUITY:                                                   │
│                                                                                     │
│ new DiamondClass() → Which path?                                                    │
│                                                                                     │
│ Path 1: DiamondClass → LeftChild → BaseClass                                       │
│ Path 2: DiamondClass → RightChild → BaseClass                                      │
│                                                                                     │
│ QUESTION: Is BaseClass constructor called once or twice?                           │
│          How are parameters passed through both paths?                             │
│                                                                                     │
│ STUDENT-EMPLOYEE EXAMPLE:                                                           │
│ new StudentEmployee("John", 20) → Which constructor path?                          │
│ Path 1: StudentEmployee → Student → Person                                         │
│ Path 2: StudentEmployee → Employee → Person                                        │
│ Does Person("John", 20) get called once or twice?                                  │
└─────────────────────────────────────────────────────────────────────────────────────┘

╔══════════════════════════════════════════════════════════════════════════════════════╗
║                          REAL-WORLD DIAMOND EXAMPLE                                  ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

STUDENT-EMPLOYEE DIAMOND PROBLEM (HYPOTHETICAL - NOT POSSIBLE IN C#):

┌─────────────────────────────────────────────────────────────────────────────────────┐
│                            ┌─────────────────┐                                     │
│                            │     Person      │  ← Base class                       │
│                            │   + name        │                                     │
│                            │   + age         │                                     │
│                            │   + GetInfo()   │                                     │
│                            └─────────┬───────┘                                     │
│                                      │                                             │
│                         ┌────────────┴────────────┐                               │
│                         │                         │                               │
│                         ▼                         ▼                               │
│                ┌─────────────────┐       ┌─────────────────┐                      │
│                │    Student      │       │    Employee     │                      │
│                │   + studentId   │       │   + employeeId  │                      │
│                │   + GetInfo()   │       │   + GetInfo()   │                      │
│                │   + Study()     │       │   + Work()      │                      │
│                └─────────┬───────┘       └─────────┬───────┘                      │
│                          │                         │                              │
│                          └────────────┬────────────┘                              │
│                                       │                                           │
│                                       ▼                                           │
│                              ┌─────────────────┐                                  │
│                              │ StudentEmployee │  ← DIAMOND PROBLEM!             │
│                              │                 │                                  │
│                              │ CONFLICTS:      │                                  │
│                              │ • Which name?   │                                  │
│                              │ • Which age?    │                                  │
│                              │ • Which GetInfo?│                                  │
│                              │ • Two Person    │                                  │
│                              │   instances?    │                                  │
│                              └─────────────────┘                                  │
└─────────────────────────────────────────────────────────────────────────────────────┘

SPECIFIC AMBIGUITIES IN STUDENT-EMPLOYEE SCENARIO:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│ StudentEmployee se = new StudentEmployee("Alice", 22, "ST001", "EMP001");           │
│                                                                                     │
│ // ❌ AMBIGUOUS METHOD CALLS:                                                       │
│ se.GetInfo();        // Student.GetInfo() or Employee.GetInfo()?                   │
│ se.name;             // Student's name or Employee's name?                         │
│ se.age;              // Student's age or Employee's age?                           │
│                                                                                     │
│ // ❌ MEMORY CONFUSION:                                                             │
│ // Does StudentEmployee contain:                                                   │
│ // 1. Two separate Person objects (wasteful, inconsistent)                         │
│ // 2. One shared Person object (complex sharing mechanism)                         │
│                                                                                     │
│ // ❌ IDENTITY CRISIS:                                                              │
│ // Is Alice the same person as a Student and Employee, or two different entities? │
└─────────────────────────────────────────────────────────────────────────────────────┘

╔══════════════════════════════════════════════════════════════════════════════════════╗
║                            LANGUAGE-SPECIFIC SOLUTIONS                               ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

┌─────────────┬─────────────────┬─────────────────────────────────────────────────────┐
│  Language   │ Multiple Class  │                 Solution Strategy                   │
│             │  Inheritance    │                                                     │
├─────────────┼─────────────────┼─────────────────────────────────────────────────────┤
│    C++      │      ✅ Yes     │ Virtual Inheritance                                 │
│             │                 │ - virtual base classes                             │
│             │                 │ - explicit resolution                              │
│             │                 │ - complex but powerful                             │
├─────────────┼─────────────────┼─────────────────────────────────────────────────────┤
│    C#       │      ❌ No      │ Single Inheritance + Multiple Interfaces           │
│             │                 │ - Prevents diamond problem entirely               │
│             │                 │ - Interface default methods (C# 8.0+)             │
│             │                 │ - Explicit interface implementation               │
├─────────────┼─────────────────┼─────────────────────────────────────────────────────┤
│   Java      │      ❌ No      │ Single Inheritance + Multiple Interfaces           │
│             │                 │ - Same approach as C#                              │
│             │                 │ - Default methods in interfaces (Java 8+)         │
├─────────────┼─────────────────┼─────────────────────────────────────────────────────┤
│  Python     │      ✅ Yes     │ Method Resolution Order (MRO)                      │
│             │                 │ - C3 linearization algorithm                       │
│             │                 │ - Predictable resolution order                     │
└─────────────┴─────────────────┴─────────────────────────────────────────────────────┘

╔══════════════════════════════════════════════════════════════════════════════════════╗
║                              C# DIAMOND PREVENTION                                   ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

C# DESIGN PHILOSOPHY: "Prevent the problem rather than solve it"

COMPILATION ERROR EXAMPLE (STUDENT-EMPLOYEE):
┌─────────────────────────────────────────────────────────────────────────────────────┐
│ class Person                                                                        │
│ {                                                                                   │
│     public string Name { get; set; }                                               │
│     public int Age { get; set; }                                                   │
│     public virtual string GetInfo() => $"Name: {Name}, Age: {Age}";               │
│ }                                                                                   │
│                                                                                     │
│ class Student : Person                                                              │
│ {                                                                                   │
│     public string StudentId { get; set; }                                          │
│     public override string GetInfo() => $"{base.GetInfo()}, StudentID: {StudentId}";│
│ }                                                                                   │
│                                                                                     │
│ class Employee : Person                                                             │
│ {                                                                                   │
│     public string EmployeeId { get; set; }                                         │
│     public override string GetInfo() => $"{base.GetInfo()}, EmployeeID: {EmployeeId}";│
│ }                                                                                   │
│                                                                                     │
│ // ❌ COMPILER ERROR CS1721: Class cannot have multiple base classes               │
│ class StudentEmployee : Student, Employee                                          │
│ {                                                                                   │
│     // This is NOT allowed in C#                                                   │
│ }                                                                                   │
└─────────────────────────────────────────────────────────────────────────────────────┘

C# ALTERNATIVE APPROACH (STUDENT-EMPLOYEE SOLUTION):
┌─────────────────────────────────────────────────────────────────────────────────────┐
│                        SINGLE INHERITANCE + INTERFACES                             │
│                                                                                     │
│                            ┌─────────────────┐                                     │
│                            │     Person      │                                     │
│                            │   + name, age   │                                     │
│                            │   + GetInfo()   │                                     │
│                            └─────────┬───────┘                                     │
│                                      │                                             │
│                                      ▼                                             │
│                            ┌─────────────────┐                                     │
│                            │ StudentEmployee │                                     │
│                            │ (Person base)   │                                     │
│                            └─────────┬───────┘                                     │
│                                      │                                             │
│              ┌─────────────────────────┼─────────────────────────┐                 │
│              │                         │                         │                 │
│              ▼                         ▼                         ▼                 │
│    ┌─────────────────┐       ┌─────────────────┐       ┌─────────────────┐         │
│    │   IStudent      │       │   IEmployee     │       │   IWorker       │         │
│    │ + GetStudentInfo│       │ + GetEmpInfo()  │       │ + DoWork()      │         │
│    │ + Study()       │       │ + Work()        │       │ + GetSchedule() │         │
│    └─────────────────┘       └─────────────────┘       └─────────────────┘         │
│                                                                                     │
│ RESULT: One Person identity with multiple role capabilities                         │
└─────────────────────────────────────────────────────────────────────────────────────┘

╔══════════════════════════════════════════════════════════════════════════════════════╗
║                         C# STUDENT-EMPLOYEE IMPLEMENTATION                           ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

PROPER C# IMPLEMENTATION:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│ // Base class - single inheritance                                                  │
│ class Person                                                                        │
│ {                                                                                   │
│     public string Name { get; set; }                                               │
│     public int Age { get; set; }                                                   │
│     public virtual string GetInfo() => $"Name: {Name}, Age: {Age}";               │
│ }                                                                                   │
│                                                                                     │
│ // Role interfaces - multiple capabilities                                          │
│ interface IStudent                                                                  │
│ {                                                                                   │
│     string StudentId { get; set; }                                                 │
│     void Study();                                                                  │
│     string GetStudentInfo();                                                       │
│ }                                                                                   │
│                                                                                     │
│ interface IEmployee                                                                 │
│ {                                                                                   │
│     string EmployeeId { get; set; }                                                │
│     void Work();                                                                   │
│     string GetEmployeeInfo();                                                      │
│ }                                                                                   │
│                                                                                     │
│ // Combined class - inherits identity, implements roles                            │
│ class StudentEmployee : Person, IStudent, IEmployee                                │
│ {                                                                                   │
│     public string StudentId { get; set; }                                          │
│     public string EmployeeId { get; set; }                                         │
│                                                                                     │
│     public void Study() => Console.WriteLine($"{Name} is studying");              │
│     public void Work() => Console.WriteLine($"{Name} is working");                │
│                                                                                     │
│     public string GetStudentInfo() => $"Student ID: {StudentId}";                 │
│     public string GetEmployeeInfo() => $"Employee ID: {EmployeeId}";              │
│                                                                                     │
│     public override string GetInfo()                                               │
│     {                                                                              │
│         return $"{base.GetInfo()}, {GetStudentInfo()}, {GetEmployeeInfo()}";      │
│     }                                                                              │
│ }                                                                                   │
│                                                                                     │
│ BENEFITS:                                                                           │
│ ✅ Single Person identity (no duplication)                                         │
│ ✅ Clear method resolution (no ambiguity)                                          │
│ ✅ Multiple role capabilities                                                       │
│ ✅ Extensible design (can add more interfaces)                                     │
└─────────────────────────────────────────────────────────────────────────────────────┘

╔══════════════════════════════════════════════════════════════════════════════════════╗
║                        INTERFACE DIAMOND (C# 8.0+ FEATURE)                          ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

With default interface methods, C# can have a limited diamond scenario:

INTERFACE DIAMOND STRUCTURE:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│                            ┌─────────────────┐                                     │
│                            │   IBaseInterface│                                     │
│                            │   + Method()    │  ← Default implementation           │
│                            └─────────┬───────┘                                     │
│                                      │                                             │
│                         ┌────────────┴────────────┐                               │
│                         │                         │                               │
│                         ▼                         ▼                               │
│                ┌─────────────────┐       ┌─────────────────┐                      │
│                │   ILeftInterface│       │  IRightInterface│                      │
│                │   + Method()    │       │   + Method()    │  ← Both override     │
│                └─────────┬───────┘       └─────────┬───────┘                      │
│                          │                         │                              │
│                          └────────────┬────────────┘                              │
│                                       │                                           │
│                                       ▼                                           │
│                              ┌─────────────────┐                                  │
│                              │  MyClass        │                                  │
│                              │ implements both │  ← Must resolve explicitly       │
│                              └─────────────────┘                                  │
└─────────────────────────────────────────────────────────────────────────────────────┘

STUDENT-EMPLOYEE INTERFACE DIAMOND EXAMPLE:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│ interface IPerson                                                                   │
│ {                                                                                   │
│     string GetRole() => "Person"; // Default implementation                        │
│ }                                                                                   │
│                                                                                     │
│ interface IStudent : IPerson                                                       │
│ {                                                                                   │
│     string IPerson.GetRole() => "Student"; // Override default                     │
│ }                                                                                   │
│                                                                                     │
│ interface IEmployee : IPerson                                                      │
│ {                                                                                   │
│     string IPerson.GetRole() => "Employee"; // Override default                    │
│ }                                                                                   │
│                                                                                     │
│ // ⚠️ Diamond problem with interfaces - must resolve explicitly                     │
│ class StudentEmployee : IStudent, IEmployee                                        │
│ {                                                                                   │
│     // ❌ Ambiguous without explicit resolution                                     │
│     // ✅ Must provide explicit implementation                                      │
│     string IPerson.GetRole() => "Student-Employee"; // Explicit resolution        │
│                                                                                     │
│     // Alternative: specific interface implementations                             │
│     string IStudent.GetRole() => "Student Role";                                  │
│     string IEmployee.GetRole() => "Employee Role";                                │
│ }                                                                                   │
│                                                                                     │
│ USAGE:                                                                              │
│ var se = new StudentEmployee();                                                    │
│ ((IPerson)se).GetRole();    // "Student-Employee"                                  │
│ ((IStudent)se).GetRole();   // "Student Role"                                      │
│ ((IEmployee)se).GetRole();  // "Employee Role"                                     │
└─────────────────────────────────────────────────────────────────────────────────────┘

RESOLUTION REQUIREMENT:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│ class MyClass : ILeftInterface, IRightInterface                                     │
│ {                                                                                   │
│     // ❌ AMBIGUOUS - Compiler error without explicit resolution                   │
│                                                                                     │
│     // ✅ EXPLICIT RESOLUTION - Required                                           │
│     void IBaseInterface.Method()                                                   │
│     {                                                                              │
│         // Explicit implementation resolves ambiguity                             │
│     }                                                                              │
│ }                                                                                   │
└─────────────────────────────────────────────────────────────────────────────────────┘

╔══════════════════════════════════════════════════════════════════════════════════════╗
║                              DESIGN IMPLICATIONS                                     ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

1. COMPLEXITY vs FLEXIBILITY TRADE-OFF:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│ Multiple Inheritance (Other Languages):                                             │
│ ✅ More flexible modeling                                                           │
│ ✅ Natural "IS-A" relationships                                                     │
│ ❌ Complex resolution rules                                                         │
│ ❌ Potential ambiguity issues                                                       │
│                                                                                     │
│ Single Inheritance + Interfaces (C#):                                              │
│ ✅ Clear, unambiguous hierarchy                                                     │
│ ✅ Multiple "CAN-DO" capabilities                                                   │
│ ❌ Sometimes less natural modeling                                                  │
│ ❌ Requires interface contracts                                                     │
│                                                                                     │
│ STUDENT-EMPLOYEE ANALYSIS:                                                          │
│ ❌ Multiple inheritance: StudentEmployee IS-A Student AND IS-A Employee (confusing)│
│ ✅ C# approach: StudentEmployee IS-A Person who CAN-DO student/employee work      │
└─────────────────────────────────────────────────────────────────────────────────────┘

2. COMPOSITION OVER INHERITANCE PRINCIPLE:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│ Instead of complex inheritance hierarchies, prefer:                                 │
│                                                                                     │
│ • Single inheritance for "IS-A" relationships                                      │
│ • Interface implementation for "CAN-DO" capabilities                               │
│ • Composition for "HAS-A" relationships                                            │
│                                                                                     │
│ EXAMPLE MODELING (STUDENT-EMPLOYEE):                                                │
│ ❌ StudentEmployee inherits from Student, Employee (diamond problem)               │
│ ✅ StudentEmployee IS-A Person, CAN-DO student work, CAN-DO employee work         │
│ ✅ StudentEmployee HAS-A StudentRecord, HAS-A EmployeeRecord (composition)        │
└─────────────────────────────────────────────────────────────────────────────────────┘

3. ARCHITECTURAL BENEFITS:
┌─────────────────────────────────────────────────────────────────────────────────────┐
│ • Predictable method resolution                                                     │
│ • Clear inheritance chains                                                          │
│ • Easier debugging and maintenance                                                  │
│ • Better performance (no complex resolution algorithms)                            │
│ • More testable code (clear contracts)                                             │
│ • Single responsibility principle (one base class, multiple capability interfaces) │
└─────────────────────────────────────────────────────────────────────────────────────┘

╔══════════════════════════════════════════════════════════════════════════════════════╗
║                                   SUMMARY                                            ║
╚══════════════════════════════════════════════════════════════════════════════════════╝

The Diamond Problem represents a fundamental challenge in object-oriented design where 
multiple inheritance paths create ambiguity. The Student-Employee example perfectly 
illustrates this:

REAL-WORLD IMPLICATIONS:
• Should a person who is both a student and employee have one identity or two?
• Which version of methods like GetInfo() should be called?
• How do we handle overlapping responsibilities and data?

C# SOLUTION PHILOSOPHY:
1. Prohibit multiple class inheritance - Prevents the problem entirely
2. Enable multiple interface implementation - Provides flexibility without ambiguity  
3. Require explicit resolution - When conflicts arise, developers must resolve them
4. Promote composition over inheritance - Encourages cleaner, more maintainable designs

STUDENT-EMPLOYEE IN C#:
✅ One Person identity (single inheritance)
✅ Multiple role capabilities (interface implementation)
✅ Clear method resolution (no ambiguity)
✅ Extensible design (can add more roles easily)

This design philosophy makes C# code more predictable, maintainable, and less prone to 
the subtle bugs that can arise from complex inheritance hierarchies.
*/