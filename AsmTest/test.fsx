module rec Test =

    open System
    open System.Collections.Generic

    type DecisionType =
    | Boolean
    | Integer of LowerBound:float * UpperBound:float
    | Continuous of LowerBound:float * UpperBound:float

    type DecisionName = DecisionName of string

    [<Struct>]
    type Decision = {
        Name : DecisionName
        Type : DecisionType
    } with

        static member ( + ) (l: float, r: Decision) =
            LinearExpr.Add (LinearExpr.Float l, LinearExpr.Decision r)

        static member ( + ) (l: Decision, r: Decision) =
            LinearExpr.Add (LinearExpr.Decision l, LinearExpr.Decision r)

        static member ( * ) (l: float, r: Decision) =
            LinearExpr.Scale (l, LinearExpr.Decision r)

        static member ( * ) (l: Decision, r: float) =
            LinearExpr.Scale (r, LinearExpr.Decision l)


    [<RequireQualifiedAccess>]
    type LinearExpr =
        | Float of float
        | Decision of Decision
        | Scale of scale: float * expr: LinearExpr
        | Add of lExpr: LinearExpr * rExpr: LinearExpr

        static member ( + ) (l: float, r: LinearExpr) =
            LinearExpr.Add (LinearExpr.Float l, r)

        static member ( + ) (l: Decision, r: LinearExpr) =
            LinearExpr.Add (LinearExpr.Decision l, r)

        static member ( + ) (l: LinearExpr, r: LinearExpr) =
            LinearExpr.Add (l, r)

        static member ( * ) (l: float, r: LinearExpr) =
            LinearExpr.Scale (l, r)

        static member ( * ) (l: LinearExpr, r: float) =
            LinearExpr.Scale (r, l)

        static member Zero = LinearExpr.Float 0.0   

    let hadamardProduct (l: SliceMap<int, float>, r: SliceMap<int, Decision>) =
        let lKeys = l.Keys.Span
        let lValues = l.Values.Span
        let rKeys = r.Keys.Span
        let rValues = r.Values.Span
        let outKeys = Array.zeroCreate l.Keys.Length
        let outValues = Array.zeroCreate r.Keys.Length

        let mutable outIdx = 0
        let mutable lIdx = 0
        let mutable rIdx = 0

        while lIdx < lKeys.Length && rIdx < rKeys.Length do
            let lKey = lKeys.[lIdx]
            let lValue = lValues.[lIdx]
            let rKey = rKeys.[rIdx]
            let rValue = rValues.[rIdx]

            let c = l.Comparer.Compare (lKey, rKey)

            if c = 0 then
                outKeys.[outIdx] <- lKey
                outValues.[outIdx] <- lValue * rValue
                outIdx <- outIdx + 1
                lIdx <- lIdx + 1
                rIdx <- rIdx + 1
            elif c < 0 then
                lIdx <- lIdx + 1
            else
                rIdx <- rIdx + 1

        SliceMap (l.Comparer, ReadOnlyMemory (outKeys, 0, outIdx), ReadOnlyMemory (outValues, 0, outIdx))

    type SliceMap<'k, 'v when 'k : comparison> (comparer: IComparer<'k>, keys: ReadOnlyMemory<'k>, values: ReadOnlyMemory<'v>) =

        let comparer = comparer
        let keys = keys
        let values = values

        new (keyValuePairs: seq<'k * 'v>) =
            let data =
                let x = Array.ofSeq keyValuePairs
                Array.sortInPlaceBy fst x
                x

            let keys = data |> Array.map fst
            let values = data |> Array.map snd
            let comparer = LanguagePrimitives.FastGenericComparer<'k>
            SliceMap (comparer, ReadOnlyMemory keys, ReadOnlyMemory values)

        member _.Keys : ReadOnlyMemory<'k> = keys
        member _.Values : ReadOnlyMemory<'v> = values
        member _.Comparer : IComparer<'k> = comparer

        // static member inline ( .* ) (l: SliceMap<_,_>, r: SliceMap<_,_>) =
        //     if l.Keys.Length > r.Keys.Length then
        //         hadamardProduct (l, r)
        //     else
        //         hadamardProduct (r, l)


// open Test

// let sm1 = SliceMap [for i in 1 .. 10 -> i, float i]
// let sm2  = SliceMap [for i in 1 .. 10 -> i, float i]
// let x = sm1 .* sm2