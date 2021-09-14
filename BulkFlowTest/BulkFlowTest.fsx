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

    member this.TryNextPriority () =
        if priorities.Count > 0 then
            Some priorities.Min
        else
            None

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

    [<StructuralComparison; StructuralEquality>]
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
        member _.FillRate = fillRate
        member _.DrainRate = drainRate

        member _.TimeToChange () =
            let accumulationRate = fillRate - drainRate
            if accumulationRate > 0.0 then
                let remainingCapacity = maxLevel - level
                let remainingTimeSecs = int (remainingCapacity / accumulationRate)
                TimeSpan (0, 0, remainingTimeSecs)
            elif accumulationRate < 0.0 then
                let remainingTimeSecs = int (level / accumulationRate)
                TimeSpan (0, 0, 0, remainingTimeSecs, 0)
            else
                TimeSpan.MaxValue

        member _.SetMaxLevel newMaxLevel =
            maxLevel <- newMaxLevel


        member _.Step (ts: TimeSpan) =
            // Fill the tank for the elapsed time
            level <- level + (fillRate - drainRate) * ts.TotalSeconds
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
        let mutable inputRate = 0.0
        let mutable maxVelocity = 0.0
        let mutable velocity = 0.0
        let mutable location = 0.0
        let mutable load : ConveyorLoad option = None

        let rec moveForward () =
            if loads.Count > 0 then
                let next = loads.Peek ()
                if location >= next.ConveyorLocation then
                    load <- Some next
                    loads.Dequeue () |> ignore
                    // We want to move forward until we have caught up to the
                    // Current Location
                    moveForward ()

        member _.Outputs = outputs
        member _.Inputs = inputs
        member _.MaxVelocity = maxVelocity
        member _.CurrentVelocity = velocity
        member _.CurrentLoad = load
        member _.TimeToChange () =
            if velocity = 0.0 then
                TimeSpan.MaxValue
            elif loads.Count = 0 then
                TimeSpan.MaxValue
            else
                let nextLoad = loads.Peek ()
                let remainingLength = nextLoad.ConveyorLocation - location
                let remainingTimeSecs = int (remainingLength / velocity)
                TimeSpan (0, 0, remainingTimeSecs)


        member _.Step (ts: TimeSpan) =
            location <- location * velocity * ts.TotalSeconds
            moveForward ()


        member _.Update (flows: IReadOnlyDictionary<Link, float>) =

            // Calculate the amount of flow leaving the Conveyor
            let outputRate = outputs |> List.sumBy (fun link -> flows.[link])


            let newVelocity = 
                match load with
                // If there is a load leaving the Conveyor, calculate the new Velocity
                | Some currLoad ->
                    (outputRate / currLoad.InputRate) * currLoad.LoadingVelocity
                // If there is no load leaving the Conveyor, the Velocity will change to 
                // the max allowed velocity
                | None ->
                    maxVelocity

            let newInputRate = inputs |> List.sumBy (fun link -> flows.[link])

            // If either the Velocity or InputRate change, we need to start a new
            // loading for the Conveyor
            if newVelocity <> velocity || newInputRate <> inputRate then
                let newLoad = {
                    ConveyorLocation = location
                    LoadingVelocity = newVelocity
                    InputRate = newInputRate
                }
                loads.Enqueue newLoad
                velocity <- newVelocity
                inputRate <- newInputRate


        member _.SetMaxVelocity newMaxVelocity =
            maxVelocity <- newMaxVelocity


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


        member this.Step (ts: TimeSpan) =
            for KeyValue (tank, tankState) in this.Tanks do
                tankState.Step ts

            for KeyValue (conveyor, conveyorState) in this.Conveyors do
                conveyorState.Step ts


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


        let private getMinTankTime (ns: NetworkState) =
            seq {
                for KeyValue (tank, tankState) in ns.Tanks ->
                    tankState.TimeToChange ()
            } |> Seq.min


        let private getMinConveyorTime (ns: NetworkState) =
            seq {
                for KeyValue (conveyor, conveyorState) in ns.Conveyors ->
                    conveyorState.TimeToChange ()
            } |> Seq.min


        let timeToNetworkChange (ns: NetworkState) =
            let minTankTime = getMinTankTime ns
            let minConveyorTime = getMinConveyorTime ns
            if minTankTime < minConveyorTime then
                minTankTime
            else
                minConveyorTime
            

module Simulation =

(*
Update cycle
1) Step the level for each tank
2) Step the Current Location of Conveyors
    - If location >= first in loads queue, pop the queue
    - Set new current load
3) Apply change to the network
4) Solve for the Flows
5) Update the TankState for each tank with new Fill/Drain rates
6) Enqueue new ConveyorLoad, only need to do this if the InputRate or Velocity changes

7) Evaluate next event for Tanks and Conveyors
8) Select next event and set it as the NextNetworkEvent

*)

    // This is where you would create events for the Discrete Components
    type EventType =
        | Nothing

    type Event = {
        Time : DateTime
        Type : EventType
    }

    [<RequireQualifiedAccess>]
    type StepType =
        | Event of dateTime: DateTime
        | NetworkChange of dateTime: DateTime

    type State (initialId: int, startTime: DateTime, network: Modeling.NetworkState) =
        let mutable nextId = initialId
        let events = PriorityQueue<DateTime, Event> ()
        let mutable now = startTime

        member val Network = network

        //member this.TryNextEvent () =
        //    match events.TryDequeue () with
        //    | Some (nextTime, nextAction) ->
        //        now <- nextTime
        //        Some (nextTime, nextAction)
        //    | None ->
        //        None

        //member _.AddEvent (time, act) =
        //    events.Add (time, act)

        member _.NextId
            with get () =
                let next = nextId 
                nextId <- next + 1
                next

        member _.Now = now
        member _.SetNow newNow =
            now <- newNow

        member _.GetNextStepTime () =
            let nextEventTime = events.TryNextPriority () |> Option.defaultValue DateTime.MaxValue
            let nextNetworkChange = 
                match Modeling.NetworkState.timeToNetworkChange network with 
                | x when x = TimeSpan.MaxValue -> DateTime.MaxValue
                | x -> now + x

            if nextEventTime < nextNetworkChange then
                StepType.Event nextEventTime
            else
                StepType.NetworkChange nextNetworkChange


    let rec run (maxDateTime: DateTime) (state: State) =
        //let timeTillNetworkChange = Modeling.NetworkState.timeToNetworkChange state.Network
        let nextStep = state.GetNextStepTime ()

        match nextStep with
        | StepType.Event eventTime when eventTime = DateTime.MaxValue ->
            let elapsedTime = maxDateTime - state.Now
            state.SetNow maxDateTime
            state.Network.Step elapsedTime
        | StepType.NetworkChange changeTime when changeTime = DateTime.MaxValue ->
            let elapsedTime = maxDateTime - state.Now
            state.SetNow maxDateTime
            state.Network.Step elapsedTime

        | StepType.Event eventTime ->
            failwith "We aren't processing events yet"
        | StepType.NetworkChange changeTime ->
            let elapsedTime = maxDateTime - state.Now
            state.SetNow maxDateTime
            state.Network.Step elapsedTime
            let newFlows = Modeling.NetworkState.solveFlows state.Network
            state.Network.Update newFlows

