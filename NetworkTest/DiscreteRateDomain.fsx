//type Coefficient = Coefficient of float
//type Proportion = Proportion of float
//    with
//    static member (+) (Proportion a, Proportion b) =
//        Proportion (a + b)
//    static member (/) (Proportion a, Proportion b) =
//        (a / b)
//    static member Zero = Proportion 0.0

//type MaxRate = MaxRate of float

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




#r "nuget: MathNet.Numerics.FSharp, 4.15.0"

module Solver =

    open MathNet.Numerics.LinearAlgebra
    
    type Variable = Variable of string
        with
        member this.Value =
            let (Variable value) = this
            value

    type Term = {
        Variable : Variable
        Coefficient : float
    }

    module Term =
        let create variable coefficient =
            if System.String.IsNullOrEmpty variable then
                invalidArg (nameof variable) $"{nameof variable} cannot be null or empty"
            {
                Variable = Variable variable
                Coefficient = coefficient
            }

    type ConversionExpr = ConversionExpr of List<Term>
    type BalanceExpr = BalanceExpr of List<Term>
    type Limit = {
        Variable : Variable
        Value : float
    }

    module Limit =

        let create (variable: string) value =
            if System.String.IsNullOrEmpty variable then
                invalidArg (nameof variable) $"{nameof variable} cannot be null or empty"
            {
                Variable = Variable variable
                Value = value
            }

    let private getConversionExpression (inboundArcs: Arc list) (outboundArcs: Arc list) (c: Conversion) =
        let inbound = 
            inboundArcs
            |> List.map (fun x -> Term.create x.Name c.ConversionFactor)
        let outbound =
            outboundArcs
            |> List.map (fun x -> Term.create x.Name -1.0)

        ConversionExpr (inbound @ outbound)

    let private getBalanceExpression (inboundArcs: Arc list) (outboundArcs: Arc list) =
        let inbound =
            inboundArcs
            |> List.map (fun x -> Term.create x.Name 1.0)
        let outbound =
            outboundArcs
            |> List.map (fun x -> Term.create x.Name -1.0)
        
        BalanceExpr (inbound @ outbound)

    let private getTankExpression (inboundArcs: Arc list) (outboundArcs: Arc list) (t: Tank) =
        let inbound =
            inboundArcs
            |> List.map (fun x -> Term.create x.Name 1.0)
        let outbound =
            outboundArcs
            |> List.map (fun x -> Term.create x.Name -1.0)
        BalanceExpr ((Term.create t.Name -1.0) :: inbound @ outbound)
        

    let private getProportionExpressions (arcs: Arc list) =
        let expressions =
            match arcs with
            | [] -> invalidArg (nameof arcs) $"Cannot create Proportion Constraints when there are no arcs"
            | [arc] -> invalidArg (nameof arcs) $"Cannot create Proportion Constraints when there is only a single arc"
            | arc::otherArcs ->
                otherArcs
                |> List.map (fun otherArc -> BalanceExpr [(Term.create arc.Name arc.Proportion); (Term.create otherArc.Name arc.Proportion)])
        
        expressions
        
    let private getLimitForConversion (c: Conversion) =
        Limit.create c.Name c.MaxRate

    let private getExpressions (m: Model) (n: Node) =
        match n with
        | Node.Conversion conversion -> 
            let conversions = getConversionExpression m.Inbound.[n] m.Outbound.[n] conversion
            let limit = getLimitForConversion conversion
            [conversions, limit], []
        | Node.Merge merge -> 
            let balances = 
                getBalanceExpression m.Inbound.[n] m.Outbound.[n]
                :: getProportionExpressions m.Inbound.[n]
            [], balances
        | Node.Split split -> 
            let balances = 
                getBalanceExpression m.Inbound.[n] m.Outbound.[n]
                :: getProportionExpressions m.Outbound.[n]
            [], balances
        | Node.Tank tank -> 
            let balances = [getTankExpression m.Inbound.[n] m.Outbound.[n] tank]
            [], balances
        | _ ->
            [], []

    let decompose (model: Model) =

        let conversionLimits, balances =
            model.Nodes
            |> Seq.map (getExpressions model)
            |> Seq.fold (fun (cl, b) (newCL, newB) -> newCL@cl, newB@b) ([], [])

        conversionLimits, balances


    let getPivot (conversionLimits: (ConversionExpr * Limit) list) =
        match conversionLimits with
        | [] -> invalidArg "Model" "Unbounded model. There must be at least 1 Conversion in the network otherwise there is unlimited flow."
        | head::tail -> head, tail


    let solve (model: Model) =

        let conversionLimits, balances = decompose model

        let (ConversionExpr pivotConversion, pivotLimit), remainingConversionLimits = getPivot conversionLimits

        let remainingConversions, remainingLimits =
            remainingConversionLimits
            |> List.fold (fun (c, l) (newC, newL) -> newC::c, newL::l) ([], [])

        // Create a map to track variable mapping
        let varToIdx = System.Collections.Generic.Dictionary()
        let idxToVar = System.Collections.Generic.Dictionary()

        let mutable nextColIdx = 0
        let mutable rowIdx = 0
        let totalRows = 
            2 + // For the special Pivot Conversion row and PivotLimit
            remainingConversions.Length +
            remainingLimits.Length +
            balances.Length

        let bVector = vector [0.0..float (totalRows - 1)]

        let firstRow =
            let pivotTerms, remainingTerms =
                pivotConversion
                |> List.partition (fun term -> pivotLimit.Variable = term.Variable)

            let pivotTerm =
                match pivotTerms with
                | [term] -> term
                | _ -> invalidArg (nameof model) $"Cannot find pivot term for model"

            varToIdx.Add(pivotTerm.Variable, nextColIdx)
            idxToVar.Add(nextColIdx, pivotTerm.Variable)
            nextColIdx <- nextColIdx + 1
            let firstElement = [(rowIdx, varToIdx.[pivotTerm.Variable]), pivotTerm.Coefficient]

            let remainingElements =
                remainingTerms
                |> List.collect (fun term ->
                    varToIdx.Add(pivotTerm.Variable, nextColIdx)
                    idxToVar.Add(nextColIdx, pivotTerm.Variable)
                    nextColIdx <- nextColIdx + 1
                    [(rowIdx, varToIdx.[term.Variable]), term.Coefficient]
                )

            firstElement@remainingElements
            
        rowIdx <- rowIdx + 1

        let remainingConversionRows =
            remainingConversions
            |> List.collect (fun (ConversionExpr terms) ->
                let newRow =
                    terms
                    |> List.collect (fun term ->
                        match varToIdx.TryGetValue term.Variable with
                        | true, varIdx -> 
                            [(rowIdx, varToIdx.[term.Variable]), term.Coefficient]
                        | fale, _ ->
                            varToIdx.Add(term.Variable, nextColIdx)
                            idxToVar.Add(nextColIdx, term.Variable)
                            nextColIdx <- nextColIdx + 1
                            [(rowIdx, varToIdx.[term.Variable]), term.Coefficient]
                    )
                rowIdx <- rowIdx + 1
                newRow
            )

        let balanceRows =
            balances
            |> List.collect (fun (BalanceExpr terms) ->
                let newRow =
                    terms
                    |> List.collect (fun term ->
                        match varToIdx.TryGetValue term.Variable with
                        | true, varIdx -> 
                            [(rowIdx, varToIdx.[term.Variable]), term.Coefficient]
                        | fale, _ ->
                            varToIdx.Add(term.Variable, nextColIdx)
                            idxToVar.Add(nextColIdx, term.Variable)
                            nextColIdx <- nextColIdx + 1
                            [(rowIdx, varToIdx.[term.Variable]), term.Coefficient]
                    )
                rowIdx <- rowIdx + 1
                newRow
            )

        let limitSplitIdx =
            totalRows - 1 - // For the first row
            remainingConversions.Length -
            balances.Length

        let prePivotLimits = remainingLimits.[..limitSplitIdx - 1]
        let postPivotLimits = remainingLimits.[limitSplitIdx..]

        let prePivotRows =
            prePivotLimits
            |> List.collect (fun limit ->
                let newRow =
                    match varToIdx.TryGetValue limit.Variable with
                    | true, varIdx -> 
                        let slackVar = Variable.Variable $"{limit.Variable.Value}_slack"
                        varToIdx.Add(slackVar, nextColIdx)
                        idxToVar.Add(nextColIdx, slackVar)
                        nextColIdx <- nextColIdx + 1
                        bVector.[rowIdx] <- limit.Value
                        [(rowIdx, varToIdx.[limit.Variable]), 1.0; (rowIdx, varToIdx.[slackVar]), 1.0]

                    | fale, _ ->
                        invalidArg (nameof limit) $"Variables for limits should already exist"
                rowIdx <- rowIdx + 1
                newRow
            )

        // Pivot here
        let pivotSlackVar = Variable.Variable $"{pivotLimit.Variable.Value}_slack"
        varToIdx.Add(pivotSlackVar, nextColIdx)
        idxToVar.Add(nextColIdx, pivotSlackVar)
        nextColIdx <- nextColIdx + 1
        bVector.[rowIdx] <- pivotLimit.Value
        let pivotRow = [(rowIdx, varToIdx.[pivotLimit.Variable]), 1.0; (rowIdx, varToIdx.[pivotSlackVar]), 1.0]

        let postPivotRows =
            postPivotLimits
            |> List.collect (fun limit ->
                let newRow =
                    match varToIdx.TryGetValue limit.Variable with
                    | true, varIdx -> 
                        let slackVar = Variable.Variable $"{limit.Variable.Value}_slack"
                        varToIdx.Add(slackVar, nextColIdx)
                        idxToVar.Add(nextColIdx, slackVar)
                        nextColIdx <- nextColIdx + 1
                        bVector.[rowIdx] <- limit.Value
                        [(rowIdx, varToIdx.[limit.Variable]), 1.0; (rowIdx, varToIdx.[slackVar]), 1.0]

                    | fale, _ ->
                        invalidArg (nameof limit) $"Variables for limits should already exist"
                rowIdx <- rowIdx + 1
                newRow
            )

        let aMatrix = DenseMatrix.zero<float> rowIdx nextColIdx

        for ((row, col), value) in firstRow do
            aMatrix.[row, col] <- value

        for ((row, col), value) in remainingConversionRows do
            aMatrix.[row, col] <- value

        for ((row, col), value) in balanceRows do
            aMatrix.[row, col] <- value

        for ((row, col), value) in prePivotRows do
            aMatrix.[row, col] <- value

        for ((row, col), value) in pivotRow do
            aMatrix.[row, col] <- value

        for ((row, col), value) in postPivotRows do
            aMatrix.[row, col] <- value

        aMatrix, bVector


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

let (conversionLimits, balances) = Solver.decompose m
let (pivotExpr, pivotLimit), remainingConversionLimits = Solver.getPivot conversionLimits

let (testA, testB) = Solver.solve m