let rng = System.Random 123
let minKey = 0
let maxKey = 100_000
let keyCount = 10

let arraySize = 32

let keys =
    [| for _ in 1 .. keyCount ->
        ((rng.Next (minKey, maxKey)) <<< 16)|]

// let bucket =
//     keys
//     |> Array.map (fun k -> k % arraySize)


open System.Numerics

let bitShift =
    32 - BitOperations.TrailingZeroCount arraySize

let fibonacciBucket =
    keys
    |> Array.map (fun k ->
        let hashProduct = (uint k) * 2654435769u
        int (hashProduct >>> bitShift))
        