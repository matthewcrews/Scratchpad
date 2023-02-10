[<Struct>]
type Value<[<Measure>] 'Measure> =
    val internal X: float
    new (v: float) =
        { X = v }
    static member ( * ) (a: Value<'LMeasure>, b: Value<'RMeasure>) =
        let newX = a.X * b.X
        Value<'LMeasure 'RMeasure> newX

type [<Measure>] Count
type [<Measure>] Size

let a = Value<Count> 1.0
let b = Value<Size> 2.0
let c = a * b
c.X