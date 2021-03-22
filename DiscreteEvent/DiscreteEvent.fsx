open System.Collections.Generic


module rec Domain =

    
    type StateId = StateId of int64
    type TimeStamp = TimeStamp of float
    type TimeSpan = TimeSpan of float
        with

        static member (+) (TimeStamp stamp, TimeSpan span) =
            TimeStamp (stamp + span)

        static member (+) (span:TimeSpan, stamp:TimeStamp) =
            stamp + span

    type ResourceName = ResourceName of string
    type ResourceId = ResourceId of int64
    type Resource = Resource of resourceName:ResourceName * resourceId:ResourceId
    [<RequireQualifiedAccess>]
    type Availability =
        | Free
        | Allocated of allocationId:AllocationId

    type AllocationId = AllocationId of int64
    module AllocationId =
        let next allocationId =
            let (AllocationId a) = allocationId
            AllocationId (a + 1L)
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

    type PlanId = PlanId of int64
    type Plan = {
        PlanId : PlanId
        Steps : Step list
    }
    type CurrentStep = {
        Step : Step
        Possibility : Possibility
    }
    type PlanState = {
        StateId : StateId
        Processed : Step list
        Pending : Step list
        Current : CurrentStep

    }
    type PossibilityId = PossibilityId of int64
    type PossibilityType = 
        | Delay

    type Possibility =
        {
            PossibilityId : PossibilityId
            TimeStamp : TimeStamp
            PossibilityType : PossibilityType
            PlanId : PlanId
        }

    //type LocationId = LocationId of int64
    //type LocationName = LocationName of string
    //type Location = Location of locationId:LocationId * locationName:LocationName
    type AllocationRequest =
        | OneOf of Set<Resource>
        // | Nof of number:int * Set<Resource>

    type ModelState = {
        LastPossibilityId : PossibilityId
        LastPlanId : PlanId
        LastAllocationId : AllocationId
        Availabilities : Map<Resource, Availability>
        Allocations : Map<AllocationId, Set<Resource>>
        PlanStates : Map<PlanId, PlanState>
        Possibilities : SortedSet<Possibility>
        AllocationRequests : Map<ResourceId, Set<Allocation>>
    }

    module ModelState =

        let nextAllocationId (s: ModelState) =
            let next = AllocationId.next s.LastAllocationId
            next, { s with LastAllocationId = next }

module Planning =

    open Domain

    type State<'a, 's> = ('s -> 'a * 's)

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

    type PlanAcc = PlanAcc of modelState:ModelState * lastStepId:StepId * steps:Step list

    let state = PlanBuilder()

    let allocateOneOf (resources: Set<Resource>) =
        printfn "AllocateOneOf"
        state {
            let! (PlanAcc (state, lastStepId, steps)) = State.getState
            let nextStepId = StepId.next lastStepId
            let nextAllocationId, newModelState = ModelState.nextAllocationId state
            let allocation = {
                AllocationId = nextAllocationId
                Resources = resources
            }
            let stepType = StepType.Allocate allocation
            let newStep = {
                StepId = nextStepId
                StepType = stepType
            }
            let newAcc = PlanAcc (newModelState, nextStepId, newStep::steps)
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

open Domain

let initialState = {
    LastEventId = EventId 0L
    LastStateId = StateId 0L
    LastPlanId = PlanId 0L
    LastAllocationId = AllocationId 0L
    Now = TimeStamp 0.0
    ItemLocations = Map []
    Locations = Map []
    Capacities = Map []
    Availabilities = Map []
    Flows = Map []
    Allocations = Map []
}

let allocate (resourceRequest: ResourceRequest list) =
    fun (state: ModelState, plan: Plan) ->
        let (AllocationId requestId) = state.LastAllocationId
        let nextAllocationId = AllocationId (requestId + 1L)
        let newState = { state with LastAllocationId = nextAllocationId }
        let newStep = Step.Allocate (nextAllocationId, resourceRequest)
        (newState, { plan with Steps = newStep::plan.Steps }), nextAllocationId


let newState, plan =
    PlanBuilder initialState {
        let! allocationId = allocate []
        pause 10.0
        pause 1.0
    }

        
