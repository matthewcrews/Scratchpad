open System
open System.Collections.Generic

module rec SliceMap =

    type Filter =
        | All

    type Interval = {
        Min : int
        Max : int
    }

    type internal KeyRecord<'Key> = {
        Key : 'Key
        Interval : Interval
        NextRecordIdx : int
    }

    type internal KeyInterval<'Key> = {
        Key : 'Key
        Interval : Interval
    }

    module internal Interval =

        let overlaps (i1: seq<Interval>) (i2: seq<Interval>) =

            let rec loop (s1: IEnumerator<Interval>, s1HasValue: bool, s2: IEnumerator<Interval>, s2HasValue: bool) =

                if s1HasValue && s2HasValue then

                    if s1.Current.Max < s2.Current.Min then
                        loop (s1, s1.MoveNext (), s2, s2HasValue)
                    elif s2.Current.Max < s1.Current.Min then
                        loop (s1, s1HasValue, s2, s2.MoveNext ())
                    else
                        let nextInterval = {
                                Min = System.Math.Max (s1.Current.Min, s2.Current.Min)
                                Max = System.Math.Min (s1.Current.Max, s2.Current.Max)
                            }

                        if s1.Current.Max > s2.Current.Max then
                            Some (nextInterval, (s1, s1HasValue, s2, s2.MoveNext ()))
                        else
                            Some (nextInterval, (s1, s1.MoveNext (), s2, s2HasValue))

                else
                    None

            let s1 = i1.GetEnumerator ()
            let s2 = i2.GetEnumerator ()

            let state = (s1, s1.MoveNext (), s2, s2.MoveNext ())
            Seq.unfold loop state
    

    // This performs a Hadamard Product of two sequences. The `joiner` function is used
    // to map the higher dimensional seq `higher` to the lower dimensional seq `lower`.
    let inline internal hadamardProductDifferentDims (higher: seq<_>) (lower: seq<_>) joiner =

        let rec loop (higher: IEnumerator<_>, higherHasValue, lower: IEnumerator<_>, lowerHasValue) =

            if higherHasValue && lowerHasValue then
                let higherKey, higherValue = higher.Current
                let lowerKey, lowerValue = lower.Current

                let higherJoinKey = joiner higherKey

                if higherJoinKey = lowerKey then
                    let nextValue = higherValue * lowerValue
                    let nextReturn = higherKey, nextValue
                    let nextState = (higher, higher.MoveNext (), lower, lower.MoveNext())
                    Some (nextReturn, nextState)
                elif higherJoinKey < lowerKey then
                    loop (higher, higher.MoveNext (), lower, lowerHasValue)
                else
                    loop (higher, higherHasValue, lower, lower.MoveNext ())

            else
                None

        let higherEnumerator = higher.GetEnumerator ()
        let lowerEnumerator = lower.GetEnumerator ()
        (higherEnumerator, higherEnumerator.MoveNext (), lowerEnumerator, lowerEnumerator.MoveNext ())
        |> Seq.unfold loop
        

    // Performs the Hadamard Product on two sequences with matching dimensions.
    let inline internal hadamardProduct (a: seq<_>) (b: seq<_>) =

        let rec loop (a: IEnumerator<_>, aHasValue, b: IEnumerator<_>, bHasValue) =

            if aHasValue && bHasValue then
                let aKey, aValue = a.Current
                let bKey, bValue = b.Current

                if aKey = bKey then
                    let nextValue = aValue * bValue
                    let nextReturn = aKey, nextValue
                    let nextState = (a, a.MoveNext (), b, b.MoveNext())
                    Some (nextReturn, nextState)
                elif aKey < bKey then
                    loop (a, a.MoveNext (), b, bHasValue)
                else
                    loop (a, aHasValue, b, b.MoveNext ())

            else
                None

        let aEnumerator = a.GetEnumerator ()
        let bEnumerator = b.GetEnumerator ()
        (aEnumerator, aEnumerator.MoveNext (), bEnumerator, bEnumerator.MoveNext ())
        |> Seq.unfold loop
        


    let internal generateKeyIntervals1D (keys: _[]) =
        keys
        |> Array.mapi (fun idx key -> { Key = key; Interval = { Min = idx; Max = idx }})


    let internal generateKeyIntervals2D (keys: _[]) =

        if keys.Length = 0 then
            [||], [||]
        else

            let mutable keys1 = []
            let mutable keys2 = []
            let mutable currKey1, _ = keys.[keys.Length - 1]
            let mutable key1IntervalEndIdx = keys.Length - 1

            for idx = keys.Length - 1 downto 0 do
                let nextKey1, nextKey2 = keys.[idx]

                keys2 <- { Key = nextKey2; Interval = { Min = idx; Max = idx }} :: keys2

                if nextKey1 <> currKey1 then
                    keys1 <- { Key = currKey1; Interval = { Min = idx + 1; Max = key1IntervalEndIdx }} :: keys1
                    currKey1 <- nextKey1
                    key1IntervalEndIdx <- idx

                if idx = 0 then
                    keys1 <- { Key = currKey1; Interval = { Min = idx; Max = key1IntervalEndIdx }} :: keys1

            List.toArray keys1, List.toArray keys2


    let internal buildKeyRecords (intervals: KeyInterval<_>[]) =

        if intervals.Length = 0 then
            Array.empty
        else
            let result = Array.zeroCreate intervals.Length
            let lastRecordIndexForInterval = Dictionary ()

            for idx = intervals.Length - 1 downto 0 do
                let keyInterval = intervals.[idx]
                let lastIdx =
                    match lastRecordIndexForInterval.TryGetValue keyInterval.Key with
                    | true, lastIdx -> lastIdx
                    | false, _ -> intervals.Length // This will be beyond the bounds of the array
                lastRecordIndexForInterval.[keyInterval.Key] <- idx
                result.[idx] <- { Key = keyInterval.Key; Interval = keyInterval.Interval; NextRecordIdx = lastIdx }

            result


    type SliceMapExpr<'Key, 'Value when 'Key : comparison> internal (keyValuePairs: seq<'Key * 'Value>) =

        let keyValuePairs = keyValuePairs

        member _.KeyValuePairs = keyValuePairs
        member _.Values = keyValuePairs |> Seq.map snd

        static member ( .* ) (expr: SliceMapExpr<_,_>, sm: SliceMap<_,_>) =
            hadamardProduct expr.KeyValuePairs sm.KeyValuePairs
            |> SliceMapExpr


    type SliceMap<'Key, 'Value when 'Key : comparison> 
        internal (keyIndexRecords: KeyRecord<'Key>[], values: 'Value[], indexIntervals: seq<Interval>) =

        let keyIndexRecords = keyIndexRecords
        let values = values
        let indexIntervals = indexIntervals

        new (data: seq<'Key * 'Value>) =
            let sortedData =
                let x = 
                    data
                    |> Seq.toArray
                    |> Array.distinctBy fst
                Array.sortInPlaceBy fst x
                x

            let keyIndexRecords = 
                sortedData
                |> Array.map fst
                |> generateKeyIntervals1D
                |> buildKeyRecords
            
            let values = sortedData |> Array.map snd
            let indexIntervals = Seq.singleton { Min = 0; Max = values.Length - 1 }
            SliceMap (keyIndexRecords, values, indexIntervals)


        member internal _.KeyValuePairs : seq<'Key * 'Value> =
            let keyInterval =
                let outputSeq = Array.toSeq keyIndexRecords
                outputSeq.GetEnumerator ()
            let indexInterval = indexIntervals.GetEnumerator ()

            let rec loop (keyInterval: IEnumerator<KeyRecord<'Key>>, keyIntervalHasValue, indexInterval: IEnumerator<Interval>, indexIntervalHasValue) =

                if keyIntervalHasValue && indexIntervalHasValue then
                    
                    if keyInterval.Current.Interval.Max < indexInterval.Current.Min then
                        // The KeyInterval is behind the IndexInterval to move KeyInterval forward
                        loop (keyInterval, keyInterval.MoveNext (), indexInterval, indexIntervalHasValue)
                    elif indexInterval.Current.Max < keyInterval.Current.Interval.Min then
                        // The IndexInterval is behind the KeyInterval so move the IndexInterval forward
                        loop (keyInterval, keyIntervalHasValue, indexInterval, indexInterval.MoveNext ())
                    else
                        // The KeyInterval and the IndexInterval overlap. By definition, the Max Value of the MinIndices
                        // must be the index for the next value. Return it and move IndexInterval forward
                        let nextIdx = Math.Max (keyInterval.Current.Interval.Min, indexInterval.Current.Min)
                        let nextValue = values.[nextIdx]
                        let nextReturn = (keyInterval.Current.Key, nextValue)

                        // NOTE: Could possibly be wrong. Need to think about this
                        let nextState =
                            if keyInterval.Current.Interval.Max > indexInterval.Current.Max then
                                keyInterval, keyIntervalHasValue, indexInterval, indexInterval.MoveNext ()
                            else
                                keyInterval, keyInterval.MoveNext (), indexInterval, indexIntervalHasValue

                        Some (nextReturn, nextState)

                else
                    None

            (keyInterval, keyInterval.MoveNext (), indexInterval, indexInterval.MoveNext ())
            |> Seq.unfold loop

        member this.Values = this.KeyValuePairs |> Seq.map snd


        static member inline ( .* ) (a: SliceMap<_,_>, b: SliceMap<_,_>) =
            hadamardProduct a.KeyValuePairs b.KeyValuePairs
            |> SliceMapExpr


    type SliceMap2D<'Key1, 'Key2, 'Value when 'Key1 : comparison and 'Key2 : comparison>
        internal (key1Intervals: KeyRecord<'Key1>[], key2Intervals: KeyRecord<'Key2>[], values: 'Value[], indexIntervals: seq<Interval>) =

        let key1Intervals = key1Intervals
        let key2Intervals = key2Intervals
        let values = values
        let indexIntervals = indexIntervals

        let getIntervalsForKey (intervals: KeyRecord<_>[]) key =
            
            if intervals.Length > 0 then
                
                let rec loop idx =
                    if idx > intervals.Length - 1 then
                        None
                    else
                        let record = intervals.[idx]
                        if record.Key = key then
                            // We now just start skipping to the intervals we care about
                            let nextIdx = record.NextRecordIdx
                            Some (record.Interval, nextIdx)
                        else
                            loop (idx + 1)

                0
                |> Seq.unfold loop

            else
                Seq.empty


        new (data: seq<'Key1 * 'Key2 * 'Value>) =
            let keySelector (k1, k2, _) = k1, k2
            let sortedData =
                let x = 
                    data
                    |> Seq.toArray
                    |> Array.distinctBy keySelector
                Array.sortInPlaceBy keySelector x
                x

            let key1Intervals, key2Intervals = 
                sortedData
                |> Array.map keySelector
                |> generateKeyIntervals2D

            let key1Records = buildKeyRecords key1Intervals
            let key2Records = buildKeyRecords key2Intervals
            
            let values = sortedData |> Array.map (fun (_,_,v) -> v)
            let indexIntervals = Seq.singleton { Min = 0; Max = values.Length - 1 }
            SliceMap2D (key1Records, key2Records, values, indexIntervals)


        member this.Item
            // NOTE: Ignoring the Filter at this time
            with get (f: Filter, sliceKey: 'Key2) =
                let key2Intervals = getIntervalsForKey key2Intervals sliceKey
                let overlaps = Interval.overlaps key2Intervals indexIntervals
                SliceMap (key1Intervals, values, overlaps)

        member this.Item
            // NOTE: Ignoring the Filter at this time
            with get (sliceKey: 'Key1, f: Filter) =
                let key1Intervals = getIntervalsForKey key1Intervals sliceKey
                let overlaps = Interval.overlaps key1Intervals indexIntervals
                SliceMap (key2Intervals, values, overlaps)

// type Summer () =

//     member inline _.Sum (x: seq<(_ * _)>) =
//         let mutable acc = FSharp.Core.LanguagePrimitives.GenericZero

//         for (_, v) in x do
//             acc <- acc + v

//         acc

//     member inline _.Sum (x: seq<(_ * _ * _)>) =
//         let mutable acc = FSharp.Core.LanguagePrimitives.GenericZero

//         for (_, _, v) in x do
//             acc <- acc + v

//         acc

// let summer = Summer ()

// let inline sum< ^InputValues, ^Value, ^Result 
//                  when 'InputValues : (member Values : seq< ^Value>)
//                  and ( ^Result or ^Value) : (static member (+) : ^Result * ^Value -> ^Result)
//                  and ^Result : (static member Zero : ^Result)>
//                  (x: ^InputValues) : ^Result =

//     let mutable acc = FSharp.Core.LanguagePrimitives.GenericZero< ^Result>
//     let values = ((^InputValues) : (member Values: seq< ^Value>) x)

//     for v in values do
//         acc <- acc + v // Problem is with the right `acc`. It says it's missing a type constraint

//     acc

let inline sum x =

    let mutable acc = FSharp.Core.LanguagePrimitives.GenericZero
    let values = ((^InputValues) : (member Values: seq< ^Value>) x)

    for v in values do
        acc <- acc + v

    acc

// let x = [0..10] |> sum
module rec TestTypes =

    type Chicken = {
        Size : int
    } with
        static member ( + ) (c: Chicken, Chickens cs) : Chickens =
            Chickens (c::cs)

        static member Zero =
            Chickens.Zero

    type Chickens = Chickens of Chicken list
        with
            static member ( + ) (Chickens cs, c: Chicken) : Chickens =
                Chickens (c::cs)

            static member Zero =
                Chickens []

// open TestTypes
// let chickenList = 
//     [for i in 0 .. 5 -> i, { Size = i }]
//     |> SliceMap

// let chickenTotal = sum chickenList

open SliceMap

let x =
    [1..10]
    |> List.map (fun x -> x, float x)
    |> SliceMap

let xTotal = sum x

let xInt =
    [1..3]
    |> List.map (fun x -> x, x)
    |> SliceMap

let xIntTotal = sum xInt



let x2 =
    [for i in 1..10 do
        for j in 1..10 ->
            i, j, float (i + j)
    ] |> SliceMap2D

let x2Slice = x2.[1, All]

let r = x .* x2Slice
let r2 = sum r

let t = [1..10] |> List.sumBy (fun x -> x * x)

// let x2 =
//     [1..5]
//     |> List.map (fun x -> x, float x)
//     |> SliceMap

// let x3 =
//     [1..3]
//     |> List.map (fun x -> x, float x)
//     |> SliceMap

// let r = x .* x2 .* x3
// let p = r.KeyValuePairs |> Array.ofSeq
