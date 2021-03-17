module rec Domain =

    type Resource = Resource of string
    type AllocationTag = AllocationTag of string
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
        LastAllocationRequestId : AllocationRequestId
        Now : TimeStamp
        ItemLocations : Map<ItemId, Location>
        Locations : Map<Location, Set<ItemId>>
        Capacities : Map<Resource, Capacity>
        Availabilities : Map<Resource, Availability>
        Flows : Map<Flow, FlowState>
        Allocations : Map<(PlanId * AllocationTag), Map<Resource, Allocation>>
    }

    type Step =
        | Allocate of planId:PlanId * allocationTag: AllocationTag * resources: ResourceRequest list
        | Free of planId:PlanId * allocationTag: AllocationTag
        | Move of item: ItemId * location: Location
        | Delay of duration: float
        | Open of flow: Flow * flowDescription: FlowDescription
        | Close of flow: Flow

    type AllocationRequest = {
        AllocationRequestId : AllocationRequestId
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

        member _.Yield _ = newState, { PlanId = planId; Steps = [] }
        member _.Run (state: ModelState, steps: Step list) = state, { PlanId = planId; Steps = List.rev steps }

        [<CustomOperation("allocate")>]
        member _.Allocate ((state: ModelState, steps: Step list), allocationTag: string, resourceRequest: ResourceRequest list) =
            let (AllocationRequestId requestId) = state.LastAllocationRequestId
            let nextAllocationRequestId = AllocationRequestId (requestId + 1L)
            let allocationTag = AllocationTag allocationTag
            let newState = { state with LastAllocationRequestId = nextAllocationRequestId }
            let allocationRequestStep = Step.Allocate (planId, allocationTag, resourceRequest)
            newState, allocationRequestStep::steps

        [<CustomOperation("free")>]
        member _.Allocate ((state: ModelState, steps: Step list), allocationTag: string) =
            let allocationTag = AllocationTag allocationTag
            let freeStep = Step.Free (planId, allocationTag)
            state, freeStep::steps

        [<CustomOperation("delay")>]
         member _.Delay ((state: ModelState, steps: Step list), duration: float) =
            let delayStep = Step.Delay duration
            state, delayStep::steps

        [<CustomOperation("open")>]
         member _.Open ((state: ModelState, steps: Step list), flow: Flow, flowDescription: FlowDescription) =
            let openStep = Step.Open (flow, flowDescription)
            state, openStep::steps

        [<CustomOperation("close")>]
         member _.Close ((state: ModelState, steps: Step list), flow: Flow) =
            let closeStep = Step.Close flow
            state, closeStep::steps

        
