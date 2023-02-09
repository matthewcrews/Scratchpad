let a = seq {
    1
    2
    3
}

let b = seq { 1; 2; 3 }

let c = seq {
    for i in 1 .. 10 do
        i
}
c
for i in c do
    printfn $"{i}"

let d = seq {
    for i in 1..2 do
    for j in 1..3 do
        i, j
}
for i in d do
    printfn $"{i}"

let e = seq {
    for i in 1..3 do
        yield! seq { 1; 2; 3 }
}
for i in e do
    printfn $"{i}"


let f = seq {
    for i in 1..3 do
        yield! [1; 2; 3]
}
for i in f do
    printfn $"{i}"

let f2 = seq {
    for i in 1..3 do
        yield! [|1; 2; 3|]
}
for i in f2 do
    printfn $"{i}"

let g = seq {
    while true do
        1
}
for i in Seq.take 10 g do
    printfn $"{i}"

let h = seq {
    while true do
        yield! [1; 2; 3]
}
for i in Seq.take 10 h do
    printfn $"{i}"