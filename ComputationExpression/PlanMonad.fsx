type Step =
    | 

type Stack<'t> = private Stack of List<'t>

module Stack =

    let ofList list = Stack list

    /// 'a -> Stack<'a> -> Stack<'a>
    let push item (Stack items) =
        Stack (item :: items)

    /// Stack<'a> -> 'a * Stack<'a>
    let pop = function
        | Stack (head :: tail) -> head, Stack tail
        | Stack [] -> failwith "Empty stack"

// let stack = Stack.ofList [1; 2]
// let item, stack' = stack |> Stack.pop
// let stack'' = stack' |> Stack.push 3

// /// Output: 1, Stack [3; 2]
// printfn "%A, %A" item stack''

/// A stateful computation.
type Stateful<'state, 'result> =
    Stateful of ('state -> 'result * 'state)


module Stateful =
    /// 'state -> Stateful<'state, 'result> -> ('result * 'state)
    let run state (Stateful f) =
        f state

    /// 'result -> Stateful<'state, 'result>
    let ret result =
        Stateful (fun state -> (result, state))

    /// ('a -> Stateful<'state, 'b>) -> Stateful<'state, 'a> -> Stateful<'state, 'b>
    let bind binder stateful =
        Stateful (fun state ->
            let result, state' = stateful |> run state
            binder result |> run state')

type StatefulBuilder() =
    let (>>=) stateful binder = Stateful.bind binder stateful
    member __.Return(result) = Stateful.ret result
    member __.ReturnFrom(stateful) = stateful
    member __.Bind(stateful, binder) = stateful >>= binder
    member __.Zero() = Stateful.ret ()
    member __.Combine(statefulA, statefulB) =
        statefulA >>= (fun _ -> statefulB)
    member __.Delay(f) = f ()

let state = StatefulBuilder()

/// Stateful<Stack<'a>, 'a>
let popC = Stateful Stack.pop

/// 'a -> Stateful<Stack<'a>, unit>
let pushC item =
    Stateful (fun stack ->
        (), Stack.push item stack)

/// Stateful<Stack<int>, int>
let comp =
    state {
        let! a = popC
        if a = 5 then
            do! pushC 7
        else
            do! pushC 3
            do! pushC 8
        return a
    }

// Output: (9, Stack [8; 3; 0; 2; 1; 0])
let stack = [9; 0; 2; 1; 0] |> Stack.ofList
printfn "%A" (Stateful.run stack comp)

// // Output: (5, Stack [7; 1])
// let stack = [5; 1] |> Stack.ofList
// printfn "%A" (Stateful.run stack comp)