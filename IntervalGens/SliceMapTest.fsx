module rec SliceMap =

    open System
    open System.Collections.Generic

    type ISeeker<'Key, 'Value> =
        abstract member TryFind : 'Key -> 'Value option

    type ISliceMap<'Key, 'Value> =
        abstract member GetSeeker : unit -> ISeeker<'Key, 'Value>


    // type Dog = {
    //     Size : float
    // }

    // type Chicken = {
    //     Size : float
    // } with
    //     static member ( * ) (c: Chicken, f: float) =
    //         {
    //             Dog.Size = c.Size * f
    //         }

    //     static member ( * ) (f: float, c: Chicken) =
    //         {
    //             Dog.Size = c.Size * f
    //         }

    type Filter<'T> =
        | All
        | GreaterThan of 'T
        | LessThan of 'T

    // type ISliceMap<'Key, 'Value> = 
    //     abstract member ValuesFor : 'Key -> seq<'Value>
    //     abstract member KeyValuePairs : seq<'Key * 'Value>

    // let inline mergeSliceMaps(l: SliceMap<_, _>, r: SliceMap<_, _>) =
    //     { new ISliceMap<_, _> with
    //         member _.ValuesFor key = [l.Values.[0] * r.Values.[0]] |> Seq.ofList
    //         member _.KeyValuePairs = Seq.ofList [l.Keys.[0], (l.Values.[0] * r.Values.[0])]
    //     }

    [<NoComparison>]
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

        // Slices
        // 1D
        member this.Item
            with get filter =
                SliceMap (keys, values, filter)

        static member inline ( .* ) (a: SliceMap<_,_>, b: SliceMap<_,_>) =
            mergeSliceMaps (a, b)

open SliceMap
let x =
    [1..10]
    |> List.map (fun x -> x, float x)
    |> SliceMap

let x2 =
    [1..10]
    |> List.map (fun x -> x, { Chicken.Size = float x } )
    |> SliceMap

let z = x .* x2

// // let y = x2 .* x.[Filter.GreaterThan 2] * 10.0