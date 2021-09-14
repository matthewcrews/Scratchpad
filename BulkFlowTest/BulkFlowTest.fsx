#r "nuget: Flips, 2.4.5"

open System
open System.Collections.Generic

#nowarn "0020"

type PriorityQueue<'Priority, 'Value when 'Priority : equality>() =
    let priorities = SortedSet<'Priority>()
    let queues = Dictionary<'Priority, Queue<'Value>>()

    member _.Add (priority, value) =
        if priorities.Contains priority then
            queues.[priority].Enqueue value
        else
            priorities.Add priority
            let queue = Queue ()
            queue.Enqueue value
            queues.[priority] <- queue

    member this.TryDequeue () =
        if priorities.Count > 0 then
            let priority = priorities.Min
            let queue = queues.[priority]
            if queue.Count > 0 then
                let next = queue.Dequeue ()
                Some (priority, next)
            else
                priorities.Remove priority
                queues.Remove priority
                this.TryDequeue ()
        else
            None


module rec Modeling =

    type INode =
        inherit System.IComparable
        abstract member Name : string

    type Distribution =
        | Static of float
        | Uniform of min:float * max:float

    type Failure = {
        Occurance : Distribution
        Recovery : Distribution
    }

    type Tank = {
        Name : string
    } with
        interface INode with
            member x.Name = x.Name

    module Tank =

        let create name =
            {
                Name = name
            }

    type Valve = {
        Name : string
    } with
        interface INode with
            member x.Name = x.Name

    //type Split = {
    //    Name : string
    //} with
    //    interface INode with
    //        member x.Name = x.Name

    //type Merge = {
    //    Name : string
    //} with
    //    interface INode with
    //        member x.Name = x.Name

    type Conveyor = {
        Name : string
    } with
        interface INode with
            member x.Name = x.Name

    type Conversion = {
        Name : string
    } with
        interface INode with
            member x.Name = x.Name

    [<StructuralComparison>]
    type Link = {
        Source : INode
        Sink : INode
    }

    module Link =

        let create source sink =
            {
                Source = source
                Sink = sink
            }

    type ValveState (inputs: Link list, outputs: Link list) =
        let mutable maxFlow = 0.0
        member _.Outputs = outputs
        member _.Inputs = inputs
        member _.MaxFlow = maxFlow

        member _.SetMaxFlow x =
            maxFlow <- x


    type ConversionState (inputs: Link list, outputs: Link list) =
        let mutable conversionRate = 1.0
        member _.Outputs = outputs
        member _.Inputs = inputs
        member _.ConversionRate = conversionRate

        member _.SetConversionRate x =
            conversionRate <- x


    type TankState (inputs: Link list, outputs: Link list) =
        let mutable maxLevel = 0.0
        let mutable level = 0.0
        let mutable drainRate = 0.0
        let mutable fillRate = 0.0
        member _.Outputs = outputs
        member _.Inputs = inputs
        member _.Level = level
        member _.MaxLevel = maxLevel

        member _.SetMaxLevel newMaxLevel =
            maxLevel <- newMaxLevel


        member _.Step (elapsedTime: float) =
            // Fill the tank for the elapsed time
            level <- level + (fillRate - drainRate) * elapsedTime
            // Protect against rounding error
            if level > maxLevel then
                level <- maxLevel

            // Protect against rounding error
            if level < 0.0 then
                level <- 0.0


        member _.Update (flows: IReadOnlyDictionary<Link, float>) =

            let newDrainRate =
                outputs
                |> List.sumBy (fun link -> flows.[link])

            let newFillRate =
                inputs
                |> List.sumBy (fun link -> flows.[link])

            drainRate <- newDrainRate
            fillRate <- newFillRate
                

    type ConveyorLoad = {
        ConveyorLocation : float
        LoadingVelocity : float
        InputRate : float
    }

    type ConveyorState (inputs: Link list, outputs: Link list) = 
        let loads = Queue<ConveyorLoad>()
        let mutable currentInputRate = 0.0
        let mutable currentMaxVelocity = 0.0
        let mutable currentVelocity = 0.0
        let mutable currentLocation = 0.0
        let mutable currentLoad : ConveyorLoad option = None

        let rec moveForward () =
            if loads.Count > 0 then
                let next = loads.Peek ()
                if currentLocation >= next.ConveyorLocation then
                    currentLoad <- Some next
                    loads.Dequeue () |> ignore
                    // We want to move forward until we have caught up to the
                    // Current Location
                    moveForward ()

        member _.Outputs = outputs
        member _.Inputs = inputs
        member _.MaxVelocity = currentMaxVelocity
        member _.CurrentLoad = currentLoad


        member _.Step (elapsedTime: float) =
            currentLocation <- currentLocation * currentVelocity * elapsedTime
            moveForward ()


        member _.Update (flows: IReadOnlyDictionary<Link, float>) =

            // Calculate the amount of flow leaving the Conveyor
            let outputRate = outputs |> List.sumBy (fun link -> flows.[link])


            let newVelocity = 
                match currentLoad with
                // If there is a load leaving the Conveyor, calculate the new Velocity
                | Some currLoad ->
                    (outputRate / currLoad.InputRate) * currLoad.LoadingVelocity
                // If there is no load leaving the Conveyor, the Velocity will change to 
                // the max allowed velocity
                | None ->
                    currentMaxVelocity

            let newInputRate = inputs |> List.sumBy (fun link -> flows.[link])

            // If either the Velocity or InputRate change, we need to start a new
            // loading for the Conveyor
            if newVelocity <> currentVelocity || newInputRate <> currentInputRate then
                let newLoad = {
                    ConveyorLocation = currentLocation
                    LoadingVelocity = newVelocity
                    InputRate = newInputRate
                }
                loads.Enqueue newLoad
                currentVelocity <- newVelocity
                currentInputRate <- newInputRate






        member _.SetMaxVelocity newMaxVelocity =
            currentMaxVelocity <- newMaxVelocity


    let private createInitialNetworkState (links: Link list) =
        let links = HashSet()
        
        let nodes, nodeInputs, nodeOutputs =
            let nodeInputs = Dictionary<INode, HashSet<Link>>()
            let nodeOutputs = Dictionary<INode, HashSet<Link>>()
            let nodes = HashSet<INode>()
            
            for link in links do
                links.Add link
                nodes.Add link.Source
                nodes.Add link.Sink

                match nodeInputs.TryGetValue link.Sink with
                | true, inputs ->  
                    inputs.Add link
                    ()
                | false, _ ->
                    let inputs = HashSet [link]
                    nodeInputs.[link.Sink] <- inputs

                match nodeOutputs.TryGetValue link.Source with
                | true, outputs ->
                    outputs.Add link
                    ()
                | false, _ ->
                    let outputs = HashSet [link]
                    nodeOutputs.[link.Source] <- outputs

            nodes, nodeInputs, nodeOutputs

        let tanks = Dictionary ()
        let conversions = Dictionary ()
        let valves = Dictionary ()
        let conveyors = Dictionary ()

        for node in nodes do
            let inputs = 
                match nodeInputs.TryGetValue node with
                | true, inputs -> List.ofSeq inputs
                | false, _ -> []

            let outputs =
                match nodeOutputs.TryGetValue node with
                | true, outputs -> List.ofSeq outputs
                | false, _ -> []

            match node with
            | :? Tank as tank ->
                let tankState = TankState (inputs, outputs)
                tanks.[tank] <- tankState
        
            | :? Valve as valve -> 
                let valveState = ValveState (inputs, outputs)
                valves.[valve] <- valveState

            | :? Conversion as conversion ->
                let conversionState = ConversionState (inputs, outputs)
                conversions.[conversion] <- conversionState

            | :? Conveyor as conveyor ->
                let conveyorState = ConveyorState (inputs, outputs)
                conveyors.[conveyor] <- conveyorState
            | _ -> invalidArg (nameof node) "Unsupported node type in network"

        (links, conversions, conveyors, tanks, valves)


    type NetworkState (links: HashSet<Link>, conversions: Dictionary<Conversion, ConversionState>, conveyors: Dictionary<Conveyor, ConveyorState>, tanks: Dictionary<Tank, TankState>, valves: Dictionary<Valve, ValveState>) = 
        member _.Links = links
        member _.Conversions = conversions
        member _.Conveyors = conveyors
        member _.Tanks = tanks
        member _.Valves = valves
        
        new (links: Link list) =
            let links, conversions, conveyors, tanks, valves = createInitialNetworkState links
            NetworkState (links, conversions, conveyors, tanks, valves)

        member this.Step (elapsedTime: float) =
            for KeyValue (tank, tankState) in this.Tanks do
                tankState.Step elapsedTime

            for KeyValue (conveyor, conveyorState) in this.Conveyors do
                conveyorState.Step elapsedTime


        member this.Update (flows: IReadOnlyDictionary<Link, float>) =
            for KeyValue (tank, tankState) in this.Tanks do
                tankState.Update flows

            for KeyValue (conveyor, conveyorState) in this.Conveyors do
                conveyorState.Update flows


    module NetworkState =
        open Flips
        open Flips.Types

        let private createDecisions (ns: NetworkState) =
            DecisionBuilder "Flow" {
                for link in ns.Links ->
                    Continuous (0.0, infinity)
            } |> dict

        let private createTankConstraints (decisions: IDictionary<Link, Decision>) (ns: NetworkState) =
            List.choose id [
                for KeyValue (tank, tankState) in ns.Tanks ->
                    if tankState.Level >= tankState.MaxLevel then
                        let inputExpr = List.sum [for input in tankState.Inputs -> 1.0 * decisions.[input]]
                        let outputExpr = List.sum [for output in tankState.Outputs -> 1.0 * decisions.[output]]
                        Constraint.create $"Tank_{tank.Name}_Full" (inputExpr <== outputExpr)
                        |> Some
                    elif tankState.Level <= 0.0 then
                        let inputExpr = List.sum [for input in tankState.Inputs -> 1.0 * decisions.[input]]
                        let outputExpr = List.sum [for output in tankState.Outputs -> 1.0 * decisions.[output]]
                        Constraint.create $"Tank_{tank.Name}_Empty" (inputExpr <== outputExpr)
                        |> Some
                    else
                        None
            ]

        let private createValveConstraints (decisions: IDictionary<Link, Decision>) (ns: NetworkState) =
            List.concat [
                for KeyValue (valve, valveState) in ns.Valves ->
                    let inputExpr = List.sum [for input in valveState.Inputs -> 1.0 * decisions.[input]]
                    let outputExpr = List.sum [for output in valveState.Outputs -> 1.0 * decisions.[output]]

                    let maxFlowConstraint = Constraint.create $"Valve_{valve.Name}_MaxFlow" (outputExpr <== valveState.MaxFlow)
                    let materialBalanceConstraint = Constraint.create $"Valve_{valve.Name}_MaterialBalance" (inputExpr == outputExpr)
                    [maxFlowConstraint; materialBalanceConstraint]
            ]

        let private createConversionConstraints (decisions: IDictionary<Link, Decision>) (ns: NetworkState) =
            [for KeyValue (conversion, conversionState) in ns.Conversions ->
                let inputExpr = List.sum [for input in conversionState.Inputs -> 1.0 * decisions.[input]]
                let outputExpr = List.sum [for output in conversionState.Outputs -> 1.0 * decisions.[output]]

                Constraint.create $"Conversion_{conversion.Name}_MaterialBalance" (conversionState.ConversionRate * inputExpr == outputExpr)
            ]

        let private createConveyorConstraints (decisions: IDictionary<Link, Decision>) (ns: NetworkState) =

            [for KeyValue (conveyor, conveyorState) in ns.Conveyors ->
                let outputExpr = List.sum [for output in conveyorState.Outputs -> 1.0 * decisions.[output]]

                let maxOutputRate =
                    match conveyorState.CurrentLoad with
                    | Some currentLoad -> conveyorState.MaxVelocity * currentLoad.InputRate
                    | None -> 0.0
                    
                Constraint.create $"Conveyor_{conveyor.Name}_Output" (outputExpr <== maxOutputRate)
            ]


        let solveFlows (ns: NetworkState) =
            let decisions = createDecisions ns
            let tankConstraints = createTankConstraints decisions ns
            let conveyorConstraints = createConveyorConstraints decisions ns
            let conversionConstraints = createConversionConstraints decisions ns
            let valveConstraints = createValveConstraints decisions ns
            let objectiveExpr =
                List.sum [for KeyValue(link, decision) in decisions -> 1.0 * decision]
            let objective = Objective.create "MaxFlow" Maximize objectiveExpr
            let model =
                Model.create objective
                |> Model.addConstraints tankConstraints
                |> Model.addConstraints conveyorConstraints
                |> Model.addConstraints conversionConstraints
                |> Model.addConstraints valveConstraints

            let result = Solver.solve Settings.basic model
            match result with
            | SolveResult.Optimal sln ->
                Solution.getValues sln decisions
            | _ -> failwith "Infeasible Network"


            

//module Simulation =

    

(*
Update cycle
1) Update the level and TankStatus for each tank
2) Update the Current Location of Conveyors
    - If location >= first in loads queue, pop the queue
3) Apply change to the network
4) Solve for the Flows
5) Update the TankState for each tank with new Fill/Drain rates
6) Enqueue new ConveyorLoad, only need to do this if the InputRate or Velocity changes
7) Evaluate next event for Tanks and Conveyors
8) Select next event and set it as the NextNetworkEvent

*)

//open Modeling

    //[<RequireQualifiedAccess>]
    //type Node =
    //    | Tank of Tank
    //    | Process of Process
    //    | Split of Split
    //    | Merge of Merge
    //    | Valve of Valve
    //    with
    //        member x.Name =
    //            match x with
    //            | Node.Tank t -> t.Name
    //            | Node.Process p -> p.Name
    //            | Node.Split s -> s.Name
    //            | Node.Merge m -> m.Name
    //            | Node.Valve v -> v.Name

    //type Link = {
    //    Source : Node
    //    Sink : Node
    //} with
    //    member x.Name = $"{x.Source.Name}->{x.Sink.Name}"

    //type Network = {
    //    Links : Link list
    //}

//module rec State =

//    open Modeling
    
//    type Proportion = {
//        Value : float
//    }


//    type TankState = {
//        Level : float
//        FillRate : float
//    }

//    [<RequireQualifiedAccess>]
//    type ProcessStatus =
//        | Up
//        | Down

//    type ProcessSetting = {
//        MaxInputRate : float
//        ConversionRate : float
//    }

//    type ProcessState = {
//        Setting : ProcessSetting
//        Status : ProcessStatus
//        Failures : DateTime list
//    }

//    [<RequireQualifiedAccess>]
//    type SplitSetting =
//        | Single of link: Modeling.Node
//        | Mix of Map<Modeling.Node, Proportion>

//    type SplitState = {
//        Setting : SplitSetting
//    }
    
//    [<RequireQualifiedAccess>]
//    type MergeSetting =
//        | Single of link: Modeling.Node
//        | Mix of Map<Modeling.Node, Proportion>

//    type MergeState = {
//        Setting : MergeSetting
//    }

//    [<RequireQualifiedAccess>]
//    type ValveSetting =
//        | Open
//        | Closed

//    type ValveState = {
//        Setting : ValveSetting
//    }

//    type Site = {
//        Tanks : Dictionary<Tank, TankState>
//        Processes : Dictionary<Process, ProcessState>
//        Merges : Dictionary<Merge, MergeState>
//        Splits : Dictionary<Split, SplitState>
//        Valves : Dictionary<Valve, ValveState>
//    }



//module Planning =

//    open Modeling
//    open State

//    [<RequireQualifiedAccess>]
//    type Action =
//        | SetMerge of MergeSetting
//        | SetSplit of SplitSetting
//        | OpenValve of Valve
//        | CloseValve of Valve

//    [<RequireQualifiedAccess>]
//    type Trigger =
//        | AtTime of time: DateTime
//        | FillTo of tank: Tank * level: float
//        | DrainTo of tank: Tank * level: float
//        | FillQuantity of tank: Tank * amount: float
//        | DrainQuantity of tank: Tank * amount: float
//        | ForDuration of duration: TimeSpan

//    type Step = {
//        Trigger : Trigger
//        Actions : Action list
//    }

//    type PlanName = PlanName of string

//    type Plan = {
//        Name : PlanName
//        Steps : Step list
//    }


//module Simulation =

//    open Modeling

//    type FlowEvent =
//        | Filled of Tank
//        | Emptied of Tank
//        | Trigger of Planning.Trigger

//    type EventType =
//        | Failed of Process
//        | Recovered of Process

//    type Event = {
//        Id : int64
//        Time : DateTime
//        Type : EventType
//    }

//    type State (initialId: int, startTime: DateTime, site: State.Site) =
//        let mutable nextId = initialId
//        let events = PriorityQueue<DateTime, Event> ()
//        let mutable now = startTime

//        member val Site = site

//        member this.TryNextEvent () =
//            match events.TryDequeue () with
//            | Some (nextTime, nextAction) ->
//                now <- nextTime
//                Some (nextTime, nextAction)
//            | None ->
//                None

//        member _.AddEvent (time, act) =
//            events.Add (time, act)

//        member _.NextId
//            with get () =
//                let next = nextId 
//                nextId <- next + 1
//                next

//        member _.Now = now
