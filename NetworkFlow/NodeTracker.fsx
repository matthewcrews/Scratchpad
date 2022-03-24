

let x = 1L

let wasVisited (a: int64) (position: int) =
    (a &&& (1L <<< position)) <> 0

let setBit (a: int64) (position: int) =
    (1L <<< position) ||| a

wasVisited x 0

let n = setBit 0L 10

wasVisited n 1

open System.Runtime.CompilerServices

// [<Struct>]
// type Test =
//     private {
//         mutable Value : int64
//     }
//     static member Create () =
//         {
//             Value = 0L
//         }

//     member this.Set (newValue: int64) =
//         this.Value <- newValue

// let mutable t = Test.Create ()
// t.Set 10L
// t // Why is t.Value not 10???


// type Test2 =
//     struct
//         val mutable Value : int64

//         member this.Set (newValue: int64) =
//             this.Value <- newValue  
//     end
    
// let mutable t2 = Test2 ()
// t2.Set 10L
// t2

[<Struct>]
type LargeIndex =
    private {
        mutable Lower : int64
        mutable Upper : int64
    }
    static member Create () =
        {
            Lower = 0L
            Upper = 0L
        }

    member this.IsSet (position: int) =
        if position < 64 then
            (this.Lower &&& (1 <<< position)) <> 0
        else
            (this.Upper &&& (1 <<< (position - 64))) <> 0

    member this.Set (position: int) =
        if position < 64 then
            this.Lower <- (1L <<< position) ||| this.Lower
        else
            this.Upper <- (1L <<< position) ||| this.Upper

    member this.UnSet (position: int) =
        if position < 64 then
            this.Lower <- ~~~ (1 <<< position) &&& this.Lower
        else
            this.Upper <- ~~~ (1 <<< position) &&& this.Upper


let mutable l = LargeIndex.Create ()
l.Set 1
l
l.UnSet 1
l
l.IsSet 10
l.Set 64
l.UnSet 64
l
open System
open FSharp.NativeInterop
#nowarn "9" // Yes, I'm using pointers
let indexRange = 100
let test () =
    let requiredInt64s = (indexRange + 64 - 1) / 64
    let mem = NativePtr.stackalloc<int64> requiredInt64s
    let mem2 = mem |> NativePtr.toVoidPtr
    let stackSpan = Span<int64>(mem2, requiredInt64s)
    stackSpan[0] <- (1 <<< 1) ||| stackSpan[0]
    printfn "%A" (stackSpan.ToArray ())

test ()
