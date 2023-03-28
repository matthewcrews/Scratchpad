open System.Collections.Generic
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
    member _.Talk (words: string) =
        printfn $"My name is: {name}"
        printfn $"Cluck: {words}"

    interface IAnimal with
        member c.Name = c.Name
        member c.Talk words = c.Talk words

type Turkey (name: string) =

    member _.Name = name
    member _.Talk (words: string) =
        printfn $"My name is: {name}"
        printfn $"Gobble Gobble: {words}"

    interface IAnimal with
        member c.Name = c.Name
        member c.Talk words = c.Talk words

let c = Chicken "Clucky"

let animalFunction (a: IAnimal) =
    printfn $"{a.Name}"
    a.Talk "Words to say"

animalFunction c

let t = Turkey "Gertrude"
animalFunction t

let myAnimal =
    { new IAnimal with
        member _.Name = "Grace"
        member a.Talk words =
            printfn $"My name is: {a.Name}"
            printfn $"{words}"}

animalFunction myAnimal

type IDatabase =
    abstract member SaveStuff : string -> unit

let mockDatabase =
    { new IDatabase with
        member _.SaveStuff input =
            raise (KeyNotFoundException ())}

