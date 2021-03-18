type Step =
    | Eat of string
    | Hunt

type Plan<'next> =
    | Step of Step * 'next
    | Done

type PlanBuilder () =
    member this.Return x = x
    member this.Zero () = Plan.Done
    member this.Yield (x) = x
    member this.Bind (x, f) = 
        f x

let plan = PlanBuilder ()

// Not sure if these are right since they have to play nice with the CE
let getFood () =
    Step.Hunt

let eatFood food f =
    Step.Eat food, f

let testPlan =
    plan {
        let! r1 = getFood ()
        do! eatFood r1
    }

// firstStep should be a `Step.Hunt`, and `continuation` is a function expecting
// a `string`
let firstStep, continuation = testPlan ()
// `secondStep` should be a `Step.Eat "Chicken"`, and `continuation2`
// should return `Step.Done` if I call it
let secondStep, continuation2 = continuation "Chicken"
