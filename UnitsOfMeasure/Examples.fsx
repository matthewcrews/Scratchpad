// 1. Defining a Unit of Measure
// Simple Units
[<Measure>] type cm
[<Measure>] type sec

let x1 = 1.0<cm>
let x2 = 3.2<sec>

// let y = x1 + x2
let y = x1 / x2

// Compound Units
[<Measure>] type speed = cm / sec
[<Measure>] type acceleration = speed / sec
[<Measure>] type carrot = cm * sec

// 2. Unit of Measure behavior
// Addition and Subtraction
// Multiplication and Division

let s1 = x1 / x2
let s2 = 2.1<speed>
let s3 = s1 + s2
let s4 = s1 - s2


// 3. Defining "Constructors"

module Domain =

    module private Units =

        [<Measure>] type PackWeight

    // module Conversions =
        //

    module PackWeight =

        let create (x: float) =
            if x <= 0.0 then
                invalidArg (nameof x) "Cannot have Non-positive PackWeight"

            x * 1.0<Units.PackWeight>
    
open Domain

let m1 = PackWeight.create 10.0


// 4. Using them in Custom Types

[<Measure>] type Cow
[<Measure>] type Chicken


type Animal<[<Measure>] 'Measure> =
    {
        Size : float
    }
    static member (+) (a: Animal<'Measure>, b: Animal<'Measure>) : Animal<'Measure> =
        { Size = a.Size + b.Size }

    static member ( * ) (a: Animal<'LMeasure>, b: Animal<'RMeasure>) =
        let result : Animal<'LMeasure * 'RMeasure> = { Size = a.Size * b.Size}
        result

    static member ( / ) (a: Animal<'LMeasure>, b: Animal<'RMeasure>) =
        let result : Animal<'LMeasure / 'RMeasure> = { Size = a.Size * b.Size}
        result
    
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Animal =

    let create (x: float<'Measure>) : Animal<'Measure> =
        { Size = float x }


let c1 = Animal.create 1.0<Chicken>
let c2 = Animal.create 2.0<Chicken>
let c3 = c1 + c2

// 5. Defining the product and division of Units
let c4 = Animal.create 1.0<Cow>
let c5 = c1 * c4

let c6 = c1 / c4

// Hint
let q1 = 1.0<Cow>
let q2 = 2.1<Chicken>
let q3 = q1 + q2 * 1.0<_>
