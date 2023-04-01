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
        mutable Happy: Range
        mutable Sick: Range
        mutable Inventory: int[]
    }
    member f.SickValues = ReadOnlySpan (f.Inventory, f.Sick.Start, f.Sick.Count)
    member f.HappyValues = ReadOnlySpan (f.Inventory, f.Happy.Start, f.Happy.Count)

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

        let copyTo (source: ReadOnlySpan<int>) (inventory: int[]) (range: byref<Range>) =
            let dest = Span (inventory, range.Start + range.Count, source.Length)
            source.CopyTo dest
            range.Count <- range.Count + source.Length

        let redistribute happyBlockCount sickBlockCount (flock: byref<Flock>) =
            let newHappyStart = 0
            let newSickStart = happyBlockCount <<< 3

            // NOTE: With only two buckets this is easy. With more than two we need to
            // move the buckets in order of the direction they are moving so as to not
            // overlap values
            let inventory = flock.Inventory
            let source = Span (inventory, flock.Sick.Start, flock.Sick.Count)
            let target = Span (inventory, newSickStart, flock.Sick.Count)
            source.CopyTo target

            // Update Starts
            flock.Sick.Start <- newSickStart

            // Update Lengths
            flock.Happy.Length <- happyBlockCount <<< 3
            flock.Sick.Length <- sickBlockCount <<< 3

        let grow happyBlockCount sickBlockCount (flock: byref<Flock>) =
            // Calculate the number of blocks of 8 we will need for the new array
            let totalBlockCount =
                happyBlockCount +
                sickBlockCount

            // Create new array for the inventory
            let newInventory = GC.AllocateUninitializedArray (totalBlockCount <<< 3)

            // Calculate the new start locations for the buckets
            let newSickStart = happyBlockCount <<< 3
            // If we had more categories, we would build on them here

            // Copy data from the old array to the new array
            let tgtHappy = Span (newInventory, 0, flock.Happy.Count)
            flock.HappyValues.CopyTo tgtHappy

            let tgtSick = Span (newInventory, newSickStart, flock.Sick.Count)
            flock.SickValues.CopyTo tgtSick

            // Update the inventory array
            flock.Inventory <- newInventory
            // Set the new starting positions for the buckets
            // The Happy group always start at 0 so we can skip it
            // flock.Happy.Start <- 0
            flock.Sick.Start <- newSickStart
            // Set the new Lengths
            flock.Happy.Length <- happyBlockCount <<< 3
            flock.Sick.Length <- sickBlockCount <<< 3


        let resize happyCapacity sickCapacity (flock: byref<Flock>) =
            let happyBlockCount = (7 + happyCapacity) >>> 3
            let sickBlockCount = (7 + sickCapacity) >>> 3
            let requiredBlockCount =
                happyBlockCount +
                sickBlockCount

            let curBlockCount = flock.Inventory.Length >>> 3

            if requiredBlockCount > curBlockCount then
                grow happyBlockCount sickBlockCount &flock

            else
                redistribute happyBlockCount sickBlockCount &flock


    open Helpers


    module Sick =

        let removeRange (index: int) (target: Span<int>) (flock: byref<Flock>) =
            removeRange index target flock.Inventory &flock.Sick

        let pop (target: Span<int>) (flock: byref<Flock>) =
            removeRange 0 target &flock

        let dequeue (target: Span<int>) (flock: byref<Flock>) =
            removeRange 0 target &flock

        let add (source: ReadOnlySpan<int>) (flock: byref<Flock>) =
            let newSickCount = flock.Sick.Count + source.Length
            // Check if we already have enough capacity. If so, copy the data
            if newSickCount <= flock.Sick.Length then
                copyTo source flock.Inventory &flock.Sick

            else
                // Ensure space available
                resize flock.Happy.Count newSickCount &flock
                // Perform the copy
                copyTo source flock.Inventory &flock.Sick


let test () =
    let f = {
        Inventory = [|1..10|]
        Happy = { Start = 0; Length = 1; Count = 1 }
        Sick = { Start = 1; Length = 7; Count = 5 }
    }
    let flocks = [|
        f
    |]

    let flock = &flocks[0]

    printfn $"Flock: {flock}"
    let inBuffer = [|-1; -1; -1; -1; -1; -1; -1|]
    let inSpan = inBuffer.AsSpan()

    Flock.Sick.add inSpan &flock

    for elem in flock.Inventory do
        printfn $"Buffer: {elem}"

    printfn $"Flock: {flock}"

test ()
