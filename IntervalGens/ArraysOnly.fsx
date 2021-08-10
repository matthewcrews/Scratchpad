open System
open System.Collections.Generic

module rec SliceMap =

    [<Struct>]
    type IndexRange = {
        Start : int
        Length : int
    }

    let inline private hadamardProduct (l: SliceMap<_,_>, r: SliceMap<_,_>) =
        let lKeys = l.Keys.Span
        let lValues = l.Values.Span
        let rKeys = r.Keys.Span
        let rValues = r.Values.Span
        let outKeys = Array.zeroCreate l.Keys.Length
        let outValues = Array.zeroCreate r.Keys.Length

        let mutable outIdx = 0
        let mutable thisIdx = 0
        let mutable thatIdx = 0

        while thisIdx < l.Keys.Length && thatIdx < r.Keys.Length do
            let c = l.Comparer.Compare (lKeys.[thisIdx], rKeys.[thatIdx])

            if c = 0 then
                outKeys.[outIdx] <- lKeys.[thisIdx]
                outValues.[outIdx] <- lValues.[thisIdx] * rValues.[thatIdx]
                outIdx <- outIdx + 1
                thisIdx <- thisIdx + 1
                thatIdx <- thatIdx + 1
            elif c < 0 then
                thisIdx <- thisIdx + 1
            else
                thatIdx <- thatIdx + 1


        // Only want the data we actually computed
        SliceMap (ReadOnlyMemory (outKeys, 0, outIdx - 1), ReadOnlyMemory (outValues, 0, outIdx - 1))

    let inline sum (x : SliceMap<_,_>) =
        let values = x.Values.Span
        let mutable acc = LanguagePrimitives.GenericZero
        for idx = 0 to x.Values.Length - 1 do
            acc <- acc + values.[idx]
        acc


    type SliceMap<'k, 'v when 'k : comparison> internal (keys: ReadOnlyMemory<'k>, values: ReadOnlyMemory<'v>) =

        let comparer = LanguagePrimitives.FastGenericComparer<'k>
        let keys = keys
        let values = values

        new (keyValuePairs: seq<'k * 'v>) =
            let data =
                let x = Array.ofSeq keyValuePairs
                Array.sortInPlaceBy fst x
                x

            let keys = data |> Array.map fst
            let values = data |> Array.map snd
            SliceMap (ReadOnlyMemory keys, ReadOnlyMemory values)

        member internal _.Keys : ReadOnlyMemory<'k> = keys
        member internal _.Values : ReadOnlyMemory<'v> = values
        member internal _.Comparer : IComparer<'k> = comparer

        static member inline ( .* ) (l: SliceMap<_,_>, r: SliceMap<_,_>) =
            if l.Keys.Length > r.Keys.Length then
                hadamardProduct (l, r)
            else
                hadamardProduct (r, l)


open SliceMap

let x =
    [for i in 0..5 -> i, float i]
    |> SliceMap

let r = sum x