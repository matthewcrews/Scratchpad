[<Struct>]
type Chicken = {
    mutable Name: string
    mutable Size: float
    mutable Age: int
}
let test () =
    let mutable c = {
        Name = "Chicke"
        Size = 1.0
        Age = 1
    }

    printfn "Chicken 1"
    printfn $"{c}"

    let mutable c2 = &c
    c2.Age <- 2

    printfn "Chicken 1"
    printfn $"{c}"
    printfn "Chicken 2"
    printfn $"{c2}"

test ()
