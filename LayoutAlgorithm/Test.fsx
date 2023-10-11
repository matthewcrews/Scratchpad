open System.Collections.Generic
open System.Collections.ObjectModel

[<RequireQualifiedAccess>]
type Elem =
    | Node of string
    | Label of string

[<Struct>]
type Position =
    {
        X: float
        Y: float
    }

[<NoComparison; NoEquality>]
type Layout =
    {
        ElemPositions: ReadOnlyDictionary<Elem, Position>
        LabelPositions: ReadOnlyDictionary<string, Position>
        Links: list<Elem * Elem>
    }


let toDomain
    (linksAndLabels: list<(string * string) * string>)
    =

    linksAndLabels
    |> List.map (fun ((source, target), label) ->
        (Elem.Node source, Elem.Node target), label)


let getSourcesAndTargets (links: seq<Elem * Elem>) =
    let sources = Dictionary<_, Stack<_>>()
    let targets = Dictionary<_, Stack<_>>()

    for source, target in links do
        match targets.TryGetValue source with
        | true, acc -> acc.Push target
        | false, _ ->
            let newStack = Stack()
            newStack.Push target
            targets[source] <- newStack

        match sources.TryGetValue target with
        | true, acc -> acc.Push source
        | false, _ ->
            let newStack = Stack ()
            newStack.Push source
            sources[target] <- newStack

    let toReadOnly (d: Dictionary<_, Stack<_>>) =
        d
        |> Seq.map (|KeyValue|)
        |> Seq.map (fun (target, sources) ->
            target, sources.ToArray())
        |> readOnlyDict


    let sources = toReadOnly sources
    let targets = toReadOnly targets

    sources, targets


let calculateColumnAssignments
    (sources: IReadOnlyDictionary<Elem, Elem[]>)
    (targets: IReadOnlyDictionary<Elem, Elem[]>)
    =

    let acc = Stack ()
    let columnAssignments = Dictionary()

    // Seed the stack with the root nodes to compute the Column assignments
    targets
    |> Seq.map ((|KeyValue|) >> fst)
    |> Seq.filter (fun source ->
        not (sources.ContainsKey source))
    |> Seq.iter (fun n ->
        acc.Push (n, 0))

    while acc.Count > 0 do

        let node, column = acc.Pop()

        let colAssignment = 
            match columnAssignments.TryGetValue node with
            | true, curCol ->
                // Push the Node out
                if column > curCol then
                    column
                else
                    curCol
            | false, _ ->
                column
                
        columnAssignments[node] <- colAssignment

        match targets.TryGetValue node with
        | true, nodeTargets ->
            for nextNode in nodeTargets do
                acc.Push (nextNode, column + 1)
        | false, _ ->
            ()

    columnAssignments
    |> Seq.map (|KeyValue|)
    |> readOnlyDict



let insertLinks
    (colAssignments: IReadOnlyDictionary<Elem, int>)
    (linksAndLabels: list<(Elem * Elem) * string>)
    =
    // We now need to insert labels when Nodes are not in adjacent columns
    let newLinks = Stack ()
    for (source, target), label in linksAndLabels do

        let sourceCol = colAssignments[source]
        let targetCol = colAssignments[target]

        if targetCol > sourceCol + 1 then
            newLinks.Push ((source, Elem.Label label), None)
            newLinks.Push ((Elem.Label label, target), None)
        else
            newLinks.Push ((source, target), Some label)

    List.ofSeq newLinks

let computeRowAssignments
    (targets: IReadOnlyDictionary<Elem, Elem[]>)
    (sources: IReadOnlyDictionary<Elem, Elem[]>)
    (colAssignments: IReadOnlyDictionary<Elem, int>)
    =

    let colCounts = Dictionary<int, int>()
    let rowAssignments = Dictionary<Elem, int>()
    let acc = Stack()

    targets
    |> Seq.map ((|KeyValue|) >> fst)
    |> Seq.filter (fun elem ->
        not (sources.ContainsKey elem))
    |> Seq.iter (fun elem ->
        acc.Push elem)

    while acc.Count > 0 do
        let elem = acc.Pop()

        if not (rowAssignments.ContainsKey elem) then
            let col = colAssignments[elem]
            match colCounts.TryGetValue col with
            | true, colCount ->
                rowAssignments[elem] <- colCount
                colCounts[col] <- colCount + 1
            | false, _ ->
                rowAssignments[elem] <- 0
                colCounts[col] <- 1

            match targets.TryGetValue elem with
            | true, targets ->
                for target in targets do
                    acc.Push target
            | false, _ ->
                ()

    ReadOnlyDictionary rowAssignments, ReadOnlyDictionary colCounts

// let adjustColCounts
//     (colCounts: ReadOnlyDictionary<int, int>)
//     (targets: IReadOnlyDictionary<Elem, Elem[]>)
//     (sources: IReadOnlyDictionary<Elem, Elem[]>)
//     (colAssignments: IReadOnlyDictionary<Elem, int>)
//     =





let calculatePositions
    (colCounts: IReadOnlyDictionary<int, int>)
    (colAssignments: IReadOnlyDictionary<Elem, int>)
    (rowAssignments: IReadOnlyDictionary<Elem, int>)
    (links: seq<Elem*Elem>)
    =

    let height =
        colCounts
        |> Seq.map ((|KeyValue|) >> snd)
        |> Seq.max
        |> float

    let positions = Dictionary ()

    let calculatePosition elem =
        let col = colAssignments[elem]
        let row = float rowAssignments[elem]
        let colCount = float colCounts[col]
        let rowHeight = height / colCount
        let y = row * rowHeight + rowHeight / 2.0
        let x = 0.5 + float col
        { X = x; Y = y}

    for source, target in links do

        if not (positions.ContainsKey source) then
            let newPosition = calculatePosition source
            positions[source] <- newPosition

        if not (positions.ContainsKey target) then
            let newPosition = calculatePosition target
            positions[target] <- newPosition

    ReadOnlyDictionary positions


let generateLayout
    (linksAndLabels: list<(string * string) * string>)
    : Layout
    =

    let linksAndLabels = toDomain linksAndLabels

    let links =
        linksAndLabels
        |> List.map fst

    let sources, targets = getSourcesAndTargets links
    let colAssignments = calculateColumnAssignments sources targets
    let newLinksAndLabels = insertLinks colAssignments linksAndLabels


    let newLinks =
        newLinksAndLabels
        |> List.map fst
    let newSources, newTargets = getSourcesAndTargets newLinks
    let newColAssignments = calculateColumnAssignments newSources newTargets
    let rowAssignments, colCounts = computeRowAssignments newSources newTargets newColAssignments
    // let colCounts = adjustColCounts newSources newTargets newColAssignments
    let newPositions = calculatePositions colCounts newColAssignments rowAssignments newLinks

    let labelPositions =
        newLinksAndLabels
        |> List.choose (fun ((source, target), label) ->
            label
            |> Option.map (fun label ->
                let sourcePos = newPositions[source]
                let targetPos = newPositions[target]
                let newPosition =
                    {
                        X = (targetPos.X - sourcePos.X) / 2.0 + sourcePos.X
                        Y = (targetPos.Y - sourcePos.Y) / 2.0 + sourcePos.Y
                    }
                KeyValuePair (label, newPosition)))
        |> Dictionary
        |> ReadOnlyDictionary

    for KeyValue(elem, position) in newPositions do
        printfn $"{elem}, {position}"

    {
        ElemPositions = newPositions
        LabelPositions = labelPositions
        Links = newLinks
    }


let linksAndLabels = [
    ("Plant CAI",   "DC MEM"),      "100.0"
    ("DC MEM",      "DC REO"),      "45"
    ("DC MEM",      "DC Chicken"),  "67.3"
    ("DC MEM",      "Customer"),    "89.4"
    ("DC REO",      "Customer"),    "42.1"
    ("DC Chicken",  "Monkey"),    "98.3"
    ("Monkey", "Customer"), "87"
]

let layout = generateLayout linksAndLabels
layout