module rec Test =

    type Chicken = {
        Size : int
    } with
        static member ( + ) (c: Chicken, Chickens cs) : Chickens =
            Chickens (c::cs)

    type Chickens = Chickens of Chicken list
        with
            static member ( + ) (Chickens cs, c: Chicken) : Chickens =
                Chickens (c::cs)

open Test

type Summer () =

    member _.Sum<'a> (x: seq<'a * float>) =
        let mutable acc = 0.0

        for (k, v) in x do
            acc <- acc + v

        acc

    member _.Sum<'a> (x: seq<'a * int>) =
        let mutable acc = 0

        for (k, v) in x do
            acc <- acc + v

        acc

    member _.Sum<'a> (x: seq<'a * Chicken>) =
        let mutable acc = Chickens []

        for (k, v) in x do
            acc <- acc + v

        acc

let summer = Summer ()

let inline sum a = 
    summer.Sum a

Seq.sum