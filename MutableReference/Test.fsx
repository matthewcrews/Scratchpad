[<Struct>]
type Chicken =
    {
        Name: string
        mutable Size: float
    }
    member c.Grow x =
        c.Size <- c.Size + x

let c = {
    Name = "Clucky"
    Size = 1.0
}

c.Grow 1.0
printfn $"{c}"

let test () =



    let flock =
        [| for i in 1.0..3.0 do
               {
                   Name = $"Clucky{i}"
                   Size = i
               } |]

    let c1 = &flock[0]
    c1.Grow 13.0

    for c in flock do
        printfn $"{c}"
    ()

test ()
