[<Measure>] type Chicken
[<Measure>] type Cow

type Arr<[<Measure>] 'Measure>(v: array<_>) =
    let values = v

    member _.Item
        with inline get (i: int<'Measure>) =
            v.[int i]

    member _.GetSlice (start, finish) =
        let start = defaultArg start 0<_> |> int
        let finish = defaultArg finish (1<_> * (v.Length - 1)) |> int
        values[start .. finish]
        |> Arr<'Measure>


let chickenSizeArray =
    [|1.0 .. 10.0|]
    |> Arr<Chicken>

let chickenSlice = chickenSizeArray[0<Chicken> .. 2<Chicken>]


let cowSizeArray =
    [|1.0 .. 10.0|]
    |> Arr<Cow>


// let chickenIndex = 1<Chicken>

// // This works
// let chickenSize = chickenSizeArray[chickenIndex]

// // This won't compile
// let cowSize = cowSizeArray[chickenIndex]




