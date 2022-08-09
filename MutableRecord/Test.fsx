
open System
open System.Runtime.CompilerServices
open FSharp.NativeInterop
#nowarn "9" // Pointers are cool

let inline stackalloc<'a when 'a: unmanaged> (length: int): Span<'a> =
    let p = NativePtr.stackalloc<'a> length |> NativePtr.toVoidPtr
    Span<'a>(p, length)


[<Struct; IsByRefLike>]
type StackStack<'T>(values: Span<'T>) =
    [<DefaultValue>] val mutable private _count : int
    
    member s.Push v =
        if s._count < values.Length then
            values[s._count] <- v
            s._count <- s._count + 1
        else
            failwith "Exceeded capacity of StackStack"
        
    member s.Pop () =
        if s._count > 0 then
            s._count <- s._count - 1
            values[s._count]
        else
            failwith "Empty StackStack"
            
    member s.Count = s._count

    
module StackStack =
    
    let inline create capacity =
        let values = stackalloc<_> capacity
        StackStack values

[<Struct; IsByRefLike>]
type Context =
        val mutable Stack1 : StackStack<int>
        val mutable Stack2 : StackStack<int>
        new (stack1, stack2) =
            { Stack1 = stack1; Stack2 = stack2 }

module Context =

    let inline create capacity =
        let stack1 = StackStack.create capacity
        let stack2 = StackStack.create capacity
        Context (stack1, stack2)


let test () =

    printfn "State"

    let mutable c = Context.create 10
    c.Stack1.Push 1
    c.Stack1.Push 2
    c.Stack1.Push 3

    while c.Stack1.Count > 0 do
        let next = c.Stack1.Pop()
        printfn $"NextValue: {next}"

test ()