[<Struct>]
type Chicken =
    {
        Name : string
        Size : float
    }

let c1 = { Name = "Clucky"; Size = 10.0 }
let c2 = { c1 with Size = 2.0 }

[<Struct>]
type Turkey =
    val Name : string
    val mutable Size : float
    new (name, size) =
        { Name = name; Size = size}
    new name =
        { Name = name; Size = 1.0 }

let t0 = Turkey ()
let t1 = Turkey ("Gobble", 20.0)

[<Struct>]
type Goose (name: string, size: float) =
    member _.Name = name
    member _.Size = size
    new (name: string) =
        Goose (name, 1.0)

let g1 = Goose ()

type Hawk =
    struct
        val Name : string
        val Size : float
        new (name, size) =
            { Name = name; Size = size }
    end

let h0 = Hawk ()
let h1 = Hawk ("Hawky", 10.0)
