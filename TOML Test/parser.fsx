open System
open System.Collections.Generic

module Headers =

    [<Literal>]
    let BUFFERS = "[BUFFERS]"
    [<Literal>]
    let CONSTRAINTS = "[CONSTRAINTS]"
    [<Literal>]
    let MERGES = "[MERGES]"
    [<Literal>]
    let SPLITS ="[SPLITS]"
    [<Literal>]
    let CONVEYORS = "[CONVEYORS]"
    [<Literal>]
    let CONVERSIONS = "[CONVERSIONS]"
    [<Literal>]
    let LINKS = "[LINKS]"


[<RequireQualifiedAccess>]
type Section =
    | Buffers
    | Constraints
    | Splits
    | Merges
    | Conveyors
    | Conversions
    | Links
    | None

let (|IsWhitespace|_|) (x: string) =
    match String.IsNullOrWhiteSpace x with
    | true -> Some ()
    | false -> None

let (|IsHeader|_|) (x: string) =
    match x.Contains '[' && x.Contains ']' with
    | true -> Some ()
    | false -> None

let parseLabelNamePair (ln: int) (labelType: string) (b: string) =
    let parts = b.Split ':'
    match parts with
    | [||] ->
        Error $"Invalid {labelType} on line {ln}. Missing Value and Label. Format should be <{labelType} Label>: <{labelType} Name>"

    | [|labelName|] ->
        let newLabelName = labelName.Trim()
        Ok (newLabelName, newLabelName)

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


[<Struct; RequireQualifiedAccess>]
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


let addLabelAndName
    (labelLines: Dictionary<_,_>)
    (nameLines: Dictionary<_,_>)
    (labelTypes: Dictionary<_,_>)
    labelType
    (labelNames: Dictionary<_,_>)
    labeler
    lineNumber
    input
    =
    
    input
    |> Result.bind (fun (label: string, name: string) ->
        match labelLines.TryGetValue label, nameLines.TryGetValue name with
        | (false, _), (false, _) ->
            labelTypes[label] <- labelType
            labelNames[labeler label] <- name
            labelLines[label] <- lineNumber
            nameLines[name] <- lineNumber
            Ok ()
        | (false, _), (true, prevLineNumber) ->
            Error $"Duplicate name {name} on line {lineNumber}. Name previously declared on line {prevLineNumber}"

        | (true, prevLineNumber), (false, _) ->
            Error $"Duplicate label {label} on line {lineNumber}. Label previously declared on line {prevLineNumber}"

        | (true, labelPrevLineNumber), (true, namePrevLineNumber) ->
            Error $"Duplicate label {label} and name {name} on line {lineNumber}. Label previously declared on line {labelPrevLineNumber}. Name previously declared on line {namePrevLineNumber}")

let parseHeader
    lineNumber
    (section: byref<Section>)
    (header: string)
    =

    match header.ToUpper() with
    | Headers.BUFFERS ->
        section <- Section.Buffers
        Ok ()
    | Headers.CONSTRAINTS ->
        section <- Section.Constraints
        Ok ()
    | Headers.SPLITS ->
        section <- Section.Splits
        Ok ()
    | Headers.MERGES ->
        section <- Section.Merges
        Ok ()
    | Headers.CONVEYORS ->
        section <- Section.Conveyors
        Ok ()
    | Headers.CONVERSIONS ->
        section <- Section.Conversions
        Ok ()
    | Headers.LINKS ->
        section <- Section.Links
        Ok ()
    | _ ->
        Error $"Unknown header {header} on line {lineNumber}"


let parse (text: string) =
    let labelLines = Dictionary()
    let nameLines = Dictionary()
    let links = HashSet()
    let buffers = Dictionary()
    let constraints = Dictionary()
    // let merges = Dictionary()
    let labelTypes = Dictionary<string, LabelType>()
    
    let hasSource = HashSet()
    let hasTarget = HashSet()

    let mutable section = Section.None
    let mutable result = Ok ()
    let mutable lineNumber = 0
    let reader = new IO.StringReader(text)
    let mutable nextLine = reader.ReadLine()

    while not (isNull nextLine) && (Result.isOk result) do

        match nextLine with
        | IsWhitespace -> 
            ()

        | IsHeader ->
            result <- parseHeader lineNumber &section nextLine

        | line ->
            match section with
            | Section.None ->
                result <- Error $"Missing section header for line {lineNumber}"
            
            | Section.Buffers ->
                result <-
                    parseLabelNamePair lineNumber "Buffer" line
                    |> (addLabelAndName labelLines nameLines labelTypes LabelType.Buffer buffers Label.Buffer lineNumber)
            
            | Section.Constraints ->
                result <-
                    parseLabelNamePair lineNumber "Constraint" line
                    |> (addLabelAndName labelLines nameLines labelTypes LabelType.Constraint constraints Label.Constraint lineNumber)

            | Section.Links ->
                result <-
                    parseLink lineNumber line
                    |> Result.bind (fun (source, target) ->
                        match labelTypes[source] with
                        | LabelType.Buffer ->
                            match hasTarget.Add source with
                            | true -> 
                                Ok (source, target)
                            | false ->
                                Error $"Invalid link on line {lineNumber}. Buffer {source} already has target."
                        | LabelType.Constraint ->
                            match hasTarget.Add source with
                            | true -> 
                                Ok (source, target)
                            | false ->
                                Error $"Invalid link on line {lineNumber}. Constraint {source} already has target.")
                    |> Result.bind (fun (source, target) ->
                        match labelTypes[target] with
                        | LabelType.Buffer ->
                            match hasSource.Add target with
                            | true -> 
                                Ok (source, target)
                            | false ->
                                Error $"Invalid link on line {lineNumber}. Buffer {target} already has source."

                        | LabelType.Constraint ->
                            match hasSource.Add target with
                            | true -> 
                                Ok (source, target)
                            | false ->
                                Error $"Invalid link on line {lineNumber}. Constraint {target} already has source.")
                    |> Result.bind (fun (source, target) ->
                        match labelLines.ContainsKey source, labelLines.ContainsKey target with
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
b3

[Constraints]
c1

[Links]
b1 -> c1
c1 -> b2
b2 -> b3
"""

match parse text with
| Ok m -> printfn $"{m}"
| Error err -> printfn $"{err}"