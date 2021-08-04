// type Type1<'k, 'v> = {
//     Key1 : 'k
//     Value : 'v
// }
// type Type2<'k1, 'k2, 'v> = {
//     Key1 : 'k1
//     Key2 : 'k2
//     Value : 'v
// }

// type Converter () =

//     member _.Convert<'k1, 'k2, 'v> (a: 'k1 * 'k2 * 'v) =
//         let k1, k2, v = a
//         { Key1 = k1; Key2 = k2; Value = v }
    
//     member _.Convert<'k1, 'v> (a: 'k1 * 'v) =
//         let k, v = a
//         { Key1 = k; Value = v }

// let converter = Converter ()

// let y = "a", 1
// let x = converter.Convert (y)
// let y2 = "a", "b", 2
// let x2 = converter.Convert y2


type TypeA<'k, 'v> = TypeA of seq<'k * 'v>
type TypeB<'k1, 'k2, 'v> = TypeB of seq<'k1 * 'k2 * 'v>

type Builder () =

    member _.build<'k1, 'k2, 'v> (x: seq<'k1 * 'k2 * 'v>) =
        TypeB x

    member _.build<'k, 'v> (x: seq<'k * 'v>) =
        TypeA x

let builder = Builder ()

let a1 = [for i in 0..2 -> i, i]
let a2 = [for i in 0..2 -> string i, i, i]

let x1 = builder.build a1
let x2 = builder.build a2