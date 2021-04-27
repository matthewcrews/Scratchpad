#r "nuget: Flips, 2.4.4"

open Flips
open Flips.Types
open Flips.SliceMap

let solve () =
    let processMaxFlow = 10.0
    let recycleMaxFlow = 8.0

    let arcs =
        DecisionBuilder "Arcs" {
            for i in 1..7 ->
                Continuous (0.0, infinity)
        } |> Map

    //for KeyValue(key, value) in arcs do
    //    printfn "%A" (key, value)

    let tankAcc = Decision.createContinuous "TankAcc" 0.0 infinity


    // Balance Constraints
    let mergeBalance = Constraint.create "MergeBalance" (arcs.[1] + arcs.[7] == arcs.[2])
    let mergeMix = Constraint.create "MergeMix" (arcs.[1] == arcs.[7])
    let procesBalance = Constraint.create "ProcessBalance" (arcs.[2] == arcs.[3])
    let splitBalance = Constraint.create "SplitBalance" (arcs.[3] == arcs.[4] + arcs.[5])
    let splitMix = Constraint.create "SplitMix" (arcs.[4] == arcs.[5])
    let tankBalance = Constraint.create "TankBalance" (tankAcc == arcs.[5] - arcs.[6])
    let recycleBalance = Constraint.create "RecycleBalance" (arcs.[6] == arcs.[7])

    // Capacity Constraints
    let processCapacity = Constraint.create "ProcessCapacity" (arcs.[3] <== processMaxFlow)
    let recycleCapacity = Constraint.create "RecycleCapacity" (arcs.[7] <== recycleMaxFlow)
    let otherCapacity = Constraint.create "OtherCapacity" (arcs.[6] <== 6.0)

    let constraints = [
        mergeBalance
        mergeMix
        procesBalance
        splitBalance
        splitMix
        tankBalance
        recycleBalance
        processCapacity
        recycleCapacity
        otherCapacity
    ]

    let objective = Objective.create "MaxFlow" Maximize (arcs |> Map.toSeq |> Seq.map (fun (key, d) -> 1.0 * d) |> Seq.sum)

    let model =
        Model.create objective
        |> Model.addConstraints constraints

    let settings =
        { Settings.basic with
            WriteLPFile = Some "Test.lp"
        }

    let result = Solver.solve Settings.basic model

    match result with
    | SolveResult.Optimal sln ->
        for KeyValue(name, value) in sln.DecisionResults do
            let (DecisionName decName) = name.Name
            printfn $"Name: {decName} | Value: {value}"

        let objValue = Solution.evaluate sln objective.Expression

        printfn $"Obj: {objValue}"
    | _ ->
        printfn "Something has gone horribly wrong"

solve()