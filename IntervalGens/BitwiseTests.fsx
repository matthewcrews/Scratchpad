open System

let x = [|1 .. 2 .. 10|]
let idx = Array.BinarySearch (x, 2)
~~~ idx