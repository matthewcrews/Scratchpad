open System.Collections.Generic
open System.Collections.ObjectModel

[<RequireQualifiedAccess>]
type Elem =
    | Node of string
    | Label of string

let linksAndLabels = [
    (Elem.Node "Plant CAI", Elem.Node "DC MEM"), Elem.Label "100.0"
    (Elem.Node "DC MEM", Elem.Node "DC REO"), Elem.Label "45"
    (Elem.Node "DC MEM", Elem.Node "DC Chicken"), Elem.Label "67.3"
    (Elem.Node "DC MEM", Elem.Node "Customer"), Elem.Label "89.4"
    (Elem.Node "DC REO", Elem.Node "Customer"), Elem.Label "42.1"
    (Elem.Node "DC Chicken", Elem.Node "Customer"), Elem.Label "98.3"
]

type Node =
    | Location of string
    | Label of string

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
    (linksAndLabels: list<(Elem * Elem) * Elem>)
    =
    // We now need to insert labels when Nodes are not in adjacent columns
    let newLinks = Stack ()
    for (source, target), label in linksAndLabels do

        let sourceCol = colAssignments[source]
        let targetCol = colAssignments[target]

        if targetCol > sourceCol + 1 then
            newLinks.Push ((source, label), None)
            newLinks.Push ((label, target), None)
        else
            newLinks.Push ((source, target), Some label)

    newLinks.ToArray()

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

[<Struct>]
type Position =
    {
        X: float
        Y: float
    }


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
        let x = float col
        { X = x; Y = y}

    for source, target in links do

        if not (positions.ContainsKey source) then
            let newPosition = calculatePosition source
            positions[source] <- newPosition

        if not (positions.ContainsKey target) then
            let newPosition = calculatePosition target
            positions[target] <- newPosition

    ReadOnlyDictionary positions



let links =
    linksAndLabels
    |> List.map fst

let sources, targets = getSourcesAndTargets links
let colAssignments = calculateColumnAssignments sources targets
let newLinksAndLabels = insertLinks colAssignments linksAndLabels


let newLinks =
    newLinksAndLabels
    |> Array.map fst
let newSources, newTargets = getSourcesAndTargets newLinks
let newColAssignments = calculateColumnAssignments newSources newTargets
let rowAssignments, colCounts = computeRowAssignments newSources newTargets newColAssignments
let newPositions = calculatePositions colCounts newColAssignments rowAssignments newLinks

for KeyValue(elem, position) in newPositions do
    printfn $"{elem}, {position}"




    // if not (columnAssignments.ContainsKey node) then
    //     columnAssignments[node] <- column
    //     // Increment the count for the number of Nodes in the Column
    //     match columnCounts.TryGetValue column with
    //     | true, curCount ->
    //         columnCounts[column] <- curCount + 1
    //     | false, _ ->
    //         columnCounts[column] <- 1

let rowCount =
    columnCounts
    |> Seq.maxBy (fun kvp -> kvp.Value)
    |> (|KeyValue|)
    |> snd

let colCount =
    columnCounts
    |> Seq.maxBy (fun kvp -> kvp.Key)
    |> (|KeyValue|)
    |> fst
