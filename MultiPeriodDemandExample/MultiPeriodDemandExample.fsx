#r "nuget: Flips, 2.4.4"

module Data =

    let holdingCostAsPercentOfProduction = 0.05
    let initialInventory = 5_000.0
    let monthProductionCost =
        [
            1, 12.40
            2, 12.55
            3, 12.70
            4, 12.80
            5, 12.85
            6, 12.95
        ]

    let productionCapacity =
        [
            1, 30_000.0
            2, 30_000.0
            3, 30_000.0
            4, 30_000.0
            5, 30_000.0
            6, 30_000.0
        ]

    let monthDemand =
        [
            1, 10_000.0
            2, 15_000.0
            3, 30_000.0
            4, 35_000.0
            5, 25_000.0
            6, 10_000.0
        ]

    let storageCapacity =
        [
            1, 10_000.0
            2, 10_000.0
            3, 10_000.0
            4, 10_000.0
            5, 10_000.0
            6, 10_000.0
        ]

module MultiPeriodDemand =

    open Flips
    open Flips.Types
    open Flips.SliceMap

    type private Month = Month of int

    module private Month =

        let getValue (Month value) = value


    type Input = private {
        InitialInventory : float
        StorageCapacity : SMap<Month, float>
        Demand : SMap<Month, float>
        ProductionCapacity : SMap<Month, float>
        ProductionCost : SMap<Month, float>
        HoldingCostPercent : float
        Months : Set<Month>
    }

    module Input =

        let private createValueForMonth (month:int, value:float) =
            if value < 0.0 then
                    invalidArg (nameof value) $"Cannot have a negative value for {nameof value} for month {month}"
            
            Month month, value

        let private createValuesForMonths (requiredMonths: Set<Month>) (input: seq<int*float>) =
            let result =
                input
                |> Seq.map createValueForMonth
                |> Seq.filter (fun (month, _) -> Set.contains month requiredMonths)
                |> SMap

            // Idiot check here :)
            for month in requiredMonths do
                if not (SMap.containsKey month result) then
                    invalidArg (nameof input) $"{nameof input} does not contain the Month {month}"

            result

        let create initialInventory storageCapacity demand productionCapacity productionCost holdingCostPercent months =
            if initialInventory < 0.0 then
                invalidArg (nameof initialInventory) $"Cannot have a negative value for {nameof initialInventory}"

            if holdingCostPercent > 1.0 || holdingCostPercent < 0.0 then
                invalidArg (nameof holdingCostPercent) $"{nameof holdingCostPercent} must be between 0.0 and 1.0"

            let newMonths = 
                months 
                |> Seq.map Month 
                |> Seq.sort 
                |> Set

            let newStorageCapacity = createValuesForMonths newMonths storageCapacity
            let newDemand = createValuesForMonths newMonths demand
            let newProductionCapacity = createValuesForMonths newMonths productionCapacity
            let newProductionCost = createValuesForMonths newMonths productionCost

            {
                InitialInventory = initialInventory
                StorageCapacity = newStorageCapacity
                Demand = newDemand
                ProductionCapacity = newProductionCapacity
                ProductionCost = newProductionCost
                HoldingCostPercent = holdingCostPercent
                Months = newMonths
            }

    let private createModel (input: Input) (production: SMap<Month, Decision>) =

        let demandConstraints =
            ConstraintBuilder "Demand" {
                for month in input.Months ->
                    (sum production.[LessOrEqual month]) + input.InitialInventory >== sum input.Demand.[LessOrEqual month]
            }

        let productionConstraints =
            ConstraintBuilder "Production" {
                for month in input.Months ->
                    production.[month] <== input.ProductionCapacity.[month]
            }

        let storageConstraints =
            ConstraintBuilder "Inventory" {
                for month in input.Months ->
                    input.InitialInventory + (sum production.[LessOrEqual month]) - (sum input.Demand.[LessOrEqual month]) 
                    <== 
                    input.StorageCapacity.[month]
            }

        let productionCostExpr = sum (production .* input.ProductionCost)
        let carryingCostExpr = 
            [for month in input.Months -> 
                let carryingCost = input.HoldingCostPercent * input.ProductionCost.[month]
                carryingCost * (input.InitialInventory + (sum production.[LessOrEqual month]) + (sum input.Demand.[LessOrEqual month]))
            ] |> List.sum

        let totalCostExpr = productionCostExpr + carryingCostExpr
        let costObjective = Objective.create "MinCost" Minimize totalCostExpr

        Model.create costObjective
        |> Model.addConstraints demandConstraints
        |> Model.addConstraints productionConstraints
        |> Model.addConstraints storageConstraints


    let private composeResult (production:SMap<Month, Decision>) (solution: Solution) =
        let productionPlan = 
            Solution.getValues solution production
            |> Map.toSeq
            |> Seq.map (fun (month, production) -> Month.getValue month, production )

        productionPlan


    let solve (input: Input) =

        let production =
            DecisionBuilder "Production" {
                for month in input.Months ->
                    Continuous (0.0, infinity)
            } |> SMap

        let model = createModel input production
        let result = Solver.solve Settings.basic model

        match result with
        | SolveResult.Optimal solution -> 
            composeResult production solution
        | err ->
            printfn "%A" err
            invalidArg (nameof input) $"Bad input yo!"

open MultiPeriodDemand

let input = Input.create Data.initialInventory Data.storageCapacity Data.monthDemand Data.productionCapacity Data.monthProductionCost Data.holdingCostAsPercentOfProduction [1..6]

let plan = solve input

for (month, production) in plan do
    printfn $"Month: {month} | Production: {production}"


