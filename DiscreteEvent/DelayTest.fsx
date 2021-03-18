type StateId = StateId of int
type PlanId = PlanId of int
type StepId = StepId of int
type State = {
    LastStateId : StateId
    LastPlanId : PlanId
    LastStepId : StepId
}

type Step =
    | Eat of stepId: StepId * food:string
    | Sleep of stepId: StepId * duration:int

type Plan = {
    PlanId : PlanId
    Steps : Step list
}

type PlanBuilder (state: State) =
    let planId, newState = 
        let (PlanId lastPlanId) = state.LastPlanId
        let nextPlanId = PlanId (lastPlanId + 1)
        let newState = { state with LastPlanId = nextPlanId }
        nextPlanId, newState

    member _.Yield (state: State, step: Step) = state, step
    // member _.Run (state: State, steps: Step list) = state, { PlanId = planId; Steps = List.rev steps }

    [<CustomOperation("close")>]
     member this.Eat (steps: Step list, food: string) =
        fun (state: State) ->
            let (StepId lastStepId) = state.LastStepId
            let nextStepId = StepId (lastStepId + 1)
            let newState = { state with LastStepId = nextStepId }
            let eatStep = Step.Eat (nextStepId, food)
            this.Yield (newState, eatStep)

    [<CustomOperation("sleep")>]
     member this.Sleep (steps: Step list, duration: int) =
        fun (state: State) ->
            let (StepId lastStepId) = state.LastStepId
            let nextStepId = StepId (lastStepId + 1)
            let newState = { state with LastStepId = nextStepId }
            let sleepStep = Step.Sleep (nextStepId, duration)
            this.Yield (newState, sleepStep)

    member _.Delay(f) =
        fun (state: State) -> f state

    member this.Combine(a, b) =
        [a; b]

let initialState = {
    LastStateId = StateId 1
    LastPlanId = PlanId 1
    LastStepId = StepId 1
}

let plan, newState = PlanBuilder (initialState) {
        eat "chicken"
        sleep 10
        eat "monkey"
    }

let newState1, eatStep, remainingPlan = plan newState
let newState2, sleepStep, remainingPlan2 = remainingPlan newState1
let newState3, eatStep2, remainingPlan3 = remainingPlan2 newState2