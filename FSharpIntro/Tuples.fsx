// [<Struct>]
type Chicken =
    {
        Size : float
        Name : string
    }

let myChicken = { Name = "Clucky"; Size = 10.0 }

let myTuple = "Clucky", 10.0
let myTripleTuple = "Clucky", 10.0, 1
let myQuadTuple = 1, 1.0, "Marcus", 1M

let x = fst myTuple
let y = snd myTuple

let name, size = myTuple
let a, b, c = myTripleTuple

// Struct Tuple
let myStructTuple = struct ("Clucky", 10.0)
let struct (n, f) = myStructTuple