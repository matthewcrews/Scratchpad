// let a = [1..5]
// let b = [2..2..8]

let a =
    [for i in 1..10 do
        if i % 2 = 0 then
            i
        if i % 3 = 0 then
            i]

let b =
    [for i in 1..5 do
        for j in 1..6 do
            printfn $"i = {i}, j = {j}"
            if i = j then
                i]

let c = 
    [for i in 1..3 do
        i
        i*i
        i * i * i]

let d =
    [for i in 1..3 do
        yield! [i..3]]

let e = [|1..3|]

let f =
    [|for i in 1..3 do
        i
        i * i|]

let g =
    [|for i in 1..3 do
        yield! [|i..3|]|]
