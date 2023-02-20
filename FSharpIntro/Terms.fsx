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
type Expr =
    | Constant of float
    | StringLiteral of string
    | Ref of Ref
    | Add of lhs: Expr * rhs: Expr
    | Multiply of lhs: Expr * rhs: Expr
    | Power of term: Expr * powerExpr: Expr
    | Parens of term: Expr



Expr.Add (
    Expr.Power (
        Expr.Add (
            Expr.Constant 1,
            Expr.Multiply (
                Expr.Constant 5
                , Expr.Constant 4
            )
        ), 3)
    , Expr.Constant 1
)

let ws = skipMany (skipChar ' ')
// let ws1 = skipMany1 (skipChar ' ')
let quote : Parser<_, unit> = skipChar '\''

let intOrFloatLiteral =
    numberLiteral (NumberLiteralOptions.DefaultFloat ||| NumberLiteralOptions.DefaultInteger) "number"
    |>> fun n ->
            if n.IsInteger then Expr.Constant (float n.String)
            else Expr.Constant (float n.String)
    .>> ws

let stringLiteral = quote >>. manyCharsTill anyChar quote |>> Expr.StringLiteral .>> ws

// let identifier = many1Chars (letter <|> digit) |>> Expr.Identifier .>> ws



let opp = OperatorPrecedenceParser<Expr, _, _>()


opp.TermParser <- choice [
    intOrFloatLiteral
    stringLiteral
    // identifier
]

opp.AddOperator <| InfixOperator("*", ws, 2, Associativity.Left, fun x y -> Expr.Multiply (x, y))
opp.AddOperator <| InfixOperator("+", ws, 3, Associativity.Left, fun x y -> Expr.Add (x, y))
opp.AddOperator <| PostfixOperator("^", ws, 1, Associativity.Right, fun x y -> Expr.Power (x, y))
// opp.AddOperator <| InfixOperator("/", ws, 2, Associativity.Left, fun x y -> Expr.Binary (x, y, BinaryExprKind.Divide))
// opp.AddOperator <| InfixOperator("-", ws, 3, Associativity.Left, fun x y -> Expr.Binary (x, y, BinaryExprKind.Subtract))
// opp.AddOperator <| InfixOperator("&&", ws, 5, Associativity.Left, fun x y -> Expr.Binary (x, y, BinaryExprKind.And))
// opp.AddOperator <| InfixOperator("||", ws, 6, Associativity.Left, fun x y -> Expr.Binary (x, y, BinaryExprKind.Or))
// opp.AddOperator <| InfixOperator("=", ws, 7, Associativity.None, fun x y -> Expr.Binary (x, y, BinaryExprKind.Equals))
// opp.AddOperator <| InfixOperator("!=", ws, 8, Associativity.None, fun x y -> Expr.Binary (x, y, BinaryExprKind.NotEquals))
// opp.AddOperator <| InfixOperator(">", ws, 9, Associativity.None, fun x y -> Expr.Binary (x, y, BinaryExprKind.GreaterThan))
// opp.AddOperator <| InfixOperator(">=", ws, 10, Associativity.None, fun x y -> Expr.Binary (x, y, BinaryExprKind.GreaterThanOrEquals))
// opp.AddOperator <| InfixOperator("<", ws, 11, Associativity.None, fun x y -> Expr.Binary (x, y, BinaryExprKind.LesserThan))
// opp.AddOperator <| InfixOperator("<=", ws, 12, Associativity.None, fun x y -> Expr.Binary (x, y, BinaryExprKind.LesserThanOrEquals))

let exprParser = opp.ExpressionParser

let t = Expr.Constant 1.0

let e1 = "1 + 5*4 + 1"
let parseResult1 = run exprParser e1

match parseResult1 with
| Success (res, _, _)->
    printfn $"Success: {res}"
| Failure (err, _, _) ->
    printfn $"Error: {err}"
