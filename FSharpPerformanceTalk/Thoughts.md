# F# for the Analyst and the Performance Engineer

## F#: A Language for Innovation

## Formula for Innovation

Rate of Innovation = Speed of Development * Speed of Execution

> F# is an excellent language for quickly creating working code. F# can also be easily optimized to have near C Levels of performance

## Who am I?

Software Engineer for Simulation Dynamics building high-performance simulations for the optimization of Supply Chains, Warehouses, and Manufacturing Facilities

## Aidos

A simulation engine for Discrete-Rate Simulation

## Speed of Development

How quickly can we go from an idea to working code.

### Types are Easy to Define

F# features a type system which makes it easy to express a problem using simple types. F# has an Algebraic Type system which enables powerful type inference which reduces the amount of code that needs to be written while still being statically typed (the importance of static typing will come up later.).

Define a Record
```fsharp
type Chicken =
    {
        Name: string
        Size: float
        Age: int
    }
```

```asm

_.add(Int32, Int32)
    L0000: lea eax, [ecx+edx]
    L0003: ret

```

Define a Discriminated Union
```fsharp
type Animal =
    | Chicken of Chicken
    | Goose of Goose
    | Turkey of Turkey
```

### Why is the F# Type System Special?

Many bugs in software come from imprecise models of the problem. Imprecise models make it easier to create non-sensical states which can be difficult to debug.

It is common to have a pure domain of types at the core of a project which ensure that assumptions have been validated before any business logic is executed.

```fsharp
type Chicken =
    {
        Name: string
        Size: float
        Age: int
    }

module Chicken =

    let create name size age =
        if size <= 0.0 then
            invalidArg (nameof size) "Cannot have negative size"

        if age <= 0 then
            invalidArg (nameof age) "Cannot have Chicken with negative age"

        if 

```

### The Absence of Null

While F# has nulls, the are uncommon in pure F# code. They exist for compatibility with the broader .NET ecosystem. F# provides many facilities for protecting users from creating null reference exceptions


### Computation Expressions

Analysts want to work as close to their domain as possible. Ideally the API they are working with reflects the reality of the problem they are trying to solve. Computation Expressions (CEs) make it possible to create an API that is in the language of the problem itself. This speeds up the development of analysts and makes it easier to catch problems with business logic.

## Speed of Execution

### Pillars of Performance

All of software performance can be summed up with the following:

Do less, with less, in a predictable way.

### Why is that true?

Modern CPUs are essentially data transform factories. They have fast execution units at their center but those execution units need to have work prepared for them by a front end and data fed from the cache. The 

### But the GC is Slow

Allocations in .NET are incredibly fast (<10ns). It's the collection of garbage that is actually slow. While the .NET API encourages the creating of many objects on the heap, it is not necessary. Let's discuss some common approaches to reducing or eleminating GC.

#### Use a Struct

If a piece of data only needs to exist for a short time, don't allocate it on the heap. You can create it on the stack and pass it by reference if you want.

#### Use the ArrayPool

.NET has a shared ArrayPool which allows you to grab an array, use it, and then return it. The array will then remain in the pool until you need it again. This can significantly cut down on the allocation of arrays which are only used for small pieces of logic.

#### Use StackAlloc Memory

If you only need a small buffer, consider using StackAlloc. StackAlloc buffers should be small. They are incredibly fast though since their memory, by definition will be hot and in the L1 cache.

#### Use ObjectPools

If you have a collection type that you using frequently for a short period of time, consider creating a an ObjectPool. This will keep the Garbage Collector from needing to collect and clean the memory.

#### Allocate Arrays of Structs

Instead of creating many, small objects on the heap, instead create a large array for the given struct type. This marries with the ECS style.

### Entity Component System Architecture

Many modern, high-performance games use an Entity Component System (ECS) style of architecture. The ECS style encourages us to layout the data of a program in a more database-like way. We then use indexes to represent the different entities of our program. The index is then used to look up data about the entities in our program. This reduces the overall size of the working set of data which allows our programs to be faster since more of it exists in cache.

It also reduces the number of objects that the GC needs to keep track of since almost everything in a program has become a set of arrays.

#### Units of Measure

If we represented all of the indexes in our program as just an `int`, it would be easy to loose track of what an index is meant to refer to. Thankfully, F# has a unique feature which is Units of Measure. Units of Measure allow us to annotate numeric primitives with the units they are meant to represent. Originally intended for validating algebra, it also has great applications in the domain of ECS.

We will leverage this capability to create wrappers for Arrays which enforce correcting types when indexing into data.

#### Bar Collection

```fsharp
[<Struct>]
type Bar<[<Measure>] 'Measure, 'T> (values: 'T[]) =

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member _._values = values

    member inline b.Length = LanguagePrimitives.Int32WithMeasure<'Measure> b._values.Length

    member b.Item
        with inline get(i: int<'Measure>) =
            b._values[int i]
```

#### Row Collection

```fsharp
[<Struct>]
type Row<[<Measure>] 'Measure, 'T>(values: 'T[]) =

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member _._values : 'T[] = values

    member r.Item
        with inline get (i: int<'Measure>) =
            r._values[int i]

        and inline set (index: int<'Measure>) value =
            r._values[int index] <- value

    member inline r.Length = LanguagePrimitives.Int32WithMeasure<'Measure> r._values.Length
```

#### Usage of Bar and Row

```fsharp
let chickenAges = Bar<Chicken, _> [|0; 1; 5; 0; 2|]
let chickenIndex = 1<Chicken>
// The Compiler enforces the correct units on the indexing Int
let chickenAge = chickenAges[chickenIndex]

// Non-Chicken Index
let cowIndex = 1<Cow>
// This a compiler error since the units of the indexing value
// does not match the expectation of the collection
let cowAge = chickenAges[cowIndex]
```

#### BitSet Collection

```fsharp
type BitSet<[<Measure>] 'Measure> internal (capacity: int, buckets: uint64[]) =

    new (itemCount: int) =
        let bucketsRequired = (itemCount + 63) >>> 6
        let buckets : uint64[] = Array.zeroCreate bucketsRequired
        BitSet<_> (itemCount, buckets)

    /// WARNING: Not intended for consumption. This needs to be public to support inlining.
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member _._itemCount = capacity
    /// WARNING: Not intended for consumption. This needs to be public to support inlining.
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member _._buckets = buckets
```

#### BitSet Methods

```fsharp
member b.Item
    with inline get (itemKey: int<'Measure>) =
        let bucketId, mask = Helpers.computeBucketAndMask itemKey b._itemCount
        let buckets = b._buckets
        let bucket = buckets[bucketId]
        (bucket &&& mask) <> 0UL

member b.Contains (itemKey: int<'Measure>) =
    let bucketId, mask = Helpers.computeBucketAndMask itemKey b._itemCount
    let buckets = b._buckets
    let bucket = buckets[bucketId]
    (bucket &&& mask) <> 0UL

member inline b.Add (itemKey: int<'Measure>) =
    let bucketId, mask = Helpers.computeBucketAndMask itemKey b._itemCount
    let buckets = b._buckets
    let bucket = buckets[bucketId]
    buckets[bucketId] <- bucket ||| mask

member inline b.Remove (itemKey: int<'Measure>) =
    let bucketId, mask = Helpers.computeBucketAndMask itemKey b._itemCount
    let buckets = b._buckets
    let bucket = buckets[bucketId]
    buckets[bucketId] <- bucket &&& ~~~mask
```

#### BitSet Usage

```fsharp
let constraintUp = BitSet<Constraint, _> 100

let constraintIndex = 10<Constraint>

let constraintIsUp = constraintUp[constraintIndex]
```


#### Struct of Arrays

```fsharp
type Flock =
    {
        Name: Bar<Chicken, string>
        Size: Row<Chicken, float>
        Age: Bar<Chicken, int>
        IsActive: BitSet<Chicken>
    }
```


## The Innovation

F# unique set of features enabled us to rapidly create a Discrete-Rate Simulation engine that we could then quickly iterate on. The concise syntax made it easy to refactor and the robust type system means that clearly express out computation. This gave us the most valuable possible resource: time.

We were able to use the time F# gave us back to develop a new algorithm for solving the max-flow problem at the heart of Discrete-Rate Simulation. This new algorithm gives Aidos a 600x performance advantage over industry standard simulation engines.

## Conclusion

- F#'s concise syntax and expressive type system makes it easy to write correct code quickly
- F# and .NET allows you to evolve the performance of your program to meet your performance requirements
- Together this makes F# an excellent language for developing innnovative software products that vastly outperform the rest of the industry


-- Old Version --

## The Analyst and the Engineer

What is the difference between an Analyst and an Engineer? An Analyst typically has a question that The Business wants answered, the sooner the better. Therefore the Analyst isn't interested in having to build up a set of tools or primitives to start working on the question. Rather, they want to have all the tools included so they can focus on answering the question. They don't want to implement data structures. They don't want to have to think about data layout or how the hardware works. They want as much of the implementation obfuscated so they can focus on answering the business question.

An Engineer is typically operating at a lower level. While they may have a question from the Business, most of the time they need to enable a new use case or make it possible to start asking new kinds of questions. They are typically not thinking about A Question but rather how to answer Questions in general. They are operating at a level or two below the analyst. They care deeply about how the hardware works and the internals of the language, runtime, and the machine. To do their job well they need to understand how the whole stack works and have the ability to control it. They have a deeper context of the problem of computation. Ideally it spans from the Business Questions down to the hardware level. For them to work efficiently, they need control. When the language or runtime obfuscates behavior, they become frustrated.

Historically the demands of the Analyst and the Engineer have been met by using different languages. Analysts often work in higher-level languages like Excel, VBA, R, Python, SQL, or some other domain specific tool. Engineers work in a lower-level language like C, C++, C#, or Java. The Analyst calls the Engineer's code by using an API that calls from one language to another. While this separation has served the industry well, it also leads to some challenges. Interop between different runtimes can be complex. It also makes it more difficult for the Analyst to evolve into an Engineer or an Engineer to directly help an Analyst.

Another approach would be to create a language that is a sythesis of these requirements. A concise and simple language that makes it easy for Analysts to import and manipulate data while also providing lower level controls for Engineers to create more optimized code. F# has emerged as a language that does an excellent job at serving both of these use cases well. The concise and simple syntax comes from the OCaml heritage while being built on the robust and fast .NET Runtime that has decades of battletesting and performance engineering.

## What makes a great language for Analysis?

### Attitude of Analyst

I have a question I want to answer and I just want to get my job done.

### Notes

- Easy to import data
- Easy to analyze data
- Easy to transform data
- Concise but clear syntax
- Rapid iteration
- High Quality libraries for the work you need to do (R, Python)
- Performant enough to answer questions in a meaningful amount of time
- Reliably able to recreate experiments. Recreate on different machines.
- 

## What makes a great language for Performance?

### Attitude of Engineer

I have something I need to build and I want the system to get out of my way.

### Design Principles of F#

F# encourages you to decompose your problem into Data and Functions. Data is the information that you need to process. Functions are the logic of transforming Data from one form to another. This leads to simple and composable solutions. Complex types are composed of many, simple types. Complex functions are composed of many smaller functions. There is another language which encourages this design philosphy: C. C is known as the king of speed. Where C and F# differ is in how they are implemented. C is compiled to machine code and assumes that the developer knows exactly what they are doing and therefore does not need to be warned about dangerous behavior. F#, on the other hand, is built on top of the .NET Runtime and is strict in the use of types and how they are used. C will impleictly case types while F# forces casting and ensures that types align.

What I have found is that F# makes an excellent Procedural/Imperitive Programming Language. When people talk about F# being slow, they are often comparing a functional, immutable approach to the problem to a procedural, impritive approach. F# is equally comfortable working in both of these styles. I would make the case it is actually easier to write procedural code in F# than it is in C. Where you will experience friction in F# is in that it will force you to be explicit about mutation and side effects. F# defaults toward safety. C defaults toward permissiveness. When we consider when C was designed and the intended use cases, this makes sense. F# was designed after decades of additional programming language research and when computers had orders of magnitude of greater performance.

### The Memory Management and the Garbage Collector

One feature of F#, or any .NET based language, that makes "hardcore" Engineers dimiss it out of hand is the presence of the Garbage Collector. It does not take long when searching the internet to find stories of engineers bemoaning the presence of the GC in .NET and citing it as the reason they had to leave .NET to achieve the performance targets they needed. While I don't want to disparage those engineers, I do want to point out that the GC in .NET does not need to be a performance bottleneck.

My trite response to these opinions is that the GC won't cause problems if you don't create garbage. I need to unpack that though. There's a good reason so many performance engineers have run afoul of the GC. .NET was conceived in a time when OO ruled business software mindshare. OO was believed to be the solution to all of our problems. If you had a problem with your software, it was because you didn't OO hard enough. You just needed to improve your OO skills.

Since that time, the OO hype has cooled and you can find numerous talks at C++ conferences where developers talk about why NOT to use the OO features of C++. Any talk on game performance will inevitably include a slide citing all of the features of C++ that the team does not use due to the performance penalties. OO features and Exceptions make up almost the entirety of these lists.

.NET is still a OO based tool though. It has classes and methods at its heart. To make Objects easy to use in .NET, a GC was included to remove the need for developers to think about allocating and deallocating memory. At the time, this improved the perceived productivity of developers since Memory Management is considered to be a hard problem. The GC in .NET is a marvel of engineering. It is incredibly fast for what it does and will save you from many errors. All of the defaults of .NET and .NET based languages impel you toward creating objects on the heap and letting the GC manage it. Almost every tutorial I have come across makes no mention of the cost of creating many objects on the Heap. All of these defaults will move you in the direciton of slow, heapp allocation heavy code. Just because it is the default doesn't mean you have to use it though.

One of the reasons that C programs often end up being faster than .NET languages is because C forces you to deal with memory layout from the beginning. It has almost no defaults when it comes to heap allocation other than malloc and its derivatives. C devs quickly learn that malloc is slow because it it ends up calling into the operating system layer. In a way, C does have automatic memory management, it's the Operating System. Because C forces a developer to think about memory there is often more design time spent on how memory is accessed, when it is allocated, and when it is freed. .NET inverted this problem by making it all automatic. That doesn't mean a .NET Developer can ignore Memory Management though. They will inevitably end up worrying about memory management as the performance requirements of their program become more strict.

The .NET Platform has made significant strides to expose more controls to developers for the management of memory. Techniques that were previosly only available to C or C++ devs have become available for .NET Developers. Stack Allocation through the use of Span<'T> can alleviate significant amounts of allocation in tight loops. Object Pools have long been used to reduce the amount of allocation and freeing that need to occur while a program is running. It is also possible to create an array of Structs and pass references to individual elements to further reduce the overhead of stack frames.

In fact, most techniques that C Developers use can be translated to F#. It just may not feel as natural for an F# developer because they are not used to being explicit about mutation and references. The F# syntax for this is still clean and simple though. Mutation and passing structs by reference is just not the default for F# where it is for C.

### Notes

- Control and visibility
- Sympathy with the hardware
- Being able to reason about how your code will behave
- Control over how memory is accessed and moved around
- Tight control of how memory is layed out
- Control over the lifetime of memory
- Access to machine specific instructions like SIMD
- Bit manipulation of primitives like Byte, Int16, Int32, etc.
- Control over caching behavior to take advantage of the L1, L2, and L3 caches
- Minimizing the amount of indirection to avoid pointer chasing
- Tools for measuring how your code interacts with hardware
- Quickly test benchmarks for rapid performance iteration
- Being able to take advantage of Procedural Programming
- Minimizing the number of stack frames you need to generate. Minimizing the size of Stack Frames.
- Primitives for being able to move work between threads.