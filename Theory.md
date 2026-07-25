Absolutely. For a lab report, the theory should be **brief, formal, and directly related to what the program demonstrates**, rather than turning every question into a 14-page philosophical investigation of why C# has 47 ways to do the same thing.

Below is report-ready theory for all 17 questions. You can place each section before its corresponding program.

# NET Centric Computing Lab Report: Theory

## 1. Constructors in C#

A **constructor** is a special member of a class that is automatically invoked when an object of that class is created. It has the same name as the class and does not have a return type. Constructors are primarily used to initialize the fields and properties of an object.

C# supports several types of constructors, including:

* **Default Constructor:** A constructor without parameters. If no constructor is explicitly defined, C# may provide a default parameterless constructor.
* **Parameterized Constructor:** Accepts one or more parameters and initializes an object with specific values.
* **Copy Constructor:** Creates a new object by copying values from an existing object.
* **Static Constructor:** Used to initialize static members of a class. It is executed automatically once before the class is first used.
* **Private Constructor:** A constructor declared with the `private` access modifier. It prevents external code from directly creating instances of the class and is commonly used in singleton or utility-style classes.

Constructors support **object initialization and encapsulation** by ensuring that objects can be created in a valid initial state.

---

## 2. Auto Property and Read-Only Property

A **property** in C# provides a controlled way to access private data members of a class. Properties use `get` and `set` accessors to retrieve and modify values.

An **auto-implemented property** is a property where the compiler automatically creates the required hidden backing field. It can be declared using:

```csharp
public string Name { get; set; }
```

A **read-only property** allows a value to be retrieved but not modified through the property. It can be declared using only a `get` accessor:

```csharp
public int Age { get; }
```

Read-only properties are useful when a value should be established during object initialization and should not be changed afterward. Properties improve **encapsulation** and reduce the need to expose fields directly.

---

## 3. Jagged Array

A **jagged array** is an array whose elements are themselves arrays. Unlike a multidimensional array, where every row has the same number of columns, each array inside a jagged array can have a different length.

For example:

```csharp
int[][] numbers = new int[3][];
```

Each inner array can then have a different size.

Jagged arrays are useful when data is naturally organized into rows or groups with different numbers of elements. They provide flexibility in storing irregular or non-rectangular data structures.

---

## 4. Indexer in C#

An **indexer** allows an object of a class to be accessed using array-like syntax. It enables a class to define how values are retrieved or assigned using an index.

An indexer is declared using the `this` keyword:

```csharp
public string this[int index]
{
    get { ... }
    set { ... }
}
```

### a) Integer Indexer

An integer indexer uses an `int` as its index. It is commonly used when objects need to behave like arrays or collections.

### b) Non-Integer Indexer

C# also allows indexers to use types other than `int`, such as `string`. For example, an object can be accessed using:

```csharp
obj["name"]
```

This is useful when data needs to be accessed using meaningful keys rather than numerical positions.

Indexers provide a convenient way to encapsulate collection-like behavior inside a class.

---

## 5. `base` Keyword in C#

The **`base` keyword** is used in a derived class to access members of its immediate base class. It is particularly useful when a derived class contains members with the same names as members in the base class.

### a) Accessing Base Class Fields

The `base` keyword can access a field defined in the parent class:

```csharp
base.fieldName
```

This is useful when the derived class has a field with the same name.

### b) Calling Base Class Methods

A base class method can be explicitly called using:

```csharp
base.MethodName();
```

This is commonly used when a derived class overrides a base class method but still needs to execute the original implementation.

### c) Calling Base Class Constructor

The base class constructor can be called from a derived class constructor using:

```csharp
Derived() : base()
{
}
```

This ensures that the base portion of the object is properly initialized before the derived class initialization takes place.

---

# 6. Method Overriding, Method Hiding and Dynamic Polymorphism

## a) Method Overriding and Method Hiding

**Method overriding** occurs when a derived class provides a new implementation for a method declared as `virtual` in the base class. The derived method uses the `override` keyword.

```csharp
class Derived : Base
{
    public override void Display()
    {
        // new implementation
    }
}
```

**Method hiding**, also called **shadowing**, occurs when a derived class defines a method having the same name as a base class method without overriding it. The `new` keyword can explicitly indicate hiding.

```csharp
public new void Display()
```

The main difference is that overriding supports runtime polymorphism, while method hiding does not behave polymorphically in the same way.

## b) Dynamic Polymorphism Using Method Overriding

**Dynamic polymorphism**, also called **runtime polymorphism**, occurs when the method that should be executed is determined at runtime rather than compile time.

In C#, this is achieved primarily through **method overriding**. A base-class reference can refer to a derived-class object, and the overridden method of the actual object is executed.

```csharp
Base obj = new Derived();
obj.Display();
```

This allows programs to work with general base-class references while providing specialized behavior through derived classes.

---

# 7. Abstract Class, Interface and Multiple Inheritance

## a) Abstract Class

An **abstract class** is a class that cannot be instantiated directly. It is designed to serve as a base class for other classes.

An abstract class may contain:

* Abstract methods
* Concrete methods
* Fields
* Properties
* Constructors

An abstract method does not contain an implementation in the abstract class and must be implemented by a derived class.

```csharp
abstract class Animal
{
    public abstract void Sound();
}
```

Abstract classes are useful for defining common functionality and enforcing a common structure among derived classes.

## b) Interface

An **interface** defines a contract that a class must implement. It specifies members that implementing classes are required to provide.

For example:

```csharp
interface IAnimal
{
    void Sound();
}
```

A class implements an interface using the `:` symbol.

Interfaces support **abstraction, loose coupling, and polymorphism**.

## c) Multiple Inheritance Using Interfaces

C# does not support multiple inheritance of classes, meaning a class cannot directly inherit from multiple classes.

However, C# supports multiple inheritance-like behavior through interfaces. A class can implement multiple interfaces:

```csharp
class Student : IStudent, IPerson
{
}
```

This allows a class to obtain contracts from multiple sources without the ambiguity associated with multiple class inheritance.

---

# 8. Structure, Enumeration and Partial Class

## a) Structure (`struct`)

A **structure** is a value type in C# that can contain fields, properties, methods, constructors, and other members.

Structures are generally used for small data structures where value-type semantics are appropriate.

Example:

```csharp
struct Student
{
    public int Id;
    public string Name;
}
```

Unlike classes, structures are value types and are typically stored directly rather than as references to objects.

## b) Enumeration (`enum`)

An **enumeration** is a value type used to define a set of named constants.

For example:

```csharp
enum Day
{
    Sunday,
    Monday,
    Tuesday
}
```

Enums improve code readability by allowing meaningful names to be used instead of numeric values.

## c) Partial Class

A **partial class** allows a single class to be divided into multiple source files using the `partial` keyword.

```csharp
partial class Student
{
}
```

The compiler combines all parts of the partial class into a single class during compilation.

Partial classes are useful for organizing large classes and are especially common in automatically generated code, where developers may need to add custom functionality without modifying generated files.

---

# 9. Delegates, Multicast Delegates, Func, Action, Anonymous Methods and Events

## a) Delegate

A **delegate** is a type-safe reference to a method. It can store a reference to a method and invoke that method later.

Delegates are commonly used for:

* Callbacks
* Event handling
* Passing methods as arguments
* Implementing flexible behavior

The signature of the delegate must be compatible with the method it references.

## b) Multicast Delegate

A **multicast delegate** can hold references to multiple methods. When the delegate is invoked, all referenced methods are called sequentially.

Methods can be added using the `+` or `+=` operators and removed using `-` or `-=`.

Multicast delegates are particularly useful when one action needs to notify multiple methods.

## c) `Func` Delegate

`Func` is a built-in generic delegate used for methods that **return a value**.

For example:

```csharp
Func<int, int> square;
```

The last generic type parameter represents the return type, while the preceding parameters represent input parameters.

## d) `Action` Delegate

`Action` is a built-in generic delegate used for methods that **do not return a value**.

For example:

```csharp
Action<string> display;
```

It can accept parameters but always has a `void` return type.

## e) Anonymous Method

An **anonymous method** is a method without an explicit method name. It can be created using the `delegate` keyword and assigned to a delegate.

```csharp
Action message = delegate()
{
    Console.WriteLine("Hello");
};
```

Anonymous methods are useful for short operations that do not require a separately named method.

## f) Event

An **event** provides a mechanism through which an object can notify other objects when something occurs.

Events are commonly implemented using delegates. The class that declares the event raises it, while other classes can subscribe to it.

Events are widely used in GUI programming, user interactions, notifications, and other event-driven applications.

---

# 10. Collections in C#

A **collection** is an object used to store and manage groups of objects. C# provides both non-generic and generic collection types.

## a) Non-Generic Collection

Non-generic collections can store objects of different types and are generally found in the `System.Collections` namespace.

Examples include:

* `ArrayList`
* `Hashtable`
* `Stack`
* `Queue`

Because they store values as `object`, they may require **boxing/unboxing** for value types and do not provide compile-time type safety.

## b) Generic Collection

Generic collections are available mainly in `System.Collections.Generic`.

Examples include:

* `List<T>`
* `Dictionary<TKey, TValue>`
* `Queue<T>`
* `Stack<T>`

They provide **type safety, better performance, and reusability** because the type of elements is specified at compile time.

For modern C# programming, generic collections are generally preferred over non-generic collections.

---

# 11. Generic Class with Generic Field and Method

**Generics** allow classes, methods, and other types to operate on different data types without duplicating code.

A **generic class** uses a type parameter:

```csharp
class Box<T>
{
    T value;
}
```

Here, `T` represents a type that will be specified when an object is created.

A generic class can contain:

* Generic fields
* Generic properties
* Generic methods
* Generic constructors

For example:

```csharp
Box<int>
Box<string>
```

Generics improve **code reusability, type safety, and performance** by avoiding unnecessary casting and boxing.

---

# 12. Keyboard Input and File Handling

C# provides classes in the `System.IO` namespace for performing file input and output operations.

Keyboard input can be obtained using:

```csharp
Console.ReadLine();
```

The `File` and `StreamWriter` classes can then be used to write data to a file.

File handling allows programs to store data permanently instead of losing it when the program terminates.

Common file operations include:

* Creating files
* Writing data
* Reading data
* Appending data
* Deleting files

The `using` statement is commonly used with file-related objects to ensure that resources are properly released.

---

# 13. LINQ

**LINQ (Language Integrated Query)** is a feature of .NET that allows developers to query and manipulate data using a consistent syntax directly within C#.

LINQ can work with different data sources, including:

* Arrays
* Collections
* Lists
* Databases
* XML
* Other objects implementing suitable interfaces

A LINQ query can perform operations such as:

* Filtering
* Sorting
* Grouping
* Projection
* Aggregation

For example, the `Where()` method can be used to filter elements based on a condition.

LINQ improves readability and reduces the amount of code required for common data-processing operations.

---

# 14. Lambda Expressions and LINQ with Lambda Expressions

## a) Lambda Expressions

A **lambda expression** is a concise way to represent an anonymous function. It uses the `=>` operator.

General syntax:

```csharp
(parameters) => expression
```

For example:

```csharp
x => x * x
```

Lambda expressions are frequently used with delegates, `Func`, `Action`, and LINQ.

They provide a concise way to express small functions without explicitly declaring a separate method.

## b) LINQ with Lambda Expressions

LINQ methods can use lambda expressions to specify conditions and operations.

For example:

```csharp
numbers.Where(x => x > 10);
```

Here, `x => x > 10` defines the condition used by `Where()` to filter the collection.

This combination makes data querying concise and readable and is commonly called **method syntax** or **LINQ method syntax**.

---

# 15. Exception Handling

An **exception** is an abnormal condition that occurs during program execution and disrupts the normal flow of a program.

C# provides exception-handling mechanisms using `try`, `catch`, `finally`, and `throw`.

## a) `try`, `catch` and `finally`

The `try` block contains code that may generate an exception.

The `catch` block handles the exception.

The `finally` block contains code that executes regardless of whether an exception occurs. It is commonly used for cleanup operations such as releasing resources.

Basic structure:

```csharp
try
{
    // risky code
}
catch
{
    // handle exception
}
finally
{
    // cleanup
}
```

## b) `throw` Keyword

The `throw` keyword is used to explicitly generate an exception or rethrow an existing exception.

It allows developers to signal that an invalid or unexpected condition has occurred.

```csharp
throw new Exception("Invalid operation");
```

## c) Custom Exception

A **custom exception** is a user-defined exception class created by inheriting from `Exception`.

```csharp
class MyException : Exception
{
}
```

Custom exceptions allow applications to represent application-specific errors in a clear and meaningful way.

Exception handling improves **program reliability, error management, and graceful recovery from runtime errors**.

---

# 16. Attributes in C#

An **attribute** is a declarative mechanism in C# used to add metadata to program elements such as classes, methods, properties, and assemblies.

Attributes are enclosed in square brackets:

```csharp
[AttributeName]
```

## a) Built-in Attributes

C# and .NET provide several built-in attributes.

Examples include:

* `Obsolete`
* `Serializable`
* `Conditional`
* `CLSCompliant`

For example, the `Obsolete` attribute can indicate that a method should no longer be used.

Attributes can be examined at runtime using **reflection**.

## b) Custom Attributes

Developers can create their own attributes by defining a class derived from `System.Attribute`.

For example:

```csharp
class AuthorAttribute : Attribute
{
}
```

Custom attributes can be used to attach application-specific metadata to classes, methods, properties, or other program elements.

They are commonly used in frameworks, serialization, testing, validation, and reflection-based systems.

---

# 17. Asynchronous Programming Using `async` and `await`

**Asynchronous programming** allows a program to perform operations without unnecessarily blocking the current thread while waiting for an operation to complete.

C# provides the `async` and `await` keywords to simplify asynchronous programming.

The `async` keyword indicates that a method contains asynchronous operations and generally returns a `Task` or `Task<T>`.

The `await` keyword is used to asynchronously wait for a `Task` to complete without blocking the thread.

Example:

```csharp
async Task ProcessData()
{
    await SomeOperationAsync();
}
```

Asynchronous programming is particularly useful for operations that involve waiting, such as:

* File operations
* Network requests
* Database operations
* Web API calls
* Other I/O operations

It can improve application responsiveness and resource utilization because the thread does not need to remain blocked while waiting for an I/O operation to finish.

---

# Quick Summary of the Lab Topics

| Lab | Main Concept                      |
| --- | --------------------------------- |
| 1   | Constructors                      |
| 2   | Properties                        |
| 3   | Jagged Arrays                     |
| 4   | Indexers                          |
| 5   | `base` keyword                    |
| 6   | Overriding, Hiding & Polymorphism |
| 7   | Abstract Classes & Interfaces     |
| 8   | Struct, Enum & Partial Class      |
| 9   | Delegates & Events                |
| 10  | Collections                       |
| 11  | Generics                          |
| 12  | File Handling                     |
| 13  | LINQ                              |
| 14  | Lambda Expressions                |
| 15  | Exception Handling                |
| 16  | Attributes                        |
| 17  | Asynchronous Programming          |

This is at about the **right depth for a BSc CSIT lab report**: enough theory to demonstrate that you understand what the code is doing, without padding every section with historical trivia about Microsoft and the suffering of software developers.
