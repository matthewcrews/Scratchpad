open System

type Chicken =
    {
        Name: string
        Size: float
        Age: int
        Birthday: DateTime
        HomeTown: string
        HomeState: string
        ShipAddress: string
        // Many other fields
    }

let rng = Random 123
let originDate = DateTime (2020, 1, 1)

let chickenData =
    [|for i in 1..1_000 do
        let dayOffset = rng.Next 365
        {
            Name = $"Clucky{i}"
            Size = rng.NextDouble () * 10.0
            Age = rng.Next 10
            Birthday = originDate.AddDays dayOffset
            HomeTown = "Clucktown"
            HomeState = "OR"
            ShipAddress = "123 Cluck Way"
        }
    |]

chickenData


let summaries =
    chickenData
    |> Array.groupBy (fun row ->
        row.Age)
    |> Array.map (fun (age, rows) ->
        let avgSize =
            rows
            |> Array.averageBy (fun row ->
                row.Size)
        {| Age = age; AvgSize = avgSize |})
    |> Array.map (fun row ->
        {| row with SillyField = "Silly" |})

let x = {| Name = "Chicken"; Size = 10 |}
let y = {| Name = "Chicken"; Size = 10.0 |}
x = y//?
type Turkey =
    {
        Name: string
    }
type Goose =
    {
        Name: string
    }
let t1 = { Turkey.Name = "T" }
let g1 = { Goose.Name = "T" }
t1 = g1

