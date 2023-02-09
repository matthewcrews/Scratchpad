let values = [|1..10|]

for v in values do
    printfn $"{v}"

for i in 1..values.Length - 1 do
    printfn $"{values[i]}"

for i in 1..2..values.Length - 1 do
    printfn $"{values[i]}"

for i = 0 to values.Length - 1 do
    printfn $"{values[i]}"

for i = values.Length - 1 downto 0 do
    printfn $"{values[i]}"


let mutable i = 0

while i < values.Length - 1 do
    printfn $"{values[i]}"
    i <- i + 1
