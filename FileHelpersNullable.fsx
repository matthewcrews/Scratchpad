#r "nuget: FileHelpers"

open System
open FileHelpers

[<CLIMutable>]
[<IgnoreFirst(1)>] 
[<DelimitedRecord(",")>]
type Chicken = {
    Name : string
    Size : Nullable<float>
    OtherSize : Nullable<float>
}

let file = @"C:\source\personal\Scratchpad\Chickens.csv"
let engine = FileHelpers.FileHelperEngine<Chicken>()
let chickens = engine.ReadFile file

let testfunc x =
    type Monkey = {
        Name : string
    }
    x

let otherFunc x =
    let aNewType = {| Name = "My Name"|}
    let anotherType = {| aNewType with Size = 10.0 |}
    x