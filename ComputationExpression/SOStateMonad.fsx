type State<'s, 'a> = State of ('s -> ('a * 's))

module State =
    let inline run state x = let (State(f)) = x in f state
    let get = State(fun s -> s, s)
    let put newState = State(fun _ -> (), newState)
    let map f s = State(fun (state: 's) ->
        let x, state = run state s
        f x, state)

/// The state monad passes around an explicit internal state that can be
/// updated along the way. It enables the appearance of mutability in a purely
/// functional context by hiding away the state when used with its proper operators
/// (in StateBuilder()). In other words, you implicitly pass around an implicit
/// state that gets transformed along its journey through pipelined code.
type StateBuilder() =
    member this.Zero () = State(fun s -> (), s)
    member this.Return x = State(fun s -> x, s)
    member inline this.ReturnFrom (x: State<'s, 'a>) = x
    member this.Bind (x, f) : State<'s, 'b> =
        State(fun state ->
            let (result: 'a), state = State.run state x
            State.run state (f result))
    member this.Combine (x1: State<'s, 'a>, x2: State<'s, 'b>) =
        State(fun state ->
            let result, state = State.run state x1
            State.run state x2)
    member this.Delay f : State<'s, 'a> = f ()
    member this.For (seq, (f: 'a -> State<'s, 'b>)) =
        seq
        |> Seq.map f
        |> Seq.reduceBack (fun x1 x2 -> this.Combine (x1, x2))
    member this.While (f, x) =
        if f () then this.Combine (x, this.While (f, x))
        else this.Zero ()

let state = new StateBuilder()


type StepId = StepId of int

type PlanState = {
    LastStepId : StepId
}

type Food =
    | Chicken
    | Rice

type Step =
    | GetFood of StepId * Food
    | Eat of StepId * Food
    | Sleep of StepId * duration:int

type PlanAccumulator = PlanAccumulator of PlanState * Step list

let rng = System.Random(123)

let getFood =
    state {
        printfn "GetFood"
        let randomFood = 
            if rng.NextDouble() > 0.5 then Food.Chicken
            else Food.Rice
        let! (PlanAccumulator (planState, steps)) = State.get
        let (StepId lastStepId) = planState.LastStepId
        let nextStepId = StepId (lastStepId + 1)
        let newState = { planState with LastStepId = nextStepId }
        let newStep = GetFood (nextStepId, randomFood)
        do! State.put (PlanAccumulator (newState, newStep :: steps))
        return randomFood
    }

let eat food =
    state {
        printfn "Eat: %A" food
        let! (PlanAccumulator (planState, steps)) = State.get
        let (StepId lastStepId) = planState.LastStepId
        let nextStepId = StepId (lastStepId + 1)
        let newState = { planState with LastStepId = nextStepId }
        let newStep = Eat (nextStepId, food)
        do! State.put (PlanAccumulator (newState, newStep :: steps))
    }

let sleep duration =
    state {
        printfn "Sleep: %A" duration
        let! (PlanAccumulator (planState, steps)) = State.get
        let (StepId lastStepId) = planState.LastStepId
        let nextStepId = StepId (lastStepId + 1)
        let newState = { planState with LastStepId = nextStepId }
        let newStep = Sleep (nextStepId, duration)
        do! State.put (PlanAccumulator (newState, newStep :: steps))
    }

let initialState = {
    LastStepId = StepId 0
}

let initialPlan =
    PlanAccumulator (initialState, List.empty)


let _, testPlan =
    state {
        let! food = getFood
        do! sleep 10
        do! eat food
    } |> State.run initialPlan

printfn "%A" testPlan
