[<Struct>]
type Interval = {
    Min : int
    Max : int
}

[<Struct>]
type IntervalRow = {
    Interval : Interval
    NextIntervalRowIdx : int
}

[<Struct>]
type KeyIndex<'Key> = {
    Keys : 'Key[]
    Intervals : IntervalRow[]
}


type IIndexIterator =
    abstract member MoveNext : unit -> bool
    abstract member JumpTo : unit -> bool
    // abstract member HasValue : bool
    abstract member Index : int

type IIntervalIterator =
    abstract member MoveNext : unit -> bool
    // abstract member JumpTo : unit -> bool
    // abstract member HasValue : bool
    abstract member Interval : Interval
    // abstract member Reset : unit -> unit

// type IIndexRow =
//     abstract member Interval : Interval
//     abstract member NextRowIndex : int

// type IndexRow<'Key> = {
//     Key : 'Key
//     Interval : Interval
//     NextRowIndex : int
// } with
//     interface IIndexRow with
//         member x.Interval = x.Interval
//         member x.NextRowIndex = x.NextRowIndex

// TODO: Define how these can be merged to compute overlapping intervals. Likely need more types :)
type IndexIntervalIterator (firstIntervalIdx: int, rows: IIndexRow[]) =
    
    let rows = rows
    let mutable currentInterval = rows.[firstIntervalIdx]

    interface IIntervalIterator with

        member _.MoveNext () =
            if currentInterval.NextRowIndex > 0 then
                currentInterval <- rows.[currentInterval.NextRowIndex]
                true
            else
                false

        member _.Interval = currentInterval.Interval

    
type IndexIteratorForIntervals (firstIntervalIdx: int, rows: IIndexRow[]) =

    let rows = rows
    let mutable currentIndexRow = rows.[firstIntervalIdx]
    let mutable currentIdx = currentIndexRow.Interval.Min

    let moveToNextInterval () =
        if currentIndexRow.NextRowIndex > 0 then
            currentIndexRow <- rows.[currentIndexRow.NextRowIndex]
            true
        else
            false

    interface IIndexIterator with

        member _.Index = currentIdx

        member _.MoveNext () =

            if currentIdx < currentIndexRow.Interval.Max then
                currentIdx <- currentIdx + 1
                true
            else 
                if moveToNextInterval () then
                    currentIdx <- currentIndexRow.Interval.Min
                    true
                else
                    false

type IndexIteratorForRange (minIdx: int, maxIdx: int) =

    let mutable currentIdx = minIdx
    let maxIdx = maxIdx

    interface IIndexIterator with

        member _.Index = currentIdx

        member _.MoveNext () =
            if currentIdx < maxIdx then
                currentIdx <- currentIdx
                true
            else
                false


// type IndexIterator (firstIntervalIdx: int, rows: IIndexRow[]) =

//     let rows = rows
//     let firstIntervalIdx = firstIntervalIdx
//     let mutable currentInterval = rows.[firstIntervalIdx]
//     let mutable currentReturnIdx = currentInterval.Min

//     let moveToNextInterval () =
//         if currentInterval.NextRowIndex > 0 then
//             currentInterval <- rows.[currentInterval.NextRowIndex]
//             true
//         else
//             false

//     member _.MoveNext () =
//         if currentReturnIdx < currentInterval.Max then
//             currentReturnIdx <- currentReturnIdx + 1
//         else
//             if moveToNextInterval () then
//                 currentReturnIdx <- currentInterval.Min
//             else
//                 currentReturnIdx <- -1

//     member _.HasValue = currentReturnIdx > 0

//     member _.Index = currentReturnIdx



type IKeyIterator =

type Iterator<'Value> (values: 'Value[]) =

    let values = values
    let mutable idx = 0

    member _.MoveNext () =
        idx <- idx + 1

    member _.Value =
        values.[idx]

    member _.HasValue =
        idx < values.Length



let values = [|0 .. 10|]
let iter = Iterator values

while iter.HasValue do

    printfn "%A" iter.Value

    iter.MoveNext ()