type IndexRange = {
    Start : int
    Length : int
}

let toIntervals (x: _[]) =

    let groups = Array.groupBy id x

    let keyLengths =
        groups
        |> Array.map (fun (key, group) -> key, group.Length)

    let startIdxs =
        keyLengths
        |> Array.scan (fun acc (k, length) -> acc + length) 0

    Array.zip x startIdxs.[.. startIdxs.Length - 2]
    |> Array.map (fun ((key, length), startIdx) -> key, { Start = startIdx; Length = length })


let x = 
    [| for i in 0..3 do
        for j in 0..1 ->
            i
    |]

let groups = Array.groupBy id x

let keyLengths =
    groups
    |> Array.map (fun (key, group) -> key, group.Length)

let runningTotal =
    keyLengths
    |> Array.scan (fun acc (k, length) -> acc + length) 0

Array.zip keyLengths runningTotal.[.. runningTotal.Length - 2]
|> Array.map (fun ((key, length), startIdx) -> key, { Start = startIdx; Length = length })

// let r = toIntervals x