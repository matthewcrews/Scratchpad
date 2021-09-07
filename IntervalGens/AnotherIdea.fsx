module rec TestTypes =

    type Chicken = {
        Size : int 
    } with
        static member ( * ) (c: Chicken, i: int) =
            { Size = c.Size * i }
        static member ( * ) (i: int, c: Chicken) =
            { Size = c.Size * i }
        static member ( + ) (l: Chicken, r: Chicken) =
            Flock [l; r]
        static member ( + ) (c: Chicken, Flock f) =
            Flock (c::f)

    // type BigChicken = {
    //     BigSize : int
    // }

    type Flock = Flock of Chicken list

    type TestMap<'k, 'v when 'k : comparison> = {
        Data : Map<'k, 'v>
    } with
        static member inline ( .* ) (l: TestMap<_,_>, r: TestMap<_,_>) =
            TestExpr<'k,_,_>.HadamardProduct (l, r)

        static member inline ( .* ) (expr: TestExpr<'k,_,_>, l: TestMap<'k,_>) =
            TestExpr<'k,_,_>.HadamardProduct (l, expr)

        static member Zero = Flock []

    // type TestExpr<'k,'l,'r> =
    //     | HadamardProduct of 'l * 'r

    [<RequireQualifiedAccess>]
    type TestExpr<'k, 'l, 'r when 'k : comparison> =
        //| SliceMaps of left:TestMap<'k, 'l> * right:TestMap<'k, 'r>
        | HadamardProduct of 'l * 'r

open TestTypes

let inline sum (expr: TestExpr<_,_,_>) =

    match expr with
    | TestExpr.HadamardProduct (l: TestMap<_, _>, r: TestMap<_,_>) ->
        let mutable acc = LanguagePrimitives.GenericZero
        // let inline zero () = (^Value : (static member Zero : ^Result) ())
        // let mutable acc = zero ()

        for KeyValue (k, v) in l.Data do
            let x = v * r.Data.[k]
            acc <- acc + (v * r.Data.[k])

        acc
    | _ -> failwith "Not handled"


let m1 = { Data = Map [(1, 1); (2, 2)]}
let m2 = { Data = Map [(1, { Size = 1 }); (2, { Size = 3 })]}
let m3 = { Data = Map [(1, 3); (2, 4)]}

let a = m1 .* m2
let s1 = sum a
// let b = a .* m3
// let s2 = sum b
// let x = m1 .* m2 .* m3 // Compiler gives a type constraint mismatch