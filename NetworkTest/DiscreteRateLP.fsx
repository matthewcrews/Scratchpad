open System

type ConversionFactor = ConversionFactor of float
    with member this.Value =
            let (ConversionFactor value) = this
            value

type Label = Label of string
    with member this.Value = 
            let (Label value) = this
            value

type Proportion = Proportion of float
    with member this.Value = 
                let (Proportion value) = this
                value

[<RequireQualifiedAccess>]
type MaxOutputRate =
    | Infinite
    | Finite of float

type Operation = {
    Label : Label
    ConversionFactor : ConversionFactor
    MaxOutputRate : MaxOutputRate
}

[<RequireQualifiedAccess>]
type Capacity =
    | Infinite
    | Finite of float

type Tank = {
    Label : Label
    Capacity : Capacity
}

type Merge = {
    Label : Label
}

type Split = {
    Label : Label
}

[<RequireQualifiedAccess>]
type Node =
    | Operation of Operation
    | Tank of Tank
    | Merge of Merge
    | Split of Split
    with
        member this.Label =
            match this with
            | Operation n -> n.Label
            | Tank    n -> n.Label
            | Merge   n -> n.Label
            | Split   n -> n.Label

type Arc = {
    Source : Node
    Sink : Node
    Proportion : Proportion
} with
    member this.Label =
        $"{this.Source.Label}->{this.Sink.Label}"


[<RequireQualifiedAccess>]
module Label =

    let create label =
        if String.IsNullOrEmpty label then
            invalidArg (nameof label) $"Cannot have a null or empty Label"

        Label label


[<RequireQualifiedAccess>]
module MaxOutputRate =

    let create maxOutputRate =
        if maxOutputRate <= 0.0 then
            invalidArg (nameof maxOutputRate) $"Cannot have a MaxOutputRate less then or equal to 0.0"

        if maxOutputRate = infinity then
            MaxOutputRate.Infinite
        else
            MaxOutputRate.Finite maxOutputRate


[<RequireQualifiedAccess>]
module ConversionFactor =

    let create conversionFactor =
        if conversionFactor <= 0.0 then
            invalidArg (nameof conversionFactor) $"Cannot have a ConversionFactor less then or equal to 0.0"

        if conversionFactor = infinity then
            invalidArg (nameof conversionFactor) $"Cannot have a ConversionFactor of infinity"

        ConversionFactor conversionFactor


[<RequireQualifiedAccess>]
module Proportion =

    let create proportion =
        if proportion <= 0.0 then
            invalidArg (nameof proportion) $"Cannot have a Proportion less then or equal to 0.0"

        if proportion = infinity then
            invalidArg (nameof proportion) $"Cannot have a Proportion of infinity"

        Proportion proportion


[<RequireQualifiedAccess>]
module Process =

    let create label conversionFactor maxOutputRate =
        if conversionFactor <= 0.0 then
            invalidArg (nameof conversionFactor) $"Cannot have a {nameof conversionFactor} <= 0.0"

        {
            Label = Label.create label
            ConversionFactor = ConversionFactor conversionFactor
            MaxOutputRate = MaxOutputRate.create maxOutputRate
        }


[<RequireQualifiedAccess>]
module Node =

    let ofOperation operation =
        Node.Operation operation

    let ofTank tank =
        Node.Tank tank

    let ofMerge merge =
        Node.Merge merge

    let ofSplit split =
        Node.Split split

    let isProcess (n: Node) =
        match n with
        | Node.Operation _ -> true
        | _ -> false


[<RequireQualifiedAccess>]
module Arc =

    let create source sink proportion =
        {
            Source = source
            Sink = sink
            Proportion = Proportion.create proportion
        }


type Model (arcs: Arc list) =

    let nodes =
        arcs
        |> List.collect (fun x -> [x.Sink; x.Source])
        |> Set

    let outboundArcs =
        arcs
        |> List.groupBy (fun x -> x.Source)
        |> Map

    let inboundArcs =
        arcs
        |> List.groupBy (fun x -> x.Sink)
        |> Map

    let (operation, tank, merge, split) =
        nodes
        |> Seq.fold (fun (operationArcs, tankArcs, mergeArcs, splitArcs) node ->
            match node with
            | Node.Operation operation ->
                let inbound =
                    match inboundArcs.[node] with
                    | [arc] -> arc
                    | _ -> invalidArg "Inbound" "Cannot have Operation with more than one input"
                let outbound =
                    match outboundArcs.[node] with
                    | [arc] -> arc
                    | _ -> invalidArg "Inbound" "Cannot have Operation with more than one output"
                let newOperationArcs = Map.add operation (inbound, outbound) operationArcs
                newOperationArcs, tankArcs, mergeArcs, splitArcs
            | Node.Tank tank ->
                let inbound = inboundArcs.[node]
                let outbound = outboundArcs.[node]
                let newTankArcs = Map.add tank (inbound, outbound) tankArcs
                operationArcs, newTankArcs, mergeArcs, splitArcs
            | Node.Merge merge ->
                let inbound = inboundArcs.[node]
                let outbound =
                    match outboundArcs.[node] with
                    | [arc] -> arc
                    | _ -> invalidArg "Inbound" "Cannot have Merge with more than one output"
                let newMergeArcs = Map.add merge (inbound, outbound) mergeArcs
                operationArcs, tankArcs, newMergeArcs, splitArcs
            | Node.Split split ->
                let inbound =
                    match inboundArcs.[node] with
                    | [arc] -> arc
                    | _ -> invalidArg "Inbound" "Cannot have Split with more than one input"
                let outbound = outboundArcs.[node]
                let newSplitArcs = Map.add split (inbound, outbound) splitArcs
                operationArcs, tankArcs, mergeArcs, newSplitArcs
        ) (Map.empty, Map.empty, Map.empty<Merge, (Arc list * Arc)>, Map.empty)

    member _.Arcs = Set arcs
    member _.Nodes = nodes
    member _.Operation = operation
    member _.Tank = tank
    member _.Merge = merge
    member _.Split = split


type arc () =

    static member connect (source: Operation, dest: Operation) =
        let s = Node.Operation source
        let d = Node.Operation dest
        let p = 1.0
        Arc.create s d p

    static member connect (source: Operation, dest: Merge, proportion:float) =
        let s = Node.Operation source
        let d = Node.Merge dest
        Arc.create s d proportion

    static member connect (source: Operation, dest: Split) =
        let s = Node.Operation source
        let d = Node.Split dest
        let p = 1.0
        Arc.create s d p

    static member connect (source: Operation, dest: Tank) =
        let s = Node.Operation source
        let d = Node.Tank dest
        let p = 1.0
        Arc.create s d p

    static member connect (source: Merge, dest: Operation) =
        let s = Node.Merge source
        let d = Node.Operation dest
        let p = 1.0
        Arc.create s d p

    static member connect (source: Merge, dest: Merge, proportion:float) =
        let s = Node.Merge source
        let d = Node.Merge dest
        Arc.create s d proportion

    static member connect (source: Merge, dest: Split) =
        let s = Node.Merge source
        let d = Node.Split dest
        let p = 1.0
        Arc.create s d p

    static member connect (source: Merge, dest: Tank) =
        let s = Node.Merge source
        let d = Node.Tank dest
        let p = 1.0
        Arc.create s d p

    static member connect (source: Tank, dest: Merge, proportion:float) =
        let s = Node.Tank source
        let d = Node.Merge dest
        Arc.create s d proportion

    static member connect (source: Tank, dest: Split) =
        let s = Node.Tank source
        let d = Node.Split dest
        let p = 1.0
        Arc.create s d p

    static member connect (source: Tank, dest: Tank) =
        let s = Node.Tank source
        let d = Node.Tank dest
        let p = 1.0
        Arc.create s d p