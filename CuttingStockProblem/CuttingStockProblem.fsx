#r "nuget: Flips"

type Cut = Cut of float
type Plan = Plan of Map<Cut, int>

module Cut =

    /// Take a Cut and return the length as a float
    let length (Cut length) =
        length

module Plan =

    /// Give me a Plan with no cuts
    let empty : Plan =
        Plan Map.empty

    /// Give me the total length of cuts in the plan
    let length (Plan plan) =
       plan
       |> Seq.sumBy (fun (KeyValue(Cut cut, count)) -> cut * float count)

    /// Add a Cut to a Plan and return a new Plan
    let addCut (cut: Cut) (Plan plan) =
        match Map.tryFind cut plan with
        | Some count -> Plan (Map.add cut (count + 1) plan)
        | None -> Plan (Map.add cut 1 plan)

    /// Give me the count of each distinct cut in a given Plan
    let cutCounts (Plan plan) =
        plan
        |> Seq.map (fun (KeyValue(cut, count)) -> cut, count)
        

let generatePlans (stockLength: float) (cuts: Cut list) : Plan list =
    let sortedCuts = 
        cuts 
        |> List.distinct
        |> List.sortBy (fun (Cut length) -> length)

    let rec generate (candidates: (Plan * Cut list) list) (approved: Plan list) =
        match candidates with
        | [] -> approved
        | testCandidate::remainingCandidates ->
            let plan, cuts = testCandidate
            match cuts with
            | [] -> 
                let newApproved = plan::approved
                generate remainingCandidates newApproved
            | nextCut::remainingCuts ->
                if Plan.length plan + Cut.length nextCut <= stockLength then
                    let newPlan = Plan.addCut nextCut plan
                    let newCandidates = (newPlan, cuts)::(plan, remainingCuts)::remainingCandidates
                    generate newCandidates approved
                else
                    let newApproved = plan::approved
                    generate remainingCandidates newApproved

    let initialCandidate = Plan.empty, sortedCuts
    generate [initialCandidate] []


// let plans = generatePlans 2.0 3.0 cuts
fsi.ShowDeclarationValues <- false
let cuts = 
    [
        1380.0
        1520.0
        1560.0
        1710.0
        1820.0
        1880.0
        1930.0
        2000.0
        2050.0
        2100.0
        2140.0
        2150.0
        2200.0
    ] |> List.map Cut

let cutRequirements =
    [
        Cut 1380.0 , 22.0
        Cut 1520.0 , 25.0
        Cut 1560.0 , 12.0
        Cut 1710.0 , 14.0
        Cut 1820.0 , 18.0
        Cut 1880.0 , 18.0
        Cut 1930.0 , 20.0
        Cut 2000.0 , 10.0
        Cut 2050.0 , 12.0
        Cut 2100.0 , 14.0
        Cut 2140.0 , 16.0
        Cut 2150.0 , 18.0
        Cut 2200.0 , 20.0
    ] |> Map


let stockLength = 5600.0
let plans = generatePlans stockLength cuts
List.length plans

open Flips
open Flips.Types
open Flips.SliceMap

let planDecs =
    DecisionBuilder "PlanCount" {
        for plan in plans ->
        Integer (0.0, infinity)
    } |> SMap

let planCutCounts =
    plans
    |> Seq.collect (fun plan -> Plan.cutCounts plan
                                |> Seq.map (fun (cut, count) -> (plan, cut), float count)
    ) |> SMap2

let cutRequirementConstraints =
    ConstraintBuilder "CutRequirements" {
        for cut in cuts ->
        sum (planDecs .* planCutCounts.[All, cut]) >== cutRequirements.[cut]
    }

let objective = Objective.create "MinRolls" Minimize (sum planDecs)

let model =
    Model.create objective
    |> Model.addConstraints cutRequirementConstraints

let result = Solver.solve Settings.basic model

match result with
| Optimal solution ->
    let values = 
        Solution.getValues solution planDecs
        |> Map.filter (fun plan quantity -> quantity > 0.0)

    let totalNumberOfCuts =
        values
        |> Seq.sumBy (fun (KeyValue(_, count)) -> count)

    printfn "Quantity | Plan"
    for KeyValue(plan, quantity) in values do
        printfn $"{quantity} | {plan}"

    printfn "=========================================="
    printfn $"Total Number of Cuts: {totalNumberOfCuts}"
    printfn "=========================================="

| _ -> failwith "Unable to solve"