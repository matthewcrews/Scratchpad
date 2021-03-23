//open System.Collections.Generic


module rec Domain =

    type Distribution =
        | Constant of float
        | Uniform of lowerBound:float * upperBound:float

    module Distribution =

        let sample (distribution: Distribution) =
            match distribution with
            | Constant c -> c
            | Uniform (lowerBound, upperBound) -> lowerBound // Yes, this is wrong

    type GeneratorName = GeneratorName of string

    type Generator = {
        Name : GeneratorName
        Distribution : Distribution
        PossibilityType : PossibilityType
    }

    module Generator =
    
        let create name distribution possibilityType =
            {
                Name = name
                Distribution = distribution
                PossibilityType = possibilityType
            }

    type Model = {
        Resources : Set<Resource>
        Generators : Set<Generator>
    }
    
    type StateId = StateId of int64
    type TimeStamp = TimeStamp of float
    module TimeStamp =

        let zero = TimeStamp 0.0

    type TimeSpan = TimeSpan of float
        with

        static member (+) (TimeStamp stamp, TimeSpan span) =
            TimeStamp (stamp + span)

        static member (+) (span:TimeSpan, stamp:TimeStamp) =
            stamp + span

    //type ResourceName = ResourceName of string
    //type ResourceId = ResourceId of int64
    //type Resource = Resource of resourceName:ResourceName * resourceId:ResourceId
    type Resource = Resource of string
    [<RequireQualifiedAccess>]
    type Availability =
        | Free
        | Allocated of allocationId:AllocationId

    type AllocationId = AllocationId of int64
    module AllocationId =

        let next (AllocationId allocationId) =
            AllocationId (allocationId + 1L)

    type Allocation = {
        AllocationId : AllocationId
        Quantity : int
        Resources : Set<Resource>
    }

    [<RequireQualifiedAccess>]
    type StepType =
        | Allocate of allocation: Allocation
        //| Free of allocationId: AllocationId
        | Delay of timeSpan: TimeSpan
        //| Move of item: ItemId * location: Location
        // | Open of flow: Flow * flowDescription: FlowDescription
        // | Close of flow: Flow

    type StepId = StepId of int64
    module StepId =
        let next stepId =
            let (StepId s) = stepId
            StepId (s + 1L)

    type Step = {
        StepId : StepId
        StepType : StepType
    }
    type Plan = Plan of Step list
    type ProcedureId = ProcedureId of int64
    module ProcedureId =
        let next (ProcedureId i) =
            ProcedureId (i + 1L)
    type ProcedureState = {
        ProcedureId : ProcedureId
        StateId : StateId
        Processed : Step list
        Pending : Step list
    }
    module ProcedureState =
        
        let create procedureId (Plan steps) =
            {
                ProcedureId = procedureId
                StateId = StateId 0L
                Pending = steps
                Processed = []
            }

    type InstantId = InstantId of int64
    module InstantId =
        
        let next (InstantId i) =
            InstantId (i + 1L)

    type InstantType =
        | Free of procedureId: ProcedureId * allocation: Allocation
        | Increment of ProcedureId
        | ProcessNext of ProcedureId
    type Instant = {
        InstantId : InstantId
        InstantType : InstantType
    }
    module Instant =

        let create instantId instantType =
            {
                InstantId = instantId
                InstantType = instantType
            }

    type PossibilityId = PossibilityId of int64
    module PossibilityId =
        
        let next (PossibilityId lastPossibilityId) =
            PossibilityId (lastPossibilityId + 1L)

    type PossibilityType = 
        | Delay of procedureId: ProcedureId * stateId: StateId
        | PlanArrival of plan: Plan
        //| Increment of procedureId: ProcedureId * stateId: StateId

    type Possibility =
        {
            PossibilityId : PossibilityId
            TimeStamp : TimeStamp
            PossibilityType : PossibilityType
        }

    module Possibility =
        
        let create possibilityId timeStamp possibilityType =
            {
                PossibilityId = possibilityId
                TimeStamp = timeStamp
                PossibilityType = possibilityType
            }

    //type LocationId = LocationId of int64
    //type LocationName = LocationName of string
    //type Location = Location of locationId:LocationId * locationName:LocationName
    type AllocationRequest = {
        ProcedureId : ProcedureId
        AllocationId : AllocationId
        StateId : StateId
        Quantity : int
        Resources : Set<Resource>
    }
    module AllocationRequest =
        let create (procedure: ProcedureState) (allocation: Allocation) =
            {
                ProcedureId = procedure.ProcedureId
                AllocationId = allocation.AllocationId
                StateId = procedure.StateId
                Quantity = allocation.Quantity
                Resources = allocation.Resources
            }

    type ModelState = {
        Now : TimeStamp
        LastPossibilityId : PossibilityId
        LastProcedureId : ProcedureId
        LastInstantId : InstantId
        FreeResources : Set<Resource>
        Allocations : Map<ProcedureId * AllocationId, Set<Resource>>
        ProcedureStates : Map<ProcedureId, ProcedureState>
        Instants : Set<Instant>
        Possibilities : Set<Possibility>
        OpenRequests : Set<AllocationRequest>
        //RequestToResource : Map<ProcedureId * AllocationId, Resource>
        //ResourceToRequest : Map<Resource, Set<ProcedureId * AllocationId>>
    }

    module ModelState =

        let initial =
            {
                Now = TimeStamp 0.0
                LastPossibilityId = PossibilityId 0L
                LastProcedureId = ProcedureId 0L
                LastInstantId = InstantId 0L
                FreeResources = Set.empty
                Allocations = Map.empty
                ProcedureStates = Map.empty
                Instants = Set.empty
                Possibilities = Set.empty
                OpenRequests = Set.empty
            }

        let nextPossibilityId (s: ModelState) =
            let next = PossibilityId.next s.LastPossibilityId
            next, { s with LastPossibilityId = next }

        let nextProcedureId (s: ModelState) =
            let next = ProcedureId.next s.LastProcedureId
            next, { s with LastProcedureId = next}

        module Initializers =

            let addPossibility possibility modelState =
                { modelState with Possibilities = Set.add possibility modelState.Possibilities }


            let addPossibilities (maxTime: TimeStamp) (generators: seq<Generator>) modelState : ModelState =

                let rec add (lastTime: TimeStamp) (maxTime: TimeStamp) (modelState: ModelState) (generator: Generator) =
                    let nextTimespan = Distribution.sample generator.Distribution
                    let nextTime = lastTime + (TimeSpan nextTimespan)
                    if nextTime > maxTime then
                        modelState
                    else
                        let nextPossibilityId, modelState = ModelState.nextPossibilityId modelState
                        let nextPossibility = Possibility.create nextPossibilityId nextTime generator.PossibilityType
                        addPossibility nextPossibility modelState


                let modelState =
                    (modelState, generators)
                    ||> Seq.fold (add TimeStamp.zero maxTime)

                modelState


            let addResources resources modelState : ModelState =

                let add modelState resource =
                    { modelState with FreeResources = Set.add resource modelState.FreeResources }
            
                (modelState, resources)
                ||> Seq.fold add

        let initialize (maxTime: TimeStamp) (model: Model) =
            
            initial
            |> Initializers.addResources (model.Resources)
            |> Initializers.addPossibilities maxTime model.Generators

        let nextPossibility (modelState: ModelState) =
            match modelState.Possibilities.IsEmpty with
            | true -> None
            | false -> 
                modelState.Possibilities
                |> Seq.sortBy (fun x -> x.TimeStamp, x.PossibilityId)
                |> Seq.head
                |> Some

        let nextInstant (m: ModelState) =
            match m.Instants.IsEmpty with
            | true -> None
            | false ->
                m.Instants
                |> Seq.sortBy (fun x -> x.InstantId)
                |> Seq.head
                |> Some

        let setProcedureState procedureId procedureState (m: ModelState) =
            { m with ProcedureStates = Map.add procedureId procedureState m.ProcedureStates }

        let addInstant instantType (m: ModelState) =
            let nextInstantId = InstantId.next m.LastInstantId
            let nextInstant = Instant.create nextInstantId instantType
            { m with 
                LastInstantId = nextInstantId
                Instants = Set.add nextInstant m.Instants
            }

        let removeInstant (i: Instant) (m: ModelState) =
            { m with Instants = Set.remove i m.Instants }

        let addAllocationRequest (a: AllocationRequest) (m: ModelState) =
            { m with OpenRequests = Set.add a m.OpenRequests }

        let addPossibility (delay: TimeSpan) (possibilityType: PossibilityType) (m: ModelState) =
            let nextPossibilityId = PossibilityId.next m.LastPossibilityId
            let possibility = Possibility.create nextPossibilityId (m.Now + delay) possibilityType
            { m with
                LastPossibilityId = nextPossibilityId
                Possibilities = Set.add possibility m.Possibilities
            }

        let removePossibility (p: Possibility) (m: ModelState) =
            { m with Possibilities = Set.remove p m.Possibilities }

        let startProcedure plan (m: ModelState) =
            let nextProcedureId = ProcedureId.next m.LastProcedureId
            let p = ProcedureState.create nextProcedureId plan
            { m with
                 ProcedureStates = Map.add nextProcedureId p m.ProcedureStates
            } |> addInstant (InstantType.ProcessNext nextProcedureId)

module Simulation =

    open Domain
    open ModelState

    type SimulationState =
        | Complete of modelState: ModelState
        | Processing of modelState: ModelState

    module Instant =

        let private free (procedureId: ProcedureId) (allocation: Allocation) (state: ModelState) =
            { state with
                FreeResources = allocation.Resources + state.FreeResources
                Allocations = Map.remove (procedureId, allocation.AllocationId) state.Allocations
            }

        let private increment (procedureId: ProcedureId) (state: ModelState) =
            let procedureState = state.ProcedureStates.[procedureId]
            match procedureState.Pending with
            | [] ->
                // Should report empty plan
                state
            | next::remaining ->
                let newProcedureState =
                    { procedureState with
                        Processed = next::procedureState.Processed
                        Pending = remaining
                    }

                setProcedureState procedureId newProcedureState state
                |> addInstant (InstantType.ProcessNext procedureId)

        let private processNext (procedureId: ProcedureId) (state: ModelState) =
            let procedureState = state.ProcedureStates.[procedureId]
            match procedureState.Pending with
            | [] ->
                // Should report an empty plan
                state
            | next::remaining ->
                match next.StepType with
                | StepType.Allocate a ->
                    let request = AllocationRequest.create procedureState a
                    addAllocationRequest request state
                | StepType.Delay s ->
                    addPossibility s (PossibilityType.Delay (procedureState.ProcedureId, procedureState.StateId)) state

        let handle (i: Instant) (m: ModelState) =
            match i.InstantType with
            | InstantType.Free (procedureId, allocationId) -> free procedureId allocationId m
            | InstantType.Increment procedureId -> increment procedureId m
            | InstantType.ProcessNext procedureId -> processNext procedureId m
            |> removeInstant i


    module Possibility =

        let private planArrival plan (modelState: ModelState) =
            startProcedure plan modelState
            // Should record the arrival of plan

        let private delay (procedureId: ProcedureId) (stateId: StateId) (m: ModelState) =
            let p = m.ProcedureStates.[procedureId]

            if p.StateId = stateId then
                addInstant (InstantType.Increment procedureId) m
            else
                // Record that ProcedureState not in the same state
                m

        let handle (next: Possibility) (modelState: ModelState) : ModelState =
            let m = { modelState with Now = next.TimeStamp }
            match next.PossibilityType with
            | PlanArrival plan -> 
                planArrival plan m
            | Delay (procedureId, stateId) -> 
                delay procedureId stateId modelState
            |> removePossibility next
            

    let step (maxTime: TimeStamp) (m: ModelState) =

        match ModelState.nextInstant m with
        | None ->
            // Allocation needs to go here
            match ModelState.nextPossibility m with
            | None ->
                Complete m
            | Some p ->
                if p.TimeStamp > maxTime then
                    Complete m
                else
                    Possibility.handle p m
                    |> Processing
        | Some i ->
            Instant.handle i m
            |> Processing
            


module Planning =

    open Domain

    type State<'a, 's> = ('s -> 'a * 's)
    type PlanAcc = PlanAcc of lastAllocationId:AllocationId * lastStepId:StepId * steps:Step list

    module State =
        // Explicit
        // let result x : State<'a, 's> = fun s -> x, s
        // Less explicit but works better with other, existing functions:
        let result x s = 
            x, s

        let bind (f:'a -> State<'b, 's>) (m:State<'a, 's>) : State<'b, 's> =
            // return a function that takes the state
            fun s ->
                // Get the value and next state from the m parameter
                let a, s' = m s
                // Get the next state computation by passing a to the f parameter
                let m' = f a
                // Apply the next state to the next computation
                m' s'

        /// Evaluates the computation, returning the result value.
        let eval (m:State<'a, 's>) (s:'s) = 
            m s 
            |> fst

        /// Executes the computation, returning the final state.
        let exec (m:State<'a, 's>) (s:'s) = 
            m s
            |> snd

        /// Returns the state as the value.
        let getState (s:'s) = 
            s, s

        /// Ignores the state passed in favor of the provided state value.
        let setState (s:'s) = 
            fun _ -> 
                (), s


    type PlanBuilder() =
        member __.Return(value) : State<'a, 's> = 
            State.result value
        member __.Bind(m:State<'a, 's>, f:'a -> State<'b, 's>) : State<'b, 's> = 
            State.bind f m
        member __.ReturnFrom(m:State<'a, 's>) = 
            m
        member __.Zero() =
            State.result ()
        member __.Delay(f) = 
            State.bind f (State.result ())


    let state = PlanBuilder()

    let allocateOneOf (resources: Set<Resource>) : State<_,PlanAcc> =
        printfn "AllocateOneOf"
        state {
            let! (PlanAcc (lastAllocationId, lastStepId, steps)) = State.getState
            let nextStepId = StepId.next lastStepId
            let nextAllocationId = AllocationId.next lastAllocationId
            let allocation : Allocation = {
                AllocationId = nextAllocationId
                Quantity = 1
                Resources = resources
            }
            let stepType = StepType.Allocate allocation
            let newStep = {
                StepId = nextStepId
                StepType = stepType
            }
            let newAcc = PlanAcc (nextAllocationId, nextStepId, newStep::steps)
            do! State.setState newAcc
            return nextAllocationId
        }

    type PlanBuilder with

        [<CustomOperation("delay", MaintainsVariableSpaceUsingBind=true)>]
        member this.Delay (st:State<_,PlanAcc>, [<ProjectionParameter>] (duration: 'a -> TimeSpan)) =
            printfn $"Delay"
            state {
                let! x = st
                let d = duration x
                let! (PlanAcc (state, lastStepId, steps)) = State.getState
                let nextStepId = StepId.next lastStepId
                let stepType = StepType.Delay d
                let newStep = {
                    StepId = nextStepId
                    StepType = stepType
                }
                let newAcc = PlanAcc (state, nextStepId, newStep::steps)
                do! State.setState newAcc
                return x 
            }


        //[<CustomOperation("free", MaintainsVariableSpaceUsingBind=true)>]
        //member this.Free (st:State<_,PlanAcc>, [<ProjectionParameter>] (allocationId: 'a -> AllocationId)) =
        //    printfn $"Free"
        //    state {
        //        let! x = st
        //        let a = allocationId x
        //        let! (PlanAcc (state, lastStepId, steps)) = State.getState
        //        let nextStepId = StepId.next lastStepId
        //        let stepType = StepType.Free a
        //        let newStep = {
        //            StepId = nextStepId
        //            StepType = stepType
        //        }
        //        let newAcc = PlanAcc (state, nextStepId, newStep::steps)
        //        do! State.setState newAcc
        //        return x 
        //    }

    let planner = PlanBuilder ()

    let create (plan: State<_,_>) =
        let initialAcc = PlanAcc (AllocationId 0L, StepId 0L, [])
        let (PlanAcc (resultState, _, resultPlan)) = State.exec plan initialAcc
        Plan (List.rev resultPlan)

open Domain
open Planning

        
let resources = 
    [for i in 1..5 -> Resource $"Resource:{i}"]



let simplePlan =
    planner {
        let! a = allocateOneOf (Set resources)
        delay (TimeSpan 10.0)
        delay (TimeSpan 2.0)
    } |> Planning.create

