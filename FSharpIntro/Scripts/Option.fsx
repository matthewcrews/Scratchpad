// type MyOption<'Value> =
//     | Some of value: 'Value
//     | None

let a1 = Some 1
let a2 = None

let a3 = Some 1

// Structural Equality
a1 = a3

// Have to unpack value to work with it
let x1 = a1 + 3

let x2 =
    match a1 with
    | Some v -> v + 3
    | None -> 3

let x2 =
    match a1 with
    | Some v -> Some (v + 3)
    | None -> None

let x3 =
    a1
    |> Option.map (fun v -> v + 3)

// We can also just do something with the value

match a1 with
| Some v ->
    printfn $"a1 had a value: {v}"
| None ->
    ()

// Alternatively we can

a1
|> Option.iter (fun v ->
    printfn $"a1 had a value: {v}")



10 / 0

let safeDivide (a: int, b: int) =
    if b = 0 then
        None
    else
        Some (a / b)

// Unpacking
let myFunction (a: option<int>) =
    match a with
    | Some v -> printfn $"Some value: {v}"
    | None -> printfn $"No Value"

let myOtherFunction (a: int option) =
    match a with
    | Some v -> printfn $"Some value: {v}"
    | None -> printfn $"No Value"



// Sorting behavior
let x = [
    None
    Some 3
    Some 1
    Some 2
    None
]

let sortResult = List.sort x

