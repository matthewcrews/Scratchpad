open System.Collections.Generic

module Units =

    [<Measure>] type RangeKey
    [<Measure>] type SkipKey
    [<Measure>] type RangeSkipKey


type RangeKey = int<Units.RangeKey>
type SkipKey = int<Units.SkipKey>
type RangeSkipKey = int<Units.RangeSkipKey>


[<Struct>]
type Bar<[<Measure>] 'Measure, 'T> (values: 'T[]) =
    // WARNING: Not for public consumption
    member _._values = values
    member b.Item
        with inline get (k: int<'Measure>) = b._values[int k]
    member inline b.Length = LanguagePrimitives.Int32WithMeasure<'Measure> b._values.Length


[<Struct>]
type SkipIterator =
    {
        mutable Key : SkipKey
        NextKey : Bar<Units.SkipKey, SkipKey>
    }
    member si.Current = si.Key

    member si.Next () =
        let nextIndex = si.NextKey[si.Key]
        if nextIndex > 0<_> then
            si.Key <- nextIndex
            Some nextIndex
        else
            None


[<Struct>]
type SkipIndex<'T>(startKey: Dictionary<'T, SkipKey>, nextKey: Bar<Units.SkipKey, SkipKey>, values: Bar<Units.SkipKey, 'T>) =

    member _.GetIterator (k: 'T) =
        let key = startKey[k]
        {
            Key = key
            NextKey = nextKey
        }

    member _.Contains v = startKey.ContainsKey v

module SkipIndex =

    let create (values: 'a[]) =
        let starts = Dictionary()
        let nexts = Array.zeroCreate values.Length

        for i = values.Length - 1 downto 0 do
            let value = values[i]
            let index = i * 1<_>
            match starts.TryGetValue value with
            | true, nextIndex ->
                nexts[i] <- nextIndex
                starts[value] <- index
            | false, _ ->
                starts[value] <- index

        let values = Bar<Units.SkipKey, _> values
        let nexts = Bar<Units.SkipKey, SkipKey> nexts

        SkipIndex (starts, nexts, values)


[<Struct>]
type Range<[<Measure>] 'Measure> =
    {
        Start : int<'Measure>
        Length : int<'Measure>
    }

[<Struct>]
type RangeIterator =
    {
        mutable Key : RangeKey
        MaxKey : RangeKey
    }
    member ri.Next () =
        if ri.Key < ri.MaxKey then
            ri.Key <- ri.Key + 1<_>
            Some ri.Key
        else
            None

type RangeIndex<'T>(ranges: Dictionary<'T, Range<Units.RangeKey>>, values: Bar<Units.RangeKey, 'T>) =

    member _.GetIterator (k: 'T) =
        let range = ranges[k]
        {
            Key = range.Start
            MaxKey = range.Start + range.Length
        }

module RangeIndex =

    let create (values: 'T[]) =
        let ranges = Dictionary()
        let mutable value = values[0]
        let mutable start = 0<Units.RangeKey>
        let mutable length = -1<Units.RangeKey>

        for i = 0 to values.Length - 1 do

            if values[i] = value then
                length <- length + 1<_>
            else
                let range = { Start = start; Length = length }
                
                if ranges.ContainsKey value then
                    invalidArg (nameof values) "Data contains not contiguous values"

                // Store the range
                ranges[value] <- range
                // Reset the mutable values
                value <- values[i]
                start <- i * 1<_>
                length <- 0<_>

        let range = { Start = start; Length = length }
        if ranges.ContainsKey value then
            invalidArg (nameof values) "Data contains not contiguous values"

        // Store the range
        ranges[value] <- range

        RangeIndex ranges

                
let v = [|
    1
    1
    1
    2
    3
    4
    4
    4
    6
    6
|]

let sk2 = RangeIndex.create v
let mutable si2 = sk2.GetIterator 4
si2.Next()
// let v = [|
//     1
//     2
//     1
//     1
//     3
//     4
//     1
// |]

// let sk1 = SkipIndex.create v
// let mutable si1 = sk1.GetIterator 1
// si1.Next()







type All = All

type SliceSet2D<'a, 'b when 'a : equality and 'b : equality>(values: seq<'a * 'b>) =
    
    let values = Array.ofSeq values
    
    member s.Item
        with get (k1: 'a, _: All) =
            values
            |> Seq.filter (fun (a, _) -> a = k1)
            
    member s.Item
        with get (_: All, k2: 'b) =
            values
            |> Seq.filter (fun (_, b) -> b = k2)
            
            
let test =
    SliceSet2D [
        1, 1
        1, 5
        1, 10
        2, 5
        2, 10
        7, 1
        8, 1
    ]
    
let x = test[1, All]
let y = test[All, 1]

for v in y do
    printfn $"{v}"
    
(*
Prints:
(1, 1)
(7, 1)
(8, 1)
val it: unit = ()
*)