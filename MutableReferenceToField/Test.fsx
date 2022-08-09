[<Measure>] type Chicken

[<Struct>]
type Row<[<Measure>] 'Measure, 'T>(values: 'T[]) =


    new (length: int, value: 'T) =
        Row (Array.create (int length) value)

    member _._values : 'T[] = values
    
    member r.Item
        with inline get (i: int<'Measure>) =
            r._values[int i]

        and inline set (index: int<'Measure>) value =
            r._values[int index] <- value

module Row =

    [<CompiledName("Create")>]
    let inline create (count: int<'Measure>) value =
        let values = Array.create (int count) value
        Row<'Measure, _> values


let mutable c = Row.create 4<Chicken> 1.0


c[0<Chicken>] <- 42.0

printfn "%A" c._values


type Settings =
    {
        Size : Row<Chicken, float>
        Age : Row<Chicken, int>
    }
let chickenCount = 5<Chicken>
let s =
    {
        Size = Row.create chickenCount 1.0
        Age = Row.create chickenCount 3
    }

printfn "%A" s.Size._values
printfn "%A" s.Age._values

let mutable sizes = s.Size

sizes[0<Chicken>] <- 3.0