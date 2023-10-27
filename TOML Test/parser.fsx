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
    | Merges
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
        Error $"Invalid {labelType} on line {ln}. Missing Value and Label. Format should be <{labelType} Label>: <{labelType} Name>"

    | [|_|] ->
        Error $"Invalid {labelType} on line {ln}. Missing Value or Label. Format should be <{labelType} Label>: <{labelType} Name>"

    | [|label; name|] ->
        let newName = name.Trim()
        Ok (label, newName)

    | _ ->
        Error $"Invalid {labelType} on line {ln}. Too many fields. Format should be <{labelType} Label>: <{labelType} Name>"

let parseLink (ln: int) (s: string) =
    if s.Contains "->" then
        match s.Split "->" with
        | [||] ->
            Error $"Invalid link on line {ln}. Missing source and target"

        | [|_|] ->
            Error $"Invalid link on line {ln}. Missing source or target"

        | [|source; target|] ->
            let newSource = source.Trim()
            let newTarget = target.Trim()
            Ok (newSource, newTarget)

        | _ ->
            Error $"Invalid link on line {ln}. Too many connections"

    else
        Error $"Link missing arrow `->` on line {ln}"


[<RequireQualifiedAccess>]
module Label =

    [<Struct>] type Buffer      = Buffer of string
    [<Struct>] type Constraint  = Constraint of string
    [<Struct>] type Split       = Split of string
    [<Struct>] type Merge       = Merge of string
    [<Struct>] type Conveyor    = Conveyor of string
    [<Struct>] type Conversion  = Conversion of string


[<Struct>]
type LabelType =
    | Buffer
    | Constraint
    | Split
    | Merge
    | Conveyor
    | Converison


[<NoComparison; NoEquality>]
type ParseResult =
    {
        Buffers: Dictionary<Label.Buffer, string>
        Constraints: Dictionary<Label.Constraint, string>
        // Merges: Dictionary<string, string>
        Links: HashSet<string * string>
    }


let addLabel (labels: HashSet<_>) (names: Dictionary<_,_>) labeler lineNumber input =
    input
    |> Result.bind (fun (label: string, name: string) ->
        match labels.Add label with
        | true ->
            names[labeler label] <- name
            Ok ()
        | false ->
            Error $"Duplicate label {label} on line {lineNumber}")


let parse (text: string) =
    let labels = HashSet()
    let links = HashSet()
    let buffers = Dictionary()
    let constraints = Dictionary()
    // let merges = Dictionary()
    let labelTypes = Dictionary<string, LabelType>()
    
    let mutable section = Section.None
    let mutable result = Ok ()
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
        | Headers.LINKS ->
            section <- Section.Links
        | line ->
            match section with
            | Section.None ->
                result <- Error $"Missing section header for line {lineNumber}"
            
            | Section.Buffers ->
                result <-
                    parseLabelNamePair lineNumber "Buffer" line
                    |> (addLabel labels buffers Label.Buffer lineNumber)
                    // |> Result.bind (fun (label, name) ->
                    //     match labels.Add label with
                    //     | true ->
                    //         buffers[Label.Buffer label] <- name
                    //         Ok ()
                    //     | false ->
                    //         Error $"Duplicate definition of label {label} on line {lineNumber}")
            
            | Section.Constraints ->
                result <-
                    parseLabelNamePair lineNumber "Constraint" line
                    |> Result.bind (fun (label, name) ->
                        match labels.Add label with
                        | true ->
                            constraints[Label.Constraint label] <- name
                            Ok ()
                        | false ->
                            Error $"Duplicate definition of label {label} on line {lineNumber}")

            // | Section.Merges ->
            //     result <-
            //         parseLabelNamePair lineNumber "Merges" line
            //         |> Result.bind (fun (label, name) ->
            //             match labels.Add label with
            //             | true ->
            //                 merges[Label.Merge label] <- name
            //                 Ok ()
            //             | false ->
            //                 Error $"Duplicate definition of Merge label {label} on line {lineNumber}")
        
            | Section.Links ->
                result <-
                parseLink lineNumber line
                |> Result.bind (fun (source, target) ->
                    match labels.Contains source, labels.Contains target with
                    | true, true ->
                        match links.Add (source, target) with
                        | true ->
                            Ok ()
                        | false ->
                            Error $"Repeated link on line {lineNumber}. {source} is already connected to {target}"
                    | false, true ->
                        Error $"Invalid link on line {lineNumber}. The source {source} does not exist"
                    | true, false ->
                        Error $"Invalid link on line {lineNumber}. The target {target} does not exist"
                    | false, false ->
                        Error $"Invalid link on line {lineNumber}. The source {source} and the target {target} do not exist")

        nextLine <- reader.ReadLine()
        lineNumber <- lineNumber + 1


    result
    |> Result.map (fun () ->
        {
            Buffers = buffers
            Constraints = constraints
            // Merges = merges
            Links = links
        })


let text = """
[Buffers]
b1: Buffer 1
b2: Buffer 2
b3: Buffer 3

[Constraints]
c1: Constraint 1

[Links]
b1 -> c1
c1 -> b2

"""

let parseResult = parse text
parseResult