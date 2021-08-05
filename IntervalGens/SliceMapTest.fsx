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

    type internal KeyRecord<'Key> = {
        Key : 'Key
        Interval : Interval
        NextRecordIdx : int
    }

    type internal KeyInterval<'Key> = {
        Key : 'Key
        Interval : Interval
    }
    
    let inline internal hadamardProduct1D (a: seq<_,_>) (b: seq<_,_>) =

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

        let result () =
            let aEnumerator = a.GetEnumerator ()
            let bEnumerator = b.GetEnumerator ()
            (aEnumerator, aEnumerator.MoveNext (), bEnumerator, bEnumerator.MoveNext ())
            |> Seq.unfold loop
        
        SliceMap1DExpression result


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


    type SliceMap1DExpression<'Key, 'Value when 'Key : comparison>(getKeyValuePairs: unit -> seq<'Key * 'Value>) =

        let getKeyValuePairs = getKeyValuePairs

        member _.GetKeyValuePairs () = getKeyValuePairs ()

        // TODO: Define ( .* )


    type SliceMap<'Key, 'Value when 'Key : comparison> 
        private (keyIndexRecords: KeyRecord<'Key>[], values: 'Value[], filter: Filter<'Key>, indexIntervals: seq<Interval>) =

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


        member internal _.GetKeyValuePairs () : seq<'Key * 'Value> =
            let keyInterval = getKeyIntervalEnumerator ()
            let indexInterval = indexIntervals.GetEnumerator ()

            let rec loop (keyInterval: IEnumerator<KeyRecord<'Key>>, keyIntervalHasValue, indexInterval: IEnumerator<Interval>, indexIntervalHasValue) =

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
                        let nextReturn = (keyInterval.Current.Key, nextValue)

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
            with get filter : SliceMap1DExpression<_, _> =
                let newSm = SliceMap (this.KeyIndexRecords, this.Values, filter, this.IndexIntervals)
                SliceMap1DExpression newSm.GetKeyValuePairs

        static member inline ( .* ) (a: SliceMap<_,_>, b: SliceMap<_,_>) =
            hadamardProduct1D (a.GetKeyValuePairs ()) (b.GetKeyValuePairs ())



open SliceMap

let x =
    [1..10]
    |> List.map (fun x -> x, float x)
    |> SliceMap

let x2 =
    [1..5]
    |> List.map (fun x -> x, float x)
    |> SliceMap


let r = x .* x2
let p = r.GetKeyValuePairs () |> Array.ofSeq
