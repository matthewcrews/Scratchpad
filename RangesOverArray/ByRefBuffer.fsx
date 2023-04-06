open System
open System.Runtime.CompilerServices


let throwBufferOverflow () =
    raise (IndexOutOfRangeException "TempBuffer does not have enough space")

let throwBufferIndexOutOfRange () =
    raise (IndexOutOfRangeException "Index out of Range for TempBuffer")

let throwBufferNegativeIndex () =
    raise (IndexOutOfRangeException "Cannot use negative index for TempBuffer")

let throwInsufficientSpaceInTarget () =
    raise (IndexOutOfRangeException "Target array does not have enough space")


[<Struct; IsByRefLike>]
type TempBuffer<'a> (values: 'a[]) =
    [<DefaultValue>]
    val mutable private count: int

    member b.Add (n: 'a) =
        if b.count < values.Length then
            values[b.count] <- n
            b.count <- b.count + 1
        else
            throwBufferOverflow()

    member b.Add (source: 'a[]) =
        if b.count + source.Length > values.Length then
            throwBufferOverflow()

        Array.Copy (source, 0, values, b.count, source.Length)

    member b.Add (source: Span<'a>) =
        if b.count + source.Length > values.Length then
            throwBufferOverflow()

        let target = Span (values, b.count, b.count - values.Length)
        source.CopyTo target

    member b.MoveTo (target: 'a[], startIdx: int) =
        if startIdx < 0 then
            throwBufferNegativeIndex()

        if b.count > target.Length - startIdx then
            throwInsufficientSpaceInTarget ()

        Array.Copy (values, 0, target, startIdx, b.count)
        b.count <- 0

    member b.Count = b.count
    member b.Values = Span (values, 0, b.count)
    member b.Remove (i: int) =
        if i < 0 then
            throwBufferNegativeIndex()
        if i >= b.count then
            throwBufferIndexOutOfRange()

        let result = values[i]
        b.count <- b.count - 1
        values[i] <- values[b.count]
        result

    member b.Item
        with get (i: int) =
            if i < 0 then
                throwBufferNegativeIndex()
            if i >= b.count then
                throwBufferIndexOutOfRange()
            &values[i]


let otherCall (t: byref<TempBuffer<int>>) =
    t.Add 100

let test () =

    let values = [|1..3|]
    let mutable b = TempBuffer values
    otherCall &b
    printfn "Start"

    for v in b.Values do
        printf $"{v},"
    printfn "Fin"
    //
    // b.Add 0
    // for v in b.Values do
    //     printf $"{v},"
    // printfn "Here: 1"
    //
    // b.Add 0
    // for v in b.Values do
    //     printf $"{v},"
    // printfn "Here: 2"
    //
    // b.Add 0
    // for v in b.Values do
    //     printf $"{v},"
    // printfn "Here: 3"
    //
    // for i in 0 .. b.Count - 1 do
    //     let v = &b[i]
    //     v <- v + 1
    // for v in b.Values do
    //     printf $"{v},"
    // printfn "Here: 4"
    //
    // for v in b.Values do
    //     v

test ()
