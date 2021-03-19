type State<'a, 's> = ('s -> 'a * 's)

module State =
    // Explicit
    // let result x : State<'a, 's> = fun s -> x, s
    // Less explicit but works better with other, existing functions:
    let result x s = 
        x, s

    let bind (f:'a -> State<'b, 's>) (m:State<'a, 's>) : State<'b, 's> =
        // return a function that takes the state
        fun s ->
            // Get the value and next state from the m parameter
            let a, s' = m s
            // Get the next state computation by passing a to the f parameter
            let m' = f a
            // Apply the next state to the next computation
            m' s'

    /// Evaluates the computation, returning the result value.
    let eval (m:State<'a, 's>) (s:'s) = 
        m s 
        |> fst

    /// Executes the computation, returning the final state.
    let exec (m:State<'a, 's>) (s:'s) = 
        m s
        |> snd

    /// Returns the state as the value.
    let getState (s:'s) = 
        s, s

    /// Ignores the state passed in favor of the provided state value.
    let setState (s:'s) = 
        fun _ -> 
            (), s


type StateBuilder() =
    member __.Return(value) : State<'a, 's> = 
        State.result value
    member __.Bind(m:State<'a, 's>, f:'a -> State<'b, 's>) : State<'b, 's> = 
        State.bind f m
    member __.ReturnFrom(m:State<'a, 's>) = 
        m
    member __.Zero() =
        State.result ()
    member __.Delay(f) = 
        State.bind f (State.result ())


let rng = System.Random(123)
type StepId = StepId of int
type Food =
    | Chicken
    | Rice
type Step =
  | GetFood of StepId * Food
  | Eat of StepId * Food
  | Sleep of StepId * duration:int
type PlanAcc = PlanAcc of lastStepId:StepId * steps:Step list

let state = StateBuilder()

let getFood =
    state {
        printfn "GetFood"
        let randomFood = 
            if rng.NextDouble() > 0.5 then Food.Chicken
            else Food.Rice
        let! (PlanAcc (StepId lastStepId, steps)) = State.getState
        let nextStepId = StepId (lastStepId + 1)
        let newStep = GetFood (nextStepId, randomFood)
        let newAcc = PlanAcc (nextStepId, newStep::steps)
        do! State.setState newAcc
        return randomFood
    }

type StateBuilder with

    [<CustomOperation("sleep", MaintainsVariableSpaceUsingBind=true)>]
    member this.Sleep (st:State<_,PlanAcc>, [<ProjectionParameter>] (duration: 'a -> int)) =
        printfn $"Sleep"
        let program d =
            state {
                // let! x = st
                printfn "Sleep: %A" duration
                let! (PlanAcc (StepId lastStepId, steps)) = State.getState
                let nextStepId = StepId (lastStepId + 1)
                let newStep = Sleep (nextStepId, d)
                let newAcc = PlanAcc (nextStepId, newStep::steps)
                do! State.setState newAcc
                // return x 
            }

        State.bind (fun x -> program (duration x)) st


    [<CustomOperation("eat", MaintainsVariableSpaceUsingBind=true)>]
    member this.Eat (st:State<_,PlanAcc>, [<ProjectionParameter>] (food: 'a -> Food)) =
        printfn $"Eat"
        let program e =
            state {
                // let! x = st
                printfn "Eat: %A" food
                let! (PlanAcc (StepId lastStepId, steps)) = State.getState
                let nextStepId = StepId (lastStepId + 1)
                let newStep = Eat (nextStepId, e)
                let newAcc = PlanAcc (nextStepId, newStep::steps)
                do! State.setState newAcc
                // return x
            }
        State.bind (fun x -> program (food x)) st


let simplePlan =
    state {
        let! f1 = getFood
        sleep 1
        eat f1
        sleep 2
        eat f1
        sleep 3
    }

let initalAcc = PlanAcc(StepId 0, [])

let x = State.exec simplePlan initalAcc
x