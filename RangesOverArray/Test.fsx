open System

[<Struct>]
type Chicken =
    {
        ChickenId: int
        Age: int
        Size: float
    }

[<Struct>]
type Range =
    {
        mutable Start: int
        mutable Length: int
        mutable Count: int
    }

[<Struct>]
type Flock =
    private {
        // mutable Happy: Range
        // mutable InTransit: Range
        mutable Sick: Range
        mutable Inventory: int[]
    }
    member f.SickValues = Span (f.Inventory, f.Sick.Start, f.Sick.Count)

module Flock =

    module private Helpers =

        let throwNegativeIndexExceptionHelper () =
            raise (IndexOutOfRangeException "Cannot have an index less than 0")

        let throwTooLargeOutBufferExceptionHelper () =
            invalidArg "outBuffer" "Out Buffer too large"


        let removeRange (index: int) (outBuffer: Span<int>) (inventory: int[]) (range: byref<Range>) =
            if index < 0 then
                throwNegativeIndexExceptionHelper()
            if index + outBuffer.Length > range.Count then
                throwTooLargeOutBufferExceptionHelper()
            let sourceSpan = Span (inventory, range.Start, range.Count)
            let copySpan = sourceSpan.Slice (index, outBuffer.Length)
            copySpan.CopyTo outBuffer

            // Copy values down
            range.Count <- range.Count - outBuffer.Length
            let fillInSource = sourceSpan.Slice (index + outBuffer.Length, range.Count - index)
            let fillInDest = sourceSpan.Slice (index, range.Count - index)
            fillInSource.CopyTo fillInDest

    open Helpers


    module Sick =

        let pop (buffer: Span<int>) (flock: byref<Flock>) =
            // Check that there are enough to Pop
            let sickCount = flock.Sick.Count
            let sick = flock.SickValues.Slice(sickCount - buffer.Length)
            sick.CopyTo buffer
            flock.Sick.Count <- sickCount - buffer.Length

        let removeRange (index: int) (buffer: Span<int>) (flock: byref<Flock>) =
            removeRange index buffer flock.Inventory &flock.Sick


let test () =
    let f = {
        Inventory = [|1..10|]
        Sick = { Start = 1; Length = 7; Count = 5 }
    }
    let flocks = [|
        f
    |]

    let flock = &flocks[0]

    printfn $"Flock: {flock}"
    let outBuffer = [|0; 0; 0|]
    let bufferSpan = outBuffer.AsSpan()

    Flock.Sick.removeRange 0 bufferSpan &flock

    for elem in outBuffer do
        printfn $"Buffer: {elem}"

    printfn $"Flock: {flock}"

test ()
