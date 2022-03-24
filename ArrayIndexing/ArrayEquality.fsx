let inline ( .+ ) (a: array<_>) (b: array<_>) =
    if a.Length <> b.Length then
        invalidArg (nameof b) "Cannot add arrays of different length"

    let result = Array.zeroCreate a.Length

    for idx = 0 to result.Length - 1 do
        result.[idx] <- a.[idx] + b.[idx]

    result

let x1 = [|1.0 .. 10.0|]
let x2 = [|1.0 .. 10.0|]
x1 = x2

x2.[1] <- 12.0
x1 .+ x2

[<Struct>]
type Chicken = {
    Values : array<float>
}

let c1 = {
    Values = [|1.0 .. 10.0|]
}

let c2 = {
    Values = [|1.0 .. 10.0|]
}
c1 = c2

c1.GetHashCode ()
c2.GetHashCode ()

c1.Values.[0] <- 2.0
c1.GetHashCode ()


type Arr<[<Measure>] 'Index, 'Value>(values: array<'Value>) =

    member this.Item
        with get(index: int<'Index>) =
            values.[int index]

        and set(index: int<'Index>) (value: 'Value) =
            values.[int index] <- value


// [<Measure>]
// type ValveIdx

// let v1 = 
//     [|1.0 .. 10.0|]
//     |> Arr<ValveIdx, _>

// v1.[1<ValveIdx>]

// for i = 0<ValveId> to 9<ValveId> do
//     printfn "%A" v1.[i]