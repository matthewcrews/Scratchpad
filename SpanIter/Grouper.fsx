
open System.Collections.Generic

type Grouper<'Key, 'Value when 'Key : equality> () =
    let acc = Dictionary<'Key, Stack<'Value>>()

    member _.Item
        with set(k: 'Key, v: 'Value) =

            match acc.TryGetValue k with
            | true, stack ->
                stack.Push v
            | false, _ ->
                let newStack = Stack()
                newStack.Push v
                acc[k] <- newStack


    member _.Values =
        acc
        |> Seq.map (|KeyValue|)
        |> Seq.map (fun (key, values) ->
            key, seq values)