#r "nuget: Flips"

type Cut = Cut of float
type Plan = Plan of Map<Cut, int>

module Cut =

    let length (Cut length) =
        length

module Plan =

    let empty : Plan =
        Plan Map.empty

    let length (Plan plan) =
       plan
       |> Seq.sumBy (fun (KeyValue(Cut cut, count)) -> cut * float count)

    let ofCuts (cuts: Cut list) =
        cuts
        |> List.groupBy id
        |> List.map (fun (cut, grp) -> cut, List.length grp)
        |> Map
        |> Plan

    let ofCut (cut: Cut) =
        Map [cut, 1]
        |> Plan

    let addCut (cut: Cut) (Plan plan) =
        match Map.tryFind cut plan with
        | Some count -> Plan (Map.add cut (count + 1) plan)
        | None -> Plan (Map.add cut 1 plan)

    // let cutCount (Plan plan) =

        

let generatePlans (minLength: float) (maxLength: float) (cuts: Cut list) =
    let sortedCuts = 
        cuts 
        |> List.distinct
        |> List.sortBy (fun (Cut length) -> length)
    let initialCandidate = Plan.empty, sortedCuts

    let rec generate (candidates: (Plan * Cut list) list) (approved: Plan list) =
        match candidates with
        | [] -> approved
        | testCandidate::remainingCandidates ->
            let plan, cuts = testCandidate
            match cuts with
            | [] ->
                if Plan.length plan >= minLength then
                    generate remainingCandidates ([plan] @ approved)
                else
                    generate remainingCandidates approved
            | nextCut::remainingCuts ->
                if Plan.length plan + Cut.length nextCut <= maxLength then
                    let newPlan = Plan.addCut nextCut plan
                    generate ([(newPlan, cuts); (plan, remainingCuts)] @ remainingCandidates) approved
                else
                    if Plan.length plan >= minLength then
                        generate remainingCandidates ([plan] @ approved)
                    else
                        generate remainingCandidates approved

    generate [initialCandidate] []

// let cuts =
//     [1.0 .. 3.0]
//     |> List.map Cut

// let plans = generatePlans 2.0 3.0 cuts

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


let maxLength = 5600.0
let minLength =
    let (Cut minCut) = List.min cuts
    maxLength - minCut

let plans = generatePlans minLength maxLength cuts
let planCount = List.length plans

open Flips
open Flips.Types
open Flips.SliceMap

// let planLengths =
//     plans
//     |> List.map (fun plan -> plan, Plan.length plan)
//     |> SMap

let planDecs =
    DecisionBuilder "PlanCount" {
        for plan in plans ->
        Integer (0.0, infinity)
    } |> SMap

let planCutCounts =
    plans
    |> List.collect (fun plan ->
                                let (Plan values) = plan
                                let cutCounts = values |> Map.toList
                                cutCounts 
                                |> List.map (fun (cut, count) -> (plan, cut), float count)
    ) |> SMap2

let cutRequirementConstraints =
    ConstraintBuilder "CutRequirements" {
        for cut in cuts ->
        sum (planDecs .* planCutCounts.[All, cut]) >== cutRequirements.[cut]
    }

let objective = Objective.create "MinCuts" Minimize (sum planDecs)

let model =
    Model.create objective
    |> Model.addConstraints cutRequirementConstraints

let result = Solver.solve Settings.basic model

