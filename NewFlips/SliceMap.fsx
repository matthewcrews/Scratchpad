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

[<Struct>]
type Group = Group

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
type RangeSetEnumerator<'T> =
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



let rec loop (left: IEnumerator<KeyValuePair<'a, _>>, right: IEnumerator<KeyValuePair<'a, _>>) =
    let fastComparer = LanguagePrimitives.FastGenericComparer
    let leftKey = left.Current.Key
    let rightKey = right.Current.Key
    
    let compareResult = fastComparer.Compare (leftKey, rightKey)

    if compareResult = 0 then
        true
    elif compareResult < 0 then
        if left.MoveNext() then
            loop (left, right)
        else
            false
    else
        if right.MoveNext() then
            loop (left, right)
        else
            false


[<Struct>]
type SliceMap<'a, 'v
    when 'a: equality
    and 'a: comparison>
    (rangeFilter: RangeFilter, aIndex: RangeSetIndex<'a>, values: bar<Index, 'v>) =

    static let createLookup (keys: bar<Index, 'Key>) (values: bar<Index, 'Value>) =
        fun index ->
            let key = keys[index]
            KeyValuePair (key, values[index])

    new (pairs: seq<'a * 'v>) =
        let pairs =
            pairs
            |> Array.ofSeq
            |> Array.distinctBy fst
            |> Array.sortBy fst

        let aIndex = pairs |> Array.map fst |> RangeSetIndex.create
        let values = pairs |> Array.map snd |> bar
        SliceMap (RangeFilter.All, aIndex, values)

    member _.GetEnumerator () =
        let keys = aIndex.Keys
        let lookup = createLookup keys values

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
            ValueLookup = lookup
        }

    member s.ToSeq =
        let mutable e = s.GetEnumerator()
        Seq.unfold (fun _ -> if e.MoveNext() then Some(e.Current, ()) else None) ()

    interface Collections.IEnumerable with
        member s.GetEnumerator() =
            s.ToSeq.GetEnumerator() :> Collections.IEnumerator

    interface IEnumerable<KeyValuePair<'a, 'v>> with
        member s.GetEnumerator() = s.ToSeq.GetEnumerator()


[<Struct>]
type SliceMap2D<'a, 'b, 'v
    when 'a: equality
    and 'a: comparison
    and 'b: equality
    and 'b: comparison>
    (rangeFilter: RangeFilter, aIndex: RangeSetIndex<'a>, bIndex: RangeSetIndex<'b>, values: bar<Index, 'v>) =

    static let createLookup
        (aKeys: bar<Index, 'a>)
        (bKeys: bar<Index, 'b>)
        (values: bar<Index, 'v>) =
        fun index ->
            let aKey = aKeys[index]
            let bKey = bKeys[index]
            let compositeKey = struct (aKey, bKey)
            KeyValuePair (compositeKey, values[index])

    new (pairs: seq<'a * 'b * 'v>) =
        let pairs =
            pairs
            |> Array.ofSeq
            |> Array.distinctBy (fun (a, b, _) -> a, b)
            |> Array.sortBy (fun (a, b, _) -> a, b)

        let aIndex = pairs |> Array.map (fun (a, _, _) -> a) |> RangeSetIndex.create
        let bIndex = pairs |> Array.map (fun (_, b, _) -> b) |> RangeSetIndex.create
        let values = pairs |> Array.map (fun (_, _, v) -> v) |> bar
        SliceMap2D (RangeFilter.All, aIndex, bIndex, values)

    member _.GetEnumerator () =
        let aKeys = aIndex.Keys
        let bKeys = bIndex.Keys
        let lookup = createLookup aKeys bKeys values

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
            ValueLookup = lookup
        }

    member _.Item
        with get (a: 'a, _: All) =
            let newRangeFilter = rangeFilter.Intersect aIndex[a]
            SliceMap (newRangeFilter, bIndex, values)

    member _.Item
        with get (_: All, b: 'b) =
            let newRangeFilter = rangeFilter.Intersect bIndex[b]
            SliceMap (newRangeFilter, aIndex, values)

    member s.ToSeq =
        let mutable e = s.GetEnumerator()
        Seq.unfold (fun _ -> if e.MoveNext() then Some(e.Current, ()) else None) ()

    interface Collections.IEnumerable with
        member s.GetEnumerator() =
            s.ToSeq.GetEnumerator() :> Collections.IEnumerator

    interface IEnumerable<KeyValuePair<struct ('a * 'b), 'v>> with
        member s.GetEnumerator() = s.ToSeq.GetEnumerator()


[<Struct>]
type SliceMap3D<'a, 'b, 'c, 'v
    when 'a: equality
    and 'a: comparison
    and 'b: equality
    and 'b: comparison
    and 'c: equality
    and 'c: comparison>
    (rangeFilter: RangeFilter, aIndex: RangeSetIndex<'a>, bIndex: RangeSetIndex<'b>, cIndex: RangeSetIndex<'c>, values: bar<Index, 'v>) =

    static let createLookup
        (aKeys: bar<Index, 'a>)
        (bKeys: bar<Index, 'b>)
        (cKeys: bar<Index, 'c>)
        (values: bar<Index, 'v>) =
        fun index ->
            let aKey = aKeys[index]
            let bKey = bKeys[index]
            let cKey = cKeys[index]
            let compositeKey = struct (aKey, bKey, cKey)
            KeyValuePair (compositeKey, values[index])

    new (pairs: seq<'a * 'b * 'c * 'v>) =
        let pairs =
            pairs
            |> Array.ofSeq
            |> Array.distinctBy (fun (a, b, c, _) -> a, b, c)
            |> Array.sortBy (fun (a, b, c, _) -> a, b, c)

        let aIndex = pairs |> Array.map (fun (a, _, _, _) -> a) |> RangeSetIndex.create
        let bIndex = pairs |> Array.map (fun (_, b, _, _) -> b) |> RangeSetIndex.create
        let cIndex = pairs |> Array.map (fun (_, _, c, _) -> c) |> RangeSetIndex.create
        let values = pairs |> Array.map (fun (_, _, _, v) -> v) |> bar
        SliceMap3D (RangeFilter.All, aIndex, bIndex, cIndex, values)

    member _.GetEnumerator () =
        let aKeys = aIndex.Keys
        let bKeys = bIndex.Keys
        let cKeys = cIndex.Keys
        let lookup = createLookup aKeys bKeys cKeys values

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
            ValueLookup = lookup
        }

    member _.Item
        with get (a: 'a, _: All, _: All) =
            let newRangeFilter = rangeFilter.Intersect aIndex[a]
            SliceMap2D (newRangeFilter, bIndex, cIndex, values)

    member _.Item
        with get (_: All, b: 'b, _: All) =
            let newRangeFilter = rangeFilter.Intersect bIndex[b]
            SliceMap2D (newRangeFilter, aIndex, cIndex, values)

    member _.Item
        with get (_: All, _: All, c: 'c) =
            let newRangeFilter = rangeFilter.Intersect cIndex[c]
            SliceMap2D (newRangeFilter, aIndex, bIndex, values)

    member _.Item
        with get (a: 'a, b: 'b, _: All) =
            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect aIndex[a]
                |> RangeFilter.intersect bIndex[b]

            SliceMap (newRangeFilter, cIndex, values)

    member _.Item
        with get (a: 'a, _: All, c: 'c) =
            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect aIndex[a]
                |> RangeFilter.intersect cIndex[c]

            SliceMap (newRangeFilter, bIndex, values)

    member _.Item
        with get (_: All, b: 'b, c: 'c) =
            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect bIndex[b]
                |> RangeFilter.intersect cIndex[c]

            SliceMap (newRangeFilter, aIndex, values)

    member s.ToSeq =
        let mutable e = s.GetEnumerator()
        Seq.unfold (fun _ -> if e.MoveNext() then Some(e.Current, ()) else None) ()

    interface Collections.IEnumerable with
        member s.GetEnumerator() =
            s.ToSeq.GetEnumerator() :> Collections.IEnumerator

    interface IEnumerable<KeyValuePair<struct ('a * 'b * 'c), 'v>> with
        member s.GetEnumerator() = s.ToSeq.GetEnumerator()


[<Struct>]
type SliceMap4D<'a, 'b, 'c, 'd, 'v
    when 'a: equality
    and 'a: comparison
    and 'b: equality
    and 'b: comparison
    and 'c: equality
    and 'c: comparison
    and 'd: equality
    and 'd: comparison>
    (rangeFilter: RangeFilter, aIndex: RangeSetIndex<'a>, bIndex: RangeSetIndex<'b>, cIndex: RangeSetIndex<'c>, dIndex: RangeSetIndex<'d>, values: bar<Index, 'v>) =

    static let createLookup
        (aKeys: bar<Index, 'a>)
        (bKeys: bar<Index, 'b>)
        (cKeys: bar<Index, 'c>)
        (dKeys: bar<Index, 'd>)
        (values: bar<Index, 'v>) =
        fun index ->
            let a = aKeys[index]
            let b = bKeys[index]
            let c = cKeys[index]
            let d = dKeys[index]
            let compositeKey = struct (a, b, c, d)
            KeyValuePair (compositeKey, values[index])

    new (pairs: seq<'a * 'b * 'c * 'd * 'v>) =
        let pairs =
            pairs
            |> Array.ofSeq
            |> Array.distinctBy (fun (a, b, c, d, _) -> a, b, c, d)
            |> Array.sortBy (fun (a, b, c, d, _) -> a, b, c, d)

        let aIndex = pairs |> Array.map (fun (a, _, _, _, _) -> a) |> RangeSetIndex.create
        let bIndex = pairs |> Array.map (fun (_, b, _, _, _) -> b) |> RangeSetIndex.create
        let cIndex = pairs |> Array.map (fun (_, _, c, _, _) -> c) |> RangeSetIndex.create
        let dIndex = pairs |> Array.map (fun (_, _, _, d, _) -> d) |> RangeSetIndex.create
        let values = pairs |> Array.map (fun (_, _, _, _, v) -> v) |> bar
        SliceMap4D (RangeFilter.All, aIndex, bIndex, cIndex, dIndex, values)

    member _.GetEnumerator () =
        let aKeys = aIndex.Keys
        let bKeys = bIndex.Keys
        let cKeys = cIndex.Keys
        let dKeys = dIndex.Keys
        let lookup = createLookup aKeys bKeys cKeys dKeys values

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
            ValueLookup = lookup
        }

    member _.Item
        with get (a: 'a, _: All, _: All, _:All) =
            let newRangeFilter = rangeFilter.Intersect aIndex[a]
            SliceMap3D (newRangeFilter, bIndex, cIndex, dIndex, values)

    member _.Item
        with get (_: All, b: 'b, _: All, _: All) =
            let newRangeFilter = rangeFilter.Intersect bIndex[b]
            SliceMap3D (newRangeFilter, aIndex, cIndex, dIndex, values)

    member _.Item
        with get (_: All, _: All, c: 'c, _: All) =
            let newRangeFilter = rangeFilter.Intersect cIndex[c]
            SliceMap3D (newRangeFilter, aIndex, bIndex, dIndex, values)

    member _.Item
        with get (_: All, _: All, _: All, d: 'd) =
            let newRangeFilter = rangeFilter.Intersect dIndex[d]
            SliceMap3D (newRangeFilter, aIndex, bIndex, cIndex, values)

    member _.Item
        with get (a: 'a, b: 'b, _: All, _: All) =
            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect aIndex[a]
                |> RangeFilter.intersect bIndex[b]

            SliceMap2D (newRangeFilter, cIndex, dIndex, values)

    member _.Item
        with get (a: 'a, _: All, c: 'c, _: All) =
            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect aIndex[a]
                |> RangeFilter.intersect cIndex[c]

            SliceMap2D (newRangeFilter, bIndex, dIndex, values)

    member _.Item
        with get (_: All, b: 'b, c: 'c, _: All) =
            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect bIndex[b]
                |> RangeFilter.intersect cIndex[c]

            SliceMap2D (newRangeFilter, aIndex, dIndex, values)

    member _.Item
        with get (a: 'a, _: All, _: All, d: 'd) =
            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect aIndex[a]
                |> RangeFilter.intersect dIndex[d]

            SliceMap2D (newRangeFilter, bIndex, cIndex, values)

    member _.Item
        with get (_: All, b: 'b, _: All, d: 'd) =
            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect bIndex[b]
                |> RangeFilter.intersect dIndex[d]

            SliceMap2D (newRangeFilter, aIndex, cIndex, values)

    member _.Item
        with get (_: All, _: All, c: 'c, d: 'd) =
            let newRangeFilter =
                rangeFilter
                |> RangeFilter.intersect cIndex[c]
                |> RangeFilter.intersect dIndex[d]

            SliceMap2D (newRangeFilter, aIndex, bIndex, values)

    member s.ToSeq =
        let mutable e = s.GetEnumerator()
        Seq.unfold (fun _ -> if e.MoveNext() then Some(e.Current, ()) else None) ()

    interface Collections.IEnumerable with
        member s.GetEnumerator() =
            s.ToSeq.GetEnumerator() :> Collections.IEnumerator

    interface IEnumerable<KeyValuePair<struct ('a * 'b * 'c * 'd), 'v>> with
        member s.GetEnumerator() = s.ToSeq.GetEnumerator()


let x = SliceMap2D [
    "a", 1, 1
    "a", 2, 2
    "b", 1, 3
    "b", 2, 4
]

for KeyValue (k, v) in x["b", All] do
    printfn $"[{k}]->{v}"


let x4 = SliceMap4D [
    1, 1, 1, 1, 1.0
    1, 3, 2, 4, 2.0
    1, 4, 2, 1, 3.0
    2, 1, 2, 1, 4.0
    2, 4, 2, 4, 5.0
    2, 1, 2, 1, 6.0
    2, 3, 1, 4, 7.0
    2, 1, 1, 1, 8.0
]

for KeyValue (k, v) in x4[All, 3, All, All] do
    printfn $"Key: {k} | Value: {v}"
