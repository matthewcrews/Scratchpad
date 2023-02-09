
let myAdder a b =
    a + b

let myValidator
    (trueHandler: int -> unit)
    (falseHandler: int -> unit)
    (a: int) 
    : bool =

    if a > 10 then
        trueHandler a
        true
    else
        falseHandler a
        false

let myTrueHandler (x: int) : unit =
    printfn $"Hit True Handler: {x}"
let myFalseHandler (x: int) : unit =
    printfn $"Hit FALSE Handler: {x}"

myValidator myTrueHandler myFalseHandler 5


let myAdd10 (a: int) =
    a + 10
let myMultiplier (a: int) =
    a * 10

let myCombo = myAdd10 >> myMultiplier
myCombo 2
let myOtherCombo = myMultiplier >> myAdd10
myOtherCombo 2

let x = myAdd10 2
let y = myMultiplier x
