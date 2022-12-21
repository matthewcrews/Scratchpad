open System.Collections.Generic

let rng = System.Random 123
let minKey = 0
let maxKey = 100_000
let maxValue = 1_000
let lookupCount = 100
let testCount = 100

let keys =
    [for _ in 1 .. 10 ->
        $"Key[{rng.Next (minKey, maxKey)}]"]

let hashes =
    keys
    |> List.map (fun key ->
        (EqualityComparer.Default.GetHashCode key) &&& 0x7FFF_FFFF)

let bucketCount = 32

let bucketBitShift =
    64 - (System.Numerics.BitOperations.TrailingZeroCount bucketCount)

let bucketIndexes =
    hashes
    |> List.map (fun h ->
        let hashProduct = (uint h) * 2654435769u
        int (hashProduct >>> bucketBitShift))

let strHasher (s: string) : int =
    let x = s.Chars.AsReadOnlySpan()

    1