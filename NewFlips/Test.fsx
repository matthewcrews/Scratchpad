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
    type Bounds =
        | Binary
        | Integer of lower: int * upper: int
        | Continuous of lower: float * upper: float
        | Unbounded

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

        static member ( - ) (lExpr: LinearExpr, rExpr: LinearExpr) =
            match lExpr, rExpr with
            | Constant lConstant, _ when lConstant = 0.0 ->
                rExpr
            | _, Constant rConstant when rConstant = 0.0 ->
                lExpr
            | _, _ ->
                Add (lExpr, Product (-1.0, rExpr))

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

    type [<Struct>] Sense =
        | Minimize
        | Maximize

    type [<Struct>] ModelName = ModelName of string

    type [<Struct>] Objective =
        {
            Sense: Sense
            Expr: LinearExpr
        }

    type [<Struct>] Model =
        {
            Name: ModelName
            Objective: Objective
            Constraints: list<struct (Constraint * Relation)>
            DecisionBounds: list<struct (Decision * Bounds)>
        }
        static member create (name, sense, expression, constraints, bounds) =
            {
                Name = name
                Objective = { Sense = sense; Expr = expression }
                Constraints = constraints
                DecisionBounds = bounds
            }

        static member create (name, sense, expression) =
            Model.create (name, sense, expression, [], [])

    module Model =

        let addConstraint (c, relation) model =
            { model with
                Constraints = struct (c, relation) :: model.Constraints }

        let addConstraints constraints model =

            let rec loop acc constraints =
                match constraints with
                | [] -> acc
                |  (c, relation)::tail ->
                    let acc = (struct (c, relation))::acc
                    loop acc tail

            let newConstraints = loop model.Constraints constraints

            { model with
                Constraints = newConstraints }

        let addBound decision bounds model =
            { model with
                DecisionBounds = struct (decision, bounds) :: model.DecisionBounds }

        let addBounds decisionBounds model =

            let rec loop acc decisionBounds =
                match decisionBounds with
                | [] -> acc
                | (decision, bounds)::tail ->
                    let acc = struct (decision, bounds)::acc
                    loop acc tail

            let newBounds = loop model.DecisionBounds decisionBounds

            { model with
                DecisionBounds = newBounds }


    module UnitsOfMeasure =

        type [<Struct>] Decision<[<Measure>] 'Measure> =
            | Value of Modeling.Decision

        type [<Struct>] LinearExpr<[<Measure>] 'Measure> =
            | Value of Modeling.LinearExpr

        type [<Struct>] Objective<[<Measure>] 'Measure> =
            | Value of Modeling.ModelName


module Reduced =

    open System.Collections.Generic
    open System.Collections.ObjectModel

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
        static member ofModeling
            (decisionBounds: Dictionary<Modeling.Decision, Modeling.Bounds>)
            (expr: Modeling.LinearExpr)
            =

            let rec loop (multiplier: float, offsets: ResizeArray<float>, coefficients: Dictionary<Modeling.Decision, ResizeArray<float>>) (expr: Modeling.LinearExpr) cont =
                match expr with
                | Modeling.LinearExpr.Constant c ->
                    offsets.Add (multiplier * c)
                    cont (multiplier, offsets, coefficients)

                | Modeling.LinearExpr.Decision d ->
                    // Check if we already have bounds for this Decision
                    // If we do not, set it as unbounded
                    if not (decisionBounds.ContainsKey d) then
                        decisionBounds[d] <- Modeling.Bounds.Unbounded

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
            Name: Modeling.Constraint
            Coefficients: ReadOnlyDictionary<Modeling.Decision, float>
            LowerBound: float
            UpperBound: float
        }

    module Constraint =

        let ofModeling
            (decisionBounds: Dictionary<Modeling.Decision, Modeling.Bounds>)
            (struct (name, relation: Modeling.Relation))
            =

            let coefficients, lowerBound, upperBound =
                match relation with
                | Modeling.Relation.Equals (lExpr, rExpr) ->
                    let newExpr = LinearExpr.ofModeling decisionBounds (lExpr - rExpr)
                    let lowerBound = -1.0 * newExpr.Offset
                    let upperBound = -1.0 * newExpr.Offset
                    newExpr.Coefficients, lowerBound, upperBound

                | Modeling.Relation.LessOrEquals (lExpr, rExpr) ->
                    let newExpr = LinearExpr.ofModeling decisionBounds (lExpr - rExpr)
                    let lowerBound = System.Double.NegativeInfinity
                    let upperBound = -1.0 * newExpr.Offset
                    newExpr.Coefficients, lowerBound, upperBound

                | Modeling.Relation.GreaterOrEquals (lExpr, rExpr) ->
                    let newExpr = LinearExpr.ofModeling decisionBounds (lExpr - rExpr)
                    let lowerBound = -1.0 * newExpr.Offset
                    let upperBound = System.Double.PositiveInfinity
                    newExpr.Coefficients, lowerBound, upperBound

            { Name = name; Coefficients = coefficients; LowerBound = lowerBound; UpperBound = upperBound }


    type [<Struct>] Sense =
        | Maximize
        | Minimize

    module Sense =

        let ofModeling (s: Modeling.Sense) =
            match s with
            | Modeling.Sense.Maximize ->
                Maximize
            | Modeling.Sense.Minimize ->
                Minimize

    type [<Struct>] Objective =
        {
            Sense: Sense
            Expr: LinearExpr
        }

    module Objective =

        let ofModeling
            (decisionBounds: Dictionary<Modeling.Decision, Modeling.Bounds>)
            (objective: Modeling.Objective)
            =
            let newExpr = LinearExpr.ofModeling decisionBounds objective.Expr
            let newSense = Sense.ofModeling objective.Sense
            { Expr = newExpr; Sense = newSense }


    type Model =
        {
            Name: string
            Objective: Objective
            Constraints: Constraint[]
            DecisionBounds: ReadOnlyDictionary<Modeling.Decision, Modeling.Bounds>
        }

    module Model =

        let ofModeling (model: Modeling.Model) =
            let constraints = Stack()
            let decisionBounds = Dictionary()

            for struct (decision, bounds) in model.DecisionBounds do
                decisionBounds[decision] <- bounds

            let newObjective = Objective.ofModeling decisionBounds model.Objective

            for entry in model.Constraints do
                let newConstraint = Constraint.ofModeling decisionBounds entry
                constraints.Push newConstraint

            let (Modeling.ModelName modelName) = model.Name

            {
                Name = modelName
                Objective = newObjective
                Constraints = constraints.ToArray()
                DecisionBounds = ReadOnlyDictionary decisionBounds
            }


#r "nuget: Google.OrTools, 9.5.2237"

module ORTools =

    open System.Collections.Generic
    open System.Collections.ObjectModel
    open Google.OrTools.LinearSolver

    type Settings =
        {
            Duration_ms: int64
            WriteLPFile: option<string>
            WriteMPSFile: option<string>
            EnableOutput: bool
        }

    type SolveResult =
        | Optimal
        | Infeasible
        | Unbounded
        | Unknown
        | Unsolved

    type Solution =
        {
            Result: SolveResult
            SolutionValues: ReadOnlyDictionary<Modeling.Decision, float>
            ReducedCosts: ReadOnlyDictionary<Modeling.Decision, float>
            ObjectiveValue: float
        }


    module private Helpers =

        let createVariables
            (solver: Solver)
            (decisionBounds: ReadOnlyDictionary<Modeling.Decision, Modeling.Bounds>) =
            let variables = Dictionary()

            for KeyValue (decision, bounds) in decisionBounds do
                let (Modeling.Decision name) = decision

                let newVar =
                    match bounds with
                    | Modeling.Bounds.Binary ->
                        solver.MakeBoolVar name
                    | Modeling.Bounds.Integer (lower, upper) ->
                        solver.MakeIntVar (float lower, float upper, name)
                    | Modeling.Bounds.Continuous (lower, upper) ->
                        solver.MakeNumVar (lower, upper, name)
                    | Modeling.Bounds.Unbounded ->
                        solver.MakeNumVar (System.Double.NegativeInfinity, System.Double.PositiveInfinity, name)

                variables[decision] <- newVar

            ReadOnlyDictionary variables


        let createExpr
            (variables: ReadOnlyDictionary<Modeling.Decision, Variable>)
            (expr: Reduced.LinearExpr)
            =

            let mutable solverExpr = LinearExpr()

            for KeyValue (decision, coefficient) in expr.Coefficients do
                solverExpr <- solverExpr + (coefficient * variables[decision])

            solverExpr + expr.Offset


        let addConstraint
            (variables: ReadOnlyDictionary<Modeling.Decision, Variable>)
            (solver: Solver)
            (reducedConstraint: Reduced.Constraint)
            =

            let (Modeling.Constraint name) = reducedConstraint.Name
            let solverConstraint = solver.MakeConstraint (reducedConstraint.LowerBound, reducedConstraint.UpperBound, name)
            for KeyValue (decision, coefficient) in reducedConstraint.Coefficients do
                solverConstraint.SetCoefficient (variables[decision], coefficient)


    open Helpers

    let solve (settings: Settings) (model: Modeling.Model) : Solution =

        let reducedModel = Reduced.Model.ofModeling model

        let solver = Solver.CreateSolver "GLOP"
        solver.SetTimeLimit settings.Duration_ms
        if settings.EnableOutput then
            solver.EnableOutput()

        let variables = createVariables solver reducedModel.DecisionBounds

        for reducedConstraint in reducedModel.Constraints do
            addConstraint variables solver reducedConstraint

        let objExpr = createExpr variables reducedModel.Objective.Expr

        match reducedModel.Objective.Sense with
        | Reduced.Sense.Maximize ->
            solver.Maximize objExpr
        | Reduced.Sense.Minimize ->
            solver.Minimize objExpr

        settings.WriteLPFile
        |> Option.iter (fun lpFile ->
            // Add Objective Name for multi-objective
            let fullFile = $"{lpFile}.lp"
            let lpString = solver.ExportModelAsLpFormat false
            System.IO.File.WriteAllText (fullFile, lpString)
            )

        let resultStatus = solver.Solve()

        let solutionValues =
            reducedModel.DecisionBounds.Keys
            |> Seq.map (fun decision ->
                let solutionValue = variables[decision].SolutionValue()
                KeyValuePair (decision, solutionValue))
            |> Dictionary
            |> ReadOnlyDictionary

        let reducedCosts =
            reducedModel.DecisionBounds.Keys
            |> Seq.map (fun decision ->
                let reducedCost = variables[decision].ReducedCost()
                KeyValuePair (decision, reducedCost))
            |> Dictionary
            |> ReadOnlyDictionary

        let solutionValue = solver.Objective().Value()

        let solveResult =
            match resultStatus with
            | Solver.ResultStatus.OPTIMAL ->
                SolveResult.Optimal

            | Solver.ResultStatus.INFEASIBLE ->
                SolveResult.Infeasible

            | Solver.ResultStatus.UNBOUNDED ->
                SolveResult.Unbounded

            | Solver.ResultStatus.ABNORMAL
            | Solver.ResultStatus.MODEL_INVALID
            | Solver.ResultStatus.FEASIBLE
            | Solver.ResultStatus.NOT_SOLVED
            | _ ->
                SolveResult.Unsolved

        {
            Result = solveResult
            ReducedCosts = reducedCosts
            SolutionValues = solutionValues
            ObjectiveValue = solutionValue
        }






//
// open Modeling
//
// let d1 = Decision "Chicken"
// let d2 = Decision "Cow"
// let e = d1 + d2
// let e1 = 2.0 * d1 + d2
// let r1 = Solver.LinearExpr.ofModeling e1
// let e2 = 2.0 * (d1 + 2.0 * d2 + 1.0) + 3.0 * (d2)
// let r2 = Solver.LinearExpr.ofModeling e2
//
// let e3 = 1.0 * d1 + 2.0 * d2
//
// let r3 = Solver.LinearExpr.ofModeling e
