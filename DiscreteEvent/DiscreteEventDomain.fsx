// A Model is what holds policy information
module Model =

    type QueuePolicy =
        | FIFO
        | LIFO

    type QueueName = QueueName of string

    type Queue = {
        Name : QueueName
        Policy : QueuePolicy
    }

    type Distribution =
        | Constant of float
        | Uniform of lowerBound:float * upperBound:float

    type OperationName = OperationName of string

    type Operation = {
        Name : string
        Source : Queue
        Sink : Queue
        Duration : Distribution
    }

    type GeneratorName = GeneratorName of string

    type Generator = {
        Name : GeneratorName
        Distribution : Distribution
        Sink : Queue
    }

    type Model = {
        Queues : Map<QueueName, Queue>
        Operations : Map<OperationName, Operation>
        Generators : Map<GeneratorName, Generator>
    }

// These are the types to record the events that occur

module Simulation =

    // A Job is a unit of action
    type JobId = JobId of int
    type Job = {
        Id : JobId
    }

    // An Operation processes jobs from a queue
    type OperationId = OperationId of int
    type Operation = {
        Id : OperationId
        Name : string
        Jobs : Set<Job>
    }

    // A Queue holds a set of Jobs waiting for an operation
    type QueueId = QueueId of int
    type Queue = {
        Id : QueueId
        Name : string
        Jobs : Set<Job>
    }

    // Events are things that are scheduled to occur in the future
    type EventType =
        | Completed of job:Job * operation:Operation
        | Entered of job:Job * queue:Queue

    type EventId = EventId of int

    type Event = {
        EventId : EventId
        EventType : EventType
    }

    // Facts are things that happend. They are pre-pended to the history list
    type FactType =
        | Enqueue of job:Job * queue:Queue
        | Dequeue of job:Job * queue:Queue
        | Start of job:Job * operation:Operation
        | End of job:Job * operation:Operation

    type FactId = FactId of int

    type Fact = {
        Source : Event
        FactId : FactId
        Time : decimal
        FactType : FactType
    }


    // The current state of the system. Likely will need to be mutable
    type State = {
        Operations : Map<OperationId, Operation>
        Queues : Map<QueueId, Queue>
    }

    type Policy = State -> Event -> State