type Chicken =
    {
        Name: string
        Size: float
        Age: int
        // Other fields
    }

let rng = System.Random 123
let maxAge = 10
let maxSize = 5.0

let chickens =
    [ for i in 1..100 do
        {
            Name = $"Clucky{i}"
            Size = maxSize * rng.NextDouble()
            Age = rng.Next maxAge
        }
    ]


module Units =

    [<Measure>]
    type ChickenIndex

type ChickenIndex = int<Units.ChickenIndex>

module ChickenIndex =

    let create (n: int) : ChickenIndex =
        if n < 0 then
            invalidArg (nameof n) "Cannot create negative ChickenIndex"

        n * 1<_>

open System.Collections.Generic

let chickenToIndex = Dictionary()

for chicken in chickens do

    if not (chickenToIndex.ContainsKey chicken) then
        chickenToIndex[chicken] <- ChickenIndex.create chickenToIndex.Count

module Bar =

    module private Helpers =

        let throwInvalidKeySet () =
            invalidArg "pairs" "Cannot create Bar with non-contiguous keys"

    open Helpers

    [<Struct>]
    type Bar<[<Measure>] 'Key, 'Value>(values: 'Value[]) =

        /// WARNING: public for inlining
        member _._values = values

        member inline b.Item
            with get (index: int<'Key>) =
                b._values[int index]

        member _.Length = LanguagePrimitives.Int32WithMeasure<'Key> values.Length

        static member ofSeq (pairs: seq<int<'Key> * 'Value>) : Bar<'Key, 'Value> =
            let sorted =
                pairs
                |> Seq.sortBy fst

            let mutable checkIndex = LanguagePrimitives.Int32WithMeasure<'Key> 0
            let inc = LanguagePrimitives.Int32WithMeasure<'Key> 1
            let acc = Queue ()

            for key, value in sorted do
                if key <> checkIndex then
                    throwInvalidKeySet ()

                acc.Enqueue value
                checkIndex <- checkIndex + inc

            let values = acc.ToArray()
            Bar<'Key, 'Value> values



open Bar

let indexToChickenName =
    chickenToIndex
    |> Seq.map (|KeyValue|)
    |> Seq.map (fun (key, value) -> value, key)
    |> Seq.sortBy fst
    |> Bar.ofSeq
    

let index0 = ChickenIndex.create 0
indexToChickenName[index0]