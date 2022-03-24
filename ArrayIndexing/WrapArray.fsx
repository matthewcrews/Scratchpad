type ImArr<[<Measure>] 'Index, 'Value>(values: array<'Value>) =
    // We want to make sure we have our own copy to protect against mutation
    let values = values |> Array.copy

    member internal _.Values= values

    member this.Item
        with get(index: int<'Index>) =
            values.[int index]


type Arr<[<Measure>] 'Index, 'Value>(values: array<'Value>) =
    // We want to make sure we have our own copy to protect against mutation
    let values = values |> Array.copy

    member this.Item
        with get(index: int<'Index>) =
            values.[int index]

        and set(index: int<'Index>) (value: 'Value) =
            values.[int index] <- value

    member internal _.Values = values


module Helpers =

    let inline inPlaceAdd<[<Measure>] 'Index, ^a, ^b when (^a or ^b) : (static member (+): ^a * ^b -> ^a)> 
        (a: Arr<'Index, ^a>) 
        (b: ImArr<'Index, ^b>)
        =
        let mutable i = 0;
        while i < a.Values.Length && i < b.Values.Length do
            a.Values.[i] <- a.Values.[i] + b.Values.[i]
            i <- i + 1