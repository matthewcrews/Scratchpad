#r "nuget: FileHelpers, 3.5.1"
#r "nuget: FSharp.Data,4.2.9"

open System
// open FileHelpers

open FSharp.Data

[<Literal>]
let ResolutionFolder = __SOURCE_DIRECTORY__

type RateData = CsvProvider<"ExampleRateData.csv", HasHeaders=true, ResolutionFolder=ResolutionFolder, Schema="DateTime, float">

let sampleData = """DateTime, Value
2020-01-01 00:00, 1.0
2020-01-01 01:00, 2.0"""

let x = RateData.Parse sampleData
for a in x.Rows do
    printfn "%A" a

[<DelimitedRecord(","); IgnoreFirst; CLIMutable>]
type RateRecord =
    {
        [<FieldConverter(ConverterKind.Date, "yyyy-MM-dd hh:mm")>]
        DateTime : DateTime
        Value : float
    }
  

let engine = FileHelperEngine<RateRecord>()
let values = engine.ReadString sampleData