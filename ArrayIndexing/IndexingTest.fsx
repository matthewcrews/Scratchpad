open System.Collections.Generic

[<Measure>] type Chicken


let test () =
    let x = [| 1.0 .. 10.0 |]
    let mutable idx = 0
    
    ()


let x = Dictionary [KeyValuePair (1, "a")]

let z =
    x 
    |> Seq.map KeyValue 
    |> Map.ofSeq