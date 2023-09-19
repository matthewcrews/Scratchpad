
open System

let test (a: _[]) =

    let s = ReadOnlySpan(a, 0, 2)
    s


let x = [|0..10|]

for z in test x do
    printfn $"{z}"

open System
open System.Collections
open System.Collections.Generic
open System.ComponentModel

[<Struct>]
type Range internal (start: int, length: int) =
    
    member _.Start = start
    member _.Length = length

    static member Zero = Range (0, 0)

    member r.ReadOnlySpanOf (a: _[]) =
        ReadOnlySpan(a, r.Start, r.Length)


[<Struct>]
type Bar<[<Measure>] 'Measure, 'T> internal (values: 'T[]) =

    static member empty = Bar<'Measure, 'T> Array.empty

    new (count, value) =
        let newValues = Array.create count value
        Bar<_,_> newValues

    /// WARNING: This member is public for the purposes of inlining but
    /// it is not meant for public consumption. This is a limitation of
    /// the F# Compiler.
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member _._values = values

    member inline b.Length = LanguagePrimitives.Int32WithMeasure<'Measure> b._values.Length

    member b.Item
        with inline get(i: int<'Measure>) =
            b._values[int i]

    interface IEnumerable<KeyValuePair<int<'Measure>, 'T>> with
            member b.GetEnumerator () : IEnumerator<KeyValuePair<int<'Measure>, 'T>> =
                let values = values
                let x =
                    0
                    |> Seq.unfold (fun i ->
                        if i < values.Length then
                            let index = LanguagePrimitives.Int32WithMeasure<'Measure> i
                            let next = KeyValuePair (index, values[i])
                            Some (next, i + 1)
                        else
                            None )

                x.GetEnumerator ()

            member b.GetEnumerator () : IEnumerator =
                (b :> IEnumerable<_>).GetEnumerator() :> IEnumerator


[<Struct>]
type Row<[<Measure>] 'Measure, 'T> internal (values: 'T[]) =

    new (length: int, value: 'T) =
        Row (Array.create (int length) value)


    new (other: Row<'Measure, 'T>) =
        let newValues = other._values
        Row<'Measure, _> newValues

    /// WARNING: This member is public for the purposes of inlining but
    /// it is not meant for public consumption. This is a limitation of
    /// the F# Compiler.
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member _._values : 'T[] = values


    member r.Item
        with inline get (i: int<'Measure>) =
            r._values[int i]

        and inline set (index: int<'Measure>) value =
            r._values[int index] <- value


    member inline r.Length = LanguagePrimitives.Int32WithMeasure<'Measure> r._values.Length


    member _.Bar = Bar<'Measure,_> values 


[<Struct>]
type BarGroup<[<Measure>] 'Measure, 'Value> internal (ranges: Bar<'Measure, Range>, values: 'Value[]) =

    static member create(values: #seq<int<'Measure> * #seq<'Value>>) =
        if Seq.isEmpty values then
            BarGroup<'Measure, 'Value> (Bar<'Measure, Range>.empty, Array.empty)

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


    /// WARNING: This member is public for the purposes of inlining but it is not meant for public consumption.
    /// This is a limitation of the F# Compiler.
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member _._values = values
    /// WARNING: This member is public for the purposes of inlining but it is not meant for public consumption.
    /// This is a limitation of the F# Compiler.
    [<EditorBrowsable(EditorBrowsableState.Never)>]
    member _._ranges = ranges

    member bg.Item
        with inline get(i: int<'Measure>) =
            let ranges = bg._ranges
            let range = ranges[i]
            ReadOnlySpan(bg._values, range.Start, range.Length)


[<Measure>]
type Chicken

let test2 () =

    let c = [|
        1<Chicken>, [1; 2]
        0<Chicken>, [3; 4; 5]
        2<Chicken>, [6]
        3<Chicken>, []
    |]

    let cBg = BarGroup.create c

    for x in cBg[0<Chicken>] do
        printfn $"0: {x}"
    
    for x in cBg[1<Chicken>] do
        printfn $"1: {x}"

    for x in cBg[2<Chicken>] do
        printfn $"2: {x}"

    for x in cBg[3<Chicken>] do
        printfn $"3: {x}"
    


test2()

[<Measure>]
type Turkey

let t = [|
    1<Turkey>, [1; 2]
    0<Turkey>, [3; 4; 5]
    2<Turkey>, [6]
    3<Turkey>, []
|]

let tBg = BarGroup.create t

