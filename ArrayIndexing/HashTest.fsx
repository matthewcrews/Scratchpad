let inline HashCombine nr x y = (x <<< 1) + y + 631 * nr

// A derivative of the F# code for array<int32> and array<int64>
// https://github.com/dotnet/fsharp/blob/main/src/fsharp/FSharp.Core/prim-types.fs#L1277
let floatHash (x: array<float>) =
    let len = x.Length
    let mutable idx = len - 1 
    let mutable acc = 0   
    while (idx >= 0) do 
        acc <- HashCombine idx acc (int32 x.[idx]);
        idx <- idx - 1
    acc

type Arr<[<Measure>] 'Measure, 'Value when 'Value : equality>(values: array<'Value>) =

    member _.Values : array<'Value> = values
    member _.Length = values.Length

    member this.Item
        with get(index: int<'Measure>) =
            values.[int index]

        and set(index: int<'Measure>) (value: 'Value) =
            values.[int index] <- value


    member this.Copy () =
        Arr<'Measure, _> (Array.copy values)

    override this.Equals b =
        match b with
        | :? Arr<_, 'Value> as other -> other.Values = this.Values
        | _ -> false

    override this.GetHashCode () =
        hash values

[<Measure>] type TankIdx
[<Measure>] type ValveIdx

[<CustomEquality; NoComparison>]
type Settings =
    {
        Tanks : Arr<TankIdx, float>
        Valves : Arr<ValveIdx, float>
    }
    override this.GetHashCode () =
        hash (struct (this.Tanks.GetHashCode(), this.Valves.GetHashCode()))

    override this.Equals b =
        match b with
        | :? Settings as other -> 
            this.Tanks = other.Tanks 
            && this.Valves = other.Valves
        | _ -> false

let tankValues = [|1.0 .. 5.0|]
let valveValues = [|1.0 .. 5.0|]
tankValues = valveValues
tankValues
valveValues

hash tankValues
hash valveValues

let tanks = [|1.0 .. 5.0|] |> Arr<TankIdx,_>
let valves = [|1.0 .. 5.0|] |> Arr<ValveIdx,_>
let s1 = {
    Tanks = tanks
    Valves = valves
}

let s2 = {
    Tanks = tanks.Copy ()
    Valves = valves.Copy ()
}

s1.GetHashCode()
s2.GetHashCode()
s1 = s2