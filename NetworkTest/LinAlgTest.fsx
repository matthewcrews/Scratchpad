#r "nuget: MathNet.Numerics.FSharp, 4.15.0"

open MathNet.Numerics
open MathNet.Numerics.LinearAlgebra

let A = matrix [
    [1.0; -1.0;  0.0;  0.0;  0.0; 0.0;  1.0; 0.0; 0.0] // 1
    [1.0;  0.0;  0.0;  0.0;  0.0; 0.0; -1.0; 0.0; 0.0] // 2
    [0.0;  1.0; -1.0;  0.0;  0.0; 0.0;  0.0; 0.0; 0.0] // 3
    [0.0;  0.0;  1.0; -1.0; -1.0; 0.0;  0.0; 0.0; 0.0] // 4
    [0.0;  0.0;  0.0;  0.0; -1.0; 1.0;  0.0; 1.0; 0.0] // 5
    [0.0;  0.0;  0.0;  0.0;  0.0; 1.0; -1.0; 0.0; 0.0] // 7
    [0.0;  0.0;  0.0;  1.0; -1.0; 0.0;  0.0; 0.0; 0.0] // 5
    [0.0;  0.0;  1.0;  0.0;  0.0; 0.0;  0.0; 0.0; 1.0] // 8

]

let B = (A.[ 0.. , 0 .. 1]).Append A.[ 0.., 3..]

let b = vector [
    0.0
    0.0
    0.0
    0.0
    0.0
    0.0
    0.0
    10.0
]

let c = vector [
    -1.0
    -1.0
    -1.0
    -1.0
    -1.0
    -1.0
    -1.0
    -1.0
    -0.0
]

let x = B.Solve b

for elem in x do
    printfn "%A" elem

// let Binv_b = A.Inverse () * b
// for elem in Binv_b do
//     printfn "%A" elem

let c_b = vector [
    1.0
    1.0
    1.0
    1.0
    1.0
    1.0
    1.0
    0.0
]

// let z = (c_b * (B.Inverse())*A)
// for elem in z do
//     printfn "%A" elem
let c_bar = c - (c_b * (B.Inverse())*A)
for elem in c_bar do
    printfn "%A" elem

let u = (B.Inverse ()) * A.[0.., 2]
for elem in u do
    printfn "%A" elem

let theta =
    x ./ u
    |> Seq.filter (fun x -> x > 0.0)
    |> Seq.min

let y = x - theta * u
for elem in y do
    printfn "%A" elem