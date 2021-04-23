#r "nuget: Flips, 2.4.4"

module ExampleData =

    let holdingCostAsPercentOfProduction = 0.05
    let initialInventory = 5000.0
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

    [<Measure>] type Item
    [<Measure>] type USD

    type Month = private Month of int
        with
            member this.Previous =
                let (Month value) = this
                Month (value - 1)

    module Month =

        let create (value: int) =
            // Validation would go here
            Month value


    type Input = private {
        InitialInventory : float<Item>
        StorageCapacity : Map<Month, float<Item>>
        Demand : Map<Month, float<Item>>
        ProductionCapacity : Map<Month, float<Item>>
        ProductionCost : Map<Month, float<USD/Item>>
        HoldingCostPercent : float
        Months : Month list
    }

    module Input =

        let private createMonthValue (coef:float<'Measure>) (month:int, value:float) =
            if value < 0.0 then
                    invalidArg (nameof value) $"Cannot have a negative value for {nameof value} for month {month}"

            Month.create month, coef * value

        let private createMonthValues (requiredMonths: Month list) (coef:float<'Measure>) (input: seq<int*float>) =
            let result =
                input
                |> Seq.map (createMonthValue coef)
                |> Map

            for month in months do
                if Map.con

        let create initialInventory storageCapacity demand productionCapacity productionCost holdingCostPercent monthsOfInterest : Input =
            if initialInventory < 0.0 then
                invalidArg (nameof initialInventory) $"Cannot have a negative value for {nameof initialInventory}"
            
            if holdingCostPercent > 1.0 || holdingCostPercent < 0.0 then
                invalidArg (nameof holdingCostPercent) $"{nameof holdingCostPercent} is a percent and must be between 0.0 and 1.0"

            let months =
                monthsOfInterest
                |> Seq.map Month
                |> Seq.sort
                |> List.ofSeq


            {
                InitialInventory = initialInventory * 1.0<Item>
                StorageCapacity = createMonthValues 1.0<Item> storageCapacity
                Demand = createMonthValues 1.0<Item> demand
                ProductionCapacity = createMonthValues 1.0<Item> productionCapacity
                ProductionCost = createMonthValues 1.0<USD/Item> productionCost
                HoldingCostPercent = holdingCostPercent
                Months = months |> Seq.map Month.create |> List.ofSeq
            }
    

    let solve (input: Input) =

        // Do something here :)
        let production =


        ()