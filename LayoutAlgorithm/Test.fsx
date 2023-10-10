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

    for (source, target), label in linksAndLabels do
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




let colOrdering = calculateColumnOrdering newSources newTargets newColAssignments

let newColAssignments = Dictionary()



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
