open System.ComponentModel
open System

type Chicken =
    {
        Name: string
        Size: float
        Age: int
    }

let equalsTest (a: Chicken, b: Chicken) =
    a = b


[<Measure>]
type CowId

[<Measure>]
type TurkeyId

[<Measure>]
type ChickenId

type ChickenData =
    {
        Name: string
        Age: int
        Size: float
    }

let equalsTestUoM (a: int<ChickenId>, b: int<ChickenId>) =
    a = b


type Turkey =
    {
        Name: string
        Size: float
        Age: int
        IsActive: bool
    }

type Goose =
    {
        Name: string
    }

type Animal =
    | Chicken of Chicken
    | Turkey of Turkey
    | Goose of Goose

let add (a, b) =
    a + b

[<Struct>]
type Bar<[<Measure>] 'Measure, 'T> (values: 'T[]) =

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member _._values = values

    member inline b.Length = LanguagePrimitives.Int32WithMeasure<'Measure> b._values.Length

    member b.Item
        with inline get(i: int<'Measure>) =
            b._values[int i]

[<Struct>]
type Row<[<Measure>] 'Measure, 'T>(values: 'T[]) =

    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member _._values : 'T[] = values

    member r.Item
        with inline get (i: int<'Measure>) =
            r._values[int i]

        and inline set (index: int<'Measure>) value =
            r._values[int index] <- value

    member inline r.Length = LanguagePrimitives.Int32WithMeasure<'Measure> r._values.Length


// [<Measure>]
// type Chicken


let UnitsExample () =

    let chickenAges = Bar<ChickenId, _> [|0; 1; 5; 0; 2|]
    let chickenIndex = 1<ChickenId>
    // The Compiler enforces the correct units on the indexing Int
    let chickenAge = chickenAges[chickenIndex]

    // Non-Chicken Index
    let cowIndex = 1<CowId>
    // This a compiler error since the units of the indexing value
    // does not match the expectation of the collection
    let cowAge = chickenAges[cowIndex]

    ()

type Flock =
    {
        Names: Bar<ChickenId, string>
        Ages: Row<ChickenId, int>
        Sizes: Row<ChickenId, float>
    }

let myFlock = {
    Names = Bar [| "Cluck1"; "Cluck2"; "Cluck3"|]
    Ages = Row [|1; 2; 3|]
    Sizes = Row [|1.0; 2.0; 3.0|]
}

let flock  = [|
    {
        Name = "Cluck1"
        Age = 1
        Size = 1.0
    }
    {
        Name = "Cluck2"
        Age = 2
        Size = 2.0
    }
    {
        Name = "Cluck3"
        Age = 3
        Size = 3.0
    }
|]


module private Helpers =

    let inline computeBucketAndMask (itemKey: int<'Item>) itemCount =
        let location  = int itemKey
        let bucket    = location >>> 6
        let offset    = location &&& 0x3F
        let mask      = 1UL <<< offset
        bucket, mask

type BitSet<[<Measure>] 'Measure> internal (capacity: int, buckets: uint64[]) =

    new (itemCount: int) =
        let bucketsRequired = (itemCount + 63) >>> 6
        let buckets : uint64[] = Array.zeroCreate bucketsRequired
        BitSet<_> (itemCount, buckets)

    /// WARNING: Not intended for consumption. This needs to be public to support inlining.
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member _._itemCount = capacity
    /// WARNING: Not intended for consumption. This needs to be public to support inlining.
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member _._buckets = buckets

    member b.Item
        with inline get (itemKey: int<'Measure>) =
            let bucketId, mask = Helpers.computeBucketAndMask itemKey b._itemCount
            let buckets = b._buckets
            let bucket = buckets[bucketId]
            (bucket &&& mask) <> 0UL

    member b.Contains (itemKey: int<'Measure>) =
        let bucketId, mask = Helpers.computeBucketAndMask itemKey b._itemCount
        let buckets = b._buckets
        let bucket = buckets[bucketId]
        (bucket &&& mask) <> 0UL

    member inline b.Add (itemKey: int<'Measure>) =
        let bucketId, mask = Helpers.computeBucketAndMask itemKey b._itemCount
        let buckets = b._buckets
        let bucket = buckets[bucketId]
        buckets[bucketId] <- bucket ||| mask

    member inline b.Remove (itemKey: int<'Measure>) =
        let bucketId, mask = Helpers.computeBucketAndMask itemKey b._itemCount
        let buckets = b._buckets
        let bucket = buckets[bucketId]
        buckets[bucketId] <- bucket &&& ~~~mask


[<Measure>]
type Constraint

// Create a BitSet to store whether a Constraint is currently Up
let constraintUp = BitSet<Constraint> 100
// Create a value for indexing into the BitSet
let constraintIndex = 10<Constraint>
// Check whether the Constraint is Up
let constraintIsUp = constraintUp[constraintIndex]

[<NoComparison; NoEquality>]
type Rafter =
    {
        Names: Bar<TurkeyId, string>
        Ages: Row<TurkeyId, int>
        Sizes: Row<TurkeyId, float>
        IsActive: BitSet<TurkeyId>
    }

// BarGroup

[<Measure>]
type FarmId

[<Struct>]
type Range internal (start: int, length: int) =

    member _.Start = start
    member _.Length = length

    static member Zero = Range (0, 0)

    member r.ReadOnlySpanOf (a: _[]) =
        ReadOnlySpan(a, r.Start, r.Length)

[<Struct>]
type BarGroup<[<Measure>] 'Measure, 'Value> internal (ranges: Bar<'Measure, Range>, values: 'Value[]) =

    /// WARNING: This member is public for the purposes of inlining but it is not meant for public consumption.
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member _._values = values
    /// WARNING: This member is public for the purposes of inlining but it is not meant for public consumption.
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member _._ranges = ranges

    member bg.Item
        with inline get(i: int<'Measure>) =
            let ranges = bg._ranges
            if i < ranges.Length then
                let range = ranges[i]
                ReadOnlySpan(bg._values, range.Start, range.Length)

            else
                ReadOnlySpan()


module BarGroup =

    let create (values: #seq<int<'Measure> * #seq<'Value>>) =
        if Seq.isEmpty values then
            BarGroup<'Measure, 'Value> (Bar<'Measure, Range> Array.empty, Array.empty)

        else
            let maxKeyValue =
                values
                |> Seq.maxBy fst
                |> fst

            let valueCount =
                values
                |> Seq.sumBy (snd >> Seq.length)

            let mutable newRanges = Row (1 + int maxKeyValue, Range.Zero)
            let newValues = Array.zeroCreate valueCount
            let mutable startIdx = 0

            for key, group in values do
                let mutable length = 0

                for value in group do
                    newValues[startIdx + length] <- value
                    length <- length + 1

                newRanges[key] <- Range (startIdx, length)
                startIdx <- startIdx + length

            // Get a ReadOnly version
            let newRanges = newRanges.Bar

            BarGroup<'Measure, 'Value> (newRanges, newValues)


type Farm =
    {
        Name: string
        Flock: Chicken[]
    }

let cooperative =[|
    {
        Name = "Farm 1"
        Flock = [|
            {
                Name = "Clucky"
                Age = 1
                Size = 1.0
            }
            {
                Name = "Drumstick"
                Age = 2
                Size = 1.0
            }
        |]
    }
|]

type Cooperative = {
    Names: Bar<FarmId, string>
    Chickens: BarGroup<FarmId, int<ChickenId>>
    Turkeys: BarGroup<TurkeyId, int<TurkeyId>>
}


let test (a: int[]) =
    let mutable i = 0
    let mutable acc = 0
    
    while i < a.Length do
    
        acc <- acc + a[i]
        
    acc
    

let myFunction a b c =
    a + b / c


module Chicken =

    let create name age size =
        {
            Name = name
            Age = age
            Size = size
        }

let getName a =
    match a with
    | Chicken c -> c.Name
    | Turkey t -> t.Name
    | Goose g -> g.Name

let checkBounds x =
    match x with
    | a when a < 0.0 -> "Negative"

#r "nuget: FSharp.Data"
open FSharp.Data

type ChickenRow = CsvProvider<"ExampleData.csv">

let chickens =
    ChickenRow.Load("ExampleData.csv")

for row in chickens.Rows do
    printfn $"{row.Name}"


let c1 = {
    Name = "Clucky"
    Age = 1
    Size = 10.0
}

let c2 = {
    Name = "Clucky"
    Age = 1
    Size = 10.0
}

// Evaluates to TRUE
c1 = c2


module Components =

    type Flock =
        {
            Names: Bar<ChickenId, string>
            Ages: Row<ChickenId, int>
            Sizes: Row<ChickenId, float>
        }

    [<NoComparison; NoEquality>]
    type Rafter =
        {
            Names: Bar<TurkeyId, string>
            Ages: Row<TurkeyId, int>
            Sizes: Row<TurkeyId, float>
            IsActive: BitSet<TurkeyId>
        }

    type Cooperative = {
        Names: Bar<FarmId, string>
        Chickens: BarGroup<FarmId, int<ChickenId>>
        Turkeys: BarGroup<TurkeyId, int<TurkeyId>>
    }


module Chickens =

    let age
        (ages: Row<ChickenId, int>)
        (sizes: Row<ChickenId, float>)
        (timeStep: TimeSpan)
        =

        // Logic for aging Chickens


        ()