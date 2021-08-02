module EqualitySpeedAnalysis 

open System
open System.Collections.Generic
open FSharp.UMX
open BenchmarkDotNet
open BenchmarkDotNet.Attributes

[<Measure>] type PartNumber

type Chicken = {
    Name : string
    Size : decimal
}

type Model (values: Map<int, Chicken>) =

    let a = values
    let b = values
    let c = values
    let d = values

    member _.A = a
    member _.B = b
    member _.C = c
    member _.D = d

    override this.GetHashCode () =
        hash (a, b, c, d)

    override this.Equals (b) =
        match b with
        | :? Model as other -> 
            this.A = other.A &&
            this.B = other.B &&
            this.C = other.C &&
            this.D = other.D
        | _ -> false

let values_100 =
    [1..100]
    |> List.map (fun i -> i, { Name = $"{i}"; Size = decimal i })
    |> Map

let modelA_100 = Model values_100
let modelB_100 = Model values_100


let values_1_000 =
    [1..1_000]
    |> List.map (fun i -> i, { Name = $"{i}"; Size = decimal i })
    |> Map

let modelA_1_000 = Model values_1_000
let modelB_1_000 = Model values_1_000


let values_10_000 =
    [1..10_000]
    |> List.map (fun i -> i, { Name = $"{i}"; Size = decimal i })
    |> Map

let modelA_10_000 = Model values_10_000
let modelB_10_000 = Model values_10_000


let values_100_000 =
    [1..100_000]
    |> List.map (fun i -> i, { Name = $"{i}"; Size = decimal i })
    |> Map

let modelA_100_000 = Model values_100_000
let modelB_100_000 = Model values_100_000

let values_1_000_000 =
    [1..1_000_000]
    |> List.map (fun i -> i, { Name = $"{i}"; Size = decimal i })
    |> Map

let modelA_1_000_000 = Model values_1_000_000
let modelB_1_000_000 = Model values_1_000_000

let numberOfKeys = 1_000

let testArr = Array.zeroCreate<int> numberOfKeys
let intDict =
    let d = Dictionary ()
    for idx in 0 .. numberOfKeys - 1 do
        d.Add (idx, 0)
    d

let int64Dict =
    let d = Dictionary ()
    for KeyValue(k, v) in intDict do
        d.Add (int64 k, v)
    d


let IntComparer = {
    new IEqualityComparer<int> with
        member _.Equals (a: int, b: int) =
            a = b
        member _.GetHashCode (a: int) =
            a
}

let intComparerDict =
    Dictionary (intDict, IntComparer)

let intUnitsDict =
    let d = Dictionary ()
    for KeyValue(key, value) in intDict do
        d.Add (key* 1<PartNumber>, value)
    d

let strDict =
    let d = Dictionary ()
    for idx in 0 .. numberOfKeys - 1 do
        d.Add (string idx, 0)
    d

let numberOfLookups = 10_000_000

let intLookups =
    let rng = System.Random(123)
    seq {for _ in 1 .. numberOfLookups ->
            rng.Next(numberOfKeys)
    } |> Array.ofSeq

let int64Lookups =
    intLookups
    |> Array.map int64

let intUnitsLookups =
    intLookups
    |> Array.map (fun x -> x * 1<PartNumber>)

let strLookups =
    intLookups
    |> Array.map string

type Benchmarks () =

    // [<Benchmark>]
    // member _.ArrayLookup () =
    //     let rng = System.Random(123)
    //     let mutable acc = 0
    //     for _ in 1 .. numberOfLookups do
    //         acc <- testArr.[rng.Next(numberOfKeys)]
    //     acc

    // [<Benchmark>]
    // member _.IntComparerDictionary () =
    //     let mutable acc = 0
    //     for idx in intLookups do
    //         acc <- intComparerDict.[idx]
    //     acc

    // [<Benchmark>]
    // member _.IntUnitsDictionary () =
    //     let mutable acc = 0
    //     for idx in intUnitsLookups do
    //         acc <- intUnitsDict.[idx]
    //     acc

    [<Benchmark>]
    member _.TestDictLookup () =
        let mutable acc = 0
        for idx in intLookups do
            acc <- intDict.[idx]
        acc

    [<Benchmark>]
    member _.TestGetValueOrDefault () =
        let mutable acc = 0
        for idx in intLookups do
            acc <- intDict.GetValueOrDefault (idx, 0)
        acc

    // [<Benchmark>]
    // member _.Int64Dictionary () =
    //     let mutable acc = 0
    //     for idx in int64Lookups do
    //         acc <- int64Dict.[idx]
    //     acc

    // [<Benchmark>]
    // member _.StrDictionary () =
    //     let mutable acc = 0
    //     for idx in strLookups do
    //         acc <- strDict.[idx]
    //     acc


    // [<Benchmark>]
    // member this.Size_100 () = 
    //     if not (modelA_100 = modelB_100) then
    //         failwith "Models didn't equate :/"

    // [<Benchmark>]
    // member this.Size_1_000 () =
    //     if not (modelA_1_000 = modelB_1_000) then
    //         failwith "Models didn't equate :/"

    // [<Benchmark>]
    // member this.Size_10_000 () =
    //     if not (modelA_10_000 = modelB_10_000) then
    //         failwith "Models didn't equate :/"

    // [<Benchmark>]
    // member this.Size_100_000 () =
    //     if not (modelA_100_000 = modelB_100_000) then
    //         failwith "Models didn't equate :/"

    // [<Benchmark>]
    // member this.Size_1_000_000 () =
    //     if not (modelA_1_000_000 = modelB_1_000_000) then
    //         failwith "Models didn't equate :/"





