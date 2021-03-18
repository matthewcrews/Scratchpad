module rec Domain =

    type Resource = Resource of string
    type AllocationId = AllocationId of int64
    type AllocationRequestId = AllocationRequestId of int64
    type Location = Location of string
    type ItemId = ItemId of int64
    type Item<'a> = {
        ItemId : ItemId
        Type : 'a
    }
    type PlanId = PlanId of int64
    type StepId = StepId of int64
    type Vessel = Vessel of string
    type Flow = Flow of string
    type FlowRate = FlowRate of float
    type FlowComponent = FlowComponent of percent:float * element:string
    type FlowComposition = FlowComposition of FlowComponent list
    type FlowDescription = {
        FlowRate : FlowRate
        FlowComposition : FlowComposition
    }
    type EventId = EventId of int64
    type StateId = StateId of int64
    type TimeStamp = TimeStamp of float

    type FlowState =
        | Closed
        | Open of FlowDescription

    type Availability =
        | Nothing
        | One
        | Discrete of max:int
        | Continuous of max:float

    type Capacity =
        | Singleton
        | Discrete of max:int
        | Continuous of max:float

    type ResourceRequest =
        | This of resource: Resource
        | OneOf of resources: Set<Resource>
        | FewOf of resource: Resource * quantity:int
        | SomeOf of resource: Resource * quantity:float

    type Allocation =
        | All
        | Discrete of quantity:int
        | Continuous of quantity:float

    type ModelState = {
        LastEventId : EventId
        LastStateId : StateId
        LastPlanId : PlanId
        LastAllocationId : AllocationId
        Now : TimeStamp
        ItemLocations : Map<ItemId, Location>
        Locations : Map<Location, Set<ItemId>>
        Capacities : Map<Resource, Capacity>
        Availabilities : Map<Resource, Availability>
        Flows : Map<Flow, FlowState>
        Allocations : Map<AllocationId, Map<Resource, Allocation>>
    }

    type Step =
        | Allocate of allocationId: AllocationId * resources: ResourceRequest list
        | Free of allocationId: AllocationId
        | Move of item: ItemId * location: Location
        | Delay of duration: float
        | Open of flow: Flow * flowDescription: FlowDescription
        | Close of flow: Flow

    type AllocationRequest = {
        AllocationId : AllocationId
        Resources : ResourceRequest list
        RemainingSteps : Step list
    }

    type Event = {
        Time : float
        EventId : int64
        RemainingSteps : Step list
    }

    type Plan = {
        PlanId : PlanId
        Steps : Step list
    }

    type PlanBuilder (state: ModelState) =
        let planId, newState = 
            let (PlanId lastPlanId) = state.LastPlanId
            let nextPlanId = PlanId (lastPlanId + 1L)
            let newState = { state with LastPlanId = nextPlanId }
            nextPlanId, newState

        // member _.Yield ((state: ModelState, plan: Plan)) = 
        //     state, plan

        member _.Bind (m, f) = f m

        member _.Yield _ = newState, { PlanId = planId; Steps = [] }

        member _.Run (state: ModelState, plan: Plan) = 
            state, { PlanId = planId; Steps = List.rev plan.Steps }

        // [<CustomOperation("allocate")>]
        // member _.Allocate ((state: ModelState, plan: Plan), resourceRequest: ResourceRequest list) =
        //     let (AllocationId requestId) = state.LastAllocationId
        //     let nextAllocationId = AllocationId (requestId + 1L)
        //     let newState = { state with LastAllocationId = nextAllocationId }
        //     let newStep = Step.Allocate (nextAllocationId, resourceRequest)
        //     (newState, { plan with Steps = newStep::plan.Steps }), nextAllocationId

        // [<CustomOperation("free")>]
        // member _.Allocate ((state: ModelState, steps: Step list), allocationId: AllocationId) =
        //     let freeStep = Step.Free (allocationId)
        //     state, freeStep::steps

        [<CustomOperation("pause")>]
         member _.Pause ((state: ModelState, plan: Plan), (duration: float)) =
            let delayStep = Step.Delay duration
            // state, delayStep::steps
            (newState, { plan with Steps = delayStep::plan.Steps })

        // [<CustomOperation("open")>]
        //  member _.Open ((state: ModelState, steps: Step list), flow: Flow, flowDescription: FlowDescription) =
        //     let openStep = Step.Open (flow, flowDescription)
        //     state, openStep::steps

        // [<CustomOperation("close")>]
        //  member _.Close ((state: ModelState, steps: Step list), flow: Flow) =
        //     let closeStep = Step.Close flow
        //     state, closeStep::steps

        // member _.Delay(f) =
        //     fun (state: ModelState) -> f state

    // let plan = PlanBuilder

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

        
