type Food =
    | Chicken
    | Rice

type Step =
    | GetFood of Food
    | Eat of Food
    | Sleep of duration:int

type Plan = Plan of Step list

type PlanBuilder () =

    member this.Bind (plan:Plan, f) =
        f plan
    member this.Yield _ = Plan []
    member this.Run (Plan x) = Plan (List.rev x)

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


let getFood (Plan p) =
    printfn "GetFood"
    let randomFood = 
        if rng.NextDouble() > 0.5 then
            Food.Chicken
        else
            Food.Rice
    (Plan ((GetFood randomFood)::p)), Food.Chicken

let testPlan =
    plan {
        let! food = getFood
        sleep 10
        eat food
    }

(*
Example result
testPlan =
    (GetFood Chicken,(
        (Sleep 10,(
            EatFood Chicken
        ))
    ))
*)
