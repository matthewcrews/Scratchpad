open System.Numerics

let x = 16u
let leadingZero = BitOperations.LeadingZeroCount x
let key = 1 <<< (32 - leadingZero)

let computeBucketAndSize x =
    let leadingZeroCount = BitOperations.LeadingZeroCount (uint (x - 1))
    let bucket = 32 - leadingZeroCount
    let size = 1 <<< (32 - leadingZeroCount)
    bucket, size

computeBucketAndSize 2

1<<<30

type Range =
    {
        Start : int
        Bound : int
    }

let startSeek (start: int) (ranges: Range[]) =
    let mutable l = 0
    let mutable r = ranges.Length

    while l < r do
        let m = (l + r) / 2
        if ranges[m].Start < start then
            l <- m + 1
        else
            r <- m

    l

let boundSeek (bound: int) (ranges: Range[]) =
    let mutable l = 0
    let mutable r = ranges.Length

    while l < r do
        let m = (l + r) / 2
        if ranges[m].Bound < bound then
            l <- m + 1
        else
            r <- m

    l


let testRanges = [|
    { Start = 0; Bound = 2}
    { Start = 3; Bound = 5}
    { Start = 7; Bound = 9}
    { Start = 12; Bound = 15}
    { Start = 20; Bound = 21}
    { Start = 30; Bound = 32}
    { Start = 40; Bound = 42}
|]

let emptyRanges = [||]

let n = boundSeek 30 testRanges