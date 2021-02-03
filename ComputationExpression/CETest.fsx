type Chicken = Chicken of string
    with
        static member ( ~requires) (a: float, b: float) =
            a + b

let (.requires) (a: float, b: float) =
    a + b


type BuilderTest () =

    member this.Yield (evalResult: bool) =
        evalResult

    member this.Zero () =
        false

    [<CustomOperation("requires", MaintainsVariableSpace=true, AllowIntoPattern=true)>]
    member this.Requires (a: bool, b: bool) =
        a && b

    // member this.For(source: seq<'a>, body:'a -> seq<'b * bool>) =
    //     source
    //     |> Seq.collect (fun x -> body x |> Seq.map (fun (idx, expr) -> (x, idx), expr))

    // member this.For(source: seq<'a>, body:'a -> bool) =
    //     source |> Seq.map (fun x -> x, body x)

    // member this.Run(source: seq<'a * bool>) =
    //     source |> Seq.map (fun (n, c) -> n, c)


let values = [1..10]

let builderTest = BuilderTest ()

let test =
    builderTest {
        let z = false
        requires (z = true)
    }
