#r "nuget: MathNet.Numerics.FSharp, 4.15.0"

open MathNet.Numerics.Distributions

let rng = System.Random 123
let mu = 6.20773330856801
let sigma = 2.86598097752801

let samples = [
    for _ in 1..1000 do
        Sample.logNormal mu sigma rng
]


let avg = List.average samples