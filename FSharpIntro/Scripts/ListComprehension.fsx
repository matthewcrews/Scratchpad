let a = [
    for i in 1 .. 10 do
        i
]

let x = 
    [for i in 1 .. 10 do
        i]

let b =
    [for i in 1..5 do
        if i > 2 then
            i
            i]

let c =
    [for i in 1..5 do
        if i > 2 then
            i
        yield! [1..2]]