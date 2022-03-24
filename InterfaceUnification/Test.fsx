module rec Test =

    type INode =
        abstract member AsNode : Node

    type Buffer = 
        { 
            Name : string
        }
        static member create n = { Name = n }
        member b.AsNode = Node.Buffer b
        interface INode with
            member this.AsNode = this.AsNode

    type Constraint = 
        { 
            Name : string
        }
        static member create n = { Name = n }
        member c.AsNode = Node.Constraint c
        interface INode with
            member this.AsNode = this.AsNode

    [<RequireQualifiedAccess>]
    type Node =
        | Buffer of Buffer
        | Constraint of Constraint


open Test

let b1 = Buffer.create "b1"
let b2 = Buffer.create "b2"
let c1 = Constraint.create "c1"

let nodes (x: INode list) = x

let nodeList : INode list =
    [
        b1
        b2
        c1
    ]

let x = infinity
let y = 1.0

x + y
x - y
x + x

let (|Chicken|Cow|) x =
    if x % 2 = 0 then
        Chicken x
    else
        Cow (string x)


open System.Runtime.InteropServices

let d = System.Collections.Generic.Dictionary<int, int>()

let test () =
    let mutable t = Unchecked.defaultof<_>
    let mutable v = &CollectionsMarshal.GetValueRefOrAddDefault (d, 1, &t)
    // The value at t should now be 1
    printfn $"{v}"
    let x = v + 1
    printfn $"{x}"
    v <- v + 1

test ()

match CollectionsMarshal.GetValueRefOrAddDefault (c, 1) with
| (x, true) -> printfn $"Value Existed: {x}"
| (x, false) -> printfn $"Value Did not exist: {x}"