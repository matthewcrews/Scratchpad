module EqualitySpeedAnalysis 

open System
open BenchmarkDotNet
open BenchmarkDotNet.Attributes

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

type Benchmarks () =

    [<Benchmark>]
    member this.Size_100 () = 
        if not (modelA_100 = modelB_100) then
            failwith "Models didn't equate :/"

    [<Benchmark>]
    member this.Size_1_000 () =
        if not (modelA_1_000 = modelB_1_000) then
            failwith "Models didn't equate :/"

    [<Benchmark>]
    member this.Size_10_000 () =
        if not (modelA_10_000 = modelB_10_000) then
            failwith "Models didn't equate :/"

    [<Benchmark>]
    member this.Size_100_000 () =
        if not (modelA_100_000 = modelB_100_000) then
            failwith "Models didn't equate :/"

    [<Benchmark>]
    member this.Size_1_000_000 () =
        if not (modelA_1_000_000 = modelB_1_000_000) then
            failwith "Models didn't equate :/"





