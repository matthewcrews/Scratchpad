/// State is a function type that takes a state and
/// returns a value and the new state.
/// Single case union of the same type.
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

let state = StateBuilder()

let c : State<unit, string> =
    state {
        if true then
            printfn "Hello"
    }

let x = State.eval c ""