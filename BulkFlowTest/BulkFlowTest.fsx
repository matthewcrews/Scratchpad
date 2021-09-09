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

    type Distribution =
        | Static of float
        | Uniform of min:float * max:float

    type Failure = {
        Occurance : Distribution
        Recovery : Distribution
    }

    type Tank = {
        Name : string
        Capacity : float
    }

    type Process = {
        Name : string
        Failure : Failure
    }

    type Split = {
        Name : string
    }

    type Merge = {
        Name : string
    }

    type Valve = {
        Name : string
    }

    [<RequireQualifiedAccess>]
    type Node =
        | Tank of Tank
        | Process of Process
        | Split of Split
        | Merge of Merge
        | Valve of Valve
        with
            member x.Name =
                match x with
                | Node.Tank t -> t.Name
                | Node.Process p -> p.Name
                | Node.Split s -> s.Name
                | Node.Merge m -> m.Name
                | Node.Valve v -> v.Name

    type Link = {
        Source : Node
        Sink : Node
    } with
        member x.Name = $"{x.Source.Name}->{x.Sink.Name}"

    type Network = {
        Links : Link list
    }

module rec Simulation =

    type Material = {
        Value : string
    }

    type Proportion = {
        Value : float
    }

    type Mix = {
        Value : Map<Material, Proportion>
    }

    type Flow = {
        Mix : Mix
        Rate : float
    }
        

    [<RequireQualifiedAccess>]
    type MergeSetting =
        | Single of link: Modeling.Link
        | Mix of Map<Modeling.Link, Proportion>

    [<RequireQualifiedAccess>]
    type SplitSetting =
        | Single of link: Modeling.Link
        | Mix of Map<Modeling.Link, Proportion>

    type Layer (mix: Mix, quantity: float) =
        let mix = mix
        let mutable quantity = quantity

        member _.Mix = mix
        member _.Quantity = quantity

        member _.Remove x =
            quantity <- quantity - x       


    type Tank (name: string, capacity: float, input: Modeling.Node list, outputs: Modeling.Node list) =
        member val Name = name
        member val Input = input
        member val Outputs = outputs
        member val Layers = Queue<Layer>()
        member val Capacity = capacity

        member x.Level =
            x.Layers
            |> Seq.sumBy (fun layer -> layer.Quantity)

    [<RequireQualifiedAccess>]
    type TankStatus =
        | Full
        | Empty
        | Filling of float

    type TankState = {
        Status : TankStatus
        Layers : Stack<Layer>
    }

    [<RequireQualifiedAccess>]
    type ProcessStatus =
        | Up
        | Down
        | Off

    type ProcessSetting = {
        MaxRate : float
        ConversionFactor : float
        Output : Material
    }

    type ProcessState = {
        Setting : ProcessSetting
        Status : ProcessStatus
        Failures : DateTime list
    }

    type SplitState = {
        Setting : SplitSetting
    }

    type MergeState = {
        Setting : MergeSetting
    }

    [<RequireQualifiedAccess>]
    type ValveSetting =
        | Open
        | Closed

    type ValveState = {
        Setting : ValveSetting
    }

    type Site = {
        Tanks : Dictionary<Tank, TankState>
        Processes : Dictionary<Process, ProcessState>
        Merges : Dictionary<Merge, MergeState>
        Splits : Dictionary<Split, SplitState>
        Valves : Dictionary<Valve, ValveState>
    }



module Planning =

    open Modeling

    [<RequireQualifiedAccess>]
    type Action =
        | SetMerge of MergeSetting
        | SetSplit of SplitSetting
        | OpenValve of Valve
        | CloseValve of Valve

    [<RequireQualifiedAccess>]
    type Trigger =
        | Time of time: DateTime
        | FillTo of tank: Tank * level: float
        | DrainTo of tank: Tank * level: float
        | FillQuantity of tank: Tank * amount: float
        | DrainQuantity of tank: Tank * amount: float
        | ForDuration of duration: TimeSpan

    type Step = {
        Trigger : Trigger
        Actions : Action list
    }

    type PlanName = PlanName of string

    type Plan = {
        Name : PlanName
        Steps : Step list
    }


module Simulation =

    open Modeling

    type FlowEvent =
        | Filled of Tank
        | Emptied of Tank
        | Trigger of Planning.Trigger

    type EventType =
        | Failed of Process
        | Recovered of Process

    type Event = {
        Id : int64
        Time : DateTime
        Type : EventType
    }

    type State (initialId: int, startTime: DateTime, site: Site) =
        let mutable nextId = initialId
        let events = PriorityQueue<DateTime, Event> ()
        let mutable now = startTime

        member val Site = site

        member this.TryNextEvent () =
            match events.TryDequeue () with
            | Some (nextTime, nextAction) ->
                now <- nextTime
                Some (nextTime, nextAction)
            | None ->
                None

        member _.AddEvent (time, act) =
            events.Add (time, act)

        member _.NextId
            with get () =
                let next = nextId 
                nextId <- next + 1
                next

        member _.Now = now
