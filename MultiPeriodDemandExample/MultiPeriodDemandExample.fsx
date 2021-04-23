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

    let solve () =

        // Do something here :)

        ()