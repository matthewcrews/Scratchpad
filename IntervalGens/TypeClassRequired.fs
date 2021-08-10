module rec TestTypes =

    type Chicken = {
        Size : int 
    } with
        static member ( * ) (c: Chicken, i: int) =
            { Size = c.Size * i }
        static member ( + ) (c1: Chicken, c2: Chicken) =
            Flock [c1; c2]
        static member ( + ) (c: Chicken, Flock f) =
            Flock c::f

    type Flock = Flock of Chicken list

    type TestMap<'k, 'v when 'k : comparison> = {
        Data : Map<'k, 'v>
    } with
        static member inline ( .* ) (l: TestMap<_,_>, r: TestMap<_,_>) =
            TestExpr.HadamardProduct (l, TestExpr.TestMap r)

        static member inline ( .* ) (l: TestMap<_,_>, r: TestExpr<_,_>) =
            TestExpr.HadamardProduct (l, r)

    type TestExpr<'k, 'v, 'Result when 'k : comparison> =
        | TestMap of TestMap<'k, 'v>
        | HadamardProduct of TestMap<'k, 'v> * TestExpr<'k, 'v>

open TestTypes

let m1 = { Data = Map [(1, 1); (2, 2)]}
let m2 = { Data = Map [(1, { Size = 1 }); (2, { Size = 3 })]}
let m2 = { Data = Map [(1, 3); (2, 4)]}


let x = m1 .* m2 .* m2 // Compiler gives a type constraint mismatch

// TestExpr<'k, ^a> -> SliceMap<'k, ^b> -> TestExpr<'k, ^Result>
// Where (^a or ^b) : (static member ( * ) : ^a * ^b -> ^Result)

type HadTree<'a>=
    |Product of HadTree * HadTree
    |Argument of 'a