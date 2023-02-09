
let myAdder a b =
    a + b

let myAdder2 (a: float) (b: float) : float =
    
    if a < 1.0 then
        a + b
    else
        a

let myAdder3 = fun a b -> a + b

let x =
    [1..10]
    |> List.map (fun x -> x * 2)


let rec myFunc x =

    if x < 10 then
        printfn $"{x}"
        myFunc (x + 1)
    else
        ()

[<RequireQualifiedAccess>]
type Animal =
    | Chicken of int
    | Cow of string

let myAnimalFunction (a: Animal) =
    match a with
    | Animal.Chicken chicken ->
        printfn $"Chicken: {chicken}"
    | Animal.Cow cow ->
        printfn $"Cow: {cow}"

let myChicken = Animal.Chicken 42

myAnimalFunction myChicken

let myOtherFunction =
    function
    | Animal.Chicken chicken ->
        printfn $"Chicken: {chicken}"
    | Animal.Cow cow ->
        printfn $"Cow: {cow}"

myOtherFunction myChicken
