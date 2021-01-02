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

let cuts =
    [1.0 .. 3.0]
    |> List.map Cut

let plans = generatePlans 2.0 3.0 cuts

let x : int list = []
let y = x.[2..]