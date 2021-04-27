#r "nuget: MathNet.Numerics.FSharp, 4.15.0"

open MathNet.Numerics.LinearAlgebra

fsi.AddPrinter<Vector<float>>(fun v -> v |> Seq.map (sprintf "%.2f") |> String.concat " " |> (sprintf "[%s]"))


let GetThetaAndIndex (input: Vector<float>) =
    let mutable minValue = infinity
    let mutable minIdx = 0

    for idx in 0..input.Count-1 do
        let value = input.[idx]
        if value > 0.0 then
            if value < minValue then
                minValue <- value
                minIdx <- idx

    minValue, minIdx

let A = matrix [
//    0     1     2     3     4     5     6     7     8     9    
    [1.0; -1.0;  0.0;  0.0;  0.0;  0.0;  1.0;  0.0;  0.0;  0.0] // 0
    [1.0;  0.0;  0.0;  0.0;  0.0;  0.0; -1.0;  0.0;  0.0;  0.0] // 1
    [0.0;  1.0; -1.0;  0.0;  0.0;  0.0;  0.0;  0.0;  0.0;  0.0] // 2
    [0.0;  0.0;  1.0; -1.0; -1.0;  0.0;  0.0;  0.0;  0.0;  0.0] // 3
    [0.0;  0.0;  0.0;  1.0; -1.0;  0.0;  0.0;  0.0;  0.0;  0.0] // 4
    [0.0;  0.0;  0.0;  0.0; -1.0;  1.0;  0.0;  1.0;  0.0;  0.0] // 5
    [0.0;  0.0;  0.0;  0.0;  0.0;  1.0; -1.0;  0.0;  0.0;  0.0] // 6
    [0.0;  0.0;  1.0;  0.0;  0.0;  0.0;  0.0;  0.0;  1.0;  0.0] // 7
    [0.0;  0.0;  0.0;  0.0;  0.0;  0.0;  1.0;  0.0;  0.0;  1.0] // 8
//    0     1     2     3     4     5     6     7     8     9    
]

let vars =
    [|0..A.ColumnCount - 1|]
    |> Array.map (sprintf "Var_%i")


// Exclude x_3 from the basis
let basisStartColumn = A.ColumnCount - A.RowCount
let B = A.[*, basisStartColumn..]
let basicVars = vars.[basisStartColumn..]
let b = vector [
    0.0
    0.0
    0.0
    0.0
    0.0
    0.0
    0.0
    10.0
    4.0
]

let c = vector [
    -1.0
    -1.0
    -1.0
    -1.0
    -1.0
    -1.0
    -1.0
    0.0
    0.0
    0.0
]

let x = B.Solve b
let c_b = c.[basisStartColumn..]
let p = B.Solve c_b

let enteringColumn =
    let mutable currPivotColumn = 0
    let mutable currReducedCost = 0.0

    for idx in 0 .. basisStartColumn - 1 do
        let reducedCost = c.[idx] - p * A.[*, idx]
        if reducedCost < currReducedCost then
            currReducedCost <- reducedCost
            currPivotColumn <- idx

    currPivotColumn

let u = B.Solve A.[*, enteringColumn]
let theta, leavingColumn =
    (x ./ u)
    |> GetThetaAndIndex

// Tracking where Decision Variables ended up
basicVars.[leavingColumn] <- vars.[enteringColumn]
let newx = x - theta * u
newx.[leavingColumn] <- theta
newx

let result =
    (newx.AsArray (), basicVars)
    ||> Array.zip
    |> Array.map (fun (value, variable) -> variable, value)
    |> Array.sortBy fst

result

(*
Name: Arcs_1 | Value: 4
Name: Arcs_2 | Value: 8
Name: Arcs_3 | Value: 8
Name: Arcs_4 | Value: 4
Name: Arcs_5 | Value: 4
Name: Arcs_6 | Value: 4
Name: Arcs_7 | Value: 4
Name: TankAcc | Value: 0
*)