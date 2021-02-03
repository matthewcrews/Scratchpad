module rec Types =
    
    open System.Collections.Generic
    // open Queue
    // open Process


    type Distribution =
        | Constant of float
        | Uniform of lowerBound:float * upperBound:float

    type Policy =
        | LIFO

    type Queue = {
        Name : string
        Policy : Policy
    }

    [<RequireQualifiedAccess>]
    module Queue =

        type EventType =
            | Enqueue of Queue
            | Dequeue of Queue


    type Process = {
        Name : string
        Duration : Distribution
    }

    [<RequireQualifiedAccess>]
    module Process =

        type EventType =
            | Enter of Process
            | Exit of Process

    type Node =
        | Queue of Queue
        | Process of Process

    type Connection = {
        Source : Node
        Sink : Node
    }


    type Event = {
        Id : int64
        Time : float
        EventType : Event.EventType
    }

    module Event =

        [<RequireQualifiedAccess>]
        type EventType =
            | QueueEventType of Queue.EventType
            | ProcessEventType of Process.EventType


    [<RequireQualifiedAccess>]
    type EntityType =
        | A


    type Generator = {
        Name : string
        Distribution : Distribution
        OutputQueue : Queue
    }

    type ProcessState = 
        | Processing of Entity
        | Idle

    type ProcessMoment = {
        ProcessState : ProcessState
        Time : float
    }

    type QueueState =
        | Empty
        | Enqueued of int

    type QueueMoment = {
        Time : float
        QueueState : QueueState
    }

    type SystemState = {
        Queues : Map<Queue, Queue<Event>>
        Processes : Map<Process, ProcessState>
    }

    type History = {
        Queues : Map<Queue, List<QueueState>>
    }
