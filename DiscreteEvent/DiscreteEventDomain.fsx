open System.Collections.Generic

// A Model is what holds policy information
module Model =


    [<RequireQualifiedAccess>]
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

    module Distribution =

        let sample (distribution: Distribution) =
            match distribution with
            | Constant c -> c
            | Uniform (lowerBound, upperBound) -> lowerBound // Yes, this is wrong


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
        Queues : Set<Queue>
        Operations : Set<Operation>
        Generators : Set<Generator>
    }

    module Model =

        let empty =
            {
                Queues = Set []
                Operations = Set []
                Generators = Set []
            }

        let addGenerator (generator: Generator) model =
            let newQueues =
                model.Queues
                |> Set.add generator.Sink
            let newGenerators =
                model.Generators
                |> Set.add generator
            { model with 
                Queues = newQueues
                Generators = newGenerators }

        let addOperation (operation: Operation) model =
            let newQueues =
                model.Queues
                |> Set.add operation.Source
                |> Set.add operation.Sink
            let newOperations =
                model.Operations
                |> Set.add operation
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

    module Job =

        let create id = 
            {
                Id = id
            }

    // An Operation processes jobs from a queue
    // type OperationId = OperationId of int
    type OperationState = OperationState of Set<Job>

    // A Queue holds a set of Jobs waiting for an operation
    // type QueueId = QueueId of int
    type QueueState = QueueState of Set<Job>

    // Events are things that are scheduled to occur in the future
    type EventType =
        | Entered of job:Job * queue:Queue
        | Completed of job:Job * operation:Operation

    type EventId = EventId of int

    type Event = {
        Time : float
        EventId : EventId
        EventType : EventType
    }

    module Event =

        let create time eventId eventType =
            {
                Time = time
                EventId = eventId
                EventType = eventType
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
        Time : float
        FactType : FactType
    }


    // The current state of the system. Likely will need to be mutable
    type ModelState = {
        LastEventId : EventId
        LastJobId : JobId
        OperationStates : Map<Operation, OperationState>
        QueueStates : Map<Queue, QueueState>
    }

    module ModelState =

        let create (model: Model) =
            let operationStates =
                model.Operations
                |> Seq.map (fun x -> x, OperationState Set.empty)
                |> Map

            let queueStates =
                model.Queues
                |> Seq.map (fun x -> x, QueueState Set.empty)
                |> Map
            
            {
                LastEventId = EventId 0
                LastJobId = JobId 0
                OperationStates = operationStates
                QueueStates = queueStates
            }

        let nextJobId (modelState: ModelState) =
            let (JobId id) = modelState.LastJobId
            let next = JobId (id + 1)
            next, { modelState with LastJobId = next }

        let nextEventId (modelState: ModelState) =
            let (EventId id) = modelState.LastEventId
            let nextEventId = EventId (id + 1)
            nextEventId, { modelState with LastEventId = nextEventId }

    type Policy = ModelState -> Event -> ModelState

    type EventQueue = EventQueue of SortedSet<Event>


open Model
open Simulation

let q1 = Queue.create "Q1" QueuePolicy.FIFO
let q2 = Queue.create "Q2" QueuePolicy.FIFO
let g1 = Generator.create "G1" (Constant 2.0) q1
let op1 = Operation.create "OP1" q1 q2 (Constant 5.0)

let model =
    Model.empty
    |> Model.addOperation op1
    |> Model.addGenerator g1


(*

Steps for simulation
1. Loops through the generators and generate events up until the time exceeds the max time

*)

let initialize (simulationDuration: float) (model: Model) : ModelState * EventQueue =
    let modelState = ModelState.create model
    let eventQueue = SortedSet<Simulation.Event>()

    let rec addEvents (lastTime: float) (maxTime: float) (modelState: ModelState, queue: SortedSet<Simulation.Event>) (generator: Generator) =
        let nextTimespan = Distribution.sample generator.Distribution
        let nextTime = lastTime + nextTimespan
        if nextTime > maxTime then
            modelState, queue
        else
            let nextJobId, modelState = ModelState.nextJobId modelState
            let newJob = Job.create nextJobId
            let nextEventId, modelState = ModelState.nextEventId modelState
            let newEvent = Event.create nextTime nextEventId (EventType.Entered (newJob, generator.Sink))
            queue.Add newEvent |> ignore
            addEvents nextTime maxTime (modelState, queue) generator


    let modelState, queue =
        ((modelState, eventQueue), model.Generators)
        ||> Seq.fold (addEvents 0.0 simulationDuration)

    modelState, EventQueue queue

let state, eventQueue = initialize 10.0 model
let (EventQueue queue) = eventQueue
for e in queue do
    printfn $"{e}"