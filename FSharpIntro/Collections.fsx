open System.Collections.Generic

// #nowarn "20"

let hashSet = HashSet()

let x = hashSet.Add 1
hashSet.Add 2

let myFunction (h: HashSet<_>) =
    let _ = h.Add 1
    

    printfn "Hello world"
    1

type Chicken =
    {
        Name: string
    }

type Turkey =
    {
        Name: string
    }

let chickenFlock = Dictionary<int, Chicken>()

let c = { Chicken.Name = "Gobble" }
chickenFlock[1] <- c

// Lots of code in between here!!!!

let myOtherChicken =
    {
        Chicken.Name = "Clucky"
    }
chickenFlock[2] <- myOtherChicken



