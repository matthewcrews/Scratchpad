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
    // | Conveyors
    // | Conversions
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

let (|IsComment|_|) (x: string) =
    match x.Trim().StartsWith "//" with
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

        | [|sources; targets|] ->
            let newSources = 
                sources.Split ','
                |> Array.map (fun source ->
                source.Trim())
            let newTargets =
                targets.Split ','
                |> Array.map (fun target ->
                    target.Trim())
            match sources.Length > 0, targets.Length > 0 with
            | true, true ->
                Ok (newSources, newTargets)
            | true, false ->
                Error $"Invalid link on line {ln}. Missing target."
            | false, true ->
                Error $"Invalid link on line {ln}. Missing source."
            | false, false ->
                Error $"Invalid link on line {ln}. Missing source and target."

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
    // | Conveyor
    // | Converison


[<NoComparison; NoEquality>]
type ParseResult =
    {
        Buffers: Dictionary<Label.Buffer, string>
        Constraints: Dictionary<Label.Constraint, string>
        Splits: Dictionary<Label.Split, string>
        Merges: Dictionary<Label.Merge, string>
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
    // | Headers.CONVEYORS ->
    //     section <- Section.Conveyors
    //     Ok ()
    // | Headers.CONVERSIONS ->
    //     section <- Section.Conversions
    //     Ok ()
    | Headers.LINKS ->
        section <- Section.Links
        Ok ()
    | _ ->
        Error $"Unknown header {header} on line {lineNumber}"


let parse (text: string) =
    let labelLines = Dictionary()
    let nameLines = Dictionary()
    let linkLines = Dictionary()
    let buffers = Dictionary()
    let constraints = Dictionary()
    let splits = Dictionary()
    let merges = Dictionary()
    let labelTypes = Dictionary<string, LabelType>()
    
    let noSource = HashSet()
    let noTarget = HashSet()

    let mutable section = Section.None
    let mutable result = Ok ()
    let mutable lineNumber = 0
    let reader = new IO.StringReader(text)
    let mutable nextLine = reader.ReadLine()

    while not (isNull nextLine) && (Result.isOk result) do

        let cleanedLine =
            if nextLine.Contains "//" then
                let commentStart = nextLine.IndexOf "//"
                nextLine.Substring (0, commentStart)
            else
                nextLine

        match cleanedLine with
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
            
            | Section.Splits ->
                result <-
                    parseLabelNamePair lineNumber "Split" line
                    |> (addLabelAndName labelLines nameLines labelTypes LabelType.Split splits Label.Split lineNumber)

            | Section.Merges ->
                result <-
                    parseLabelNamePair lineNumber "Merge" line
                    |> (addLabelAndName labelLines nameLines labelTypes LabelType.Merge merges Label.Merge lineNumber)
            
            | Section.Links ->
                result <-
                    parseLink lineNumber line
                    |> Result.bind (fun (sources, targets) ->
                        // At this point I know that Sources and Targets have a length > 0
                        match sources.Length > 1, targets.Length > 1 with
                        | true, true ->
                            Error $"Invalid link on line {lineNumber}. Cannot have multi-way links."
                        | false, false ->
                            Ok [|sources[0], targets[0]|]
                        | false, true ->
                            let source = sources[0]
                            match labelTypes.TryGetValue source with
                            | true, labelType ->
                                match labelType with
                                | LabelType.Split ->
                                    let links =
                                        targets
                                        |> Array.map (fun target ->
                                            source, target)
                                    Ok links
                                | _ ->
                                    Error $"Invalid link on line {lineNumber}. Only Split nodes can have multilple targets."

                            | false, _ ->
                                Error $"Invalid source on line {lineNumber}. The source has not been declared"
                        | true, false ->
                            let target = targets[0]
                            match labelTypes.TryGetValue target with
                            | true, labelType ->
                                match labelType with
                                | LabelType.Merge ->
                                    let links =
                                        sources
                                        |> Array.map (fun source ->
                                            source, target)
                                    Ok links
                                | _ ->
                                    Error $"Invalid link on line {lineNumber}. Only Merge nodes can have multiple sources."

                            | false, _ ->
                                Error $"Invalid target on line {lineNumber}. The target has not been declared"
                    )
                    |> Result.bind (fun (links: (string * string)[]) ->
                        
                        (Ok (), links)
                        ||> Array.fold (fun state (source, target) ->
                            
                            state
                            |> Result.bind (fun () ->
                                match labelTypes.TryGetValue source, labelTypes.TryGetValue target with
                                | (true, sourceType), (true, targetType) ->
                                    match linkLines.TryGetValue ((source, target)) with
                                    | true, prevLineNumber ->
                                        if lineNumber = prevLineNumber then
                                            Error $"Repeated link on line {lineNumber}. {source} is connected to {target} multiple times on line {prevLineNumber}"
                                        else
                                            Error $"Duplicate link on line {lineNumber}. {source} is already connected to {target} from line {prevLineNumber}"

                                    | false, _ ->
                                        
                                        match noTarget.Add source, noSource.Add target with
                                        | true, true ->
                                            linkLines[(source, target)] <- lineNumber
                                            Ok ()

                                        | true, false ->
                                            match targetType with
                                            | LabelType.Merge ->
                                                linkLines[(source, target)] <- lineNumber
                                                Ok ()
                                            | _ ->
                                                Error $"Invalid link on line {lineNumber}. The target {target} cannot have multiple sources"

                                        | false, true ->
                                            match sourceType with
                                            | LabelType.Split ->
                                                linkLines[(source, target)] <- lineNumber
                                                Ok ()
                                            | _ ->
                                                Error $"Invalid link on line {lineNumber}. The source {source} cannot have multiple targets"

                                        | false, false ->
                                            match sourceType, targetType with
                                            | LabelType.Merge, LabelType.Split ->
                                                linkLines[(source, target)] <- lineNumber
                                                Ok ()
                                            | LabelType.Merge, _ ->
                                                Error $"Invalid link on line {lineNumber}. The target {target} cannot have multiple sources"
                                            | _, LabelType.Split ->
                                                Error $"Invalid link on line {lineNumber}. The source {source} cannot have multiple targets"
                                            | _, _ ->
                                                Error $"Invalid link on line {lineNumber}. The source {source} cannot have multiple targets and the target {target} cannot have multilple sources."

                                | (false, _), (true, _) ->
                                    Error $"Invalid link on line {lineNumber}. The source {source} does not exist"

                                | (true, _), (false, _) ->
                                    Error $"Invalid link on line {lineNumber}. The target {target} does not exist"

                                | (false, _), (false, _) ->
                                    Error $"Invalid link on line {lineNumber}. The source {source} and the target {target} do not exist")))
                        
                        

        nextLine <- reader.ReadLine()
        lineNumber <- lineNumber + 1


    result
    |> Result.map (fun () ->
        {
            Buffers = buffers
            Constraints = constraints
            Splits = splits
            Merges = merges
            Links = HashSet linkLines.Keys
        })


let text = """
[Buffers]
b1: Buffer 1
b2 // My Comment
b3
b4
b5
b6

[Constraints]
// Another comment
c1

[Splits]
s1

[Merges]
m1

[Links]
b1 -> c1
c1 -> b2
s1 -> b1, b3
b4, b5 -> m1
s1 -> b6
"""

match parse text with
| Ok m -> 
    printfn $"{m}"
    for link in m.Links do
        printfn $"{link}"
| Error err -> printfn $"{err}"