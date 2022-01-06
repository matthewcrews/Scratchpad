#r "nuget: Flips,2.4.7"

open Flips
open Flips.Types

let x =
    DecisionBuilder "x" {
        for index in 1..5 ->
            Continuous (0.0, infinity)
    } |> Map


let y = Decision.createContinuous "y" 0.0 infinity

let vvegConstraint = Constraint.create "VVEG" (x[1] + x[2] <== 200.0)
let nvegConstraint = Constraint.create "NVEG" (x[3] + x[4] + x[5] <== 250.0)
let uhrdConstraint = Constraint.create "UHRD" (8.8*x[1] + 6.1*x[2] + 2.0*x[3] + 4.2*x[4] + 5.0*x[5] - 6.0*y <== 0.0)
let lhrdConstraint = Constraint.create "LHRD" (8.8*x[1] + 6.1*x[2] + 2.0*x[3] + 4.2*x[4] + 5.0*x[5] - 3.0*y >== 0.0)
let contConstraint = Constraint.create "CONT" (x[1] + x[2] + x[3] + x[4] + x[5] - y == 0.0)
let constraints =
    [
        vvegConstraint
        nvegConstraint
        uhrdConstraint
        lhrdConstraint
        contConstraint
    ]

let profitExpr = -110.0 * x[1] - 120.0 * x[2] - 130.0 * x[3] - 110.0 * x[4] - 115.0 * x[5] + 150.0 * y
let objective = Objective.create "Profit" Maximize profitExpr

let model =
    Model.create objective
    |> Model.addConstraints constraints


let result = Solver.solve Settings.basic model