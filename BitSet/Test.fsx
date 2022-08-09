open System
open System.Collections
open System.Collections.Generic

module private Helpers =

    let inline computeBucketAndMask (itemKey: int<'Item>) itemCount =

        if (uint itemKey) >= (uint itemCount) then
            raise (IndexOutOfRangeException (nameof itemKey))

        let location  = int itemKey
        let bucket    = location >>> 6
        let offset    = location &&& 0b111111
        let mask      = 1UL <<< offset
        bucket, mask

type BitSet<[<Measure>] 'Measure> (itemCount: int, buckets: uint64[]) =

    new (itemCount: int) =
        let bucketsRequired = (itemCount + 63) >>> 6
        let buckets : uint64[] = Array.zeroCreate bucketsRequired
        BitSet<_> (itemCount, buckets)
    
    // These need to be public to support inlining
    member _.ItemCount = itemCount
    member _._buckets = buckets
    
    member _.Capacity = buckets.Length * 64
    member _.Values : ReadOnlySpan<uint64> = ReadOnlySpan buckets
    
    member inline b.Count =
        let mutable total = 0
        
        for bucket in b._buckets do
            total <- total + System.Numerics.BitOperations.PopCount bucket
        
        total
    
    member b.Item
        with inline get (itemKey: int<'Measure>) =
            let bucketId, mask = Helpers.computeBucketAndMask itemKey b.ItemCount
            let buckets = b._buckets
            let bucket = buckets[bucketId]
            (bucket &&& mask) <> 0UL
    
    member b.Clear () =
        for i = 0 to b._buckets.Length - 1 do
            b._buckets[i] <- 0UL
    
    member b.Contains (itemKey: int<'Measure>) =
        let bucketId, mask = Helpers.computeBucketAndMask itemKey b.ItemCount
        let buckets = b._buckets
        let bucket = buckets[bucketId]
        (bucket &&& mask) <> 0UL
    
    member inline b.Add (itemKey: int<'Measure>) =
        let bucketId, mask = Helpers.computeBucketAndMask itemKey b.ItemCount
        let buckets = b._buckets
        let bucket = buckets[bucketId]
        buckets[bucketId] <- bucket ||| mask

    member inline b.Remove (itemKey: int<'Measure>) =
        let bucketId, mask = Helpers.computeBucketAndMask itemKey b.ItemCount
        let buckets = b._buckets
        let bucket = buckets[bucketId]
        buckets[bucketId] <- bucket &&& ~~~mask

    static member create (count: int<'Measure>) =
            BitSet<'Measure> (int count)

    static member inline iter ([<InlineIfLambda>] f: int<'Measure> -> unit) (b: BitSet<'Measure>) =
            let mutable i = 0

            // Source of algorithm: https://lemire.me/blog/2018/02/21/iterating-over-set-bits-quickly/            
            while i < b._buckets.Length do
                let mutable bitSet = b._buckets[i]
                
                while bitSet <> 0UL do
                    let r = System.Numerics.BitOperations.TrailingZeroCount bitSet
                    let itemId =
                        (i <<< 6) + r
                        |> LanguagePrimitives.Int32WithMeasure<'Measure>
                        
                    (f itemId)
                    bitSet <- bitSet ^^^ (1UL <<< r)

                i <- i + 1


[<Measure>] type Chicken

let test (b: BitSet<Chicken>) (myByRef: int<Chicken> byref) =

    b
    |> BitSet.iter (fun a -> myByRef <- myByRef + a)
