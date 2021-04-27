type Variable = Variable of string
type Coefficient = Coefficient of float
type Proportion = Proportion of float
type MaxRate = MaxRate of float

// Node types
type Source = Source of string
type Sink = Sink of string
type Conversion = {
    Name : string
    Coefficient : Coefficient
    MaxRate : MaxRate
}
type Merge = Merge of string
type Split = Split of string
type Tank = Tank of string
type Limit = {
    Name : string
    Variable : Variable
    Value : float
}
[<RequireQualifiedAccess>]
type Node =
    | Conversion of Conversion
    | Merge of Merge
    | Sink of Sink
    | Source of Source
    | Split of Split
    | Tank of Tank

type Arc = {
    Source : Node
    Sink : Node
    Proportion : Proportion
}
type Model = {
    Nodes : Set<Node>
    Inbound : Map<Node, Arc list>
    Outbound : Map<Node, Arc list>
}

module Variable =
    let create variable =
        if System.String.IsNullOrEmpty variable then
            invalidArg (nameof variable) "Cannot create Variable from null or empty string"

        Variable variable

module Coefficient =
    let create coefficient =
        if coefficient <= 0.0 then
            invalidArg (nameof coefficient) "Cannot have a Coefficient <= 0.0"

        Coefficient coefficient

module Proportion =
    let create proportion =
        if proportion <= 0.0 then
            invalidArg (nameof proportion) "Cannot have a Proportion <= 0.0"

        Proportion proportion

module MaxRate =
    let create maxRate =
        if maxRate <= 0.0 then
            invalidArg (nameof maxRate) "Cannot have a MaxRate <= 0.0"

        MaxRate maxRate

module Arc =
    let create source sink proportion =
        {
            Source = source
            Sink = sink
            Proportion = proportion
        }

module Model =

    let empty =
        {
            Nodes = Set.empty
            Inbound = Map.empty
            Outbound = Map.empty
        }

    let private addNode (node: Node) (model: Model) =
        { model with
            Nodes = model.Nodes.Add node
        }

    let private addArc (sourceNode: Node) (destNode: Node) (proportion: Proportion) (model: Model) =
        let newArc = Arc.create sourceNode destNode proportion
        let newInbound =
            match Map.tryFind destNode model.Outbound with
            | Some arcs -> model.Outbound.Add (destNode, newArc::arcs)
            | None -> model.Outbound.Add (destNode, [newArc])
        let newOutbound =
            match Map.tryFind sourceNode model.Outbound with
            | Some arcs -> model.Inbound.Add (sourceNode, newArc::arcs)
            | None -> model.Inbound.Add (sourceNode, [newArc])
        { model with
            Inbound = newInbound
            Outbound = newOutbound
        }

    let internal sendsTo (sourceNode: Node) (destNode: Node) (proportion: Proportion) (model: Model) =
        model
        |> addNode sourceNode
        |> addNode destNode
        |> addArc sourceNode destNode proportion

type Model with

    static member connect (source: Conversion, dest: Conversion, model: Model) =
        let s = Node.Conversion source
        let d = Node.Conversion dest
        let p = Proportion 1.0
        Model.sendsTo s d p model

    static member connect (source: Conversion, dest: Merge, proportion:Proportion, model: Model) =
        let s = Node.Conversion source
        let d = Node.Merge dest
        Model.sendsTo s d proportion model

    static member connect (source: Conversion, dest: Sink, model: Model) =
        let s = Node.Conversion source
        let d = Node.Sink dest
        let p = Proportion 1.0
        Model.sendsTo s d p model

    static member connect (source: Conversion, dest: Split, model: Model) =
        let s = Node.Conversion source
        let d = Node.Split dest
        let p = Proportion 1.0
        Model.sendsTo s d p model

    static member connect (source: Conversion, dest: Tank, model: Model) =
        let s = Node.Conversion source
        let d = Node.Tank dest
        let p = Proportion 1.0
        Model.sendsTo s d p model

    static member connect (source: Merge, dest: Conversion, model: Model) =
        let s = Node.Merge source
        let d = Node.Conversion dest
        let p = Proportion 1.0
        Model.sendsTo s d p model

    static member connect (source: Merge, dest: Merge, proportion:Proportion, model: Model) =
        let s = Node.Merge source
        let d = Node.Merge dest
        Model.sendsTo s d proportion model

    static member connect (source: Merge, dest: Sink, model: Model) =
        let s = Node.Merge source
        let d = Node.Sink dest
        let p = Proportion 1.0
        Model.sendsTo s d p model
        
    static member connect (source: Merge, dest: Split, model: Model) =
        let s = Node.Merge source
        let d = Node.Split dest
        let p = Proportion 1.0
        Model.sendsTo s d p model

    static member connect (source: Merge, dest: Tank, model: Model) =
        let s = Node.Merge source
        let d = Node.Tank dest
        let p = Proportion 1.0
        Model.sendsTo s d p model

    static member connect (source: Source, dest: Conversion, model: Model) =
        let s = Node.Source source
        let d = Node.Conversion dest
        let p = Proportion 1.0
        Model.sendsTo s d p model
    
    static member connect (source: Source, dest: Merge, proportion:Proportion, model: Model) =
        let s = Node.Source source
        let d = Node.Merge dest
        Model.sendsTo s d proportion model

    static member connect (source: Source, dest: Split, model: Model) =
        let s = Node.Source source
        let d = Node.Split dest
        let p = Proportion 1.0
        Model.sendsTo s d p model

    static member connect (source: Tank, dest: Conversion, model: Model) =
        let s = Node.Tank source
        let d = Node.Conversion dest
        let p = Proportion 1.0
        Model.sendsTo s d p model

    static member connect (source: Tank, dest: Merge, proportion:Proportion, model: Model) =
        let s = Node.Tank source
        let d = Node.Merge dest
        Model.sendsTo s d proportion model

    static member connect (source: Tank, dest: Split, model: Model) =
        let s = Node.Tank source
        let d = Node.Split dest
        let p = Proportion 1.0
        Model.sendsTo s d p model

    static member connect (source: Tank, dest: Tank, model: Model) =
        let s = Node.Tank source
        let d = Node.Tank dest
        let p = Proportion 1.0
        Model.sendsTo s d p model


let source = Source "Source1"
let sink = Sink "Sink1"
let process1 = {
    Name = "Process1"
    Coefficient = Coefficient 1.0
    MaxRate = MaxRate 10.0
}
let process2 = {
    Name = "Process2"
    Coefficient = Coefficient 1.0
    MaxRate = MaxRate 5.0
}
let tank1 = Tank "Tank1"

let m =
    Model.empty
    |> Model.connect (source, process1)