#r "nuget: Google.OrTools, 9.1.9490"

open Google.OrTools.LinearSolver

// Create my initial Solver object
let s = Solver.CreateSolver "GLOP"
// Build the model
let x1 = s.MakeNumVar (0.0, infinity, "x1")
let x2 = s.MakeNumVar (0.0, infinity, "x2")
// Create constraints and set coefficients
let c1 = s.MakeConstraint (-infinity, 10.0, "C1")
c1.SetCoefficient (x1, 1.0)
let c2 = s.MakeConstraint(-infinity, 10.0, "C2")
c2.SetCoefficient (x2, 1.0)
let c3 = s.MakeConstraint (-infinity, 15.0, "Blench")
c3.SetCoefficient (x1, 1.0)
c3.SetCoefficient (x2, 1.0)

// Create linear expression for objective
let o1 = 2.0 * x1 + 1.0 * x2
// Set the objective function and goal
s.Maximize o1
// Call Solve the first time
let r = s.Solve()
match r with
| Solver.ResultStatus.OPTIMAL ->
    printfn "Solved"
| _ ->
    printfn "Failed to solve"
    
// Make an update to one of the coefficients in a constraint
c1.SetBounds (-infinity, 12.0)
// Resolve using the same Solver instance
// Question, is this solving the problem from scratch or is it starting
// from the previous solution? Do I need to provide hints from the first
// solve to give it a warm start or does it just remember previous solution?
let r2 = s.Solve()
match r2 with
| Solver.ResultStatus.OPTIMAL ->
    printfn "Solved"
| _ ->
    printfn "Failed to solve"

c1.SetCoefficient (x1, 2.0)
let r3 = s.Solve()
match r3 with
| Solver.ResultStatus.OPTIMAL ->
    printfn "Solved"
    printfn "%A" (x1.SolutionValue ())
    printfn "%A" (x2.SolutionValue ())
| _ -> ()

//let constraints = s.constraints()
s.constraints()
s.constraints().Remove(c1)
let r4 = s.Solve()
match r4 with
| Solver.ResultStatus.OPTIMAL ->
    printfn "Solved"
    printfn "%A" (x1.SolutionValue ())
    printfn "%A" (x2.SolutionValue ())
| _ -> ()