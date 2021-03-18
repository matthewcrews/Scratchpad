type Food =
    | Chicken
    | Rice

type Tool =
  | Shovel

type Step =
  | GetFood of Food
  | GetTool of Tool
  | Eat of Food
  | Sleep of duration:int

type Plan<'T> = Plan of Step list * 'T

let rng = System.Random(123)

let getFood () =
  printfn "GetFood"
  let randomFood = 
    if rng.NextDouble() > 0.5 then Food.Chicken
    else Food.Rice
  Plan ([GetFood randomFood], randomFood)

let getTool ()  =
  let tool = Shovel
  Plan ([GetTool tool], tool)

type PlanBuilder () =

    member this.For (Plan(steps1, res):Plan<'T>, f:'T -> Plan<'R>) : Plan<'R> =
      printfn "For"
      let (Plan(steps2, res2)) = f res
      Plan (steps2 @ steps1, res2)

    member this.Bind (Plan(steps1, res):Plan<'T>, f:'T -> Plan<'R>) : Plan<'R> =
        printfn "Bind"
        let (Plan(steps2, res2)) = f res
        Plan (steps2 @ steps1, res2)

    member this.Yield x = 
        printfn "Yield"
        Plan ([], x)

    member this.Run (Plan(p,r)) = 
        printfn "Run"
        List.rev p

    [<CustomOperation("eat", MaintainsVariableSpace=true)>]
    member this.Eat (Plan(p, r), [<ProjectionParameter>] food) =
        printfn $"Eat: {food}"
        Plan ((Eat (food r))::p, r)

    [<CustomOperation("sleep", MaintainsVariableSpace=true)>]
    member this.Sleep (Plan(p, r), [<ProjectionParameter>] duration) =
        printfn $"Sleep: {duration}"
        Plan ((Sleep (duration r))::p, r)

let plan = PlanBuilder()

let testPlan =
  plan {
      let! food = getFood ()
      let! tool = getTool ()
      printfn $"Food1: {food}"
      sleep 1
      let! food2 = getFood ()
      printfn $"Food2: {food2}"
      sleep 10
      eat food2
      sleep 5
      eat food
      // return ()
  }

testPlan

let simplePlan =
  plan {
    let! food = getFood ()
    sleep 10
    eat food
  }
