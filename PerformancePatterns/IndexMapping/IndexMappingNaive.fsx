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

type Location = Location of string

let chickenLocation = Dictionary<Chicken, Location>()

let chickenLocation : Location[] = [||]