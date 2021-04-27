#r "nuget: MathNet.Numerics.FSharp, 4.15.0"

open MathNet.Numerics.LinearAlgebra

fsi.AddPrinter<Vector<float>>(fun v -> v |> Seq.map (sprintf "%.2f") |> String.concat " " |> (sprintf "[%s]"))

// Node types
type Source = {
    Name : string
}
type Sink = {
    Name : string
}
type Conversion = {
    Name : string
    ConversionFactor : float
    MaxRate : float
}

module Conversion =

    let create name conversionFactor maxRate =
        if conversionFactor <= 0.0 then
            invalidArg (nameof conversionFactor) $"Cannot have a {nameof conversionFactor} <= 0.0"

        if maxRate <= 0.0 then
            invalidArg (nameof maxRate) $"Cannot have a {nameof maxRate} <= 0.0"

        {
            Name = name
            ConversionFactor = conversionFactor
            MaxRate = maxRate
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

module Node =

    let isSourceOrSink (n: Node) =
        match n with
        | Node.Source _ | Node.Sink _ -> true
        | _ -> false

    let isConversion (n: Node) =
        match n with
        | Node.Conversion _ -> true
        | _ -> false


type Arc = {
    Source : Node
    Sink : Node
    Proportion : float
} with
    member this.Name =
        $"{this.Source.Name}->{this.Sink.Name}"


//module Coefficient =
//    let create coefficient =
//        if coefficient <= 0.0 then
//            invalidArg (nameof coefficient) "Cannot have a Coefficient <= 0.0"

//        Coefficient coefficient

//module Proportion =
//    let create proportion =
//        if proportion <= 0.0 then
//            invalidArg (nameof proportion) "Cannot have a Proportion <= 0.0"

//        Proportion proportion

//module MaxRate =
//    let create maxRate =
//        if maxRate <= 0.0 then
//            invalidArg (nameof maxRate) "Cannot have a MaxRate <= 0.0"

//        MaxRate maxRate

module Arc =
    let create source sink proportion =
        if proportion <= 0.0 then
            invalidArg (nameof proportion) "Cannot have a Proportion <= 0.0"
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

    let outboundArcs =
        arcs
        |> List.groupBy (fun x -> x.Source)
        |> Map

    let inboundArcs =
        arcs
        |> List.groupBy (fun x -> x.Sink)
        |> Map

    let (conversion, tank, merge, split) =
        nodes
        |> Seq.fold (fun (conversionArcs, tankArcs, mergeArcs, splitArcs) node ->
            match node with
            | Node.Conversion conversion ->
                let inbound = 
                    match inboundArcs.[node] with
                    | [arc] -> arc
                    | _ -> invalidArg "Inbound" "Cannot have Conversion with more than one input"
                let outbound = 
                    match outboundArcs.[node] with
                    | [arc] -> arc
                    | _ -> invalidArg "Inbound" "Cannot have Conversion with more than one output"
                let newConversionArcs = Map.add conversion (inbound, outbound) conversionArcs
                newConversionArcs, tankArcs, mergeArcs, splitArcs
            | Node.Merge merge ->
                let inbound = inboundArcs.[node]
                let outbound = 
                    match outboundArcs.[node] with
                    | [arc] -> arc
                    | _ -> invalidArg "Inbound" "Cannot have Merge with more than one output"
                let newMerge = Map.add merge (inbound, outbound) mergeArcs
                conversionArcs, tankArcs, newMerge, splitArcs
            | Node.Split split ->
                let inbound = 
                    match inboundArcs.[node] with
                    | [arc] -> arc
                    | _ -> invalidArg "Inbound" "Cannot have Split with more than one input"
                let outbound = outboundArcs.[node]
                let newSplit = Map.add split (inbound, outbound) splitArcs
                conversionArcs, tankArcs, mergeArcs, newSplit
            | Node.Tank tank ->
                let inbound = inboundArcs.[node]
                let outbound = outboundArcs.[node]
                let newTank = Map.add tank (inbound, outbound) tankArcs
                conversionArcs, newTank, mergeArcs, splitArcs
            | _ -> 
                conversionArcs, tankArcs, mergeArcs, splitArcs
        ) (Map.empty, Map.empty, Map.empty<Merge, (Arc list * Arc)>, Map.empty)

    member _.Arcs = Set arcs
    member _.Nodes = nodes
    member _.Conversion = conversion
    member _.Tank = tank
    member _.Merge = merge
    member _.Split = split


type arc () =

    static member connect (source: Conversion, dest: Conversion) =
        let s = Node.Conversion source
        let d = Node.Conversion dest
        let p = 1.0
        Arc.create s d p

    static member connect (source: Conversion, dest: Merge, proportion:float) =
        let s = Node.Conversion source
        let d = Node.Merge dest
        Arc.create s d proportion

    static member connect (source: Conversion, dest: Sink) =
        let s = Node.Conversion source
        let d = Node.Sink dest
        let p = 1.0
        Arc.create s d p

    static member connect (source: Conversion, dest: Split) =
        let s = Node.Conversion source
        let d = Node.Split dest
        let p = 1.0
        Arc.create s d p

    static member connect (source: Conversion, dest: Tank) =
        let s = Node.Conversion source
        let d = Node.Tank dest
        let p = 1.0
        Arc.create s d p

    static member connect (source: Merge, dest: Conversion) =
        let s = Node.Merge source
        let d = Node.Conversion dest
        let p = 1.0
        Arc.create s d p

    static member connect (source: Merge, dest: Merge, proportion:float) =
        let s = Node.Merge source
        let d = Node.Merge dest
        Arc.create s d proportion

    static member connect (source: Merge, dest: Sink) =
        let s = Node.Merge source
        let d = Node.Sink dest
        let p = 1.0
        Arc.create s d p
        
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

    static member connect (source: Source, dest: Conversion) =
        let s = Node.Source source
        let d = Node.Conversion dest
        let p = 1.0
        Arc.create s d p
    
    static member connect (source: Source, dest: Merge, proportion:float) =
        let s = Node.Source source
        let d = Node.Merge dest
        Arc.create s d proportion

    static member connect (source: Source, dest: Split) =
        let s = Node.Source source
        let d = Node.Split dest
        let p = 1.0
        Arc.create s d p

    static member connect (source: Tank, dest: Conversion) =
        let s = Node.Tank source
        let d = Node.Conversion dest
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


module Solver =

    open MathNet.Numerics.LinearAlgebra

    type Variable = Variable of string

    module Variable =

        let ofArc (arc: Arc) =
            Variable $"{arc.Name}_Flow"

        let ofTank (tank: Tank) =
            Variable $"{tank.Name}_Accumulation"

        let slackFor (arc: Arc) =
            Variable $"{arc.Name}_Slack"

    let buildInitialSystem (model: Model) =

        // Create a map to track variable mapping
        let varToIdx = System.Collections.Generic.Dictionary<Variable, int>()
        let idxToVar = System.Collections.Generic.Dictionary<int, Variable>()

        let totalColumns = 
            model.Arcs.Count + // A variable for each Flow rate
            model.Conversion.Count + // There is a variable for each slack
            model.Tank.Count // There is a variable for the accumulation rate in the tank

        let mergeProportionConstraintCount =
            model.Merge
            |> Map.toSeq
            |> Seq.sumBy (fun (_, (inbound, _)) -> inbound.Length - 1)

        let splitProportionConstraintCount =
            model.Split
            |> Map.toSeq
            |> Seq.sumBy (fun (_, (_, outbound)) -> outbound.Length - 1)

        let totalRows =
            model.Conversion.Count * 2 + // There is 1 material balance for a Conversion and 1 Limit
            model.Split.Count + // There is 1 material balance equations for Splits
            model.Merge.Count + // There is 1 material balance equations for Merges
            model.Tank.Count + // There is 1 material balance for a Tank
            mergeProportionConstraintCount +
            splitProportionConstraintCount
            
        let b = DenseVector.zero<float> totalRows
        let A = DenseMatrix.zero<float> totalRows totalColumns


        let mutable rowIdx = 0
        let mutable nextColIdx = 0

        // Add rows for Conversions
        for KeyValue(conversion, (inboundArc, outboundArc)) in model.Conversion do

            let inboundVar = Variable.ofArc inboundArc
            if not (varToIdx.ContainsKey inboundVar) then
                varToIdx.Add(inboundVar, nextColIdx)
                idxToVar.Add(nextColIdx, inboundVar)
                nextColIdx <- nextColIdx + 1
            A.[rowIdx, varToIdx.[inboundVar]] <- conversion.ConversionFactor

            let outboundVar = Variable.ofArc outboundArc
            if not (varToIdx.ContainsKey outboundVar) then
                varToIdx.Add(outboundVar, nextColIdx)
                idxToVar.Add(nextColIdx, outboundVar)
                nextColIdx <- nextColIdx + 1
            A.[rowIdx, varToIdx.[outboundVar]] <- 1.0

            // Add balance row
            
            rowIdx <- rowIdx + 1
            
            // Add limit row
            let outboundSlackVar = Variable.slackFor outboundArc
            if not (varToIdx.ContainsKey outboundSlackVar) then
                varToIdx.Add(outboundSlackVar, nextColIdx)
                idxToVar.Add(nextColIdx, outboundSlackVar)
                nextColIdx <- nextColIdx + 1

            A.[rowIdx, varToIdx.[outboundVar]] <- 1.0
            A.[rowIdx, varToIdx.[outboundSlackVar]] <- 1.0
            b.[rowIdx] <- conversion.MaxRate

            rowIdx <- rowIdx + 1

        // Add Tank rows
        for KeyValue (tank, (inboundArcs, outboundArcs)) in model.Tank do

            for arc in inboundArcs do
                let variable = Variable.ofArc arc
                if not (varToIdx.ContainsKey variable) then
                    varToIdx.Add(variable, nextColIdx)
                    idxToVar.Add(nextColIdx, variable)
                    nextColIdx <- nextColIdx + 1
                A.[rowIdx, varToIdx.[variable]] <- 1.0

            for arc in outboundArcs do
                let variable = Variable.ofArc arc
                if not (varToIdx.ContainsKey variable) then
                    varToIdx.Add(variable, nextColIdx)
                    idxToVar.Add(nextColIdx, variable)
                    nextColIdx <- nextColIdx + 1
                A.[rowIdx, varToIdx.[variable]] <- -1.0

            let accVariable = Variable.ofTank tank
            if not (varToIdx.ContainsKey accVariable) then
                varToIdx.Add(accVariable, nextColIdx)
                idxToVar.Add(nextColIdx, accVariable)
                nextColIdx <- nextColIdx + 1
            A.[rowIdx, varToIdx.[accVariable]] <- -1.0

            rowIdx <- rowIdx + 1

        // Add Merge rows
        for KeyValue (merge, (inboundArcs, outboundArc)) in model.Merge do
            for arc in inboundArcs do
                let variable = Variable.ofArc arc
                if not (varToIdx.ContainsKey variable) then
                    varToIdx.Add(variable, nextColIdx)
                    idxToVar.Add(nextColIdx, variable)
                    nextColIdx <- nextColIdx + 1
                A.[rowIdx, varToIdx.[variable]] <- 1.0

            let outboundVar = Variable.ofArc outboundArc
            if not (varToIdx.ContainsKey outboundVar) then
                varToIdx.Add(outboundVar, nextColIdx)
                idxToVar.Add(nextColIdx, outboundVar)
                nextColIdx <- nextColIdx + 1
            A.[rowIdx, varToIdx.[outboundVar]] <- -1.0

        // Add Split rows
        for KeyValue (split, (inboundArc, outboundArcs)) in model.Split do
            let inboundVar = Variable.ofArc inboundArc
            if not (varToIdx.ContainsKey inboundVar) then
                varToIdx.Add(inboundVar, nextColIdx)
                idxToVar.Add(nextColIdx, inboundVar)
                nextColIdx <- nextColIdx + 1
            A.[rowIdx, varToIdx.[inboundVar]] <- 1.0

            for arc in outboundArcs do
                let variable = Variable.ofArc arc
                if not (varToIdx.ContainsKey variable) then
                    varToIdx.Add(variable, nextColIdx)
                    idxToVar.Add(nextColIdx, variable)
                    nextColIdx <- nextColIdx + 1
                A.[rowIdx, varToIdx.[variable]] <- -1.0

        A, b, varToIdx, idxToVar

    let solve (model: Model) =

        let A, b, varToIdx, idxToVar = buildInitialSystem model

        ()


let source : Source = { Name = "Source1" }
let sink : Sink = { Name = "Sink1" }
let process1 = Conversion.create "Process1" 1.0 10.0
let process2 = Conversion.create "Process2" 1.0 5.0
let tank1 : Tank = { Name = "Tank1" }
let m =
    Model [
        arc.connect (source, process1)
        arc.connect (process1, tank1)
        arc.connect (tank1, process2)
        arc.connect (process2, sink)
    ]

let (A, b, varToIdx, idxToVar) = Solver.buildInitialSystem m
b
//let (conversionLimits, balances) = Solver.decompose m
//let (pivotExpr, pivotLimit), remainingConversionLimits = Solver.getPivot conversionLimits

//let (testA, testB) = Solver.solve m