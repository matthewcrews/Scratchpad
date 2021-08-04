open System
open System.Collections.Generic

module rec SliceMap =

    type Filter<'T> =
        | All
        | GreaterThan of 'T
        | LessThan of 'T

    type Interval = {
        MinIdx : int
        MaxIdx : int
    }

    type internal IndexRecord<'Key> = {
        Key : 'Key
        Interval : Interval
        NextRecordIdx : int
    }

    type internal KeyInterval<'Key> = {
        Key : 'Key
        Interval : Interval
    }

    let internal generateKeyIntervals1D (keys: _[]) =
        keys
        |> Array.mapi (fun idx key -> { Key = key; Interval = { MinIdx = idx; MaxIdx = idx }})
        |> List.ofArray


    let internal generateKeyIntervals2D (keys: _[]) =

        if keys.Length = 0 then
            [], []
        else

            let mutable keys1 = []
            let mutable keys2 = []
            let mutable currKey1, _ = keys.[keys.Length - 1]
            let mutable key1IntervalEndIdx = keys.Length - 1

            for idx = keys.Length - 1 downto 0 do
                let nextKey1, nextKey2 = keys.[idx]

                keys2 <- { Key = nextKey2; Interval = { MinIdx = idx; MaxIdx = idx }} :: keys2

                if nextKey1 <> currKey1 then
                    keys1 <- { Key = currKey1; Interval = { MinIdx = idx + 1; MaxIdx = key1IntervalEndIdx }} :: keys1
                    currKey1 <- nextKey1
                    key1IntervalEndIdx <- idx

                if idx = 0 then
                    keys1 <- { Key = currKey1; Interval = { MinIdx = idx; MaxIdx = key1IntervalEndIdx }} :: keys1

            keys1, keys2


    let internal buildKeyRecords (intervals: KeyInterval<_> list) =

        if intervals.IsEmpty then
            Array.empty

        else
            let x = List.toArray intervals
            let result = Array.zeroCreate x.Length
            let lastRecordIndexForInterval = Dictionary ()

            for idx = x.Length - 1 downto 0 do
                let keyInterval = x.[idx]
                let lastIdx =
                    match lastRecordIndexForInterval.TryGetValue keyInterval.Key with
                    | true, lastIdx -> lastIdx
                    | false, _ -> -1
                lastRecordIndexForInterval.[keyInterval.Key] <- idx
                result.[idx] <- { Key = keyInterval.Key; Interval = keyInterval.Interval; NextRecordIdx = lastIdx }

            result

    type SliceMapExpression<'Key, 'Value when 'Key : comparison>(getKeyValuePairs: unit -> seq<KeyValuePair<'Key, 'Value>>) =

        let getKeyValuePairs = getKeyValuePairs

        member _.GetKeyValuePairs () = getKeyValuePairs ()

        // TODO: Define ( .* )


    type SliceMap<'Key, 'Value when 'Key : comparison> 
        private (keyIndexRecords: IndexRecord<'Key>[], values: 'Value[], filter: Filter<'Key>, indexIntervals: seq<Interval>) =

        let keyIndexRecords = keyIndexRecords
        let values = values
        let filter = filter
        let indexIntervals = indexIntervals

        let getKeyIntervalEnumerator () =
            let outputSeq =
                match filter with
                | Filter.All -> Array.toSeq keyIndexRecords
                | Filter.GreaterThan x -> keyIndexRecords |> Array.toSeq |> Seq.where (fun record -> record.Key > x)
                | Filter.LessThan x -> keyIndexRecords |> Array.toSeq |> Seq.where (fun record -> record.Key < x)

            outputSeq.GetEnumerator ()

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
            let indexIntervals = Seq.singleton { MinIdx = 0; MaxIdx = values.Length - 1 }
            SliceMap (keyIndexRecords, values, Filter.All, indexIntervals)


        member internal _.GetKeyValuePairs () =
            let keyInterval = getKeyIntervalEnumerator ()
            let indexInterval = indexIntervals.GetEnumerator ()

            let rec loop (keyInterval: IEnumerator<IndexRecord<'Key>>, keyIntervalHasValue, indexInterval: IEnumerator<Interval>, indexIntervalHasValue) =

                if keyIntervalHasValue && indexIntervalHasValue then
                    
                    if keyInterval.Current.Interval.MaxIdx < indexInterval.Current.MinIdx then
                        // The KeyInterval is behind the IndexInterval to move KeyInterval forward
                        loop (keyInterval, keyInterval.MoveNext (), indexInterval, indexIntervalHasValue)
                    elif indexInterval.Current.MaxIdx < keyInterval.Current.Interval.MinIdx then
                        // The IndexInterval is behind the KeyInterval so move the IndexInterval forward
                        loop (keyInterval, keyIntervalHasValue, indexInterval, indexInterval.MoveNext ())
                    else
                        // The KeyInterval and the IndexInterval overlap. By definition, the Max Value of the MinIndices
                        // must be the index for the next value. Return it and move IndexInterval forward
                        let nextIdx = Math.Max (keyInterval.Current.Interval.MinIdx, indexInterval.Current.MinIdx)
                        let nextValue = values.[nextIdx]
                        let nextReturn = KeyValuePair (keyInterval.Current.Key, nextValue)

                        // TODO: need to move the correct thing forward
                        let nextState =
                            if keyInterval.Current.Interval.MaxIdx > indexInterval.Current.MaxIdx then
                                keyInterval, keyIntervalHasValue, indexInterval, indexInterval.MoveNext ()
                            else
                                keyInterval, keyInterval.MoveNext (), indexInterval, indexIntervalHasValue

                        Some (nextReturn, nextState)

                else
                    None

            (keyInterval, keyInterval.MoveNext (), indexInterval, indexInterval.MoveNext ())
            |> Seq.unfold loop


        member internal _.KeyIndexRecords = keyIndexRecords
        member internal _.Values = values
        member internal _.Filter = filter
        member internal _.IndexIntervals = indexIntervals

        // Slices
        // 1D
        member this.Item
            with get filter : SliceMapExpression<_, _> =
                let newSm = SliceMap (this.KeyIndexRecords, this.Values, filter, this.IndexIntervals)
                SliceMapExpression newSm.GetKeyValuePairs

        // static member inline ( .* ) (a: SliceMap<_,_>, b: SliceMap<_,_>) =
        //     mergeSliceMaps (a, b)

open SliceMap
let x =
    [1..10]
    |> List.map (fun x -> x, float x)
    |> SliceMap

let y = x.[GreaterThan 5]
let a = y.GetKeyValuePairs () |> Array.ofSeq
a

// let x2 =
//     [1..10]
//     |> List.map (fun x -> x, { Chicken.Size = float x } )
//     |> SliceMap

// let z = x .* x2

// // let y = x2 .* x.[Filter.GreaterThan 2] * 10.0