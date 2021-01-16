type BuilderTest () =
    member this.Yield (evalResult: bool) =
        evalResult

    member this.For(source: seq<'a>, body:'a -> seq<'b * bool>) =
        source
        |> Seq.collect (fun x -> body x |> Seq.map (fun (idx, expr) -> (x, idx), expr))

    member this.For(source: seq<'a>, body:'a -> bool) =
        source |> Seq.map (fun x -> x, body x)

    member this.Run(source: seq<'a * bool>) =
        source |> Seq.map (fun (n, c) -> n, c)

    [<CustomOperation("requires")>]
    member this.Requires(a: bool, b: bool) =
        a && b

let values = [1..10]

let builderTest = BuilderTest ()

let test =
    builderTest {
        for x in values ->
            let z = x > 2
            z requires true
    }