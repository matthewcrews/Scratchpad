open System.Collections.Generic

let allDistinct (x: 'a list) =
    let mutable result = true
    let acc = HashSet ()

    let rec loop (elements: 'a list) =
        match elements with
        | next::remaining ->
            if acc.Contains next then
                result <- false
            else
                acc.Add next 
                |> ignore
                loop remaining

        | [] -> ()

    loop x

    result


let test1 = [1..10]

// Should return true
allDistinct test1

let test2 = [1; 2; 2]

// Should return false
allDistinct test2