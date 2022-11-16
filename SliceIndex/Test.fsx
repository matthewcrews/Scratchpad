open System
open System.Collections.Generic
open System.Runtime.CompilerServices
open FSharp.NativeInterop

#nowarn "9"

let inline stackalloc<'a when 'a: unmanaged> (length: int): Span<'a> =
  let p = NativePtr.stackalloc<'a> length |> NativePtr.toVoidPtr
  Span<'a>(p, length)

module Units =

    [<Measure>] type ValueKey
    [<Measure>] type RangeKey


type ValueKey = int<Units.ValueKey>
type RangeKey = int<Units.RangeKey>


[<Struct>]
type Block<'T>(values: 'T[]) =
    // WARNING: Public for inlining only
    member _._values = values
    member b.Item
        with inline get k = b._values[k]
    member inline b.Length = b._values.Length
    member _.ToArray () = Array.copy values

type block<'T> = Block<'T>

[<Struct>]
type Bar<[<Measure>] 'Measure, 'T> (values: 'T[]) =
    // WARNING: Not for public consumption
    member _._values = values
    member b.Item
        with inline get (k: int<'Measure>) = b._values[int k]
    member inline b.Length = LanguagePrimitives.Int32WithMeasure<'Measure> b._values.Length


[<Struct>]
type Range =
    {
        Start : ValueKey
        Length : ValueKey
    }

          
[<Struct>]
type SliceIndex<'T> = {
    StartRange : Dictionary<'T, RangeKey>
    Ranges : Bar<Units.RangeKey, Range>
    NextRange : Bar<Units.RangeKey, RangeKey>
    Values : Bar<Units.ValueKey, 'T>
}
          
module SliceIndex =
    
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

let sk2 = SliceIndex.create v
sk2.Ranges
sk2.NextRange

type All = All

type Math () =
    
    static member inline min (a: int<'Measure>, b: int<'Measure>) =
        Math.Min (int a, int b)
        |> LanguagePrimitives.Int32WithMeasure<'Measure>

    static member inline max (a: int<'Measure>, b: int<'Measure>) =
        Math.Max (int a, int b)
        |> LanguagePrimitives.Int32WithMeasure<'Measure>

[<Struct>]
type KeyFilter =
    {
        IndexRanges: block<Bar<Units.RangeKey, Range>>
        NextRanges: block<Bar<Units.RangeKey, RangeKey>>
        StartRanges: block<RangeKey>
    }

[<Struct>]
type IndexSetIterator =
    {
        KeyFilter : KeyFilter
        CurRangeKeys: array<RangeKey>
        MinKeys: array<ValueKey>
        MaxKeys: array<ValueKey>
        mutable CurValueKey: ValueKey
        mutable CurMinValueKey: ValueKey
        mutable CurMaxValueKey: ValueKey
    }
    static member ofKeyFilter (kf: KeyFilter) : IndexSetIterator =
        let curRangeKeys = kf.StartRanges.ToArray()
        let minKeys = [|0<_>|]
        let maxKeys = [|0<_>|]
        {
            KeyFilter = kf
            CurRangeKeys = curRangeKeys
            MinKeys = minKeys
            MaxKeys = maxKeys
            CurValueKey = -1<_>
            CurMinValueKey = 0<_>
            CurMaxValueKey = 0<_>
        }
        
    // We are moving the ranges forward
    member private x.Seek() : bool =
        // Tracking whether we are still seeing new ranges.
        let mutable result = true
        
        // Move all the ranges forward whose MaxValueKey is equal to CurMaxValueKey
        for i = 0 to x.MaxKeys.Length - 1 do
            if x.MaxKeys[i] = x.CurMaxValueKey then
                let curRangeKey = x.CurRangeKeys[i]
                let nextRangeKey = x.KeyFilter.NextRanges[i][curRangeKey]
                // We only want to perform this update if we are moving forward
                // in the index. If we are moving back, we have reached the end.
                if nextRangeKey > curRangeKey then
                    x.CurRangeKeys[i] <- nextRangeKey
                    // Get the new CurRange for the Index
                    let curRange = x.KeyFilter.IndexRanges[i][nextRangeKey]
                    // Update the Min and Max Value Key for the new Range
                    x.CurMinValueKey <- curRange.Start
                    x.CurMaxValueKey <- curRange.Start + curRange.Length
                else
                    result <- false
                    
                    
        // Check whether we are still making progress before re-evaluating. result
        // will be FALSE in the case one of the SliceIndexes no longer has ranges.
        if result then
            
            for minKey in x.MinKeys do
                x.CurMinValueKey <- Math.max (x.CurMinValueKey, minKey)
                
            for maxKey in x.MaxKeys do
                x.CurMaxValueKey <- Math.min (x.CurMaxValueKey, maxKey)
                
            if x.CurMinValueKey <= x.CurMaxValueKey then
                // We have found a new range of overlaps
                x.CurValueKey <- x.CurMinValueKey
            else
                // The ranges still do not overlap so we need to seek again
                result <- x.Seek()
                
        result
    
    member private x.Initialize() : bool =
        let minKeys = x.MinKeys
        let maxKeys = x.MaxKeys
        let indexRanges = x.KeyFilter.IndexRanges
        let curRangeKeys = x.CurRangeKeys
        
        for i = 0 to minKeys.Length - 1 do
            let ranges = indexRanges[i]
            let curRangeKey = curRangeKeys[i]
            let curRange = ranges[curRangeKey]
            minKeys[i] <- curRange.Start
            maxKeys[i] <- curRange.Start + curRange.Length
            x.CurMinValueKey <- Math.max (x.CurMinValueKey, minKeys[i])
            x.CurMaxValueKey <- Math.min (x.CurMaxValueKey, maxKeys[i])
            
        if x.CurMinValueKey <= x.CurMaxValueKey then
            x.CurValueKey <- x.CurMinValueKey
            true
        else
            x.Seek()
    
    member x.MoveNext () : bool =
        if x.CurValueKey < 0<_> then
            x.Initialize()
        elif x.CurValueKey < x.CurMaxValueKey then
            x.CurValueKey <- x.CurValueKey + 1<_>
            true
        else
            x.Seek()
        
    member x.Current = x.CurValueKey
  
  
[<Struct>]
type ValueIterator<'T> =
    {
        mutable IndexSet : IndexSetIterator
        ValueLookup : ValueKey -> 'T
    }
    
    member x.MoveNext () = x.IndexSet.MoveNext()

    member x.Current =
        if x.IndexSet.CurValueKey < 0<_> then
            raise (InvalidOperationException "Enumeration has not started. Call MoveNext.")
        else
            x.ValueLookup x.IndexSet.CurValueKey
        
type SliceSet<'a when 'a : equality>(
    keyFilter: KeyFilter,
    index: SliceIndex<'a>
    ) =
    
    member _.GetEnumerator () =
        let indexSet = IndexSetIterator.ofKeyFilter keyFilter
        let lookup = fun k -> index.Values[k]
        {
            IndexSet = indexSet
            ValueLookup = lookup
        }

type SliceSet2D<'a, 'b when 'a : equality and 'b : equality>(
    keyFilter: KeyFilter option,
    index1: SliceIndex<'a>,
    index2: SliceIndex<'b>
    ) =
    
    new (values: array<'a * 'b>) =
        let aValues = values |> Array.map fst
        let bValues = values |> Array.map snd
        let aIndex = SliceIndex.create aValues
        let bIndex = SliceIndex.create bValues
        SliceSet2D (None, aIndex, bIndex)
        
    new (values: list<'a * 'b>) =
        let values = Array.ofList values
        SliceSet2D values
        
    member _.Item
        with get (k1: 'a, _: All) =
            match index1.StartRange.TryGetValue k1 with
            | true, startRangeKey ->
                let indexRanges = block [|index1.Ranges|]
                let nextRanges = block [|index1.NextRange|]
                let startRanges = block [|startRangeKey|]
                let keyFilter : KeyFilter = {
                    IndexRanges = indexRanges
                    NextRanges = nextRanges
                    StartRanges = startRanges
                }
                SliceSet (keyFilter, index2)
                
            | false, _ ->
                raise (KeyNotFoundException "Index does not contain the value")
            
    member _.Item
        with get (_: All, k2: 'b) =
            NotImplementedException()
            
            
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

for a in x do
    printfn $"{a}"
// let y = test[All, 1]

// let a = [1; 2; 3]
// let z = seq a
// let e = z.GetEnumerator()
// e.Current
    
(*
Prints:
(1, 1)
(7, 1)
(8, 1)
val it: unit = ()
*)