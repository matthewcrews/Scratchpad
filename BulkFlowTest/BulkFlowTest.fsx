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

    //type TankNode = {
    //    Inputs : INode list
    //    Outputs : INode list
    //}

    //type ValveNode = {
    //    Input : INode
    //    Output : INode
    //}

    //type ConversionNode = {
    //    Input : INode
    //    Output : INode
    //}

    //type ConveyorNode = {
    //    Inputs : INode list
    //    Output : INode
    //}

    //type Network = {
    //    Tanks : TankNode list
    //    Valves : ValveNode list
    //    Conversions : ConversionNode list
    //    Conveyors : ConveyorNode list
    //}


    type ValveStatus =
        | Open
        | Closed
        | Set of maxRate: float

    type ValveState = {
        Status : ValveStatus
        Inputs : Link list
        Outputs : Link list
    }

    module ValveState =

        let create inputs outputs =
            {
                Status = ValveStatus.Closed
                Inputs = inputs
                Outputs = outputs
            }

    type ConversionState = {
        ConversionRate : float
        Inputs : Link list
        Outputs : Link list
    }

    module ConversionState =

        let create inputs outputs =
            {
                ConversionRate = 1.0
                Inputs = inputs
                Outputs = outputs
            }

    type TankStatus =
        | Full
        | Empty
        | Partial of currentLevel: float

    type TankState = {
        //MaxCapacity : float
        Status : TankStatus
        DrainRate : float
        FillRate : float
        Inputs : Link list
        Outputs : Link list
    }

    module TankState =

        let create inputs outputs =
            {
                Status = TankStatus.Empty
                DrainRate = 0.0
                FillRate = 0.0
                Inputs = inputs
                Outputs = outputs
            }

    type ConveyorLoad = {
        ConveyorLocation : float
        ConveyorLoadingVelocity : float
        InputRate : float
    }

    type ConveyorState = {
        Inputs : Link list
        Outputs : Link list
        CurrentVelocity : float
        CurrentLocation : float
        Loads : ResizeArray<ConveyorLoad>
    }

    module ConveyorState =

        let create inputs outputs =
            {
                Inputs = inputs
                Outputs = outputs
                CurrentVelocity = 1.0
                CurrentLocation = 0.0
                Loads = ResizeArray()
            }

    type NetworkState = {
        Valves : Dictionary<Valve, ValveState>
        Conversions : Dictionary<Conversion, ConversionState>
        Tanks : Dictionary<Tank, TankState>
        Conveyors : Dictionary<Conveyor, ConveyorState>
    }

    module NetworkState =

        let empty () =
            {
                Valves = Dictionary ()
                Conversions = Dictionary ()
                Tanks = Dictionary ()
                Conveyors = Dictionary ()
            }

        let private createInputOutputMapping (links: Link list) =
            let nodeInputs = Dictionary<INode, HashSet<Link>>()
            let nodeOutputs = Dictionary<INode, HashSet<Link>>()
            let nodes = HashSet<INode>()
            
            for link in links do
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

        let create (links: Link list) =
            let nodes, nodeInputs, nodeOutputs = createInputOutputMapping links
            let result = empty ()

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
                    let tankState = TankState.create inputs outputs
                    result.Tanks.[tank] <- tankState
                
                | :? Valve as valve -> 
                    let valveState = ValveState.create inputs outputs
                    result.Valves.[valve] <- valveState

                | :? Conversion as conversion ->
                    let conversionState = ConversionState.create inputs outputs
                    result.Conversions.[conversion] <- conversionState

                | :? Conveyor as conveyor ->
                    let conveyorState = ConveyorState.create inputs outputs
                    result.Conveyors.[conveyor] <- conveyorState
                | _ -> invalidArg (nameof node) "Unsupported node type in network"

            result
            


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
