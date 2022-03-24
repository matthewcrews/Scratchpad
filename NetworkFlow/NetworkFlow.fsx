open System

[<Struct>]
type NodeType =
    | Buffer = 0uy
    | Constraint = 1uy

[<Struct>]
type Arc =
    {
        Source : int
        Target : int
    }

let nodes =
    [|
        NodeType.Buffer
        NodeType.Buffer
        NodeType.Buffer
        NodeType.Constraint
        NodeType.Constraint
        NodeType.Constraint
    |]

let arcs =
    [|

    |]


[<Struct>]
type SmallArray =
    | Empty
    | One of one:int
    | Multiple of multiple:array<int>

[<Struct>]
type Node =
    {
        Type : NodeType
        Sources : SmallArray
        Targets : SmallArray
    }

type Nodes =
    {
        Type : array<NodeType>
        Sources : array<SmallArray>
        Targets : array<SmallArray>
    }

[<Struct; RequireQualifiedAccess>]
type BufferState =
    | Empty = 0uy
    | Partial = 1uy
    | Full = 2uy


type Arcs =
    {
        Capacity : array<float>
        Supply : array<float>
    }

type Node =
    {
        Sources : array<int>
        Targets : array<int>
    }

type Network =
    {
        BufferCount : int
        ConstraintCount : int
        BufferStates : array<BufferState>
        ConstraintLimits : array<float>
        // Indexed by Buffers then Constraints
        Sources : array<SmallArray>
        // Indexed by Buffers then Constraints
        Targets : array<SmallArray>
    }

let bufferStates =
    [| 
        BufferState.Full
        BufferState.Full
        BufferState.Empty
    |]
let flowLimits =
    [|
        infinity
        infinity
        infinity
        5.0
        8.0
        10.0
    |]

let bufferCount = bufferStates.Length
let constraintCount = flowLimits.Length
let nodeCount = bufferCount + constraintCount

let targets =
    [|
        One 3 // Buffer 0
        One 4 // Buffer 1
        Empty // Buffer 2
        One 5 // Constraint 0
        One 5 // Constraint 1
        One 2 // Constraint 2
    |]

let sources =
    [|
        Empty // Buffer 0
        Empty // Buffer 1
        One 5 // Buffer 2
        One 0 // Constraint 0
        One 2 // Constraint 1
        Multiple [|3; 4|] // Constraint 2
    |]

let supply = 
    [|
        infinity
        infinity
        0.0
        0.0
        0.0
        0.0
    |]

let capacity = Array.create nodeCount 0.0
let visited = Array.create nodeCount false


// For each Buffer, check if it is not Empty
// If not empty, send infinite supply to Children
let rec propogateSupply (sourceIdx: int, supplyAmount: float) =
    match targets[sourceIdx] with
    | Empty -> ()
    | One targetIdx ->
        // We're adding supply to the target
        let addAmount = Math.Min (supplyAmount, flowLimits[targetIdx] - supply[targetIdx])
        supply[targetIdx] <- supply[targetIdx] + addAmount
        propogateSupply (targetIdx, addAmount)
    | Multiple targets ->
        for targetIdx in targets do
            // We're adding supply to the target
            let addAmount = Math.Min (supplyAmount, flowLimits[targetIdx] - supply[targetIdx])
            supply[targetIdx] <- supply[targetIdx] + addAmount
            propogateSupply (targetIdx, addAmount)

for bufferIdx = 0 to bufferCount - 1 do
    let supplyAmount = supply[bufferIdx]
    propogateSupply (bufferIdx, supplyAmount)