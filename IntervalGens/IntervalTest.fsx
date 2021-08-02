open System.Collections.Generic

type IndexInterval = {
    Min : int
    Max : int
} 

module IndexInterval =

    let overlaps (i1: seq<IndexInterval>) (i2: seq<IndexInterval>) =

        let rec loop (s1: IEnumerator<IndexInterval>, s1HasValue: bool, s2: IEnumerator<IndexInterval>, s2HasValue: bool) =

            if s1HasValue && s2HasValue then

                if s1.Current.Max < s2.Current.Min then
                    loop (s1, s1.MoveNext (), s2, s2HasValue)
                elif s2.Current.Max < s1.Current.Min then
                    loop (s1, s1HasValue, s2, s2.MoveNext ())
                else
                    let nextInterval = {
                            Min = System.Math.Max (s1.Current.Min, s2.Current.Min)
                            Max = System.Math.Min (s1.Current.Max, s2.Current.Max)
                        }

                    if s1.Current.Max > s2.Current.Max then
                        Some (nextInterval, (s1, s1HasValue, s2, s2.MoveNext ()))
                    else
                        Some (nextInterval, (s1, s1.MoveNext (), s2, s2HasValue))

            else
                None

        let s1 = i1.GetEnumerator ()
        let s2 = i2.GetEnumerator ()

        let state = (s1, s1.MoveNext (), s2, s2.MoveNext ())
        Seq.unfold loop state


module Indices =

    let filter (intervals: seq<IndexInterval>) (indices: seq<int>) =

        let rec loop (interval: IEnumerator<IndexInterval>, intervalHasValue, index: IEnumerator<int>, indexHasValue) =

            if intervalHasValue && indexHasValue then

                if index.Current > interval.Current.Max then
                    loop (interval, interval.MoveNext (), index, indexHasValue)
                elif interval.Current.Min > index.Current then
                    loop (interval, intervalHasValue, index, index.MoveNext ())
                else
                    let nextIndex = index.Current
                    Some (nextIndex, (interval, intervalHasValue, index, index.MoveNext ()))

            else
                None

        let intervalEnum = intervals.GetEnumerator ()
        let indexEnum = indices.GetEnumerator ()

        (intervalEnum, intervalEnum.MoveNext (), indexEnum, indexEnum.MoveNext ())
        |> Seq.unfold loop


let seq1 = seq { 
    for x in 0..10..50 ->
        {
            Min = x
            Max = x + 5
        }
}

let seq2 = seq { 
    for x in 0..10..50 ->
        {
            Min = x + 1
            Max = x + 2
        }
}

let seq3 = seq { 
    for x in 0..20..50 ->
        {
            Min = x
            Max = x + 5
        }
}

let indexFilters = 
    IndexInterval.overlaps seq1 seq2
    |> IndexInterval.overlaps seq3

// let i3 = overlaps seq1 seq2 |> Seq.toArray
let indices = seq { for idx in 0 .. 2 .. 100 -> idx }

let resultIndices =
    Indices.filter indexFilters indices
    |> Seq.toArray

// for i in overlaps seq1 seq2 do
//     printfn "%A" i