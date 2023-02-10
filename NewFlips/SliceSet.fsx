open System
open System.Buffers
open System.Collections.ObjectModel
open System.Collections.Generic


type Math() =

    static member inline abs (a: float<'Measure>) =
        Math.Abs (float a) |> LanguagePrimitives.FloatWithMeasure<'Measure>

    static member inline min(a: int<'Measure>, b: int<'Measure>) =
        Math.Min(int a, int b) |> LanguagePrimitives.Int32WithMeasure<'Measure>

    static member inline max(a: int<'Measure>, b: int<'Measure>) =
        Math.Max(int a, int b) |> LanguagePrimitives.Int32WithMeasure<'Measure>


[<Struct>]
type Bar<[<Measure>] 'Measure, 'T>(values: 'T[]) =
    /// WARNING: Not for public consumption
    member _._values = values

    member inline b.Item
        with get (k: int<'Measure>) = b._values[int k]

    member inline b.Length = LanguagePrimitives.Int32WithMeasure<'Measure> b._values.Length

type bar<[<Measure>] 'Measure, 'T> = Bar<'Measure, 'T>


[<Struct>]
type All = All

[<Measure>]
type private Index
// module private Units =

//     [<Measure>]
//     type Index

//     [<Measure>]
//     type Key

//     [<Measure>]
//     type RangeIndex

//     [<Measure>]
//     type ValueKey

//     [<Measure>]
//     type RangeKey

//     [<Measure>]
//     type IndexKey

// type ValueKey = int<Units.ValueKey>
// type RangeKey = int<Units.RangeKey>
// type IndexKey = int<Units.IndexKey>


// [<Struct>]
// type Range<[<Measure>] 'Measure> =
//     {
//         Start: int<'Measure>
//         // This is the EXCLUSIVE upper bound
//         Bound: int<'Measure>
//     }

[<Struct>]
type Range =
    {
        Start: int<Index>
        Length: int<Index>
    }
    /// Exclusive upper bound of the Range
    member r.Bound = r.Start + r.Length

[<Struct; RequireQualifiedAccess>]
type RangeSet =
    | Empty
    | All
    | Ranges of ranges: Range[]

module RangeSet =

    let all = RangeSet.All

    let empty = RangeSet.Empty

    let intersect (a: RangeSet) (b: RangeSet) : RangeSet =

        match a, b with
        | RangeSet.Empty, _
        | _, RangeSet.Empty -> RangeSet.Empty
        | RangeSet.All, _ -> b
        | _, RangeSet.All -> a
        | RangeSet.Ranges aRanges, RangeSet.Ranges bRanges ->
            let resultAcc = ArrayPool.Shared.Rent(aRanges.Length + bRanges.Length)
            let mutable aIdx = 0
            let mutable bIdx = 0
            let mutable resultIdx = 0
            let mutable aRange = Unchecked.defaultof<_>
            let mutable bRange = Unchecked.defaultof<_>

            while aIdx < aRanges.Length && bIdx < bRanges.Length do
                aRange <- aRanges[aIdx]
                bRange <- bRanges[bIdx]

                if bRange.Bound <= aRange.Start then
                    bIdx <- bIdx + 1
                elif aRange.Bound <= bRange.Start then
                    aIdx <- aIdx + 1
                else
                    let newStart = Math.max (aRange.Start, bRange.Start)
                    let newLength = Math.min (aRange.Bound, bRange.Bound) - newStart
                    let newRange = { Start = newStart; Length = newLength }
                    resultAcc[resultIdx] <- newRange
                    resultIdx <- resultIdx + 1

                    if aRange.Bound < bRange.Bound then
                        aIdx <- aIdx + 1
                    else
                        bIdx <- bIdx + 1


            let result = 
                // Check to see if there were any overlapping Ranges. If there are,
                // we will write the results out
                if resultIdx > 0 then
                    // Copy the final results
                    let result = GC.AllocateUninitializedArray resultIdx
                    Array.Copy(resultAcc, result, resultIdx)
                    RangeSet.Ranges result
                // If there were no overlapping Ranges we just return an
                // empty RangeSet
                else
                    RangeSet.Empty

            // Return the rented array
            ArrayPool.Shared.Return(resultAcc, false)
            // Return the resuling RangeSet
            result


let aRange = RangeSet.Ranges [|
    { Start = 100<_>; Length = 10<_>}
|]

let bRange = RangeSet.Ranges [|
    { Start = 1<_>; Length = 2<_>}
    { Start = 5<_>; Length = 20<_>}
|]

let t = RangeSet.intersect aRange bRange


[<Struct>]
type RangeSetHashTable<'Key> (d: Dictionary<'Key, RangeSet>) =

    /// WARNING: Public for inlining only
    member _._values = d

    /// We want an API that allows you to search for an arbitrary
    /// Key value so that you don't have to be as careful when
    /// composing models. It will just return an Empty if the
    /// Key is not present in the Dictionary
    member inline r.Item
        with get k =
            let d = r._values
            match d.TryGetValue k with
            | true, rangeSet -> rangeSet
            | false, _ -> RangeSet.Empty


type RangeSetIndex<'Key> =
    {
        RangeSets: RangeSetHashTable<'Key>
        Keys: bar<Index, 'Key>
    }


module RangeSetIndex =

    let create (values: 'T[]) =
        let ranges = Dictionary<'T, Queue<_>>()
        let mutable value = values[0]
        let mutable start = 0<Index>
        let mutable length = 0<Index>

        for i = 0 to values.Length - 1 do
            let index = i * 1<Index>

            if values[i] = value then
                length <- length + 1<_>
            else
                // Create the new range
                let range =
                    {
                        Start = start
                        Length = length
                    }
                // Get the Range queue for the current value
                if not (ranges.ContainsKey value) then
                    ranges[value] <- Queue()

                ranges[ value ].Enqueue range

                // Reset the mutable values
                value <- values[i]
                start <- index
                length <- 1<_>

        // Wrap up the last Range the loop was working on
        // Create the new range
        let range =
            {
                Start = start
                Length = length
            }

        // Get the Range queue for the current value
        if not (ranges.ContainsKey value) then
            ranges[value] <- Queue()

        ranges[value].Enqueue range

        // We now want to turn all of the Queues into Arrays
        let rangeSets =
            ranges
            |> Seq.map (fun (KeyValue (value, ranges)) ->
                let rangeArray = ranges.ToArray()
                let rangeSet = RangeSet.Ranges rangeArray
                KeyValuePair(value, rangeSet))
            |> Dictionary
            |> RangeSetHashTable

        {
            RangeSets = rangeSets
            Keys = bar<Index, _> values
        }


[<Struct>]
type SliceSetEnumerator<'T> =
    private
        {
            mutable CurRangeIndex: int
            mutable CurRange: Range
            mutable CurIndex: int<Index>
            mutable CurValue: 'T
            Ranges: Range[]
            ValueLookup: int<Index> -> 'T
        }

    member e.MoveNext() =
        if e.CurIndex < 0<_> && e.CurRangeIndex < e.Ranges.Length then
            e.CurRange <- e.Ranges[e.CurRangeIndex]
            e.CurIndex <- e.CurRange.Start
            e.CurValue <- e.ValueLookup e.CurIndex
            true
        else
            e.CurIndex <- e.CurIndex + 1<_>

            if e.CurIndex < e.CurRange.Bound then
                e.CurValue <- e.ValueLookup e.CurIndex
                true
            else
                e.CurRangeIndex <- e.CurRangeIndex + 1

                if e.CurRangeIndex < e.Ranges.Length then
                    e.CurRange <- e.Ranges[e.CurRangeIndex]
                    e.CurIndex <- e.CurRange.Start
                    e.CurValue <- e.ValueLookup e.CurIndex
                    true
                else
                    false

    member e.Current: 'T =
        if e.CurIndex < 0<_> then
            raise (InvalidOperationException "Enumeration has not started. Call MoveNext.")
        else
            e.CurValue


[<Struct>]
type SliceSet<'a when 'a: equality>(rangeSet: RangeSet, index: RangeSetIndex<'a>) =

    member _.GetEnumerator() =
        let values = index.Keys

        let ranges =
            match rangeSet with
            | RangeSet.Empty -> Array.empty
            | RangeSet.Ranges ranges -> ranges
            | RangeSet.All ->

        {
            CurRangeIndex = 0
            CurRange = Unchecked.defaultof<Range>
            CurIndex = -1<_>
            CurValue = Unchecked.defaultof<'a>
            Ranges = ranges
            ValueLookup = fun vk -> values[vk]
        }

    member s.ToSeq =
        let mutable e = s.GetEnumerator()
        Seq.unfold (fun _ -> if e.MoveNext() then Some(e.Current, ()) else None) ()

    interface System.Collections.IEnumerable with
        member s.GetEnumerator() =
            s.ToSeq.GetEnumerator() :> System.Collections.IEnumerator

    interface IEnumerable<'a> with
        member s.GetEnumerator() = s.ToSeq.GetEnumerator()


// [<Struct>]
// type SliceSet2D<'a, 'b when 'a: equality and 'b: equality>
//     (
//         keyRanges: Series<Units.ValueKey>,
//         aIndex: ValueIndex<'a>,
//         bIndex: ValueIndex<'b>
//     ) =

//     new(values: array<'a * 'b>) =
//         let values = Array.distinct values
//         let aValues = values |> Array.map fst
//         let bValues = values |> Array.map snd
//         let aIndex = ValueIndex.create aValues
//         let bIndex = ValueIndex.create bValues
//         let keyFilter = Series.all values.Length
//         SliceSet2D(keyFilter, aIndex, bIndex)

//     new(values: seq<'a * 'b>) =
//         let values = values |> Array.ofSeq
//         SliceSet2D values

//     member _.GetEnumerator() =
//         let aValues = aIndex.Values
//         let bValues = bIndex.Values

//         {
//             CurKeyRangeIdx = 0
//             CurValueKey = -1<_>
//             CurValueKeyBound = 0<_>
//             CurValue = Unchecked.defaultof<struct ('a * 'b)>
//             KeyRanges = keyRanges
//             ValueLookup = fun vk -> struct (aValues[vk], bValues[vk])
//         }


//     member s.ToSeq =
//         let mutable e = s.GetEnumerator()
//         Seq.unfold (fun _ -> if e.MoveNext() then Some(e.Current, ()) else None) ()


//     member _.Item
//         with get (aKey: 'a, _: All) =

//             let aSeries =
//                 match aIndex.ValueSeries.TryGetValue aKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let newKeyRanges = keyRanges |> Series.intersect aSeries

//             SliceSet(newKeyRanges, bIndex)


//     member _.Item
//         with get (_: All, bKey: 'b) =

//             let bSeries =
//                 match bIndex.ValueSeries.TryGetValue bKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let newKeyRanges = keyRanges |> Series.intersect bSeries

//             SliceSet(newKeyRanges, aIndex)


//     interface System.Collections.IEnumerable with
//         member s.GetEnumerator() =
//             s.ToSeq.GetEnumerator() :> System.Collections.IEnumerator

//     interface IEnumerable<struct ('a * 'b)> with
//         member s.GetEnumerator() = s.ToSeq.GetEnumerator()


// module Slice2 =

//     let inline sumBy ([<InlineIfLambda>] f) (slice: SliceSet2D<'a, 'b>) =
//         let mutable acc = LanguagePrimitives.GenericZero

//         for a, b in slice do
//             acc <- acc + (f a b)

//         acc


// [<Struct>]
// type SliceSet3D<'a, 'b, 'c when 'a: equality and 'b: equality and 'c: equality>
//     (
//         keyRanges: Series<Units.ValueKey>,
//         aIndex: ValueIndex<'a>,
//         bIndex: ValueIndex<'b>,
//         cIndex: ValueIndex<'c>
//     ) =

//     new(values: array<'a * 'b * 'c>) =
//         let values = Array.distinct values
//         let aValues = values |> Array.map (fun (a, _, _) -> a)
//         let bValues = values |> Array.map (fun (_, b, _) -> b)
//         let cValues = values |> Array.map (fun (_, _, c) -> c)
//         let aIndex = ValueIndex.create aValues
//         let bIndex = ValueIndex.create bValues
//         let cIndex = ValueIndex.create cValues
//         let keyFilter = Series.all values.Length
//         SliceSet3D(keyFilter, aIndex, bIndex, cIndex)

//     new(values: seq<'a * 'b * 'c>) =
//         let values = Array.ofSeq values
//         SliceSet3D values

//     member _.GetEnumerator() =
//         let aValues = aIndex.Values
//         let bValues = bIndex.Values
//         let cValues = cIndex.Values

//         {
//             CurKeyRangeIdx = 0
//             CurValueKey = -1<_>
//             CurValueKeyBound = 0<_>
//             CurValue = Unchecked.defaultof<struct ('a * 'b * 'c)>
//             KeyRanges = keyRanges
//             ValueLookup = fun vk -> struct (aValues[vk], bValues[vk], cValues[vk])
//         }


//     member s.ToSeq =
//         let mutable e = s.GetEnumerator()
//         Seq.unfold (fun _ -> if e.MoveNext() then Some(e.Current, ()) else None) ()


//     member _.Item
//         with get (aKey: 'a, bKey: 'b, _: All) =

//             let aSeries =
//                 match aIndex.ValueSeries.TryGetValue aKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let bSeries =
//                 match bIndex.ValueSeries.TryGetValue bKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let newKeyRanges = keyRanges |> Series.intersect aSeries |> Series.intersect bSeries

//             SliceSet(newKeyRanges, cIndex)


//     member _.Item
//         with get (aKey: 'a, _: All, cKey: 'c) =

//             let aSeries =
//                 match aIndex.ValueSeries.TryGetValue aKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let cSeries =
//                 match cIndex.ValueSeries.TryGetValue cKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let newKeyRanges = keyRanges |> Series.intersect aSeries |> Series.intersect cSeries

//             SliceSet(newKeyRanges, bIndex)


//     member _.Item
//         with get (_: All, bKey: 'b, cKey: 'c) =

//             let bSeries =
//                 match bIndex.ValueSeries.TryGetValue bKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let cSeries =
//                 match cIndex.ValueSeries.TryGetValue cKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let newKeyRanges = keyRanges |> Series.intersect bSeries |> Series.intersect cSeries

//             SliceSet(newKeyRanges, aIndex)


//     member _.Item
//         with get (aKey: 'a, _: All, _: All) =

//             let aSeries =
//                 match aIndex.ValueSeries.TryGetValue aKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let newKeyRanges = keyRanges |> Series.intersect aSeries

//             SliceSet2D(newKeyRanges, bIndex, cIndex)


//     member _.Item
//         with get (_: All, bKey: 'b, _: All) =

//             let bSeries =
//                 match bIndex.ValueSeries.TryGetValue bKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let newKeyRanges = keyRanges |> Series.intersect bSeries

//             SliceSet2D(newKeyRanges, aIndex, cIndex)


//     member _.Item
//         with get (_: All, _: All, cKey: 'c) =

//             let cSeries =
//                 match cIndex.ValueSeries.TryGetValue cKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let newKeyRanges = keyRanges |> Series.intersect cSeries

//             SliceSet2D(newKeyRanges, aIndex, bIndex)


//     interface System.Collections.IEnumerable with
//         member s.GetEnumerator() =
//             s.ToSeq.GetEnumerator() :> System.Collections.IEnumerator

//     interface IEnumerable<struct ('a * 'b * 'c)> with
//         member s.GetEnumerator() = s.ToSeq.GetEnumerator()


// [<Struct>]
// type SliceSet4D<'a, 'b, 'c, 'd
//     when 'a: equality
//     and 'b: equality
//     and 'c: equality
//     and 'd: equality>
//     (
//         keyRanges: Series<Units.ValueKey>,
//         aIndex: ValueIndex<'a>,
//         bIndex: ValueIndex<'b>,
//         cIndex: ValueIndex<'c>,
//         dIndex: ValueIndex<'d>
//     ) =

//     new(values: array<'a * 'b * 'c * 'd>) =
//         let values = Array.distinct values
//         let aValues = values |> Array.map (fun (a, _, _, _) -> a)
//         let bValues = values |> Array.map (fun (_, b, _, _) -> b)
//         let cValues = values |> Array.map (fun (_, _, c, _) -> c)
//         let dValues = values |> Array.map (fun (_, _, _, d) -> d)
//         let aIndex = ValueIndex.create aValues
//         let bIndex = ValueIndex.create bValues
//         let cIndex = ValueIndex.create cValues
//         let dIndex = ValueIndex.create dValues
//         let keyFilter = Series.all values.Length
//         SliceSet4D (keyFilter, aIndex, bIndex, cIndex, dIndex)

//     new(values: seq<'a * 'b * 'c * 'd>) =
//         let values = Array.ofSeq values
//         SliceSet4D values

//     member _.GetEnumerator() =
//         let aValues = aIndex.Values
//         let bValues = bIndex.Values
//         let cValues = cIndex.Values
//         let dValues = dIndex.Values

//         {
//             CurKeyRangeIdx = 0
//             CurValueKey = -1<_>
//             CurValueKeyBound = 0<_>
//             CurValue = Unchecked.defaultof<struct ('a * 'b * 'c * 'd)>
//             KeyRanges = keyRanges
//             ValueLookup = fun vk -> struct (aValues[vk], bValues[vk], cValues[vk], dValues[vk])
//         }


//     member s.ToSeq =
//         let mutable e = s.GetEnumerator()
//         Seq.unfold (fun _ -> if e.MoveNext() then Some(e.Current, ()) else None) ()


//     member _.Item
//         with get (aKey: 'a, bKey: 'b, cKey: 'c, _: All) =

//             let aSeries =
//                 match aIndex.ValueSeries.TryGetValue aKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let bSeries =
//                 match bIndex.ValueSeries.TryGetValue bKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let cSeries =
//                 match cIndex.ValueSeries.TryGetValue cKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let newKeyRanges =
//                 keyRanges
//                 |> Series.intersect aSeries
//                 |> Series.intersect bSeries
//                 |> Series.intersect cSeries

//             SliceSet(newKeyRanges, cIndex)

//     member _.Item
//         with get (aKey: 'a, bKey: 'b, _: All, dKey: 'd) =

//             let aSeries =
//                 match aIndex.ValueSeries.TryGetValue aKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let bSeries =
//                 match bIndex.ValueSeries.TryGetValue bKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let dSeries =
//                 match dIndex.ValueSeries.TryGetValue dKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let newKeyRanges =
//                 keyRanges
//                 |> Series.intersect aSeries
//                 |> Series.intersect bSeries
//                 |> Series.intersect dSeries

//             SliceSet(newKeyRanges, cIndex)

//     member _.Item
//         with get (aKey: 'a, _: All, cKey: 'c, _: All) =

//             let aSeries =
//                 match aIndex.ValueSeries.TryGetValue aKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let cSeries =
//                 match cIndex.ValueSeries.TryGetValue cKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let newKeyRanges =
//                 keyRanges
//                 |> Series.intersect aSeries
//                 |> Series.intersect cSeries

//             SliceSet2D(newKeyRanges, bIndex, dIndex)

//     member _.Item
//         with get (aKey: 'a, bKey: 'b, _: All, _: All) =

//             let aSeries =
//                 match aIndex.ValueSeries.TryGetValue aKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let bSeries =
//                 match bIndex.ValueSeries.TryGetValue bKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let newKeyRanges =
//                 keyRanges
//                 |> Series.intersect aSeries
//                 |> Series.intersect bSeries

//             SliceSet2D(newKeyRanges, cIndex, dIndex)


//     member _.Item
//         with get (_: All, _: All, cKey: 'c, _: All) =

//             let cSeries =
//                 match cIndex.ValueSeries.TryGetValue cKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let newKeyRanges =
//                 keyRanges
//                 |> Series.intersect cSeries

//             SliceSet3D(newKeyRanges, aIndex, bIndex, dIndex)

//     member _.Item
//         with get (_: All, bKey: 'b, _: All, _: All) =

//             let bSeries =
//                 match bIndex.ValueSeries.TryGetValue bKey with
//                 | true, s -> s
//                 | false, _ -> Series.empty

//             let newKeyRanges =
//                 keyRanges
//                 |> Series.intersect bSeries

//             SliceSet3D(newKeyRanges, aIndex, cIndex, dIndex)

//     interface System.Collections.IEnumerable with
//         member s.GetEnumerator() =
//             s.ToSeq.GetEnumerator() :> System.Collections.IEnumerator

//     interface IEnumerable<struct ('a * 'b * 'c * 'd)> with
//         member s.GetEnumerator() = s.ToSeq.GetEnumerator()
