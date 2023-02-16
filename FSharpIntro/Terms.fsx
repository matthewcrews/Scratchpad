#r "nuget: FParsec"
open FParsec

type CellRef =
    | A1 of columnName: string * rowNumber: int
    | R1C1 of colNumber: int * rowNumber : int


type Ref =
    | Relative of string
    | Absolute of string
    | OffSheet of sheetName: string * cellName: string

type BinaryOperation =
    | Add
    | Subtract
    | Multiply
    | Divide

[<RequireQualifiedAccess>]
type Term =
    | Constant of float
    | Ref of Ref
    | Add of lhs: Term * rhs: Term
    | Multiply of lhs: Term * rhs: Term
    | Power of term: Term * power: float
    | Parens of term: Term

let t = Term.Constant 1.0

let expr = "(1 + 5*4)^3 + 1"

Term.Add (
    Term.Power (
        Term.Add (
            Term.Constant 1,
            Term.Multiply (
                Term.Constant 5
                , Term.Constant 4
            )
        ), 3)
    , Term.Constant 1
)