[<Measure>] type Chicken

type Arr<[<Measure>] 'Measure, ..>(v: array<'T>) =

    member _.Item
        with get (i: int<'Measure>) =
            v.[int i]

    member _.Length = FSharp.Core.LanguagePrimitives


let v =
    [|1.0 .. 10.0|]
    |> Arr


let test (a: Arr<Chicken, >) =
    let mutable i = 0<Chicken>
    let len = 
