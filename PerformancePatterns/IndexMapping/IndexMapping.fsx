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

open System.Collections.Generic

let chickenToIndex = Dictionary()

for chicken in chickens do

    if not (chickenToIndex.ContainsKey chicken) then
        chickenToIndex[chicken] <- chickenToIndex.Count


let indexToChickenName =
    chickenToIndex
    |> Seq.map (|KeyValue|)
    |> Seq.map (fun (key, value) -> value, key)
    |> Seq.sortBy fst
    |> Seq.map snd
    |> Array.ofSeq

let clucky1 = chickens[0]
indexToChickenName[0]
chickenToIndex[clucky1]
