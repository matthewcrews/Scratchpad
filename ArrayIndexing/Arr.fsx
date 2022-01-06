[<Struct>]
type StructWrapper<[<Measure>] 'Measure, 'Value> =
    val Values : array<'Value>

    new (values: array<'Value>) =
        {
            Values = values
        }

    member this.Item
        with get (idx: int<'Measure>) =
            this.Values.[int idx]

    member this.Length = 
        LanguagePrimitives.Int32WithMeasure<'Measure> this.Values.Length

[<Measure>] type ItemIdx


let test1 (x: array<float>) =
    let mutable acc = 0.0
    let mutable idx = 0
    let len = x.Length

    while idx < len do
        acc <- acc + x[idx]
        idx <- idx + 1

    acc


let test2 (x: StructWrapper<ItemIdx, float>) =
    let mutable acc = 0.0
    let mutable idx = 0<ItemIdx>
    let len = x.Length

    while idx < len do
        acc <- acc + x[idx]
        idx <- idx + 1<ItemIdx>

    acc


let numberCount = 1_000_000
let rawArray = [|1.0 .. (float numberCount)|]
let structApproach =
    [|1.0 .. (float numberCount)|] 
    |> StructWrapper<ItemIdx, _>

let r1 = test1 rawArray
let r2 = test2 structApproach
