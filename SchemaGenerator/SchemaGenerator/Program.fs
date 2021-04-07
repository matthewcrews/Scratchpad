// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open FSharp.Data
open System.Reflection
type Test = CsvProvider<"ExampleData.csv">

let typeMappings =
    Map [
        "int32", "BIGINT"
        "int64", "BIGINT"
        "int16", "BIGINT"
        "Double", "DECIMAL (29, 6)"
        "Single", "DECIMAL (29, 6)"
        "Decimal", "DECIMAL (29, 6)"
        "Boolean", "BIT"
    ]



[<EntryPoint>]
let main argv =
    let inputFile = "TestData.csv"
    let data = Test.Load inputFile

    let aRow = Seq.head data.Rows
    let rowType = typeof<Test.Row>
    
    for field in rowType.GetProperties() do
        let fieldValue = field.GetValue aRow
        printfn "%A" field.PropertyType.Name
        printfn "%A" fieldValue
    ////printfn "%A" data.Headers
    //printfn "%A" typeof<Test.Row>

    match data.Headers with
    | None -> invalidArg "InputFile" "Input file must have headers"
    | Some headers ->

        let columnFieldPairs =
            (headers, (rowType.GetProperties ()))
            ||> Array.zip

        let (nullableStringProperties, nonNullStringProperties) =
            columnFieldPairs
            |> Array.filter (fun (_, p) -> p.PropertyType.Name = "String")
            |> Array.partition (fun (_, field) -> data.Rows
                                                  |> Seq.exists (fun row -> String.IsNullOrEmpty ((field.GetValue row) :?> string)))

        //let doubleFields =
        //    columnFieldPairs

        let (nullDoubleProperties, nonNullDoubleProperties) =
            columnFieldPairs
            |> Array.filter (fun (_, p) -> p.PropertyType.Name = "Double")
            |> Array.partition (fun (_, field) -> data.Rows
                                                  |> Seq.exists (fun row -> System.Double.IsNaN ((field.GetValue row) :?> float)))

        let nullableFields =
            columnFieldPairs
            |> Array.filter (fun (_, p) -> p.PropertyType.Name = "Nullable`1")
            |> Array.map (fun (header, p) -> header, p, p.PropertyType.GenericTypeArguments.[0])

        let optionFields =
            columnFieldPairs
            |> Array.filter (fun (_, p) -> p.PropertyType.Name = "FSharpOption`1")
            |> Array.map (fun (header, p) -> header, p, p.PropertyType.GenericTypeArguments.[0])
            
        //let boolFields

        ()

    |> ignore

    0 // return an integer exit code