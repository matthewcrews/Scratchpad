type StepId = StepId of int
type State = {
  LastStepId : StepId
}

type Food =
    | Chicken
    | Rice

type Tool =
  | Shovel

type Step =
  | GetFood of StepId * Food
  | GetTool of StepId * Tool
  | Eat of StepId * Food
  | Sleep of StepId * duration:int

type PlanAccumulator<'T> = PlanAccumulator of State * Step list * 'T

let rng = System.Random(123)

let getFood (state: State) =
  printfn "GetFood"
  let randomFood = 
    if rng.NextDouble() > 0.5 then Food.Chicken
    else Food.Rice
  let (StepId lastStepId) = state.LastStepId
  let nextStepId = StepId (lastStepId + 1)
  let newState = { state with LastStepId = nextStepId }
  let newStep = GetFood (nextStepId, randomFood)
  PlanAccumulator (newState, [newStep], randomFood)

type PlanBuilder (state: State) =

    member this.For (PlanAccumulator (state, steps1, res):PlanAccumulator<'T>, f:'T -> PlanAccumulator<'R>) : PlanAccumulator<'R> =
      printfn "For"
      let (PlanAccumulator(state2, steps2, res2)) = f res
      PlanAccumulator (state2, steps2 @ steps1, res2)

    member this.Bind (PlanAccumulator (state1, steps1, res):PlanAccumulator<'T>, f:State -> 'T -> PlanAccumulator<'R>) : PlanAccumulator<'R> =
        printfn "Bind"
        let (PlanAccumulator(state2, steps2, res2)) = f state1 res
        PlanAccumulator (state2, steps2 @ steps1, res2)

    member this.Yield x = 
        printfn "Yield"
        PlanAccumulator (state, [], x)

    member this.Run (PlanAccumulator (s, p,r)) = 
        printfn "Run"
        s, List.rev p

    [<CustomOperation("eat", MaintainsVariableSpace=true)>]
    member this.Eat (PlanAccumulator(s, p, r), [<ProjectionParameter>] food) =
        printfn $"Eat: {food}"
        let (StepId lastStepId) = state.LastStepId
        let nextStepId = StepId (lastStepId + 1)
        let newState = { state with LastStepId = nextStepId }
        let newStep = Eat (nextStepId, (food r))
        PlanAccumulator (newState, newStep::p, r)

    [<CustomOperation("sleep", MaintainsVariableSpace=true)>]
    member this.Sleep (PlanAccumulator (s, p, r), [<ProjectionParameter>] duration) =
        printfn $"Sleep: {duration}"
        let (StepId lastStepId) = state.LastStepId
        let nextStepId = StepId (lastStepId + 1)
        let newState = { state with LastStepId = nextStepId }
        let newStep = Sleep (nextStepId, (duration r))
        PlanAccumulator (newState, newStep::p, r)

// let plan = PlanBuilder()
let initialState = {
  LastStepId = StepId 0
}

let testPlan =
  PlanBuilder initialState {
      let! food = getFood
      printfn $"Food1: {food}"
      sleep 1
      let! food2 = getFood
      printfn $"Food2: {food2}"
      sleep 10
      eat food2
      sleep 5
      eat food
  }



