
#r "nuget: FSharp.Data, 4.1.1"

open FSharp.Data.CsvExtensions
open FSharp.Data

[<Literal>]
let inputFile = "ExampleDates.csv"

type TimeData = CsvProvider<inputFile, ResolutionFolder= __SOURCE_DIRECTORY__>
let data = TimeData.Load $"{__SOURCE_DIRECTORY__}\\{inputFile}"

let analysis =
    data.Rows
    |> Seq.map (fun row -> row.ExampleDateTime.Hour)
    |> Seq.groupBy id
    |> Seq.map (fun (hour, grp) -> hour, Seq.length grp)