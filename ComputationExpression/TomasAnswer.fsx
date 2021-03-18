type Food =
    | Chicken
    | Rice

type Step =
  | GetFood of Food
  | Eat of Food
  | Sleep of duration:int

type Plan<'T> = Plan of Step list * 'T

let rng = System.Random(123)

let getFood () =
  printfn "GetFood"
  let randomFood = 
    if rng.NextDouble() > 0.5 then Food.Chicken
    else Food.Rice
  Plan([GetFood randomFood], randomFood)

type PlanBuilder () =

    member this.For (Plan(steps1, res):Plan<'T>, f:'T -> Plan<'R>) : Plan<'R> =
      printfn "For"
      let (Plan(steps2, res2)) = f res
    //   Plan(steps1 @ steps2, res2)
      Plan(steps2 @ steps1, res2)

    member this.Bind (Plan(steps1, res):Plan<'T>, f:'T -> Plan<'R>) : Plan<'R> =
        printfn "Bind"
        let (Plan(steps2, res2)) = f res
        // Plan(steps1 @ steps2, res2)
        Plan(steps2 @ steps1, res2)

    // member this.Zero _ = Plan([], ())
    member this.Yield x = 
        printfn "Yield"
        Plan([], x)
    member this.Return x = 
        printfn "Retrun"
        Plan([], x)
    member this.Run (Plan(p,r)) = 
        printfn "Run"
        Plan(List.rev p, r)

    [<CustomOperation("eat", MaintainsVariableSpace=true)>]
    member this.Eat (Plan(p, r), [<ProjectionParameter>] food) =
        printfn $"Eat: {food}"
        Plan((Eat (food r))::p, r)

    [<CustomOperation("sleep", MaintainsVariableSpace=true)>]
    member this.Sleep (Plan(p, r), [<ProjectionParameter>] duration) =
        printfn $"Sleep: {duration}"
        Plan ((Sleep (duration r))::p, r)

let plan = PlanBuilder()

let (Plan (testPlan, _)) =
  plan {
      let! food = getFood ()
      printfn $"Food1: {food}"
      sleep 1
      let! food2 = getFood ()
      printfn $"Food2: {food2}"
      sleep 10
      eat food2
      sleep 5
      eat food
      return ()
  }

testPlan

// let otherPlan =
//     plan {

//     }