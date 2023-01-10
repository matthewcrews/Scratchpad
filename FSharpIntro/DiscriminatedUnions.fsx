type Chicken =
    {
        Name : string
        Size : float
    }

type Turkey =
    {
        Name : string
        Size : float
    }

[<RequireQualifiedAccess>]
type Bird =
    | Chicken of Chicken
    | Turkey of Turkey

let c1 : Chicken = { Name = "Clucky"; Size = 10.0 }
let b1 = Bird.Chicken c1

let t1 : Turkey = { Name = "Gobble"; Size = 20.0 }
let b2 = Bird.Turkey t1


let myBirdFunction (b: Bird) =
    match b with
    | Bird.Chicken c ->
        printfn $"Chicken: {c.Name}"
    | Bird.Turkey t ->
        printfn $"Turkey: {t.Name}"

myBirdFunction b2
