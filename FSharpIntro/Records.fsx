// type Turkey =
//     {
//         Name : string
//         Size : float
//     }
// let c1 : Turkey = { Name = "Clucky"; Size = 10.0 }

type Chicken =
    {
        Name : string
        Size : float
    }
    static member Create (name, size) =
        // Validation logic
        if size <= 0.0 then
            invalidArg (nameof size) "Cannot have negative size chicken"
        {
            Name = name
            Size = size
        }

    static member Create name =
        {
            Name = name
            Size = 10.0
        }

module Chicken =

    let create name size =
        {
            Name = name
            Size = size
        }

let c1 = Chicken.create "Clucky2" 20.0
let c2 = Chicken.Create ("Clucky3", 30.0)
let c3 = Chicken.Create "Clucky4"