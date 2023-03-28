type MyEnumerator () =
    let maxValue = 10
    let mutable curValue = 0

    member _.MoveNext () =
        curValue <- curValue + 1
        curValue < maxValue

    member _.Current =
        curValue

type MyCollection () =

    member _.GetEnumerator () = MyEnumerator()


let myCollection = MyCollection()

for x in myCollection do
    printfn $"{x}"


type IAnimal =
    abstract member Name: string
    abstract member Talk: string -> unit

type Chicken (name: string) =

    member _.Name = name
    member _.Talk (talk: string) =
        printfn $"My name is: {name}"
        printfn $"Cluck: {talk}"

    interface IAnimal with
        member c.Name = c.Name
        member c.Talk whatToSay = c.Talk whatToSay

type Turkey (name: string) =

    member _.Name = name
    member _.Talk (talk: string) =
        printfn $"My name is: {name}"
        printfn $"Gobble Gobble: {talk}"

    interface IAnimal with
        member c.Name = c.Name
        member c.Talk whatToSay = c.Talk whatToSay

let myAnimal =
    { new IAnimal with
        member _.Name = "Grace"
        member a.Talk talk =
            printfn $"My name is: {a.Name}"
            printfn $"{talk}"
    }

myAnimal.Talk "Hello"


let animalFunction (a: IAnimal) =
    printfn $"{a.Name}"
    a.Talk "In Animal Function"

animalFunction myAnimal
let c = Chicken "Gertrude"
animalFunction c


let mySeqFunction (a: unit -> seq<int>) =
    let values = a ()
    for x in values do
        printfn $"{x}"

let myListGen () = [1..5]
mySeqFunction myListGen