type State = State of System.Random

type Food =
    | Chicken
    | Rice

type Step<'next> =
    | GetFood of Food * 'next
    | Eat of Food * 'next
    | Sleep of duration:int * 'next

module Step =

    let map f step =
        match step with
        | GetFood (food, next) -> GetFood (food, next |> f)
        | Eat (food, next) -> Eat (food, next |> f)
        | Sleep (hours, next) -> Sleep (hours, next |> f)

type Plan<'a> =
    | Next of Step<Plan<'a>>
    | Finished of 'a

module Plan =

    // ('a -> Plan<'b>) -> Plan<'a> -> Plan<'b>
    let rec bind f plan =
        match plan with
        | Next step ->
            Next (Step.map (bind f) step)
        | Finished x ->
            f x

    let returnT x =
        Finished x

type PlanBuilder () =
    member this.Return x = Plan.returnT x
    member this.Bind (x, f) = Plan.bind f x
    member this.Zero (x) = Plan.returnT ()

let plan = PlanBuilder()

let rng = System.Random(123)

let randomFood (State rng) =
    let food =
        match rng.NextDouble() > 0.5 with
        | true -> Chicken
        | false -> Rice
    food, State rng

let finished = Plan.Finished ()
let getFood food = Next (GetFood (food, Finished ()))
let eat food = Next (Eat (food, Finished ()))
let sleep duration = Next (Sleep (duration, Finished ()))

let testPlan =
    plan {
        let! food = getFood 
    }

// let rng = System.Random(123)

// let getFood () = 
//     let randomFood state =
//         match rng.NextDouble() > 0.5 with
//         | true -> Chicken
//         | false -> Rice
//     Do (GetFood (randomFood, Return randomFood))

// let eat food =
//     Do (Eat (food, Return ()))

// let sleep state length =
//     Do (Sleep (length, Return ()))

// module Plan =

//     let rec bind f plan =
//         match plan with
//         | Do steps ->
//             steps 
//             |> Step.map (bind f) 
//             |> Do
//         | Return value -> f value


// type PlanBuilder () =
//     member this.Return      v = Return v
//     member this.ReturnFrom mv = mv
//     member this.Zero       () = Return ()
//     member this.Bind   (v, f) = Plan.bind f v


// let plan = PlanBuilder ()

// let myPlan =
//     plan {
//         let! food = getFood ()
//         do! sleep 10
//         do! eat food
//     }


// type NestBuilder() =
//     member __.Bind(nest, func) = nest |> Nest.bind func
//     member __.Return(value) = Pure value
//     member __.ReturnFrom(nest) = nest

// let nest = NestBuilder()

// let toList value = [value]

// let step value =
//     value |> Pure |> toList |> Free

// let x =
//     nest {
//         let! a = step "Chicken"
//         let
//         return! a
//     }

// let myPlan =
//     plan {
//         let! food = getFood ()
//         do! sleep
//         do! eat food
//     }

// let sleep (time: int) (plan: Plan) =
//     Sleep (time, plan)

// let getFood (plan: Plan) =
//     let food = Food.Chicken // Will become random
//     GetFood (food, plan)

// let eat (food: Food) (plan: Plan) =
//     Eat (food, plan)

// type PlanBuilder () =
//     member this.Bind(m, f) =
//         match m with

// GetFood food, (
//     Sleep 2, (
//         Eat food, (
//             Done
//         )
//     )
// )