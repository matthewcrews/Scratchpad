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

    module Queue =

        let create name policy =
            {
                Name = QueueName name
                Policy = policy
            }

    type Distribution =
        | Constant of float
        | Uniform of lowerBound:float * upperBound:float

    type OperationName = OperationName of string

    type Operation = {
        Name : OperationName
        Source : Queue
        Sink : Queue
        Duration : Distribution
    }

    module Operation =

        let create name source sink duration =
            {
                Name = OperationName name
                Source = source
                Sink = sink
                Duration = duration
            }

    type GeneratorName = GeneratorName of string

    type Generator = {
        Name : GeneratorName
        Distribution : Distribution
        Sink : Queue
    }

    module Generator =

        let create name distribution sink =
            {
                Name = GeneratorName name
                Distribution = distribution
                Sink = sink
            }

    type Model = {
        Queues : Map<QueueName, Queue>
        Operations : Map<OperationName, Operation>
        Generators : Map<GeneratorName, Generator>
    }

    module Model =

        let empty =
            {
                Queues = Map []
                Operations = Map []
                Generators = Map []
            }

        let addGenerator (generator: Generator) model =
            let newQueues =
                model.Queues
                |> Map.add generator.Sink.Name generator.Sink
            let newGenerators =
                model.Generators
                |> Map.add generator.Name generator
            { model with 
                Queues = newQueues
                Generators = newGenerators }

        let addOperation (operation: Operation) model =
            let newQueues =
                model.Queues
                |> Map.add operation.Source.Name operation.Source
                |> Map.add operation.Sink.Name operation.Sink
            let newOperations =
                model.Operations
                |> Map.add operation.Name operation
            { model with
                Queues = newQueues
                Operations = newOperations }

// These are the types to record the events that occur

module Simulation =

    open Model

    // A Job is a unit of action
    type JobId = JobId of int
    type Job = {
        Id : JobId
    }

    // An Operation processes jobs from a queue
    // type OperationId = OperationId of int
    type OperationState = {
        Operation : Operation
        Jobs : Set<Job>
    }

    // A Queue holds a set of Jobs waiting for an operation
    // type QueueId = QueueId of int
    type QueueState = {
        Queue : Queue
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