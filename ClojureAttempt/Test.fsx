type Chicken =
    {
        Name : string
        Size : float
        Age : int
    }

let a = {
    Name = "Cluck"
    Size = 10.0
    Age = 1
}

let { Name = b } = a

printfn $"{b}"


let funky ({ Name = b}: Chicken) =
    printfn $"In funky: {b}"


funky a