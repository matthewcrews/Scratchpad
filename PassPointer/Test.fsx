open System
open System.Runtime.CompilerServices
open System.Runtime.InteropServices
open System.Runtime.Intrinsics


let test (a: int[]) =
    let a = a.AsSpan()
    let v1 = Vector256.LoadUnsafe &a[0]
    let v2 = Vector256.LoadUnsafe &a[0]
    printfn $"{v1 = v2}"
    ()

let x = [| 0 .. 10|]
test x