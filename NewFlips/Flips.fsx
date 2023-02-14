module rec Modeling =

    open System.Collections.Generic

    type [<Struct>] Decision =
        | Decision of string
        static member ( + ) (f: float, d: Decision) : LinearExpr =
            f + (LinearExpr.Decision d)

        static member ( + ) (d: Decision, f: float) : LinearExpr =
            d + (LinearExpr.Constant f)

        static member ( + ) (lDecision: Decision, rDecision: Decision) : LinearExpr =
            lDecision + (LinearExpr.Decision rDecision)

        static member ( * ) (f: float, d: Decision) : LinearExpr =
            f * (LinearExpr.Decision d)

        static member ( * ) (d: Decision, f: float) : LinearExpr =
            f * d

        // Comparisons
        static member ( <== ) (f: float, d: Decision) : Relation =
            f <== (LinearExpr.Decision d)

        static member ( <== ) (d: Decision, f: float) : Relation =
            d <== (LinearExpr.Constant f)

        static member ( <== ) (lDecision: Decision, rDecision: Decision) : Relation =
            lDecision <== (LinearExpr.Decision rDecision)

        static member ( == ) (f: float, d: Decision) : Relation =
            f == (LinearExpr.Decision d)

        static member ( == ) (d: Decision, f: float) : Relation =
            d == (LinearExpr.Constant f)

        static member ( == ) (lDecision: Decision, rDecision: Decision) : Relation =
            lDecision == (LinearExpr.Decision rDecision)

        static member ( >== ) (f: float, d: Decision) : Relation =
            f >== (LinearExpr.Decision d)

        static member ( >== ) (d: Decision, f: float) : Relation =
            d >== (LinearExpr.Constant f)

        static member ( >== ) (lDecision: Decision, rDecision: Decision) : Relation =
            lDecision >== (LinearExpr.Decision rDecision)


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

        static member ( + ) (c: float, expr: LinearExpr) : LinearExpr =
            match expr with
            | Constant exprConstant -> Constant (c + exprConstant)
            | _ ->
                Add (Constant c, expr)

        static member ( + ) (expr: LinearExpr, c: float) : LinearExpr =
            c + expr

        static member ( + ) (d: Decision, expr: LinearExpr) : LinearExpr =
            match expr with
            | Constant x when x = 0.0 ->
                Decision d
            | _ ->
                Add (Decision d, expr)

        static member ( + ) (expr: LinearExpr, d: Decision) : LinearExpr =
            d + expr

        static member ( + ) (lExpr: LinearExpr, rExpr: LinearExpr) : LinearExpr =
            match lExpr, rExpr with
            | Constant lConstant, _ when lConstant = 0.0 ->
                rExpr
            | _, Constant rConstant when rConstant = 0.0 ->
                lExpr
            | _, _ ->
                Add (lExpr, rExpr)

        static member ( - ) (lExpr: LinearExpr, rExpr: LinearExpr) : LinearExpr =
            match lExpr, rExpr with
            | Constant lConstant, _ when lConstant = 0.0 ->
                rExpr
            | _, Constant rConstant when rConstant = 0.0 ->
                lExpr
            | _, _ ->
                Add (lExpr, Product (-1.0, rExpr))

        static member ( * ) (f: float, expr: LinearExpr) : LinearExpr =
            if f = 0.0 then
                Constant 0.0
            else
                Product (f, expr)

        static member ( * ) (expr: LinearExpr, f: float) : LinearExpr =
            f * expr

        // Comparisons
        // Less Or Equals
        static member ( <== ) (lExpr: LinearExpr, rExpr: LinearExpr) : Relation =
            Relation.LessOrEquals (lExpr, rExpr)

        static member ( <== ) (f: float, expr: LinearExpr) : Relation =
            (Constant f) <== expr

        static member ( <== ) (expr: LinearExpr, f: float) : Relation =
            expr <== (Constant f)

        static member ( <== ) (d: Decision, expr: LinearExpr) : Relation =
            (Decision d) <== expr

        static member ( <== ) (expr: LinearExpr, d: Decision) : Relation =
            expr <== (Decision d)

        // Equals
        static member ( == ) (lExpr: LinearExpr, rExpr: LinearExpr) : Relation =
            Relation.Equals (lExpr, rExpr)

        static member ( == ) (f: float, expr: LinearExpr) : Relation =
            (Constant f) == expr

        static member ( == ) (expr: LinearExpr, f: float) : Relation =
            expr == (Constant f)

        static member ( == ) (d: Decision, expr: LinearExpr) : Relation =
            (Decision d) == expr

        static member ( == ) (expr: LinearExpr, d: Decision) : Relation =
            expr == (Decision d)

        // Greater or Equals
        static member ( >== ) (lExpr: LinearExpr, rExpr: LinearExpr) : Relation =
            Relation.GreaterOrEquals (lExpr, rExpr)

        static member ( >== ) (f: float, expr: LinearExpr) : Relation =
            (Constant f) >== expr

        static member ( >== ) (expr: LinearExpr, f: float) : Relation =
            expr >== (Constant f)

        static member ( >== ) (d: Decision, expr: LinearExpr) : Relation =
            (Decision d) >== expr

        static member ( >== ) (expr: LinearExpr, d: Decision) : Relation =
            expr >== (Decision d)


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

    module Model =

        let create (name, sense, expr) : Model =
            {
                Name = ModelName name
                Objective = { Sense = sense; Expr = expr }
                Constraints = []
                DecisionBounds = []
            }

        let addConstraint (c, relation) model : Model =
            { model with
                Constraints = struct (Constraint c, relation) :: model.Constraints }

        let addConstraints constraints model : Model =

            let rec loop acc constraints =
                match constraints with
                | [] -> acc
                |  (c, relation)::tail ->
                    let acc = (struct (Constraint c, relation))::acc
                    loop acc tail

            let newConstraints = loop model.Constraints constraints

            { model with
                Constraints = newConstraints }

        let addBound (decision, bounds) model : Model =
            { model with
                DecisionBounds = struct (decision, bounds) :: model.DecisionBounds }

        let addBounds decisionBounds model : Model =

            let rec loop acc decisionBounds =
                match decisionBounds with
                | [] -> acc
                | (decision, bounds)::tail ->
                    let acc = struct (decision, bounds)::acc
                    loop acc tail

            let newBounds = loop model.DecisionBounds decisionBounds

            { model with
                DecisionBounds = newBounds }


    type SumBuilder () =

        member _.Zero () = LinearExpr.Zero
        member _.Delay (expr: unit -> LinearExpr) = expr
        member _.Run (expr: unit -> LinearExpr) = expr ()
        member _.Combine (a: LinearExpr, b: unit -> LinearExpr) = a + b ()

        member _.Yield (a: float) = a
        member _.Yield (a: Decision) = a
        member _.Yield (a: LinearExpr) = a

        member _.Yield (a: seq<float>) =
            let mutable acc = LinearExpr.Zero
            for x in a do
                acc <- acc + x
            acc
        
        member _.Yield (a: seq<Decision>) =
            let mutable acc = LinearExpr.Zero
            for x in a do
                acc <- acc + x
            acc

        member _.Yield (a: seq<LinearExpr>) =
            let mutable acc = LinearExpr.Zero
            for x in a do
                acc <- acc + x
            acc

        member _.Yield (a: seq<KeyValuePair<_, float>>) =
            let mutable acc = LinearExpr.Zero
            for KeyValue (_, x) in a do
                acc <- acc + x
            acc

        member _.Yield (a: seq<KeyValuePair<_, Decision>>) =
            let mutable acc = LinearExpr.Zero
            for KeyValue (_, x) in a do
                acc <- acc + x
            acc

        member _.Yield (a: seq<KeyValuePair<_, LinearExpr>>) =
            let mutable acc = LinearExpr.Zero
            for KeyValue (_, x) in a do
                acc <- acc + x
            acc

        member _.For (a: seq<'a>, body: 'a -> float) =
            let mutable acc = 0.0
            for x in a do
                let result = body x
                acc <- acc + result
            LinearExpr.Constant acc

        member _.For (a: seq<'a>, body: 'a -> Decision) =
            let mutable acc = LinearExpr.Zero
            for x in a do
                let result = body x
                acc <- acc + result
            acc

        member _.For (a: seq<'a>, body: 'a -> LinearExpr) =
            let mutable acc = LinearExpr.Zero
            for x in a do
                let result = body x
                acc <- acc + result
            acc


    module UnitsOfMeasure =

        type Bounds<[<Measure>] 'Measure> =
            | Binary
            | Integer of lower: int<'Measure> * upper: int<'Measure>
            | Continuous of lower: float<'Measure> * upper: float<'Measure>
            | Unbounded

        [<Struct>]
        type Decision<[<Measure>] 'Measure> =
            val internal Value : Modeling.Decision
            new (name: string) =
                { Value = Modeling.Decision name }

            static member ( + ) (f: float<'Measure>, d: Decision<'Measure>) : LinearExpr<'Measure> =
                let newExpr = (float f) + d.Value
                LinearExpr<'Measure> newExpr

            static member ( + ) (d: Decision<'Measure>, f: float<'Measure>) : LinearExpr<'Measure> =
                let newExpr = (LinearExpr.Decision d.Value) + (LinearExpr.Constant (float f))
                LinearExpr<'Measure> newExpr

            static member ( + ) (lDecision: Decision<'Measure> , rDecision: Decision<'Measure>) : LinearExpr<'Measure> =
                let newExpr = lDecision.Value + rDecision.Value
                LinearExpr<'Measure> newExpr

            static member ( * ) (f: float<'LMeasure>, d: Decision<'RMeasure>) =
                let newExpr = (float f) * d.Value
                LinearExpr<'LMeasure 'RMeasure> newExpr

            static member ( * ) (d: Decision<'RMeasure>, f: float<'LMeasure>) =
                let newExpr = d.Value * (float f)
                LinearExpr<'LMeasure 'RMeasure> newExpr

            // Comparisons
            // Less or Equals
            static member ( <== ) (f: float<'Measure>, d: Decision<'Measure>) : Relation =
                (LinearExpr.Constant (float f)) <== (LinearExpr.Decision d.Value)

            static member ( <== ) (d: Decision<'Measure>, f: float<'Measure>) : Relation =
                (LinearExpr.Decision d.Value) <== (LinearExpr.Constant (float f))

            static member ( <== ) (lDecision: Decision<'Measure>, rDecision: Decision<'Measure>) : Relation =
                (LinearExpr.Decision lDecision.Value) <== (LinearExpr.Decision rDecision.Value)

            // Equals
            static member ( == ) (f: float<'Measure>, d: Decision<'Measure>) : Relation =
                (LinearExpr.Constant (float f)) == (LinearExpr.Decision d.Value)

            static member ( == ) (d: Decision<'Measure>, f: float<'Measure>) : Relation =
                (LinearExpr.Decision d.Value) == (LinearExpr.Constant (float f))

            static member ( == ) (lDecision: Decision<'Measure>, rDecision: Decision<'Measure>) : Relation =
                (LinearExpr.Decision lDecision.Value) == (LinearExpr.Decision rDecision.Value)

            // Greater or Equals
            static member ( >== ) (f: float<'Measure>, d: Decision<'Measure>) : Relation =
                (LinearExpr.Constant (float f)) >== (LinearExpr.Decision d.Value)

            static member ( >== ) (d: Decision<'Measure>, f: float<'Measure>) : Relation =
                (LinearExpr.Decision d.Value) >== (LinearExpr.Constant (float f))

            static member ( >== ) (lDecision: Decision<'Measure>, rDecision: Decision<'Measure>) : Relation =
                (LinearExpr.Decision lDecision.Value) >== (LinearExpr.Decision rDecision.Value)
                

        [<Struct>] 
        type LinearExpr<[<Measure>] 'Measure> =
            val internal Value : Modeling.LinearExpr
            new (linearExpr: Modeling.LinearExpr) =
                { Value = linearExpr }

            static member Zero : LinearExpr<'Measure> = LinearExpr<'Measure> (LinearExpr.Zero)

            static member ( + ) (f: float<'Measure>, expr: LinearExpr<'Measure>) : LinearExpr<'Measure> =
                let newExpr = (float f) + expr.Value
                LinearExpr<'Measure> newExpr

            static member ( + ) (expr: LinearExpr<'Measure>, f: float<'Measure>) : LinearExpr<'Measure> =
                let newExpr = expr.Value + (float f)
                LinearExpr<'Measure> newExpr

            static member ( + ) (d: Decision<'Measure>, expr: LinearExpr<'Measure>) : LinearExpr<'Measure> =
                let newExpr = d.Value + expr.Value
                LinearExpr<'Measure> newExpr

            static member ( + ) (expr: LinearExpr<'Measure>, d: Decision<'Measure>) : LinearExpr<'Measure> =
                let newExpr = expr.Value + d.Value
                LinearExpr<'Measure> newExpr

            static member ( + ) (lExpr: LinearExpr<'Measure> , rExpr: LinearExpr<'Measure>) : LinearExpr<'Measure> =
                let newExpr = lExpr.Value + rExpr.Value
                LinearExpr<'Measure> newExpr

            static member ( * ) (f: float<'M1>, expr: LinearExpr<'M2>) =
                let newExpr = LinearExpr.Product ((float f), expr.Value)
                LinearExpr<'LMeasure 'RMeasure> newExpr

            static member ( * ) (expr: LinearExpr<'LMeasure>, f: float<'RMeasure>) =
                let newExpr = LinearExpr.Product ((float f), expr.Value)
                LinearExpr<'LMeasure 'RMeasure> newExpr

            // Comparisons
            // Less or Equals
            static member ( <== ) (f: float<'Measure>, expr: LinearExpr<'Measure>) : Relation =
                (float f) <== expr.Value

            static member ( <== ) (expr: LinearExpr<'Measure>, f: float<'Measure>) : Relation =
                expr.Value <== (float f)

            static member ( <== ) (d: Decision<'Measure>, expr: LinearExpr<'Measure>) : Relation =
                d.Value <== expr.Value

            static member ( <== ) (expr: LinearExpr<'Measure>, d: Decision<'Measure>) : Relation =
                expr.Value <== d.Value

            static member ( <== ) (lExpr: LinearExpr<'Measure>, rExpr: LinearExpr<'Measure>) : Relation =
                lExpr.Value <== rExpr.Value

            // Equals
            static member ( == ) (f: float<'Measure>, expr: LinearExpr<'Measure>) : Relation =
                (float f) == expr.Value

            static member ( == ) (expr: LinearExpr<'Measure>, f: float<'Measure>) : Relation =
                expr.Value == (float f)

            static member ( == ) (d: Decision<'Measure>, expr: LinearExpr<'Measure>) : Relation =
                d.Value == expr.Value

            static member ( == ) (expr: LinearExpr<'Measure>, d: Decision<'Measure>) : Relation =
                expr.Value == d.Value

            static member ( == ) (lExpr: LinearExpr<'Measure>, rExpr: LinearExpr<'Measure>) : Relation =
                lExpr.Value == rExpr.Value

            // Greater or Equals
            static member ( >== ) (f: float<'Measure>, expr: LinearExpr<'Measure>) : Relation =
                (float f) >== expr.Value

            static member ( >== ) (expr: LinearExpr<'Measure>, f: float<'Measure>) : Relation =
                expr.Value >== (float f)

            static member ( >== ) (d: Decision<'Measure>, expr: LinearExpr<'Measure>) : Relation =
                d.Value >== expr.Value

            static member ( >== ) (expr: LinearExpr<'Measure>, d: Decision<'Measure>) : Relation =
                expr.Value >== d.Value

            static member ( >== ) (lExpr: LinearExpr<'Measure>, rExpr: LinearExpr<'Measure>) : Relation =
                lExpr.Value >== rExpr.Value


        module Model =

            let create name sense (expr: LinearExpr<_>) : Model=
                {
                    Name = ModelName name
                    Objective = { Sense = sense; Expr = expr.Value}
                    Constraints = []
                    DecisionBounds = []
                }

            let addBound (decision: Decision<'Measure>, bounds: Bounds<'Measure>) model : Model =
                let newBounds =
                    match bounds with
                    | Binary -> Modeling.Bounds.Binary
                    | Integer (lower, upper) -> Modeling.Bounds.Integer (int lower, int upper)
                    | Continuous (lower, upper) -> Modeling.Bounds.Continuous (float lower, float upper)
                    | Unbounded -> Modeling.Bounds.Unbounded

                { model with
                    DecisionBounds = struct (decision.Value, newBounds) :: model.DecisionBounds }

        
        type SumBuilder () =

            member _.Zero () = LinearExpr.Zero
            member _.Delay (expr: unit -> LinearExpr<_>) = expr
            member _.Run (expr: unit -> LinearExpr<_>) = expr ()
            member _.Combine (a: LinearExpr<_>, b: unit -> LinearExpr<_>) = a + b ()

            member _.Yield (a: float<_>) = a
            member _.Yield (a: Decision<_>) = a
            member _.Yield (a: LinearExpr<_>) = a

            member _.Yield (a: seq<float<'Measure>>) =
                let mutable acc = LinearExpr<'Measure>.Zero
                for x in a do
                    acc <- acc + x
                acc
            
            member _.Yield (a: seq<Decision<'Measure>>) =
                let mutable acc = LinearExpr<'Measure>.Zero
                for x in a do
                    acc <- acc + x
                acc

            member _.Yield (a: seq<LinearExpr<'Measure>>) =
                let mutable acc = LinearExpr<'Measure>.Zero
                for x in a do
                    acc <- acc + x
                acc

            member _.Yield (a: seq<KeyValuePair<_, float<'Measure>>>) =
                let mutable acc = LinearExpr<'Measure>.Zero
                for KeyValue (_, x) in a do
                    acc <- acc + x
                acc

            member _.Yield (a: seq<KeyValuePair<_, Decision<'Measure>>>) =
                let mutable acc = LinearExpr<'Measure>.Zero
                for KeyValue (_, x) in a do
                    acc <- acc + x
                acc

            member _.Yield (a: seq<KeyValuePair<_, LinearExpr<'Measure>>>) =
                let mutable acc = LinearExpr<'Measure>.Zero
                for KeyValue (_, x) in a do
                    acc <- acc + x
                acc

            member _.For (a: seq<'a>, body: 'a -> float<'Measure>) =
                let mutable acc = LanguagePrimitives.FloatWithMeasure<'Measure> 0.0
                for x in a do
                    let result = body x
                    acc <- acc + result
                LinearExpr<'Measure> (LinearExpr.Constant (float acc))

            member _.For (a: seq<'a>, body: 'a -> Decision<'Measure>) =
                let mutable acc = LinearExpr<'Measure>.Zero
                for x in a do
                    let result = body x
                    acc <- acc + result
                acc

            member _.For (a: seq<'a>, body: 'a -> LinearExpr<'Measure>) =
                let mutable acc = LinearExpr<'Measure>.Zero
                for x in a do
                    let result = body x
                    acc <- acc + result
                acc
        

        let sum = SumBuilder()

        // [<Struct>] 
        // type Objective<[<Measure>] 'Measure> =
        //     val internal Objective : Modeling.Objective
        //     new (objective: Modeling.Objective) =
        //         { Objective = objective}




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

    [<RequireQualifiedAccess>]
    type Backend =
        | GLOPS
        | CBC

    type Settings =
        {
            Duration_ms: int64
            Backend: Backend
            WriteLPFile: option<string>
            EnableOutput: bool
        }

    [<RequireQualifiedAccess>]
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

        let solver =
            match settings.Backend with
            | Backend.GLOPS ->
                Solver.CreateSolver "GLOP"
            | Backend.CBC ->
                Solver.CreateSolver "CBC"
                
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
            | Solver.ResultStatus.MODEL_INVALID ->
                SolveResult.Unknown

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


open Modeling
open Modeling.UnitsOfMeasure

// type [<Measure>] Count
// type [<Measure>] Size

// let chicken = Decision<Count> "Chicken"
// let cow = Decision<Count> "Cow"
// let objExpr = 1.0 * chicken + 1.0 * cow

// let constraints =
//     [
//         ("Chicken Limit", chicken <== 10.0<Count>)
//         ("Cow Limit", cow <== 5.0<_>)
//         ("AnimalLimit", 2.0*chicken + 3.0*cow <== 30.0<_>)
//     ]

// let m =
//     Model.create "Test" Maximize objExpr
//     |> Model.addConstraints constraints
//     |> Model.addBound (chicken, Integer (0<_>, 100<_>))
//     |> Model.addBound (cow, Integer (0<_>, 100<_>))

// let settings : ORTools.Settings = {
//     Duration_ms = 1_000L
//     Backend = ORTools.Backend.GLOPS
//     WriteLPFile = None
//     EnableOutput = true
// }

// let s = ORTools.solve settings m


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

[<Measure>]
type Count

let decs =
    [ for i in 1 .. 4 do
        for j in 1..2 do
            let name = $"Dec{i}{j}"
            (float (i * 10 + j)) * (Decision<Count> name)]

let decMap =
    Map [ for i in 1 .. 4 do
            let name = $"Dec{i}"
            i, (float i) * (Decision<Count> name)]

let fSum =
    sum { for i in 1.0..4.0 do
            i * 1.0<Count> }

let dSeqSum =
    sum { for i in 1..4 do
            let name = $"Dec{i}"
            Decision<Count> name}


let exprSeqSum =
    sum { for i in 1..4 do
            let d = Decision<Count> ($"Dec{i}")
            (float i) * d }

let x = exprSeqSum + dSeqSum


// let fSum = sum { floats }
let dSum = sum { decs }
let decMapSum = sum { decMap }


dSum.Value
decMapSum.Value