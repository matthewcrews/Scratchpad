#r "nuget: Tomlyn, 0.16.2"

open Tomlyn
open Tomlyn.Model

type Buffer = {
    Key: string
}

let data = System.IO.File.ReadAllText "test.toml"

let model = Toml.ToModel data
let buffers = model["Buffers"] :?> TomlTable

for x in buffers do
    printfn $"{x.Key}, {x.Value}"



model["Constraints"]
let structure : TomlTable = 
    model["Structure"] 
    :?> TomlTable
structure["Connections"]
for x in model do
    printfn $"{x}"
model.Keys
let r = Toml.FromModel model
r