module Domain =

    
    type StateId = StateId of int64
    type TimeStamp = TimeStamp of float

    type ResourceName = ResourceName of string
    type ResourceId = ResourceId of int64
    type Resource = Resource of resourceName:ResourceName * resourceId:ResourceId

    type AllocationId = AllocationId of int64
    type Allocation = {
        AllocationId : AllocationId
        Resources : Set<Resource>
    }

    type StepType =
        | Allocate of allocationId: AllocationId * resources: ResourceRequest list
        | Free of allocationId: AllocationId
        | Move of item: ItemId * location: Location
        | Delay of duration: float
        // | Open of flow: Flow * flowDescription: FlowDescription
        // | Close of flow: Flow

    type StepId = StepId of int64
    type Step = stepId:StepId * stepType:StepType

    type PlanId = PlanId of int64
    type Plan = {
        PlanId : PlanId
        Steps : Step list
    }
    type PlanState = {
        Processed : Step list
        Pending : Step list
        Current : Step * Event

    }


    type LocationId = LocationId of int64
    type LocationName = LocationName of string
    type Location = Location of locationId:LocationId * locationName:LocationName
    // type ItemId = ItemId of int64
    // type Item<'a> = {
    //     ItemId : ItemId
    //     Type : 'a
    // }



    // type Vessel = Vessel of string
    // type VesselId = VesselId of int64
    // type Flow = Flow of string
    // type FlowId = FlowId of int64
    // type FlowRate = FlowRate of float
    // type FlowComponent = FlowComponent of percent:float * element:string
    // type FlowComposition = FlowComposition of FlowComponent list
    // type FlowDescription = {
    //     FlowRate : FlowRate
    //     FlowComposition : FlowComposition
    // }
    // type FlowState =
    //     | Closed
    //     | Open of FlowDescription

    type PossibilityId = PossibilityId of int64
    type PossibilityType = 
        | Allocate of planId:PlanId * allocationId:AllocationId
        | Free of planId:PlanId * allocationId:AllocationId
        | Delay

    type Possibility =
        {
            PossibilityId : PossibilityId
            TimeStamp : TimeStamp
            PossibilityType : PossibilityType
            PlanId : PlanId

        }






    // type Availability =
    //     | Nothing
    //     | One
    //     | Discrete of max:int
    //     | Continuous of max:float

    // type Capacity =
    //     | Singleton
    //     | Discrete of max:int
    //     | Continuous of max:float

    // type ResourceRequest =
    //     | This of resource: Resource
    //     | OneOf of resources: Set<Resource>
    //     | FewOf of resource: Resource * quantity:int
    //     | SomeOf of resource: Resource * quantity:float

    // type Allocation =
    //     | All
    //     | Discrete of quantity:int
    //     | Continuous of quantity:float

    type AllocationRequest =
        | OneOf of Set<Resource>
        // | Nof of number:int * Set<Resource>

    type ModelState = {
        LastEventId : EventId
        LastStateId : StateId
        LastPlanId : PlanId
        LastAllocationId : AllocationId
        Now : TimeStamp
        // Locations : Map<Location, Set<ItemId>>
        Availabilities : Map<Resource, Availability>
        Flows : Map<Flow, FlowState>
        Allocations : Map<AllocationId, Map<Resource, Allocation>>
        PlanStates : Map<PlanId, PlanState>
    }


    type AllocationRequest = {
        AllocationId : AllocationId
        Resources : ResourceRequest list
        RemainingSteps : Step list
    }

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


    type StateBuilder() =
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

    type PlanAcc = PlanAcc of lastStepId:StepId * steps:Step list

    let state = StateBuilder()

    let getFood =
        state {
            printfn "GetFood"
            let randomFood = 
                if rng.NextDouble() > 0.5 then Food.Chicken
                else Food.Rice
            let! (PlanAcc (StepId lastStepId, steps)) = State.getState
            let nextStepId = StepId (lastStepId + 1)
            let newStep = GetFood (nextStepId, randomFood)
            let newAcc = PlanAcc (nextStepId, newStep::steps)
            do! State.setState newAcc
            return randomFood
        }

    type StateBuilder with

        [<CustomOperation("sleep", MaintainsVariableSpaceUsingBind=true)>]
        member this.Sleep (st:State<_,PlanAcc>, [<ProjectionParameter>] (duration: 'a -> int)) =
            printfn $"Sleep"
            state {
                let! x = st
                let d = duration x
                printfn "Sleep: %A" duration
                let! (PlanAcc (StepId lastStepId, steps)) = State.getState
                let nextStepId = StepId (lastStepId + 1)
                let newStep = Sleep (nextStepId, d)
                let newAcc = PlanAcc (nextStepId, newStep::steps)
                do! State.setState newAcc
                return x 
            }


        [<CustomOperation("eat", MaintainsVariableSpaceUsingBind=true)>]
        member this.Eat (st:State<_,PlanAcc>, [<ProjectionParameter>] (food: 'a -> Food)) =
            printfn $"Eat"
            state {
                let! x = st
                let f = food x
                printfn "Eat: %A" food
                let! (PlanAcc (StepId lastStepId, steps)) = State.getState
                let nextStepId = StepId (lastStepId + 1)
                let newStep = Eat (nextStepId, f)
                let newAcc = PlanAcc (nextStepId, newStep::steps)
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

        
