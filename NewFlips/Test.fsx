module rec Domain =

    open System.Collections.Generic
    open System.Collections.ObjectModel

    type SignInsenstiveComparer private () =
        static let instance = SignInsenstiveComparer ()
        static member Instance = instance
        interface IComparer<float> with 
            member _.Compare (a:float, b:float) =
                let aAbs = System.Math.Abs a
                let bAbs = System.Math.Abs b
                aAbs.CompareTo bAbs


    type [<Struct>] Decision =
        | Decision of string
        static member ( + ) (c: float, d: Decision) =
            LinearExpr.Add (LinearExpr.Constant c, LinearExpr.Decision d)

        static member ( + ) (d: Decision, c: float) =
            LinearExpr.Add (LinearExpr.Decision d, LinearExpr.Constant c)

        static member ( + ) (lDecision: Decision, rDecision: Decision) =
            LinearExpr.Add (LinearExpr.Decision lDecision, LinearExpr.Decision rDecision)

        static member ( * ) (c: float, d: Decision) =
            LinearExpr.Product (c, LinearExpr.Decision d)

        static member ( * ) (d: Decision, c: float) =
            LinearExpr.Product (c, LinearExpr.Decision d)


    type [<Struct>] Constraint = Constraint of string
    type [<Struct>] Objective = Objective of string
    type [<Struct>] Bounds =
        {
            Lower: float
            Upper: float
        }



    [<RequireQualifiedAccess>]
    type LinearExpr =
        | Constant of float
        | Decision of decision: Decision
        | Product of coefficient: float * expr: LinearExpr
        | Add of lExpr: LinearExpr * rExpr: LinearExpr

        static member Zero = Constant 0.0

        static member ( + ) (c: float, expr: LinearExpr) =
            match expr with
            | Constant exprConstant -> Constant (c + exprConstant)
            | _ ->
                Add (Constant c, expr)

        static member ( + ) (expr: LinearExpr, c: float) =
            c + expr

        static member ( + ) (d: Decision, expr: LinearExpr) =
            match expr with
            | Constant x when x = 0.0 ->
                Decision d
            | _ ->
                Add (Decision d, expr)

        static member ( + ) (expr: LinearExpr, d: Decision) =
            d + expr

        static member ( + ) (lExpr: LinearExpr, rExpr: LinearExpr) =
            match lExpr, rExpr with
            | Constant lConstant, _ when lConstant = 0.0 ->
                rExpr
            | _, Constant rConstant when rConstant = 0.0 ->
                lExpr
            | _, _ ->
                Add (lExpr, rExpr)

        static member ( * ) (c: float, expr: LinearExpr) =
            if c = 0.0 then
                Constant 0.0
            else
                Product (c, expr)

        static member ( * ) (expr: LinearExpr, c: float) =
            c * expr

    
    type [<Struct>] ReducedExpr =
        {
            Offset: float
            Coefficients: ReadOnlyDictionary<Decision, float>
        }
        static member ofLinearExpr (expr: LinearExpr) =

            let rec loop (multiplier: float, offsets: ResizeArray<float>, coefficients: Dictionary<Decision, ResizeArray<float>>) (expr: LinearExpr) cont =
                match expr with
                | LinearExpr.Constant c ->
                    offsets.Add (multiplier * c)
                    cont (multiplier, offsets, coefficients)

                | LinearExpr.Decision d ->
                    match coefficients.TryGetValue d with
                    | true, decisionCoefficients -> 
                        decisionCoefficients.Add multiplier
                    | false, _ ->
                        let newDecisionCoefficients = ResizeArray()
                        newDecisionCoefficients.Add multiplier
                        coefficients[d] <- newDecisionCoefficients

                    cont (multiplier, offsets, coefficients)

                | LinearExpr.Product (coefficient, expr) ->
                    loop ((coefficient * multiplier), offsets, coefficients) expr cont

                | LinearExpr.Add (lExpr, rExpr) ->
                    loop (multiplier, offsets, coefficients) lExpr (fun (_, offsets, coefficients) -> loop (multiplier, offsets, coefficients) rExpr cont)


            let offsets = ResizeArray()
            let coefficients = Dictionary()
            let _ = loop (1.0, offsets, coefficients) expr id

            let offset =
                // Sort to reduce error from float addition
                offsets.Sort SignInsenstiveComparer.Instance
                Seq.sum offsets

            let resultCoefficients = Dictionary()
            for KeyValue (decision, decisionCoefficients) in coefficients do
                // Sort to reduce error from float addition
                decisionCoefficients.Sort SignInsenstiveComparer.Instance
                let coefficient = Seq.sum decisionCoefficients
                resultCoefficients[decision] <- coefficient

            {
                Offset = offset
                Coefficients = ReadOnlyDictionary resultCoefficients
            }

open Domain

let d1 = Decision "Chicken"
let d2 = Decision "Cow"
let e = d1 + d2
let e1 = 2.0 * d1 + d2
let r1 = ReducedExpr.ofLinearExpr e1
let e2 = 2.0 * (d1 + 2.0 * d2 + 1.0) + 3.0 * (d2)
let r2 = ReducedExpr.ofLinearExpr e2

let e3 = 1.0 * d1 + 2.0 * d2

let r3 = ReducedExpr.ofLinearExpr e