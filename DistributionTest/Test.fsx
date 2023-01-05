open System

let rng = Random ()

let numberOfSamples = 1024 * 4
let maxValue = 1024 * 8 - 1

let dist =
    [| for _ in 1 .. numberOfSamples ->
        rng.Next maxValue |]
    |> Array.groupBy id
    |> Array.map (fun (_, grp) -> grp.Length)
    |> Array.groupBy id
    |> Array.map (fun (c, grp) -> c, grp.Length)
    |> Array.sortBy fst