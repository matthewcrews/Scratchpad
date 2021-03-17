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
    type Vessel = Vessel of string
    type Flow = Flow of string
    type FlowRate<[<Measure>] 'Measure> = FlowRate of float<'Measure>
    type FlowComponent = FlowComponent of percent:float * element:string
    type FlowComposition = FlowComposition of FlowComponent list
    type FlowDescription<[<Measure>] 'Measure> = {
        FlowRate : FlowRate<'Measure>
        FlowComposition : FlowComposition
    }
    type EventId = EventId of int64
    type StateId = StateId of int64
    type TimeStamp = TimeStamp of float

    type FlowState<[<Measure>] 'Measure> =
        | Closed
        | Open of FlowDescription<'Measure>

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
        | One of resource:Resource
        | Discrete of resource:Resource * quantity:int
        | Continuous of resource:Resource * quantity:float


    type Allocation =
        | All
        | Discrete of quantity:int
        | Continuous of quantity:float

    type ModelState<[<Measure>] 'Measure> = {
        LastEventId : EventId
        LastStateId : StateId
        Now : TimeStamp
        ItemLocations : Map<ItemId, Location>
        Locations : Map<Location, Set<ItemId>>
        Capacities : Map<Resource, Capacity>
        Availabilities : Map<Resource, Availability>
        Flows : Map<Flow, FlowState<'Measure>>
        Allocations : Map<AllocationId, Map<Resource, Allocation>>
    }

    type Step<[<Measure>] 'Measure> =
        | Allocate of allocationRequest: AllocationRequest<'Measure>
        | Free of allocationId: AllocationId
        | Move of item: ItemId * location: Location
        | Delay of duration: float
        | Open of flow: Flow * flowDescription: FlowDescription<'Measure>
        | Close of flow: Flow

    type AllocationRequest<[<Measure>] 'Measure> = {
        AllocationRequestId : AllocationRequestId
        Resources : ResourceRequest list
        RemainingSteps : Step<'Measure> list
    }

    type Event<[<Measure>] 'Measure> = {
        Time : float
        EventId : int64
        RemainingSteps : Step<'Measure> list
    }