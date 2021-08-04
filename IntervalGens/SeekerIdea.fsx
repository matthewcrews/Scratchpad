
open System
open System.Collections.Generic

module rec SliceMaps =

    type ISeeker<'Key, 'Value> =
        abstract member TryFind : 'Key -> 'Value option

    type ISliceMap<'Key, 'Value> =
        abstract member GetSeeker : unit -> ISeeker<'Key, 'Value>

    type Filter<'T> =
        | All
        | GreaterThan of 'T
        | LessThan of 'T

    type SliceMapSeeker<'Key, 'Value when 'Key : comparison>(keys: 'Key[], values: 'Value[], filter: Filter<'Key>) =
        let keys = keys
        let values = values
        let filter = filter        
        let mutable currIdx = 0
        let mutable minIdx = 0
        let mutable maxIdx = keys.Length - 1

        do 
            match filter with
            | All -> ()
            | GreaterThan x ->
                let index = Array.BinarySearch (keys, x)
                if index >= 0 then`
                    minIdx <- index
                else
                    minIdx <- ~~~ index
            | LessThan x ->
                let index = Array.BinarySearch (keys, x)
                if index >= 0 then
                    maxIdx <- index
                else
                    maxIdx <- ~~~ index
        
        let seek key =
            let index = Array.BinarySearch (sm.Keys, key)


        interface ISeeker<'Key, 'Value> with

            member _.TryFind key =
                match sm.Filter with
                | All -> seek key
                | GreaterThan x -> if key < x then None else seek key
                | LessThan x -> if key > x then None else seek key

    type SliceMap<'Key, 'Value when 'Key : comparison> 
        private (keys: 'Key[], values: 'Value[], filter: Filter<'Key>) =

        let keys = keys
        let values = values
        let filter = filter

        new (data: seq<'Key * 'Value>) =
            let sortedData =
                let x = 
                    data
                    |> Seq.toArray
                    |> Array.distinctBy fst
                Array.sortInPlaceBy fst x
                x

            let keys = sortedData |> Array.map fst
            let values = sortedData |> Array.map snd
            SliceMap (keys, values, Filter.All)

        member internal _.Keys : 'Key[] = keys
        member internal _.Values : 'Value[] = values
        member internal _.Filter = filter

        interface ISliceMap with
            member this.GetSeeker () =
                { new ISeeker<'Key, 'Value> with
                    member this.TryFind key =

                }



        // Slices
        // 1D
        member this.Item
            with get filter =
                SliceMap (keys, values, filter)

        // static member inline ( .* ) (a: SliceMap<_,_>, b: SliceMap<_,_>) =
        //     mergeSliceMaps (a, b)

