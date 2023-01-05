[<Struct>]
type Chicken =
    {
        Name : string
        Size : float
    }

let c1 = { Name = "Clucky"; Size = 10.0 }

type Turkey =
    struct
        val Name : string
        val Size : float
    new (name, size) =
        // Validation logic!
        {
            Name = name
            Size = size
        }
    end

let t = Turkey ("Turkey", 10.0)

type Goose (name: string, size: float) =
    struct
        member _.Name = name
        member _.Size = size
    end

let g = Goose ("Goosey", 10.0)
g.Size