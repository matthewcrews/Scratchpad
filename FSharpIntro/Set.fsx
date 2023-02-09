let a = Set [1; 2; 3]
let b = Set [
    1
    2
    3
]
// let a = Set [1; 2; 3; 3; 3; 3]
let c = Set [
    for i in 1..3 do
        i
]

a = b

let d = a.Add 4

let e = d.Remove 4
a = e

let f = a.Add 1
let g = a.Remove 100

let x = Set [1; 2; 3]
let y = Set [4; 5; 6]
let z = x + y

let ``The Subtraction Result`` = z - x
``The Subtraction Result`` = y

let i1 = Set [1; 2; 3]
let i2 = Set [2; 3; 4]
let i3 = Set.intersect i1 i2

let d1 = Set.difference i1 i2
let d2 = Set.difference i2 i1

