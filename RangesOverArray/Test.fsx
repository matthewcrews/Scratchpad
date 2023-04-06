open System
open System.Numerics

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
        mutable Happy: Range
        mutable Sick: Range
        mutable Inventory: int[]
    }
    member f.SickValues = ReadOnlySpan (f.Inventory, f.Sick.Start, f.Sick.Count)
    member f.HappyValues = ReadOnlySpan (f.Inventory, f.Happy.Start, f.Happy.Count)

[<Measure>]
type FlockId

[<Struct>]
type Flocks internal (flocks: Flock[], changeTracker: bool[]) =

    member f.Item
        with get (x: int<FlockId>) =
            changeTracker[int x] <- true
            &flocks[int x]

    // Can have the Inventory Summaries returned as a ReadOnlySpan

module Flocks =

    let create count =
        let flocks = Array.zeroCreate count
        let changeTracker = Array.zeroCreate count
        Flocks (flocks, changeTracker)

/// Compute the next power of 2 that is equal or greater than n
let computeNextBucketLength (n: int) =
    1 <<< (64 - (BitOperations.LeadingZeroCount (uint (n - 1))))

module Flock =

    module private Helpers =

        let throwNegativeIndexExceptionHelper () =
            raise (IndexOutOfRangeException "Cannot have an index less than 0")


        let throwTooLargeOutBufferExceptionHelper () =
            invalidArg "outBuffer" "Out Buffer too large"

        /// WARNING: This function assumes that the proper range check have already occured
        let move (source: 'a[]) (range: byref<Range>) (index: int) (target: Span<'a>) =
            let sourceSpan = Span (source, range.Start, range.Count)
            let copySpan = sourceSpan.Slice (index, target.Length)
            copySpan.CopyTo target

            // Copy values down
            range.Count <- range.Count - target.Length
            let fillInSource = sourceSpan.Slice (index + target.Length, range.Count - index)
            let fillInDest = sourceSpan.Slice (index, range.Count - index)
            fillInSource.CopyTo fillInDest

        /// WARNING: This function assumes that the proper range check have already occured
        let pop (source: 'a[]) (range: byref<Range>) (outBuffer: Span<'a>) =
            move source &range (range.Count - outBuffer.Length) outBuffer

        /// WARNING: This function assumes that the proper range check have already occured
        let dequeue (source: 'a[]) (range: byref<Range>) (outBuffer: Span<'a>) =
            move source &range 0 outBuffer

        let resize newHappyLength newSickLength (flock: byref<Flock>) =
            let newTotalLength = newHappyLength + newSickLength
            let newInventory = GC.AllocateUninitializedArray newTotalLength
            let oldInventory = flock.Inventory

            let sourceHappy = Span (oldInventory, flock.Happy.Start, flock.Happy.Count)
            let targetHappy = Span (newInventory, 0, newHappyLength)
            sourceHappy.CopyTo targetHappy
            flock.Happy.Length <- newHappyLength

            let sourceSick = Span (oldInventory, flock.Sick.Start, flock.Sick.Count)
            let targetSick = Span (newInventory, flock.Happy.Length, newSickLength)
            sourceSick.CopyTo targetSick
            flock.Sick.Start <- flock.Happy.Start + flock.Happy.Length
            flock.Sick.Length <- newSickLength

            flock.Inventory <- newInventory

    open Helpers

    module Happy =

        let removeRange (index: int) (outSpan: Span<int>) (flock: byref<Flock>) =
            move flock.Inventory &flock.Happy index outSpan

        let pop (outSpan: Span<int>) (flock: byref<Flock>) =
            removeRange (flock.Happy.Count - outSpan.Length) outSpan &flock

        let dequeue (outSpan: Span<int>) (flock: byref<Flock>) =
            removeRange 0 outSpan &flock

        let add (source: ReadOnlySpan<int>) (flock: byref<Flock>) =
            let newHappyCount = flock.Happy.Count + source.Length

            // Ensure space available
            if newHappyCount > flock.Happy.Length then
                let newHappyLength = computeNextBucketLength (newHappyCount - 1)
                resize newHappyLength flock.Sick.Length &flock

            // Perform the copy
            move source flock.Inventory &flock.Happy

    module Sick =

        let removeRange (index: int) (outSpan: Span<int>) (flock: byref<Flock>) =
            move flock.Inventory &flock.Sick index outSpan

        let pop (outSpan: Span<int>) (flock: byref<Flock>) =
            removeRange (flock.Sick.Count - outSpan.Length) outSpan &flock

        let dequeue (outSpan: Span<int>) (flock: byref<Flock>) =
            removeRange 0 outSpan &flock

        let add (source: ReadOnlySpan<int>) (flock: byref<Flock>) =
            let newSickCount = flock.Sick.Count + source.Length

            // Ensure space available
            if newSickCount > flock.Sick.Length then
                let newSickLength = computeNextBucketLength (newSickCount - 1)
                resize flock.Happy.Length newSickLength &flock

            // Perform the copy
            copyTo source flock.Inventory &flock.Sick


let test () =
    let f = {
        Inventory = Array.empty
        Happy = { Start = 0; Length = 0; Count = 0 }
        Sick = { Start = 0; Length = 0; Count = 0 }
    }
    let flocks = [|
        f
    |]

    let flock = &flocks[0]

    printfn $"Flock: {flock}"
    let buffer = [|7; 7; 7|]
    let s = buffer.AsSpan()

    Flock.Happy.pop s &flock

    for elem in buffer do
        printfn $"Buffer: {elem}"

    printfn $"Flock: {flock}"

test ()
// nextPowerOf2 -1

let mutable x = 10

let add1 (a: byref<int>) =
    a <- a + 1

printfn $"{x}"
add1 &x
printfn $"{x}"

type Turkey =
    internal {
        mutable Size : int
        mutable Values: int[]
    }
    static member create i =
        { Size = i; Values = Array.empty }
    member t.Grow () =
        t.Size <- t.Size + 1

let changeArray (a: byref<int[]>) =
    a <- [|1; 2; 3|]

let t = {
    Size = 10
    Values = [||]
}

changeArray (&t.Values)
t

let readOnlyTest () =

    let a = [|for i in 1..3 do
                  Turkey.create i |]
    let r = ReadOnlySpan a
    let mutable m = &r[0]
    m.Grow()

    for x in a do
        printfn $"{x}"

readOnlyTest()

let ts = TimeSpan()
ts.Days

type Chicken =
    private {
        name: string
    }
    member c.Name = c.name
