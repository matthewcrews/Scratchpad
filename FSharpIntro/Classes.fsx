// type AltChicken =
//     {
//         Name: string
//         Size: float
//     }

// type Chicken (name: string, size: float) =

//     do
//         if System.String.IsNullOrEmpty name then
//             invalidArg (nameof name) "Chickens must have names"

//     let grow (newSize: float) =
//         Chicken (name, newSize)

//     new () =
//         Chicken ("DefaultChicken", 10.0)

//     new (name: string) =
//         Chicken (name, 1.0)

//     member _.Name = name
//     member _.Size = size
//     member _.Grow (newSize: float) = grow newSize

// let c1 = Chicken ("Clucky", 10.0)
// let c12 = c1.Grow 20.0
// // let c2 = Chicken ("", 10.0)
// let c3 = Chicken ()
// let c4 = Chicken ("BockBock")


open System

type Chicken =
    {
        Name: string
    }

type Flock (chickens: seq<Chicken>) =
    let _chickens = Array.ofSeq chickens

    member _.AsSpan () =
        // Method example
        _chickens.AsSpan()
    member _.DumbMethod () =
        let m = _chickens.AsMemory()
        // Why isn't this a method?
        m.Span

// c1.