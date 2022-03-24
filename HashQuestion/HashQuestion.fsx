
open System.Collections.Generic

type Settings = 
    {
        Capacities : array<float>
        MaxRates : array<float>
    }

let c1 = [|1.0 .. 10.0|]
let m1 = [|1.0 .. 10.0|]
let v1 = [|1.0 .. 5.0|]
let s1 = {
    Capacities = c1
    MaxRates = m1
}

let c2 = [|1.0 .. 10.0|]
let m2 = [|1.0 .. 10.0|]
let v2 = [|1.0 .. 5.0|]
let s2 = {
    Capacities = c1
    MaxRates = m1
}

s1 = s2

// [<Struct>]
type LookupKey =
    {
        Settings : Settings
        Values : array<float>

    }

let l1 = {
    Settings = s1
    Values = v1
}

let d = Dictionary ()
d.[l1] <- 1.0

let l2 = {
    Settings = s2
    Values = v2
}

d.[l2]