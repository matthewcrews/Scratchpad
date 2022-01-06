#r "nuget: Flips, 2.4.5"

open System
open System.Collections.Generic

#nowarn "0020"

type PriorityQueue<'Priority, 'Value when 'Priority : equality>() =
    let priorities = SortedSet<'Priority>()
    let queues = Dictionary<'Priority, Queue<'Value>>()

    member _.Enqueue (priority, value) =
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

    member this.Dequeue () =
        match this.TryDequeue () with
        | Some v -> v
        | None -> raise (InvalidOperationException ("There are no elements in the PriorityQueue"))


module rec Modeling =

    type Distribution =
        | Static of float
        | Uniform of min:float * max:float

    type Failure = {
        Occurance : Distribution
        Recovery : Distribution
    }

    type Tank = {
        Name : string
    } 

    module Tank =

        let create name =
            {
                Name = name
            }

    type Valve = {
        Name : string
    } 

    //type Split = {
    //    Name : string
    //}

    //type Merge = {
    //    Name : string
    //}

    type Conveyor = {
        Name : string
    } 

    type Conversion = {
        Name : string
    } 

    [<RequireQualifiedAccess>]
    type Node =
        | Tank of Tank
        | Valve of Valve
        | Conveyor of Conveyor
        | Conversion of Conversion

    type Link = {
        Source : Node
        Sink : Node
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

        member internal _.SetMaxFlow x =
            maxFlow <- x


    type ConversionState (inputs: Link list, outputs: Link list) =
        let mutable conversionRate = 1.0
        member _.Outputs = outputs
        member _.Inputs = inputs
        member _.ConversionRate = conversionRate

        member internal _.SetConversionRate x =
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
        member internal _.FillRate = fillRate
        member internal _.DrainRate = drainRate

        member internal _.Empty () =
            level <- 0.0

        member internal _.TimeToChange () =
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

        member internal _.SetMaxLevel newMaxLevel =
            maxLevel <- newMaxLevel


        member internal _.Step (ts: TimeSpan) =
            // Fill the tank for the elapsed time
            level <- level + (fillRate - drainRate) * ts.TotalSeconds
            // Protect against rounding error
            if level > maxLevel then
                level <- maxLevel

            // Protect against rounding error
            if level < 0.0 then
                level <- 0.0


        member internal _.Update (flows: IReadOnlyDictionary<Link, float>) =

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
        //member _.CurrentVelocity = velocity
        member _.CurrentLoad = load

        member internal _.TimeToChange () =
            if velocity = 0.0 then
                TimeSpan.MaxValue
            elif loads.Count = 0 then
                TimeSpan.MaxValue
            else
                let nextLoad = loads.Peek ()
                let remainingLength = nextLoad.ConveyorLocation - location
                let remainingTimeSecs = int (remainingLength / velocity)
                TimeSpan (0, 0, remainingTimeSecs)


        member internal _.Step (ts: TimeSpan) =
            location <- location * velocity * ts.TotalSeconds
            moveForward ()


        member internal _.Update (flows: IReadOnlyDictionary<Link, float>) =

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


        member internal _.SetMaxVelocity newMaxVelocity =
            maxVelocity <- newMaxVelocity


    type NetworkState (links: HashSet<Link>, conversions: Dictionary<Conversion, ConversionState>, conveyors: Dictionary<Conveyor, ConveyorState>, tanks: Dictionary<Tank, TankState>, valves: Dictionary<Valve, ValveState>) = 
        let mutable isDirty = false
        let listeners = HashSet<Listener>()
        member _.Links = links
        member _.Conversions = conversions
        member _.Conveyors = conveyors
        member _.Tanks = tanks
        member _.Valves = valves

        
        new (links: Link list) =
            let links, conversions, conveyors, tanks, valves = NetworkState.createInitialNetworkState links
            NetworkState (links, conversions, conveyors, tanks, valves)

        member _.SetValveMaxFlow (valve, maxFlow) =
            valves.[valve].SetMaxFlow maxFlow
            isDirty <- true

        member _.SetConversionRate (conversion, newRate) =
            conversions.[conversion].SetConversionRate newRate
            isDirty <- true

        member _.SetTankMaxLevel (tank, maxLevel) =
            tanks.[tank].SetMaxLevel maxLevel
            isDirty <- true

        member _.SetConveyorMaxVelocity (conveyor, maxVelocity) =
            conveyors.[conveyor].SetMaxVelocity maxVelocity
            isDirty <- true

        member _.EmptyTank tank =
            tanks.[tank].Empty ()
            isDirty <- true


        member this.Step (ts: TimeSpan) =
            for KeyValue (tank, tankState) in tanks do
                tankState.Step ts

            for KeyValue (conveyor, conveyorState) in conveyors do
                conveyorState.Step ts

            // TODO: This needs to return a list of Triggers
            let mutable triggers = []

            for listener in listeners do
                match listener with
                | Listener.InFlow inFlow -> 
                    inFlow.Step (this, ts)
                    if inFlow.IsCompleted () then
                        listeners.Remove listener
                        triggers <- inFlow.Trigger :: triggers

                | Listener.ElapsedTime time -> 
                    time.Step (ts)
                    if time.IsCompleted () then
                        listeners.Remove listener
                        triggers <- time.Trigger :: triggers

                | Listener.TankFill tankFill ->
                    if tankFill.IsCompleted this then
                        listeners.Remove listener
                        triggers <- tankFill.Trigger :: triggers

                | Listener.TankDrain tankDrain ->
                    if tankDrain.IsCompleted this then
                        listeners.Remove listener
                        triggers <- tankDrain.Trigger :: triggers

            triggers


        member this.Refresh () =
            if isDirty then
                
                let flows = NetworkState.solveFlows this

                for KeyValue (tank, tankState) in tanks do
                    tankState.Update flows

                for KeyValue (conveyor, conveyorState) in conveyors do
                    conveyorState.Update flows

                isDirty <- false


    module NetworkState =
        open Flips
        open Flips.Types

        let private createNodeMappings (links: Link list) =
            let links = HashSet()
            let nodeInputs = Dictionary<Node, HashSet<Link>>()
            let nodeOutputs = Dictionary<Node, HashSet<Link>>()
            let nodes = HashSet<Node>()
        
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

            links, nodes, nodeInputs, nodeOutputs

        let createInitialNetworkState (links: Link list) =
            let links, nodes, nodeInputs, nodeOutputs = createNodeMappings links

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
                | Node.Tank tank ->
                    let tankState = TankState (inputs, outputs)
                    tanks.[tank] <- tankState
            
                | Node.Valve valve -> 
                    let valveState = ValveState (inputs, outputs)
                    valves.[valve] <- valveState

                | Node.Conversion conversion ->
                    let conversionState = ConversionState (inputs, outputs)
                    conversions.[conversion] <- conversionState

                | Node.Conveyor conveyor ->
                    let conveyorState = ConveyorState (inputs, outputs)
                    conveyors.[conveyor] <- conveyorState

            (links, conversions, conveyors, tanks, valves)


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
            
    
    type TankFillListener (trigger: Trigger, tank: Tank, target: float) =

        member _.IsCompleted (ns: NetworkState) =
            ns.Tanks.[tank].Level >= target

        member _.Trigger = trigger


    type TankDrainListener (trigger: Trigger, tank: Tank, target: float) =
    
        member _.IsCompleted (ns: NetworkState) =
            ns.Tanks.[tank].Level <= target
        
        member _.Trigger = trigger


    type AccumulatedInFlowListener (trigger: Trigger, tank: Tank, targetFlow: float) =
        let mutable accumulatedFlow = 0.0

        member _.Step (networkState: NetworkState, ts: TimeSpan) =
            accumulatedFlow <- networkState.Tanks.[tank].FillRate * ts.TotalSeconds

        member _.IsCompleted () =
            accumulatedFlow >= targetFlow

        member _.Trigger = trigger


    type ElapsedTimeListener (trigger: Trigger, timespan: TimeSpan) =
        let mutable accumulatedTime = TimeSpan ()

        member _.Step (ts: TimeSpan) =
            accumulatedTime <- accumulatedTime + ts

        member _.IsCompleted () =
            accumulatedTime >= timespan

        member _.Trigger = trigger


    type Listener =
        | TankFill of TankFillListener
        | TankDrain of TankDrainListener
        | InFlow of AccumulatedInFlowListener
        | ElapsedTime of ElapsedTimeListener

    module Listener =

        let create (trigger: Trigger) =
            match trigger.TriggerType with
            | TriggerType.TankFillTo (tank, target) -> Listener.TankFill (TankFillListener (trigger, tank, target))
            | TriggerType.TankDrainTo (tank, target) -> Listener.TankDrain (TankDrainListener (trigger, tank, target))
            | TriggerType.AccumulatedInFlow (tank, targetFlow) -> Listener.InFlow (AccumulatedInFlowListener (trigger, tank, targetFlow))
            | TriggerType.ElapsedTime timeSpan -> Listener.ElapsedTime (ElapsedTimeListener (trigger, timeSpan))


    type TriggerType =
        | TankFillTo of tank: Tank * target: float
        | TankDrainTo of tank: Tank * target: float
        | AccumulatedInFlow of tank: Tank * totalFlow: float
        // Will enable later if required
        //| AccumulatedOutFlow of node: INode * totalFlow: float
        | ElapsedTime of timespan: TimeSpan

    type Trigger = {
        Id : int64
        TriggerType : TriggerType
    }

    type Event<'EventType> = {
        Time : DateTime
        Type : 'EventType
    }

    type TriggerHandler<'Message, 'EventType, 'Facility when 'Facility :> IFacility> = 'Message -> State<'Message, 'EventType, 'Facility> -> unit
    type MessageHandler<'Message, 'EventType, 'Facility when 'Facility :> IFacility> = 'Message -> State<'Message, 'EventType, 'Facility> -> unit
    type EventHandler<'Message, 'EventType, 'Facility when 'Facility :> IFacility> = Event<'EventType> -> State<'Message, 'EventType, 'Facility> -> unit

    type IFacility =
        abstract member TimeTillNetworkChange : unit -> TimeSpan
        abstract member StepNetwork : TimeSpan -> Trigger list
        abstract member RefreshNetwork : unit -> unit

    type State<'Message, 'EventType, 'Facility when 'Facility :> IFacility> 
        (initialId: int, 
         startTime: DateTime, 
         facility: 'Facility) =

        let mutable nextId = initialId
        let events = PriorityQueue<DateTime, Event<'EventType>> ()
        let messages = Queue<'Message>()
        let mutable now = startTime

        // Only directly manipulated by the `run` function
        member internal _.Messages = messages
        member internal _.Events = events

        member val Facility = facility

        member _.AddEvent (event: Event<_>) =
            events.Enqueue (event.Time, event)

        member _.AddMessage message =
            messages.Enqueue message

        member _.NextId =
            let next = nextId 
            nextId <- next + 1
            next

        member _.Now = now
        member _.SetNow newNow =
            now <- newNow


    module Simulation =

        let rec private messageLoop (messageHandler: MessageHandler<_,_,_>) (state: State<_,_,_>) =
            if state.Messages.Count > 0 then
                let nextMessage = state.Messages.Dequeue ()
                messageHandler nextMessage state
                messageLoop messageHandler state


        let rec run (triggerHandler: TriggerHandler<_,_,_>) (messageHandler: MessageHandler<_,_,_>) (eventHandler: EventHandler<_,_,_>) (maxDateTime: DateTime) (state: State<_,_,_>) =
            messageLoop messageHandler state
        
            let nextEventTime = state.Events.TryNextPriority ()
            let nextNetworkTime = state.Now + state.Facility.TimeTillNetworkChange ()

            match nextEventTime with
            | Some nextEventTime ->

                // Check that we are still within the desired simulation time period
                if nextEventTime > maxDateTime && nextNetworkTime > maxDateTime then
                    let elapsedTime = maxDateTime - state.Now
                    state.SetNow maxDateTime
                    state.Facility.StepNetwork elapsedTime
                    |> ignore
                    // We're done at this point

                // If an Event is going to occur before a network change
                elif nextEventTime < nextNetworkTime then
                    let elapsedTime = nextEventTime - state.Now
                    state.SetNow nextEventTime
                    let newTriggers = state.Facility.StepNetwork elapsedTime
                    for trigger in newTriggers do
                        triggerHandler trigger state

                    let nextTime, nextEvent = state.Events.Dequeue ()
                    eventHandler nextEvent state

                    // Evaluate network flows
                    state.Facility.RefreshNetwork ()
                    // Recurse
                    run triggerHandler messageHandler eventHandler maxDateTime state
                
                // The Network is going to change before an Event occurs
                else
                    // Network change is the next thing. We will 
                    let elapsedTime = nextNetworkTime - state.Now
                    state.SetNow nextNetworkTime
                    let newTriggers = state.Facility.StepNetwork elapsedTime
                    for trigger in newTriggers do
                        triggerHandler trigger state

                    // Evaluate network flows
                    state.Facility.RefreshNetwork ()
                    // Recurse
                    run triggerHandler messageHandler eventHandler maxDateTime state

            // There are no future events
            | None ->
                // The Network is changing before the MaxDateTime
                if nextNetworkTime < maxDateTime then
                    let elapsedTime = nextNetworkTime - state.Now
                    state.SetNow nextNetworkTime
                    let newMessages = state.Facility.StepNetwork elapsedTime
                    for message in newMessages do
                        state.Messages.Enqueue message
                    // Recurse
                    run triggerHandler messageHandler eventHandler maxDateTime state

                // There is no change to the State which occurs before the MaxDateTime
                else
                    let elapsedTime = maxDateTime - state.Now
                    state.SetNow maxDateTime
                    state.Facility.StepNetwork elapsedTime
                    |> ignore
                    // We're done at this point

