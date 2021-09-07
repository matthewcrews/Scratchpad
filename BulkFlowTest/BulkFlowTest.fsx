open System
open System.Collections.Generic

module Modeling =

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
        MaxRate : float
        Conversion : float
        Failure : Failure
    }

    type Split = {
        Name : string
    }

    type Merge = {
        Name : string
    }

    type Node =
        | Tank of Tank
        | Process of Process
        | Split of Split
        | Merge of Merge

    type Link = {
        Source : Node
        Sink : Node
    }

    type Network = {
        Links : Link list
    }

    type Proportion = {
        Proportion : float
        Link : Link
    }

    type MergeSetting =
        | Single of link: Link
        | Proportional of proportions: Proportion list

    type SplitSetting =
        | Single of link: Link
        | Proportional of proportions: Proportion list

    type Action =
        | SetMerge of MergeSetting
        | SetSplit of SplitSetting

    type Trigger =
        | FillsTo of tank: Tank * level: float
        | EmptiesTo of tank: Tank * level: float
        | ForDuration of duration: TimeSpan

    type Step = {
        Trigger : Trigger
        Actions : Action list
    }

    type Plan = {
        Steps : Step list
    }


module rec State =

    type StateId = {
        Value : int64
    }

    type Status =
        | Up
        | Down

    type Layer (material: string, quantity: float) =
        let material = material
        let mutable quantity = quantity

        member _.Material = material
        member _.Quantity = quantity

        member _.Remove x =
            quantity <- quantity - x
        
        member _.Add x =
            quantity <- quantity + x

    type Tank = {
        StateId : StateId
        Layers : Stack<Layer>
    }

    type Process = {
        StateId : StateId
        Status : Status
        Failures : DateTime list
    }

    type Split = {
        StateId : StateId
        Setting : Modeling.SplitSetting
    }

    type Merge = {
        StateId : StateId
        Setting : Modeling.MergeSetting
    }


module Simulation =

    open Modeling

    type EventType =
        | Filled of Tank
        | Emptied of Tank
        | Failed of Process
        | Recovered of Process