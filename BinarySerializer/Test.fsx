#r "nuget: MemoryPack"

open MemoryPack

[<Struct>]
type Chicken =
    {
        Name: string
        Age: int
        Size: float
    }

let mutable flock1 =
    [| for i in 1..5 do
        {
            Name = $"Chicken{i}"
            Age = i
            Size = float i
        }|]

let filePath = "test.bin"
let bin = MemoryPackSerializer.Serialize &flock1


let result =
    let bits = System.IO.File.ReadAllBytes filePath
    MemoryPackSerializer.Deserialize<Chicken[]> bits






let flock2 =
    [| for i in 1..5 do
        {
            Name = $"Chicken{i}"
            Age = i
            Size = float i
        }|]
