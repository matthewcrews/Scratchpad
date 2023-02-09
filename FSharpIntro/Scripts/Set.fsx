let a = Set [1; 2; 3]
let b =
    Set [
        1
        2
        3
    ]

let c =
    Set [
        for i in 1..3 do
            i
    ]

// Structural Equality
a = b

// Adding elements
let d = a.Add 4

// Remove elements
let e = d.Remove 4

// When you add an existing element is does nothing
let f = a.Add 1

// When you remove an element that isn't there it
// does not raise an error
let g = a.Remove 100

// You can add sets together
let x = Set [1; 2; 3]
let y = Set [4; 5; 6]
let z = x + y

// You can subtract sets
let ``The Subtraction Result`` = z - x

// You can also have the intersect
let i1 = Set [1; 2; 3]
let i2 = Set [2; 3; 4]
let i3 = Set.intersect i1 i2
Set.