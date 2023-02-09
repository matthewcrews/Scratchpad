let myList = [1; 2; 3]
let myList2 =
    [
        1
        2
        3
    ]
myList = myList2
let firstElem = myList[0]
// myList[0] <- 10

let myList3 = 4 :: myList
let four :: theRest = []

let combinedList = myList @ myList2

let coolList =
    [ for i in 1 .. 3 do
        for j in 1 .. 3 do
            i * j]



let myValues = [1..10]

let mutable acc = []

for v in myValues do
    if v % 2 = 0 then
        acc <- v :: acc


let myPrinter values =

    let rec loop values acc =
        match values with
        | [] -> acc
        | head::tail ->
            if head % 2 = 0 then
                loop tail (head::acc)
            else
                loop tail acc

    loop values []

let x = myPrinter myValues



let myEvens =
    List.filter (fun x -> x % 2 = 0) myValues

let myEvensDoubled =
    List.map (fun x -> x * 2) myEvens

let mySolution =
    myValues
    |> List.filter (fun x -> x % 2 = 0)
    |> List.map (fun x -> x * 2)
    |> List.filter (fun x -> x > 10)
    |> List.map (fun x -> x + 2)