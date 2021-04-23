type Chicken = {
    Name : string
    Size : float
}

let newFUnction x y =
    x + y

newFUnction 1 2


type Parameter = {
    Name: string
    Value: float
}

type Model = {
    Parameters: Parameter []
}

let c1 = {
    Name = "Clucky"
    Size = 42.0
}

let testThing c =
    { c with Size = 10.0 }

let c2 = testThing c1