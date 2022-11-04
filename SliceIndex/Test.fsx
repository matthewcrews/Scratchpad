type All = All

type SliceIndex2D<'a, 'b when 'a : equality and 'b : equality>(values: seq<'a * 'b>) =
    
    let values = Array.ofSeq values
    
    member s.Item
        with get (k1: 'a, _: All) =
            values
            |> Seq.filter (fun (a, _) -> a = k1)
            
    member s.Item
        with get (_: All, k2: 'b) =
            values
            |> Seq.filter (fun (_, b) -> b = k2)
            
            
let test =
    SliceIndex2D [
        1, 1
        1, 5
        1, 10
        2, 5
        2, 10
        7, 1
        8, 1
    ]
    
let x = test[1, All]
let y = test[All, 1]

for v in y do
    printfn $"{v}"
    
(*
Prints:
(1, 1)
(7, 1)
(8, 1)
val it: unit = ()
*)