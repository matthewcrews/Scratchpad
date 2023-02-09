module rec Modeling =


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


    [<RequireQualifiedAccess>]
    type Relation =
        | Equals of lExpr: LinearExpr * rExpr: LinearExpr
        | LessOrEquals of lExpr: LinearExpr * rExpr: LinearExpr
        | GreaterOrEquals of lExpr: LinearExpr * rExpr: LinearExpr

    type [<Struct>] ObjectiveSense =
        | Minimize
        | Maximize

    type [<Struct>] Objective =
        {
            Name: string
            Sense: ObjectiveSense
            Expression: LinearExpr
        }

    type [<Struct>] Model =
        {
            Name: string
            Objectives: list<Objective>
            Constraints: list<struct (Constraint * Relation)>
            Bounds: list<struct (Decision * Bounds)>
        }
        static member create (name, objectives, constraints, bounds) =
            {
                Name = name
                Objectives = objectives
                Constraints = constraints
                Bounds = bounds
            }

        static member create name =
            Model.create (name, [], [], [])

    module Model =

        let addObjective objective model =
            { model with
                Objectives = objective :: model.Objectives }

        let addConstraint c model =
            { model with
                Constraints = c :: model.Constraints }

        let addConstraints constraints model =
            { model with
                Constraints = constraints @ model.Constraints }

        let addBound bound model =
            { model with
                Bounds = bound :: model.Bounds }

        let addBounds bounds model =
            { model with
                Bounds = bounds @ model.Bounds }


    module UnitsOfMeasure =

        type [<Struct>] Decision<[<Measure>] 'Measure> =
            | Value of Modeling.Decision

        type [<Struct>] LinearExpr<[<Measure>] 'Measure> =
            | Value of Modeling.LinearExpr

        type [<Struct>] Objective<[<Measure>] 'Measure> =
            | Value of Modeling.Objective


module Solver =

    open System.Collections.Generic
    open System.Collections.ObjectModel

    type [<Struct>] Block<'T> (values: 'T[]) =

        member _.Item
            with inline get i = values[i]

        member inline _.Length = values.Length

    type block<'T> = Block<'T>


    type SignInsensitiveComparer private () =
        static let instance = SignInsensitiveComparer ()
        static member Instance = instance
        interface IComparer<float> with
            member _.Compare (a:float, b:float) =
                let aAbs = System.Math.Abs a
                let bAbs = System.Math.Abs b
                aAbs.CompareTo bAbs


    type [<Struct>] LinearExpr =
        {
            Offset: float
            Coefficients: ReadOnlyDictionary<Modeling.Decision, float>
        }
        static member ofModeling (expr: Modeling.LinearExpr) =

            let rec loop (multiplier: float, offsets: ResizeArray<float>, coefficients: Dictionary<Modeling.Decision, ResizeArray<float>>) (expr: Modeling.LinearExpr) cont =
                match expr with
                | Modeling.LinearExpr.Constant c ->
                    offsets.Add (multiplier * c)
                    cont (multiplier, offsets, coefficients)

                | Modeling.LinearExpr.Decision d ->
                    match coefficients.TryGetValue d with
                    | true, decisionCoefficients ->
                        decisionCoefficients.Add multiplier

                    | false, _ ->
                        let newDecisionCoefficients = ResizeArray()
                        newDecisionCoefficients.Add multiplier
                        coefficients[d] <- newDecisionCoefficients

                    cont (multiplier, offsets, coefficients)

                | Modeling.LinearExpr.Product (coefficient, expr) ->
                    loop ((coefficient * multiplier), offsets, coefficients) expr cont

                | Modeling.LinearExpr.Add (lExpr, rExpr) ->
                    loop (multiplier, offsets, coefficients) lExpr (fun (_, offsets, coefficients) -> loop (multiplier, offsets, coefficients) rExpr cont)


            let offsets = ResizeArray()
            let coefficients = Dictionary()
            let _ = loop (1.0, offsets, coefficients) expr id

            let offset =
                // Sort to reduce error from float addition
                offsets.Sort SignInsensitiveComparer.Instance
                Seq.sum offsets

            let resultCoefficients = Dictionary()
            for KeyValue (decision, decisionCoefficients) in coefficients do
                // Sort to reduce error from float addition
                decisionCoefficients.Sort SignInsensitiveComparer.Instance
                let coefficient = Seq.sum decisionCoefficients
                resultCoefficients[decision] <- coefficient

            {
                Offset = offset
                Coefficients = ReadOnlyDictionary resultCoefficients
            }

    [<RequireQualifiedAccess>]
    type [<Struct>] RelationType =
        | Equals
        | LessOrEquals
        | GreaterOrEquals

    type Constraint =
        {
            Name: string
            Relation: RelationType
            LeftExpr: LinearExpr
            RightExpr: LinearExpr
        }

    module Constraint =

        let ofModeling (struct (Modeling.Constraint name, relation: Modeling.Relation)) =
            let relationType, lExpr, rExpr =
                match relation with
                | Modeling.Relation.Equals (lExpr, rExpr) ->
                    let newLExpr = LinearExpr.ofModeling lExpr
                    let newRExpr = LinearExpr.ofModeling rExpr
                    RelationType.Equals, newLExpr, newRExpr

                | Modeling.Relation.LessOrEquals (lExpr, rExpr) ->
                    let newLExpr = LinearExpr.ofModeling lExpr
                    let newRExpr = LinearExpr.ofModeling rExpr
                    RelationType.LessOrEquals, newLExpr, newRExpr

                | Modeling.Relation.GreaterOrEquals (lExpr, rExpr) ->
                    let newLExpr = LinearExpr.ofModeling lExpr
                    let newRExpr = LinearExpr.ofModeling rExpr
                    RelationType.GreaterOrEquals, newLExpr, newRExpr
            { Name = name; Relation = relationType; LeftExpr = lExpr; RightExpr = rExpr }

    type [<Struct>] ObjectiveSense =
        | Maximize
        | Minimize

    module ObjectiveSense =

        let ofModeling (s: Modeling.ObjectiveSense) =
            match s with
            | Modeling.ObjectiveSense.Maximize ->
                Maximize
            | Modeling.ObjectiveSense.Minimize ->
                Minimize

    type [<Struct>] Objective =
        {
            Name: string
            Sense: ObjectiveSense
            Expr: LinearExpr
        }

    module Objective =

        let ofModeling (objective: Modeling.Objective) =
            let newExpr = LinearExpr.ofModeling objective.Expression
            let newSense = ObjectiveSense.ofModeling objective.Sense
            { Name = objective.Name; Expr = newExpr; Sense = newSense }

    type Model =
        {
            Name: string
            Objectives: block<Objective>
            Constraints: block<Constraint>
            Bounds: ReadOnlyDictionary<Modeling.Decision, Modeling.Bounds>
        }

    module Model =

        let ofModeling (model: Modeling.Model) =
            // A Stack is used here on purpose for the correct ordering of Objectives
            let objectives = Stack()
            let constraints = Stack()
            let decisionBounds = Dictionary()

            for objective in model.Objectives do
                let newObjective = Objective.ofModeling objective
                objectives.Push newObjective

            for entry in model.Constraints do
                let newConstraint = Constraint.ofModeling entry
                constraints.Push newConstraint

            for struct (decision, bounds) in model.Bounds do
                decisionBounds[decision] <- bounds

            {
                Name = model.Name
                Objectives = block (objectives.ToArray())
                Constraints = block (constraints.ToArray())
                Bounds = ReadOnlyDictionary decisionBounds
            }


#r "nuget: Google.OrTools, 9.5.2237"



open Modeling

let d1 = Decision "Chicken"
let d2 = Decision "Cow"
let e = d1 + d2
let e1 = 2.0 * d1 + d2
let r1 = Solver.LinearExpr.ofModeling e1
let e2 = 2.0 * (d1 + 2.0 * d2 + 1.0) + 3.0 * (d2)
let r2 = Solver.LinearExpr.ofModeling e2

let e3 = 1.0 * d1 + 2.0 * d2

let r3 = Solver.LinearExpr.ofModeling e
