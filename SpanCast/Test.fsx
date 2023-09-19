open System
open System.Runtime.InteropServices

[<Struct>]
type Chicken =
    {
        mutable Age: int
    }

let test () =
    let a : byte[] = Array.zeroCreate 8
    let s = Span (a, 0, sizeof<int>)
    let mutable c = &MemoryMarshal.AsRef<Chicken> s
    c.Age <- 1
    
test ()

