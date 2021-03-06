#r "nuget: FSharp.Data"

open FSharp.Data

type Test = CsvProvider<"ExampleData.csv">
let inputFile = "TestData.csv"
let inputFilepath = System.IO.Path.Combine(__SOURCE_DIRECTORY__, inputFile)
let data = Test.Load inputFilepath

for row in data.Rows do
    printfn "%A" row.Birthday

open System.Reflection
open Microsoft.FSharp.Reflection

let aRow = Seq.head data.Rows
let rowType = typeof<Test.Row>
printfn "%A" data.Headers
let flags = 
    BindingFlags.Public |||
    BindingFlags.Static |||
    BindingFlags.Instance |||
    BindingFlags.DeclaredOnly
let properties = aRow.GetType().GetProperties(flags)

for property in properties do
    printfn "%A" property.Name
