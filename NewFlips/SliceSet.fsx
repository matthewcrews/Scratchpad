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
    | Ranges of ranges: Range[]

    static member Empty = Ranges Array.empty

    member rs.Length =
        let (Ranges ranges) = rs
        ranges.Length

    member a.Intersect (b: RangeSet) =
        let (Ranges aRanges) = a
        let (Ranges bRanges) = b

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
                Ranges result
            // If there were no overlapping Ranges we just return an
            // empty RangeSet
            else
                Ranges Array.empty

        // Return the rented array
        ArrayPool.Shared.Return(resultAcc, false)
        // Return the resuling RangeSet
        result


[<Struct; RequireQualifiedAccess>]
type RangeFilter =
    | Empty
    | All
    | RangeSet of rangeSet: RangeSet

    member a.Intersect (b: RangeFilter) =
        match a, b with
        | Empty, _
        | _, Empty -> Empty
        | All, _ -> b
        | _, All -> a
        | RangeSet aRangesSet, RangeSet bRangeSet ->
            let result = aRangesSet.Intersect bRangeSet
            if result.Length > 0 then
                RangeSet result
            else
                Empty

    member a.Intersect (b: RangeSet) =
        match a with
        | Empty -> Empty
        | All -> RangeSet b
        | RangeSet aRangeSet ->
            let result = aRangeSet.Intersect b
            if result.Length > 0 then
                RangeSet result
            else
                Empty

module RangeFilter =

    let intersect (rangeSet: RangeSet) (rangeFilter: RangeFilter) =
        rangeFilter.Intersect rangeSet


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
    member r.Item
        with get k = r.RangeSets[k]


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
type SliceSet<'a when 'a: equality>(rangeFilter: RangeFilter, index: RangeSetIndex<'a>) =

    member _.GetEnumerator() =
        let values = index.Keys

        let ranges =
            match rangeFilter with
            | RangeFilter.Empty ->
                Array.empty
            | RangeFilter.All ->
                let start = 0<_>
                let length = index.Keys.Length
                let range = { Start = start; Length = length}
                [|range|]
            | RangeFilter.RangeSet (RangeSet.Ranges ranges) ->
                ranges

        {
            CurRangeIndex = 0
            CurRange = Unchecked.defaultof<_>
            CurIndex = -1<_>
            CurValue = Unchecked.defaultof<_>
            Ranges = ranges
            ValueLookup = fun index -> values[index]
        }

    member s.ToSeq =
        let mutable e = s.GetEnumerator()
        Seq.unfold (fun _ -> if e.MoveNext() then Some(e.Current, ()) else None) ()

    interface Collections.IEnumerable with
        member s.GetEnumerator() =
            s.ToSeq.GetEnumerator() :> Collections.IEnumerator

    interface IEnumerable<'a> with
        member s.GetEnumerator() = s.ToSeq.GetEnumerator()


[<Struct>]
type SliceSet2D<'a, 'b
    when 'a: equality 
    and 'a: comparison
    and 'b: equality
    and 'b: comparison>
    (
        rangeFilter: RangeFilter,
        aIndex: RangeSetIndex<'a>,
        bIndex: RangeSetIndex<'b>
    ) =

    new(values: array<'a * 'b>) =
        let values =
            values
            |> Array.distinct
            |> Array.sort
        let aValues = values |> Array.map fst
        let bValues = values |> Array.map snd
        let aIndex = RangeSetIndex.create aValues
        let bIndex = RangeSetIndex.create bValues
        let rangeFilter = RangeFilter.All
        SliceSet2D(rangeFilter, aIndex, bIndex)

    new(values: seq<'a * 'b>) =
        let values = values |> Array.ofSeq
        SliceSet2D values

    member _.GetEnumerator() =
        let aKeys = aIndex.Keys
        let bKeys = bIndex.Keys

        let ranges =
            match rangeFilter with
            | RangeFilter.Empty ->
                Array.empty
            | RangeFilter.All ->
                let start = 0<_>
                let length = aIndex.Keys.Length
                let range = { Start = start; Length = length}
                [|range|]
            | RangeFilter.RangeSet (RangeSet.Ranges ranges) ->
                ranges

        {
            CurRangeIndex = 0
            CurRange = Unchecked.defaultof<_>
            CurIndex = -1<_>
            CurValue = Unchecked.defaultof<_>
            Ranges = ranges
            ValueLookup = fun index -> struct (aKeys[index], bKeys[index])
        }


    member s.ToSeq =
        let mutable e = s.GetEnumerator()
        Seq.unfold (fun _ -> if e.MoveNext() then Some(e.Current, ()) else None) ()


    member _.Item
        with get (aKey: 'a, _: All) =
            let newRangeFilter = rangeFilter.Intersect aIndex[aKey]
            SliceSet (newRangeFilter, bIndex)


    member _.Item
        with get (_: All, bKey: 'b) =
            let newRangeFilter = rangeFilter.Intersect bIndex[bKey]
            SliceSet (newRangeFilter, aIndex)


    interface System.Collections.IEnumerable with
        member s.GetEnumerator() =
            s.ToSeq.GetEnumerator() :> System.Collections.IEnumerator

    interface IEnumerable<struct ('a * 'b)> with
        member s.GetEnumerator() = s.ToSeq.GetEnumerator()


// let s1 = SliceSet2D [
//     "a", 1
//     "a", 2
//     "a", 3
//     "b", 1
//     "b", 3
// ]

// for x in s1[All, 4] do
//     printfn $"{x}"


[<Struct>]
type SliceSet3D<'a, 'b, 'c
    when 'a: equality 
    and 'a: comparison
    and 'b: equality
    and 'b: comparison
    and 'c: equality
    and 'c: comparison>
    (
        rangeFilter: RangeFilter,
        aIndex: RangeSetIndex<'a>,
        bIndex: RangeSetIndex<'b>,
        cIndex: RangeSetIndex<'c>
    ) =

    new(values: array<'a * 'b * 'c>) =
        let values =
            values
            |> Array.distinct
            |> Array.sort
        let aValues = values |> Array.map (fun (a, _, _) -> a)
        let bValues = values |> Array.map (fun (_, b, _) -> b)
        let cValues = values |> Array.map (fun (_, _, c) -> c)
        let aIndex = RangeSetIndex.create aValues
        let bIndex = RangeSetIndex.create bValues
        let cIndex = RangeSetIndex.create cValues
        let rangeFilter = RangeFilter.All
        SliceSet3D(rangeFilter, aIndex, bIndex, cIndex)

    new(values: seq<'a * 'b * 'c>) =
        let values = Array.ofSeq values
        SliceSet3D values

    member _.GetEnumerator() =
        let aKeys = aIndex.Keys
        let bKeys = bIndex.Keys
        let cKeys = cIndex.Keys

        let ranges =
            match rangeFilter with
            | RangeFilter.Empty ->
                Array.empty
            | RangeFilter.All ->
                let start = 0<_>
                let length = aIndex.Keys.Length
                let range = { Start = start; Length = length}
                [|range|]
            | RangeFilter.RangeSet (RangeSet.Ranges ranges) ->
                ranges

        {
            CurRangeIndex = 0
            CurRange = Unchecked.defaultof<_>
            CurIndex = -1<_>
            CurValue = Unchecked.defaultof<_>
            Ranges = ranges
            ValueLookup = fun index -> struct (aKeys[index], bKeys[index], cKeys[index])
        }


    member s.ToSeq =
        let mutable e = s.GetEnumerator()
        Seq.unfold (fun _ -> if e.MoveNext() then Some(e.Current, ()) else None) ()


    member _.Item
        with get (aKey: 'a, bKey: 'b, _: All) =

            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect aIndex[aKey]
                |> RangeFilter.intersect bIndex[bKey]

            SliceSet(newRangeFilter, cIndex)


    member _.Item
        with get (aKey: 'a, _: All, cKey: 'c) =

            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect aIndex[aKey]
                |> RangeFilter.intersect cIndex[cKey]

            SliceSet(newRangeFilter, bIndex)

    member _.Item
        with get (_: All, bKey: 'b, cKey: 'c) =

            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect bIndex[bKey]
                |> RangeFilter.intersect cIndex[cKey]

            SliceSet(newRangeFilter, aIndex)


    member _.Item
        with get (aKey: 'a, _: All, _: All) =

            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect aIndex[aKey]

            SliceSet2D(newRangeFilter, bIndex, cIndex)


    member _.Item
        with get (_: All, bKey: 'b, _: All) =

            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect bIndex[bKey]

            SliceSet2D(newRangeFilter, aIndex, cIndex)


    member _.Item
        with get (_: All, _: All, cKey: 'c) =

            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect cIndex[cKey]

            SliceSet2D(newRangeFilter, aIndex, bIndex)


    interface Collections.IEnumerable with
        member s.GetEnumerator() =
            s.ToSeq.GetEnumerator() :> Collections.IEnumerator

    interface IEnumerable<struct ('a * 'b * 'c)> with
        member s.GetEnumerator() = s.ToSeq.GetEnumerator()


[<Struct>]
type SliceSet4D<'a, 'b, 'c, 'd
    when 'a: equality
    and 'a: comparison
    and 'b: equality
    and 'b: comparison
    and 'c: equality
    and 'c: comparison
    and 'd: equality
    and 'd: comparison>
    (
        rangeFilter: RangeFilter,
        aIndex: RangeSetIndex<'a>,
        bIndex: RangeSetIndex<'b>,
        cIndex: RangeSetIndex<'c>,
        dIndex: RangeSetIndex<'d>
    ) =

    new(values: array<'a * 'b * 'c * 'd>) =
        let values =
            values
            |> Array.distinct
            |> Array.sort
        let aValues = values |> Array.map (fun (a, _, _, _) -> a)
        let bValues = values |> Array.map (fun (_, b, _, _) -> b)
        let cValues = values |> Array.map (fun (_, _, c, _) -> c)
        let dValues = values |> Array.map (fun (_, _, _, d) -> d)
        let aIndex = RangeSetIndex.create aValues
        let bIndex = RangeSetIndex.create bValues
        let cIndex = RangeSetIndex.create cValues
        let dIndex = RangeSetIndex.create dValues
        let rangeFilter = RangeFilter.All
        SliceSet4D (rangeFilter, aIndex, bIndex, cIndex, dIndex)

    new(values: seq<'a * 'b * 'c * 'd>) =
        let values = Array.ofSeq values
        SliceSet4D values

    member _.GetEnumerator() =
        let aKeys = aIndex.Keys
        let bKeys = bIndex.Keys
        let cKeys = cIndex.Keys
        let dKeys = dIndex.Keys

        let ranges =
            match rangeFilter with
            | RangeFilter.Empty ->
                Array.empty
            | RangeFilter.All ->
                let start = 0<_>
                let length = aIndex.Keys.Length
                let range = { Start = start; Length = length}
                [|range|]
            | RangeFilter.RangeSet (RangeSet.Ranges ranges) ->
                ranges

        {
            CurRangeIndex = 0
            CurRange = Unchecked.defaultof<_>
            CurIndex = -1<_>
            CurValue = Unchecked.defaultof<_>
            Ranges = ranges
            ValueLookup = fun index -> struct (aKeys[index], bKeys[index], cKeys[index], dKeys[index])
        }


    member s.ToSeq =
        let mutable e = s.GetEnumerator()
        Seq.unfold (fun _ -> if e.MoveNext() then Some(e.Current, ()) else None) ()


    member _.Item
        with get (a: 'a, b: 'b, c: 'c, _: All) =

            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect aIndex[a]
                |> RangeFilter.intersect bIndex[b]
                |> RangeFilter.intersect cIndex[c]

            SliceSet(newRangeFilter, dIndex)


    member _.Item
        with get (a: 'a, b: 'b, _: All, d: 'd) =

            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect aIndex[a]
                |> RangeFilter.intersect bIndex[b]
                |> RangeFilter.intersect dIndex[d]

            SliceSet(newRangeFilter, cIndex)

    member _.Item
        with get (a: 'a, _: All, c: 'c, d: 'd) =

            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect aIndex[a]
                |> RangeFilter.intersect cIndex[c]
                |> RangeFilter.intersect dIndex[d]

            SliceSet(newRangeFilter, bIndex)

    member _.Item
        with get (_: All, b: 'b, c: 'c, d: 'd) =

            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect bIndex[b]
                |> RangeFilter.intersect cIndex[c]
                |> RangeFilter.intersect dIndex[d]

            SliceSet(newRangeFilter, aIndex)


    interface Collections.IEnumerable with
        member s.GetEnumerator() =
            s.ToSeq.GetEnumerator() :> Collections.IEnumerator

    interface IEnumerable<struct ('a * 'b * 'c * 'd)> with
        member s.GetEnumerator() = s.ToSeq.GetEnumerator()
