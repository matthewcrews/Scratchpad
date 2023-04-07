open System
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

[<Struct; IsByRefLike>]
type VecFill<'T> =
    private {
        values: Span<'T>
        mutable count: int
    }
    member v.Add x =
        if v.count < 0 then
            invalidOp "Cannot add to a VecFree after it has been check in"
        if v.count < v.values.Length then
            v.values[v.count] <- x
            v.count <- v.count + 1

[<Struct>]
type Vec =
    private {
        mutable start: int
        mutable length: int
        mutable count: int
    }
    static member create (s, l, c) =
        {
            start = s
            length = l
            count = c
        }

    member v.Values (a: 'a[]) =
        Span (a, v.start, v.count)

    member v.Add (a: 'a, values: 'a[]) =
        if v.count < 0 then
            invalidOp "Cannot add to Vec when it's free space is checked out"
        if v.count >= v.length then
            invalidOp "Cannot add to full Vec"

        values[v.start + v.count] <- a
        v.count <- v.count + 1

    member v.CheckOutFreeSpace (values: 'a[]) =
        if v.count < 0 then
            invalidOp "Cannot checkout Free Space when Free Space is already checked out"

        let freeSpace = Span (values, v.start + v.count, v.length - v.count)
        // Indicate that the Vec cannot be added/removed from while checked out
        v.count <- v.count * -1
        {
            values = freeSpace
            count = 0
        }

    member v.CheckInFreeSpace (vf: byref<VecFill<'T>>) =
        if v.count > 0 then
            invalidOp "Cannot check in Free Space when Free Space not checked out"

        v.count <- v.count * -1

        if v.length - v.count <> vf.values.Length then
            invalidOp "Checking in a VecFree with wrong length"

        v.count <- v.count + vf.count
        // Invalidate the VecFree
        vf.count <- -1
        

let test () =

    let a = [|1..10|]
    let mutable v = Vec.create (0, 4, 2)
    let mutable vf = v.CheckOutFreeSpace a
    v.Add (1, a)
    // vf.Add 10
    // vf.Add 13
    // v.CheckInFreeSpace &vf
    // v.CheckInFreeSpace &vf

    // for e in v.Values a do
    //     printfn $"{e}"
    ()

test()
