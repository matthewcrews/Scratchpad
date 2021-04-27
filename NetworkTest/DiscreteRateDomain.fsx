type Coefficient = Coefficient of float
type Proportion = Proportion of float
type MaxRate = MaxRate of float

// Node types
type Source = {
    Name : string
}
type Sink = {
    Name : string
}
type Conversion = {
    Name : string
    Coefficient : Coefficient
    MaxRate : MaxRate
}
type Merge = {
    Name : string
}
type Split = {
    Name : string
}
type Tank = {
    Name : string
}
[<RequireQualifiedAccess>]
type Node =
    | Conversion of Conversion
    | Merge of Merge
    | Sink of Sink
    | Source of Source
    | Split of Split
    | Tank of Tank
    with
        member this.Name =
            match this with
            | Conversion c -> c.Name
            | Merge m -> m.Name
            | Sink s -> s.Name
            | Source s -> s.Name
            | Split s -> s.Name
            | Tank t -> t.Name

type Arc = {
    Source : Node
    Sink : Node
    Proportion : Proportion
} with
    member this.Name =
        $"{this.Source.Name}->{this.Sink.Name}"


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

type Model (arcs: Arc list) = 

    let nodes =
        arcs
        |> List.collect (fun x -> [x.Sink; x.Source])
        |> Set

    let inbound =
        arcs
        |> List.groupBy (fun x -> x.Source)
        |> Map

    let outbound =
        arcs
        |> List.groupBy (fun x -> x.Sink)

    member _.Nodes = nodes
    member _.Inbound = inbound
    member _.Outbound = outbound


type arc () =

    static member connect (source: Conversion, dest: Conversion) =
        let s = Node.Conversion source
        let d = Node.Conversion dest
        let p = Proportion 1.0
        Arc.create s d p

    static member connect (source: Conversion, dest: Merge, proportion:Proportion) =
        let s = Node.Conversion source
        let d = Node.Merge dest
        Arc.create s d proportion

    static member connect (source: Conversion, dest: Sink) =
        let s = Node.Conversion source
        let d = Node.Sink dest
        let p = Proportion 1.0
        Arc.create s d p

    static member connect (source: Conversion, dest: Split) =
        let s = Node.Conversion source
        let d = Node.Split dest
        let p = Proportion 1.0
        Arc.create s d p

    static member connect (source: Conversion, dest: Tank) =
        let s = Node.Conversion source
        let d = Node.Tank dest
        let p = Proportion 1.0
        Arc.create s d p

    static member connect (source: Merge, dest: Conversion) =
        let s = Node.Merge source
        let d = Node.Conversion dest
        let p = Proportion 1.0
        Arc.create s d p

    static member connect (source: Merge, dest: Merge, proportion:Proportion) =
        let s = Node.Merge source
        let d = Node.Merge dest
        Arc.create s d proportion

    static member connect (source: Merge, dest: Sink) =
        let s = Node.Merge source
        let d = Node.Sink dest
        let p = Proportion 1.0
        Arc.create s d p
        
    static member connect (source: Merge, dest: Split) =
        let s = Node.Merge source
        let d = Node.Split dest
        let p = Proportion 1.0
        Arc.create s d p

    static member connect (source: Merge, dest: Tank) =
        let s = Node.Merge source
        let d = Node.Tank dest
        let p = Proportion 1.0
        Arc.create s d p

    static member connect (source: Source, dest: Conversion) =
        let s = Node.Source source
        let d = Node.Conversion dest
        let p = Proportion 1.0
        Arc.create s d p
    
    static member connect (source: Source, dest: Merge, proportion:Proportion) =
        let s = Node.Source source
        let d = Node.Merge dest
        Arc.create s d proportion

    static member connect (source: Source, dest: Split) =
        let s = Node.Source source
        let d = Node.Split dest
        let p = Proportion 1.0
        Arc.create s d p

    static member connect (source: Tank, dest: Conversion) =
        let s = Node.Tank source
        let d = Node.Conversion dest
        let p = Proportion 1.0
        Arc.create s d p

    static member connect (source: Tank, dest: Merge, proportion:Proportion) =
        let s = Node.Tank source
        let d = Node.Merge dest
        Arc.create s d proportion

    static member connect (source: Tank, dest: Split) =
        let s = Node.Tank source
        let d = Node.Split dest
        let p = Proportion 1.0
        Arc.create s d p

    static member connect (source: Tank, dest: Tank) =
        let s = Node.Tank source
        let d = Node.Tank dest
        let p = Proportion 1.0
        Arc.create s d p


let source : Source = { Name = "Source1" }
let sink : Sink = { Name = "Sink1" }
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
let tank1 : Tank = { Name = "Tank1" }

let m =
    Model [
        arc.connect (source, process1)
        arc.connect (process1, tank1)
        arc.connect (tank1, process2)
        arc.connect (process2, sink)
    ]

m

#r "nuget: MathNet.Numerics.FSharp, 4.15.0"

module Solver =

    open MathNet.Numerics.LinearAlgebra
    
    type Variable = Variable of string
    type LinExpr = Map<Variable, Coefficient>

    let getBalanceExpressionForConversion (inboundArcs: Arc list) (outboundArcs: Arc list) (c: Conversion) =
        let inbound = 
            inboundArcs
            |> List.map (fun x -> x.Name, c.Coefficient)
        let outbound =
            outboundArcs
            |> List.map (fun x -> x.Name, Coefficient -1.0)
        inbound @ outbound
        |> Map

    let getBalanceExpressionForMerge (inboundArcs: Arc list) (outboundArcs: Arc list) (m: Merge) =
        let inboundTotal = inboundArcs |> List.sumBy (fun a -> a.Proportion)
        let inbound =
            inboundArcs
            |> 
        

    let decompose (node: Node) =
        match node with
        | Node.Sink _ | Node.Source _ -> []
        | Node.Conversion c -> 

    let solve (model: Model) =

        // Create a map to track Arc to variable
        let arcToIdx = System.Collections.Generic.Dictionary()
        let tankToIdx = System.Collections.Generic.Dictionary()

        let mutable lastVarIdx = 0
        let mutable rowIdx = 0

        // Create a map to track Tank to variable
        for node in model.Nodes do
        
            

            rowIdx <- rowIdx + 1