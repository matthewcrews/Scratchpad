type State = State of System.Random

type Food =
    | Chicken
    | Rice

type Step =
    | GetFood of Food
    | Eat of Food
    | Sleep of duration:int

type Plan = Plan of Step list

type PlanBuilder () =
    // member this.Return x = x
    member this.Yield _ = Plan []
    // member this.Bind (x, f) = f::x
    member this.Run (Plan x) = Plan (List.rev x)
    // member this.Zero (x) = []

    [<CustomOperation("getFood")>]
    member this.GetFood (Plan p, food) =
        printfn "GetFood"
        Plan ((GetFood food)::p)

    [<CustomOperation("eat")>]
    member this.Eat (Plan p, food) =
        printfn "Eat"
        Plan ((Eat food)::p)

    [<CustomOperation("sleep")>]
    member this.Sleep (Plan p, duration) =
        printfn "Sleep"
        Plan ((Sleep duration)::p)

let plan = PlanBuilder()

let rng = System.Random(123)


let testPlan =
    plan {
        getFood Chicken
        sleep 10
        eat Rice
    }
