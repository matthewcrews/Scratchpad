let a = Map [ 1, "a"; 2, "b"; 3, "c"]
let b = Map [
    1, "a"
    2, "b"
    3, "c"
]

let c = a.Add (1, "Chicken")
a

a[1]

let d = a.Remove 1

if a.ContainsKey 1 then
    let v = a.[1]
    printfn $"Value: {v}"

match a.TryGetValue 10 with
| true, v -> 
    printfn $"Value: {v}"
| false, _ -> 
    printfn "No Value"

match a.TryFind 1 with
| Some v -> 
    printfn $"Value: {v}"
| None ->
    printfn "No Value"

    
a.TryFind 1
|> Option.map (fun v ->
    printfn $"Value: {v}")

for KeyValue (key, value) in a do
    printfn $"Key: {key}"
    printfn $"Value: {value}"

let newMap = 
    a
    |> Map.map (fun key value ->
        value + "More string")
