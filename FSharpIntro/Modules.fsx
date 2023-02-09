module MyOrganization.MyAwesomeGame.Farm

type Chicken =
    {
        Name: string
        Size: float
    }

[<RequireQualifiedAccess>]
module Chicken =

    module private Helpers =

        let myAdder a b = a + b

    open Helpers


    let grow (c: Chicken) =
        // More complex code here!
        { c with Size = c.Size + 10.0 }

type Cow =
    {
        Name: string
        Age: int
    }

[<RequireQualifiedAccess>]
module Cow =

    let grow (c: Cow) =
        { c with Age = c.Age + 1 }


let aChicken = { Name = "a"; Size = 1.0 }
let bChicken = Chicken.grow aChicken
