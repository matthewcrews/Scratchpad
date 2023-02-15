let emptyValue = None

let aValue = Some 1
let bValue = Some 1

aValue = bValue
aValue = emptyValue
// let x1 = aValue + 1

let x1 =
    match aValue with
    | Some v -> v + 1
    | None -> 0

let x1Empty =
    match emptyValue with
    | Some v -> v + 1
    | None -> 0

let myAddOneToOption (a: option<int>) =
    match a with
    | Some v -> v + 1
    | None -> 0

myAddOneToOption emptyValue
myAddOneToOption aValue

aValue
|> Option.map (fun v -> v + 1)

emptyValue
|> Option.map (fun v -> v + 1)

let optionPrinter (a: option<int>) =
    a
    |> Option.iter (fun v ->
    printfn $"Has a value: {v}")

optionPrinter aValue
optionPrinter emptyValue

aValue
|> Option.iter (fun v ->
    printfn $"Has a value: {v}")

10 / 0


let safeDivide (a: int) (b: int) =
    if b = 0 then
        None
    else
        Some (a / b)

safeDivide 10 0
safeDivide 10 2


