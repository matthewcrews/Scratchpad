open System
open System.Collections.Generic

module Headers =

    [<Literal>]
    let BUFFERS = "[Buffers]"
    [<Literal>]
    let CONSTRAINTS = "[Constraints]"
    [<Literal>]
    let MERGES = "[Merges]"
    [<Literal>]
    let SPLIT ="[Splits]"
    [<Literal>]
    let CONVEYORS = "[Conveyors]"
    [<Literal>]
    let CONVERSIONS = "[Conversions]"
    [<Literal>]
    let LINKS = "[Links]"


[<RequireQualifiedAccess>]
type Section =
    | Buffers
    | Constraints
    | Links
    | None

let (|IsWhitespace|_|) (x: string) =
    match String.IsNullOrWhiteSpace x with
    | true -> Some ()
    | false -> None

let parseLabelNamePair (ln: int) (labelType: string) (b: string) =
    let parts = b.Split ':'
    match parts with
    | [||] ->
        Result.Error ($"Invalid {labelType} on line {ln}. Missing Value and Label. Format should be <{labelType} Label>: <{labelType} Name>")

    | [|_|] ->
        Result.Error ($"Invalid {labelType} on line {ln}. Missing Value or Label. Format should be <{labelType} Label>: <{labelType} Name>")

    | [|label; name|] ->
        let newName = name.Trim()
        Result.Ok (label, newName)
    | _ ->
        Result.Error ($"Invalid {labelType} on line {ln}. Too many fields. Format should be <{labelType} Label>: <{labelType} Name>")

[<NoComparison; NoEquality>]
type ParseResult =
    {
        Buffers: Dictionary<string, string>
        Constraints: Dictionary<string, string>
    }

let parse (text: string) =
    let buffers = Dictionary()
    let constraints = Dictionary()
    let mutable section = Section.None
    let mutable result = Result.Ok ()
    let mutable lineNumber = 0
    let reader = new IO.StringReader(text)
    let mutable nextLine = reader.ReadLine()

    while not (isNull nextLine) && (Result.isOk result) do

        match nextLine with
        | IsWhitespace -> 
            printfn "HERE"
            ()
        | Headers.BUFFERS ->
            section <- Section.Buffers
        | Headers.CONSTRAINTS ->
            section <- Section.Constraints
        | line ->
            match section with
            | Section.None ->
                result <- Error ($"Missing section header for line {lineNumber}")
            | Section.Buffers ->
                result <-
                    parseLabelNamePair lineNumber "Buffer" line
                    |> Result.map (fun (label, name) ->
                        buffers[label] <- name)
            | Section.Constraints ->
                result <-
                    parseLabelNamePair lineNumber "Constraint" line
                    |> Result.map (fun (label, name) ->
                        constraints[label] <- name)
        
        nextLine <- reader.ReadLine()
        lineNumber <- lineNumber + 1

    result
    |> Result.map (fun () ->
        {
            Buffers = buffers
            Constraints = constraints
        })


let text = """
[Buffers]
b1: Buffer 1
b2: Buffer 2
b3: Buffer 3
"""

let parseResult = parse text
parseResult