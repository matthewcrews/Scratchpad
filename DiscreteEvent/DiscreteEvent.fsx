open System.Collections.Generic


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
        Resources : Set<Resource>
    }

    [<RequireQualifiedAccess>]
    type StepType =
        | Allocate of allocation: Allocation
        | Free of allocationId: AllocationId
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
    type Procedure = {
        ProcedureId : ProcedureId
        Steps : Step list
    }
    type CurrentStep = {
        Step : Step
        Possibility : Possibility
    }
    type ProcedureState = {
        StateId : StateId
        Processed : Step list
        Pending : Step list
        Current : CurrentStep

    }
    type PossibilityId = PossibilityId of int64
    module PossibilityId =
        
        let next (PossibilityId lastPossibilityId) =
            PossibilityId (lastPossibilityId + 1L)

    type PossibilityType = 
        | Delay of procedureId: ProcedureId
        | PlanArrival of plan: Plan

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
    type AllocationRequest =
        | OneOf of Set<Resource>
        // | Nof of number:int * Set<Resource>

    type ModelState = {
        LastPossibilityId : PossibilityId
        LastProcedureId : ProcedureId
        FreeResources : Set<Resource>
        Allocations : Map<ProcedureId * AllocationId, Set<Resource>>
        ProcedureStates : Map<ProcedureId, ProcedureState>
        Possibilities : Set<Possibility>
        OpenRequests : Set<ProcedureId * AllocationId>
        AllocationRequests : Map<Resource, Set<ProcedureId * AllocationId>>
    }

    module ModelState =

        let nextPossibilityId (s: ModelState) =
            let next = PossibilityId.next s.LastPossibilityId
            next, { s with LastPossibilityId = next }

        let initial =
            {
                LastPossibilityId = PossibilityId 0L
                LastProcedureId = ProcedureId 0L
                FreeResources = Set.empty
                Allocations = Map.empty
                ProcedureStates = Map.empty
                Possibilities = Set.empty
                OpenRequests = Set.empty
                AllocationRequests = Map.empty
            }

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
                    ModelState.addPossibility nextPossibility modelState


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
            |> addResources (model.Resources)
            |> addPossibilities maxTime model.Generators
            


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
            let allocation = {
                AllocationId = nextAllocationId
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


        [<CustomOperation("free", MaintainsVariableSpaceUsingBind=true)>]
        member this.Free (st:State<_,PlanAcc>, [<ProjectionParameter>] (allocationId: 'a -> AllocationId)) =
            printfn $"Free"
            state {
                let! x = st
                let a = allocationId x
                let! (PlanAcc (state, lastStepId, steps)) = State.getState
                let nextStepId = StepId.next lastStepId
                let stepType = StepType.Free a
                let newStep = {
                    StepId = nextStepId
                    StepType = stepType
                }
                let newAcc = PlanAcc (state, nextStepId, newStep::steps)
                do! State.setState newAcc
                return x 
            }

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
        free a
    } |> Planning.create



module Model =


