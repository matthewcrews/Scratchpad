// let test (x: byref<ValueOption<float>>) =
//     match x with
//     | ValueSome v ->
//         // v <- v + 1.0
//         x <- ValueSome (v + 1.0)
//     | ValueNone ->
//         ()

// let mutable t = ValueSome 1.0
// test &t

// printfn $"{t}"
// What I get
(*
val test: x: byref<ValueOption<float>> -> unit
val mutable t: float voption = ValueSome 1.0
val it: unit = ()
*)

// What I want
(*
val test: x: byref<ValueOption<float>> -> unit
val mutable t: float voption = ValueSome 2.0
val it: unit = ()
*)

// [<Struct>]
// type Entry<'Key, 'Value> =
//     {
//         IsFull: bool
//         Key: 'Key
//         Value: 'Value
//         mutable Next: ref<Entry<'Key, 'Value>>
//     }
//     static member Empty =
//         {
//             IsFull = false
//             Key = Unchecked.defaultof<'Key>
//             Value = Unchecked.defaultof<'Value>
//             Next = Unchecked.defaultof<ref<Entry<'Key, 'Value>>>
//         }


[<Struct>]
type Chicken =
    {
        Name: string
        mutable Size: float
        mutable ChildRef: ref<Chicken>
    }

// let addChild (c: byref<Chicken>) =
//     if obj.ReferenceEquals(c.ChildRef, null) then
//         c.ChildRef <- ref { Name = "Clucky2"; Size = 1.0; ChildRef = Unchecked.defaultof<_> }

// let updateChild (c: byref<Chicken>) =
//     if not (obj.ReferenceEquals(c.ChildRef, null)) then
//         let child = &c.ChildRef.contents
//         child.Size <- child.Size + 1.0

// let test () =
//     let mutable c = { Name = "Clucky"; Size = 1.0; ChildRef = Unchecked.defaultof<_> }
//     addChild &c
//     updateChild &c
//     printfn $"{c}"



type RefEntry<'Key, 'Value>(key: 'Key, value: 'Value) =
    member val Key = key with get
    member val Value = value with get, set
    [<DefaultValue>] val mutable Tail : RefEntry<'Key, 'Value>

[<Struct>]
type StructEntry<'Key, 'Value> =
    {
        Key: 'Key
        mutable Value: 'Value
        mutable Tail: RefEntry<'Key, 'Value>
    }

let rec printEntry (sEntry: StructEntry<'Key, 'Value>) =

    let rec refLoop (rEntry: RefEntry<_,_>) =

        if not (obj.ReferenceEquals (rEntry, null)) then
            printfn $"Key: {rEntry.Key} | Value: {rEntry.Value}"
            refLoop rEntry.Tail

    printfn $"Key: {sEntry.Key} | Value: {sEntry.Value}"
    refLoop sEntry.Tail


let chickenUpdate (sEntry: byref<StructEntry<string, int>>) =

    let rec refLoop (rEntry: RefEntry<_,_>) =
        if not (obj.ReferenceEquals (rEntry, null)) then
            if rEntry.Key = "Chicken" then
                rEntry.Value <- 10
            refLoop rEntry.Tail

    if sEntry.Key = "Chicken" then
        sEntry.Value <- 10

    refLoop sEntry.Tail


let rec appendEntry (sEntry: byref<StructEntry<'Key, 'Value>>) (key: 'Key, value: 'Value) =
    
    let rec refLoop (rEntry: RefEntry<'Key, 'Value>) =

        if obj.ReferenceEquals (rEntry.Tail, null) then
            rEntry.Tail <- RefEntry (key, value)
        else
            refLoop rEntry.Tail

    if obj.ReferenceEquals (sEntry.Tail, null) then
        sEntry.Tail <- RefEntry (key, value)
    else
        refLoop sEntry.Tail


let rec updateEntry (sEntry: byref<StructEntry<'Key, 'Value>>) (key: 'Key, value: 'Value) =
    
    let rec refLoop (rEntry: RefEntry<'Key, 'Value>) =

        if obj.ReferenceEquals (rEntry.Tail, null) then
            rEntry.Tail <- RefEntry (key, value)
        elif rEntry.Key = key then
            rEntry.Value <- value
        else
            refLoop rEntry.Tail

    if sEntry.Key = key then
        sEntry.Value <- value
    else
        refLoop sEntry.Tail

let mutable x =
    { 
        Key = "Chicken"
        Value = 1
        Tail = Unchecked.defaultof<_>
    }
x
printEntry x
chickenUpdate &x
printEntry x

let newEntry = {
    Key = "Chicken"
    Value = 3
    Tail = Unchecked.defaultof<_>
}

appendEntry &x ("Turkey", 1)
printEntry x

appendEntry &x ("Chicken", 2)
printEntry x

updateEntry &x ("Turkey", 13)


// let dumbUpdate (input: Entry<string, int>[]) =

//     let rec loop (entry: byref<Entry<string, int>>) =
//         if entry.Key = "Chicken" then
//             entry.Value <- 10
//         elif obj.ReferenceEquals (entry.Next, null) then
//             loop &entry.Next.contents
//         else
//             ()

//     let first = &input[0]
//     loop &first



//     let rec loop (entry: byref<Entry<string, int>>) =
//         if entry.Key = "Chicken" then
//             entry.Value <- 10
//         elif obj.ReferenceEquals (entry.Next, null) then
//             loop &entry.Next.contents
//         else
//             ()
//     loop &first

// let updateChild (entry: byref<Entry<int, float>>) =
//     if not (obj.ReferenceEquals(entry.Next, null)) then
//         let next = &entry.Next.contents
//         next.Value <- next.Value + 1.0

// let addChild (entry: byref<Entry<int, float>>) child =
//     if obj.ReferenceEquals(entry.Next, null) then
//         entry.Next <- ref child

// let test () =
//     let mutable c = { Key = 1; Value = 1.0; Next = Unchecked.defaultof<_> }
//     let c2 = { Key = 2; Value = 2.0; Next = Unchecked.defaultof<_> }
//     addChild &c c2
//     updateChild &c

//     printfn $"Result: {c}"