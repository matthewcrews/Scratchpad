open System
// open FSharp.Linq

let mutable x = "x"
x <- null

let inline ( ?<- ) (a: 'a) (b: 'a) =
    match a with
    | null -> b
    | x -> x

let y = x ?<- "chicken"