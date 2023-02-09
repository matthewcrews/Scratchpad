// Creating
let a = seq {
    1
    2
    3
}

// Or you can
let b = seq { 1; 2; 3 }

// You can use a loop
let c = seq {
    for i in 1 .. 10 do
        i
}

// You can nest
let d = seq {
    for i in 1..10 do
    for j in 1..10 do
        i * j
}

// You can use for loops
for i in d do
    printfn $"{i}"


// YOu can Yield!
let e = seq {
    for i in 1..10 do
        yield! seq { 1; 2; 3 }
}

// Print out values
for i in e do
    printfn $"{i}"

// You can Yield! different types that are IEnumerable
let f = seq {
    for i in 1..10 do
        yield! [1; 2; 3]
}

for i in f do
    printfn $"{i}"

// They can express infinite series
let g = seq {
    while true do
        1
}

for i in Seq.take 10 g do
    printfn $"{i}"

// YOu can express an infinite repeating sequence
let h = seq {
    while true do
        yield! [1; 2; 3]
}

for i in Seq.take 10 h do
    printfn $"{i}"

