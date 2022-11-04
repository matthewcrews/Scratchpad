
let add = [Array.add; List.add; Map.add]

type Chicken =
    {
        Size : float
    }

type Bear =
    {
        Size : float
    }

// Nominal vs Structural typing
shape SAnimal =
    {
        Size : float
    }

let animalFunc (a: SAnimal) =
    let x = a.Size
    ()

module Chicken =

    let equals (a: Chicken) (b: Chicken) =
        ()

    let add (a: Chicken) (b: Chicken) =
        ()

    module Bear =

        let add (a: Chicken) (b: Bear) =
            ()

let equals = equals :: Chicken.equals

let inline add = add :: Chicken.add :: Chicken.Bear.add

let a : Chicken = { Size = 10.0 }
let b : Chicken = { Size = 1.0 }

let c = add a b

let d : Bear = { Size = 1.0 }
let x = add a d

// 🥲
let inline mySadFunc = add a

mySadFunc b
mySadFunc d

let myFunc a n =

    let h = hash a
    let x = add h n
    x 


let h = Dictionary<Chicken>(hashFunc, equalsFunc)
