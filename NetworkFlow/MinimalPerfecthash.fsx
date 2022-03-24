open System
open System.Runtime.CompilerServices
open System.Collections.Generic

[<Struct; IsByRefLike>]
type BloomFilter =
    private {
        Salt1 : int
        Salt2 : int
        Values : Span<byte>
    }

let desiredKeys = [|0 .. 11|]
let goodHash contained =
    let mutable result = true

    for key in desiredKeys do
        if not (Array.contains key contained) then
            result <- false

    result

let keys = 
    [|
        1
        2
        3
        4
        6
        7
        8
        9
        10
        12
        13
        14
    |]

let keyCount = 12

for multiplier = 0 to 10_000_000 do

    let test1 = 
        keys
        |> Array.map (fun x -> (x * multiplier) % keyCount)

    if goodHash test1 then
        printfn "%A" multiplier