# F# for the Analyst and the Performance Engineer

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