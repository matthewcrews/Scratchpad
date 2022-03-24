open System

let x = [|1.0 .. 10.0|]
let value = 5.5
let idx = Array.BinarySearch (x, value)

let newIdx = ~~~ idx

let lower = x.[newIdx - 1]
let upper = x.[newIdx]