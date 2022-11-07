open System.Collections.Generic

module Units =

    [<Measure>] type ValueKey
    [<Measure>] type RangeKey


type ValueKey = int<Units.ValueKey>
type RangeKey = int<Units.RangeKey>


[<Struct>]
type Bar<[<Measure>] 'Measure, 'T> (values: 'T[]) =
    // WARNING: Not for public consumption
    member _._values = values
    member b.Item
        with inline get (k: int<'Measure>) = b._values[int k]
    member inline b.Length = LanguagePrimitives.Int32WithMeasure<'Measure> b._values.Length


[<Struct>]
type SkipIndex<'T> = {
    StartKeys: Dictionary<'T, ValueKey>
    NextKey: Bar<Units.ValueKey, ValueKey>
    Values: Bar<Units.ValueKey, 'T>
}

module SkipIndex =

    let create (values: 'a[]) =
        let startKeys = Dictionary()
        let nexts = Array.zeroCreate values.Length

        for i = values.Length - 1 downto 0 do
            let value = values[i]
            let index = i * 1<_>
            match startKeys.TryGetValue value with
            | true, nextIndex ->
                nexts[i] <- nextIndex
                startKeys[value] <- index
            | false, _ ->
                startKeys[value] <- index

        let values = Bar<Units.ValueKey, _> values
        let nextKey = Bar<Units.ValueKey, ValueKey> nexts

        {
            StartKeys = startKeys
            NextKey = nextKey
            Values = values
        }


[<Struct>]
type Range =
    {
        Start : ValueKey
        Length : ValueKey
    }

[<Struct>]
type RangeIndex<'T> = {
    Ranges: Dictionary<'T, Range>
    Values: Bar<Units.ValueKey, 'T>
}

module RangeIndex =

    let create (values: 'T[]) =
        let ranges = Dictionary()
        let mutable value = values[0]
        let mutable start = 0<Units.ValueKey>
        let mutable length = 0<Units.ValueKey>

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
                length <- 1<_>

        let range = { Start = start; Length = length }
        if ranges.ContainsKey value then
            invalidArg (nameof values) "Data contains not contiguous values"

        // Store the range
        ranges[value] <- range
        let values = Bar<Units.ValueKey, _> values
        
        {
            Ranges = ranges
            Values = values
        }

          
[<Struct>]
type RangeSkipIndex<'T> = {
    StartRange : Dictionary<'T, RangeKey>
    Ranges : Bar<Units.RangeKey, Range>
    NextRange : Bar<Units.RangeKey, RangeKey>
    Values : Bar<Units.ValueKey, 'T>
}
          
module RangeSkipIndex =
    
    let create (values: 'T[]) =
        let startRange = Dictionary()
        let ranges = Queue()
        let prevRangeKeys = Dictionary()
        let nextRangeKeys = Dictionary()
        let mutable value = values[0]
        let mutable start = 0<Units.ValueKey>
        let mutable length = 0<Units.ValueKey>
        let mutable rangeKey = 0<Units.RangeKey>

        for i = 0 to values.Length - 1 do
            let valueKey = i * 1<Units.ValueKey>
            
            if values[i] = value then
                length <- length + 1<_>
            else
                // Create the new range and add it to the Queue
                let range = { Start = start; Length = length }
                ranges.Enqueue range
                
                // We check if we have entered a starting range for
                // this value or not. If we have not, we need to store it
                if not (startRange.ContainsKey value) then
                    startRange[value] <- rangeKey
                    
                // If we have seen it before, then we need to record an entry
                // for the NextRange Bar
                else
                    let prevRangeKey = prevRangeKeys[value]
                    nextRangeKeys[prevRangeKey] <- rangeKey
                    
                // Record that this is the last RangeKey for this value
                prevRangeKeys[value] <- rangeKey

                // Reset the mutable values
                value <- values[i]
                start <- valueKey
                length <- 1<_>
                rangeKey <- rangeKey + 1<_>


        // Wrap up the last Range the loop was working on
        // Create the new range and add it to the Queue
        let range = { Start = start; Length = length }
        ranges.Enqueue range
        
        // We check if we have entered a starting range for
        // this value or not. If we have not, we need to store it
        if not (startRange.ContainsKey value) then
            startRange[value] <- rangeKey
            
        // If we have seen it before, then we need to record an entry
        // for the NextRange Bar
        else
            let prevRangeKey = prevRangeKeys[value]
            nextRangeKeys[prevRangeKey] <- rangeKey
        

        // Create the final collections for the Index
        let ranges =
            let r = ranges.ToArray()
            Bar<Units.RangeKey, _> r

        let nextRange =
            let r = Array.zeroCreate (int ranges.Length)
            for KeyValue (rangeKey, nextRangeKey) in nextRangeKeys do
                r[int rangeKey] <- nextRangeKey
            Bar<Units.RangeKey, _> r


        let values = Bar<Units.ValueKey, _> values

        {
            StartRange = startRange
            Ranges = ranges
            NextRange = nextRange
            Values = values
        }
          
                
                
                
let v = [|
    1
    1
    1
    2
    2
    2
    3
    1
    1
    2
    2
    2
|]

let sk2 = RangeSkipIndex.create v
sk2.Ranges
sk2.NextRange


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