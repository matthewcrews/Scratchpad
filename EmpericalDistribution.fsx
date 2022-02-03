#r "nuget: FSharp.Stats"

open FSharp.Stats
open FSharp.Stats.Distributions


let testData = [
    1.0
    1.0
    1.0
    1.0
    1.0
    2.0
    2.0
    2.0
    3.0
    4.0
    4.0
    4.0
    4.0
    5.0
    5.0
    5.0
    5.0
    6.0
    6.0
    6.0
]

let empiricalDistribution = Empirical.create 0.5 testData

let empCdf =
    (0.0, empiricalDistribution)
    |> Seq.scan
    ||> Map.scan (fun acc key value -> acc + value)