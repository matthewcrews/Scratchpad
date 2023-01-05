
[<Struct>]
type Chicken =
    {
        Name : string
        Size : float
    }

type AltChicken =
    struct
        val Name : string
        val Size : float

        new (name, size) =
            { Name = name; Size = size }
    end

let aChicken = AltChicken ("a", 1.0)

type AltAltChicken (name: string, size: float) =
    struct
        member _.Name = name
        member _.Size = size
    end

let aChicken = AltAltChicken ("c", 10)


type MutAltChicken =
    struct
        val Name : string
        val mutable Size : float

        new (name, size) =
            if System.String.IsNullOrEmpty name then
                invalidArg (nameof name)  "Whoops"
            { Name = name; Size = size }
    end

let mutable mutAltChicken = MutAltChicken ("C", 1.0)
mutAltChicken.Size <- 10.0


let altChicken : AltChicken = { Name = "c"; Size = 10.0 }


[<Struct>]
type MutChicken =
    {
        Name : string
        mutable Size : float
    }

let c1 = { Name = "Clucky"; Size = 10.0 }
c1.Size <- 10

let mutable c1 = { Name = "Clucky"; Size = 10.0 }
c1.Size <- 20
c1

open System
open System.Runtime.CompilerServices

[<Struct; IsByRefLike>]
type StackStack<'T> =
    {
        Values : Span<'T>
        mutable Count : int
    }
    member s.Push x =
        if s.Count < s.Values.Length then
            s.Values[s.Count] <- x
            s.Count <- s.Count + 1
        else
            failwith "WTF?"

    member s.Pop () =
        if s.Count > 0 then
            s.Count <- s.Count - 1
            s.Values[s.Count]
        else
            failwith "WTF?"

