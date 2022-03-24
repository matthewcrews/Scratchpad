// 1. What do you personally use them for?

// - Checking algebra

[<Measure>] type Inch
[<Measure>] type cm

let x = 1.0<Inch>
let y = 1.0<cm>

let z = x + y // Compiler error

// Tracking what an index corresponds to
open System

[<Struct>]
type Row<[<Measure>] 'Measure, 'T>(values: array<'T>) =

    new (length: int<'Measure>, value: 'T) =
        Row (Array.create (int length) value)

    member row.Values = values

    member row.Item
        with get (i: int<'Measure>) =
            row.Values[int i]

        and set (index: int<'Measure>) value =
            row.Values[int index] <- value

    member row.Length =
        LanguagePrimitives.Int32WithMeasure<'Measure> row.Values.Length

[<Measure>] type BufferId
[<Measure>] type Count

// Example of me tracking how many times I've observed a BufferId in a hot loop
// using an array
let myRow = Row (10<BufferId>, 0<Count>)

let bufferId0 = 0<BufferId>
let buffer0Count = myRow[bufferId0]

// Won't work
let buffer0CountFail = myRow[0] // Compiler error because 0 does not have UoM


// More extensive example
// https://flipslibrary.com/#/units-of-measure/

// Can also annotate other primitives like string
// Blog post
// https://www.compositional-it.com/news-blog/more-powerful-units-of-measure-with-fsharp-umx/

// Library that extends to other types
// https://github.com/fsprojects/FSharp.UMX


// Simple function requiring int with cm UoM
let myFunction (x: int<cm>) =
    x + 1<cm>

myFunction 1 // Error because of the lack of units

// No performance penalty though because it's just a compiler check. By the time it's IL, it's gone.
// https://sharplab.io/#v2:EYLgxg9gTgpgtADwGwBYA0AbEAzAzmgFxAENcBbAHwFgAoAbQB4BZGUgV1gD4BdAAgICeABxi8wZWrQwwCvYgBN5AeQB2ohLwC8tXrt4aA1LwCMAOgAMOvVJlzFqmAGEy+rVd2GTFhuM5A==

// Which means you can't use reflection to get them which a surprising number of people ask about

// 2. How have you seen them used by others?
// Answer: Yes, in smaller domains. I haven't seen them in a large scale system but I haven't
// been into many large codebases

// 3. What do you personally like about them?
// Answer: The guarantees they provide without a performance penalty. Domains that demand correctness
// but don't want to sacrifice speed. It's easy to write code whith hidden bugs


// 4. What do you personally dislike about them?
// Answer: F# applies them too strictly. This may be a feature but it makes working with them painful sometimes.
// I wish the compiler UoM agnostic methods/functions with UoM values or that there was more built-in overloads

let a = 1.0<cm>
let b = 2.0<cm>
let r = System.Math.Min (a, b) // Compiler error because Math.Min expects float, not float<cm>

// So you end up having to write "wrappers" which are annoying. The performance is the same, if you know
// what you're doing, but it's annoying

type Math () =

    static member Min (a: float<'Measure>, b: float<'Measure>) =
        // This all gets erased by the compiler but it's still annoying
        System.Math.Min (float a, float b)
        |> LanguagePrimitives.FloatWithMeasure<'Measure>

// Emitted x86-64 assembly
// https://sharplab.io/#v2:EYLgxg9gTgpgtADwGwBYA0AbEAzAzmgFxAENcBbAHwIE8AHGAAgFliCALBgCgEoGBeALAAoYQzENcBVgEswDMjDLAYUZtIB2XYiAbYMEVgB4A5ExikArrAB8aBqF36jp87isxrvQUPG+GAen8GABU2aVwGYgwMBgBzGAIIlVIYABN7agZ2RkgyWmkMFXsLAgZpAmMIyQKY4nV1CGoNWNE/MQBlaklFADoWdj6NLj0DUuI7EdZ7bla2imsGABk62ItieIAFKGkycukANxhcHoAxJwIAdXK2M0tYE1u3G2FhQtKAd2uIEsf3LgQdJMCHZqIDzl5ZgxOt0yH1WGxBppOAgQTMfOJXgkGJ92L9YP8waMHq53LYGKDHESXHcPBD0WJ+gimENkaigA
// .NET emits IEEE-754 compliant float operations which is the reason for the bloat


// 5. What are the "gotchas" that beginners trip over with them?
// The built-in libraries not having overloads for them so you end up having to write your own wrappers.
// Wanting to do reflection on them.
