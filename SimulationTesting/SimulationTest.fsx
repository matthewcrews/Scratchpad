#r "nuget: Microsoft.ML"
#r "nuget: MathNet.Numerics.FSharp"
#r "nuget: Spectre.Console"
#r "nuget: FileHelpers"
#r "nuget: Flips"


module Types =

    open FileHelpers
    open Microsoft.ML.Data

    [<Measure>] type USD
    [<Measure>] type cm
    [<Measure>] type gm
    [<Measure>] type serving

    type Demand = Demand of float<serving>
    type Sales = Sales of float<serving>
    type Inventory = Inventory of float<serving>
    type Revenue = Revenue of float<USD>
        with
            static member Zero = Revenue 0.0<USD>
            static member (+) (Revenue a, Revenue b) = Revenue (a + b)
            override x.ToString () =
                let (Revenue r) = x
                (float r).ToString("C")

    type RevenuePerServing = RevenuePerServing of float<USD/serving>
        with
            static member (*) (Sales sales, RevenuePerServing revenuePerServing) =
                sales * revenuePerServing
                |> Revenue

            static member (*) (revenuePerItem: RevenuePerServing, sales: Sales) =
                sales * revenuePerItem
    type NthItem = NthItem of int
    type DemandRate = DemandRate of float
    type Day = Day of int
    type Temperature = Temperature of float
    type ModelFile = ModelFile of string
    type DataFile = DataFile of string
    type OutputDirectory = OutputDirectory of string

    type Condition =
        | Sunny
        | Cloudy
        | Rainy

    type Weather = {
        Condition : Condition
        Temperature : Temperature
    }

    type Food =
        | Burger
        | Pizza
        | Taco
        with
            override this.ToString () =
                match this with
                | Burger -> "Burger"
                | Pizza -> "Pizza"
                | Taco -> "Taco"

    let foods = [
        Burger
        Pizza
        Taco
    ]

    type DemandSimulationResult = {
        Day : Day
        Temperature : Temperature
        Condition : Condition
        Demand : Demand
    }

    [<CLIMutable>]
    [<DelimitedRecord(",")>]
    type DemandSimulationRecord = {
        Day : int
        Temperature : float
        Condition : string
        Demand : float
    }

    [<CLIMutable>]
    type DemandData = {
        [<LoadColumn(1)>] 
        Temperature : single
        [<LoadColumn(2)>]
        Condition : string
        [<LoadColumn(3); ColumnName("Label")>]
        Demand : single
    }
    
    type TemperatureModel = {
        Coefficient : float
        Intercept : float
    }

    type DemandModelParameters = {
        ConditionOffsets : Map<Condition, float>
        TemperatureModel : TemperatureModel
    }

    [<CLIMutable>]
    type WeatherInput = {
        Temperature : single
        Condition : string
    }

    [<CLIMutable>]
    type DemandPrediction = {
      [<ColumnName("Score")>]
      Demand : single
    }

    type DayDemand = DayDemand of Map<Food, Demand>
    type InventoryPlan = InventoryPlan of Map<Food, Inventory>

    type IDemandPredictor =
        abstract member predict : weather:Weather -> DemandRate

    type IInventoryOptimizer =
        abstract member plan : demandRates: Map<Food, DemandRate> -> Map<Food, Inventory>

    type InventoryOptimizerConfig = {
        Revenue : Map<Food, float<USD/serving>>
        Storage : Map<Food, float<cm^3/serving>>
        FridgeSpace : Map<Food, float<cm^3/serving>>
        Weight : Map<Food, float<gm/serving>>
        MaxStorage : float<cm^3>
        MaxWeight : float<gm>
        MaxFridgeSpace : float<cm^3>
        MaxItemCount : int<serving>
    }

module Condition =

    open Types

    let ofString (s: string) =
        match s.ToUpper() with
        | "SUNNY" -> Sunny
        | "RAINY"-> Rainy
        | "CLOUDY" -> Cloudy
        | _ -> invalidArg (nameof s) "Invalid string for Condition"

    let toString (c: Condition) =
        match c with
        | Sunny -> "Sunny"
        | Rainy -> "Rainy"
        | Cloudy -> "Cloudy"


module WeatherInput =

    open Types

    let ofWeather (weather: Weather) : WeatherInput =
        {
            Temperature = 
                let (Temperature t) = weather.Temperature
                single t
            Condition = Condition.toString weather.Condition
        }


module DemandSimulationRecord =

    open Types

    let ofDemandSimulationResult (d: DemandSimulationResult) =

        {
            Day = 
                let (Day d) = d.Day
                d
            Temperature = 
                let (Temperature t) = d.Temperature
                t
            Condition = 
                match d.Condition with
                | Sunny -> "Sunny"
                | Cloudy -> "Cloudy"
                | Rainy -> "Rainy"
            Demand = 
                let (Demand d) = d.Demand
                float d
        }

    let importFile (filePath: string) =
        let engine = FileHelpers.FileHelperEngine<DemandSimulationRecord>()
        engine.ReadFile filePath


module DemandSimulationResult =

    open Types

    let create (day: Day) (weather: Weather) (demand: Demand) : DemandSimulationResult =
        {
            Day = day
            Temperature = weather.Temperature
            Condition = weather.Condition
            Demand = demand
        }

    let ofDemandSimulationRecord (record: DemandSimulationRecord) : DemandSimulationResult =
        {
            Day = Day record.Day
            Temperature = Temperature record.Temperature
            Condition = Condition.ofString record.Condition
            Demand = Demand (record.Demand * 1.0<serving>)
        }

    let demand (d: DemandSimulationResult) =
        d.Demand

    let day (d: DemandSimulationResult) =
        d.Day

    let fromFile (DataFile filePath) =
        DemandSimulationRecord.importFile filePath
        |> Array.map ofDemandSimulationRecord


module Sales =

    open Types

    let calculate (Inventory inventory) (Demand demand) =
        
        1.0<serving> * System.Math.Min (float demand, float inventory) 
        |> Sales


module RevenueModel =

    open Types

    let evaluate (revenuePerServing: Map<Food, RevenuePerServing>) (inventoryPlan: Map<Food, Inventory>) (demand: Map<Food, Demand>) =
        inventoryPlan
        |> Map.toSeq
        |> Seq.sumBy (fun (food, inventory) -> revenuePerServing.[food] * (Sales.calculate inventory demand.[food]))


module Simulation =

    open System
    open MathNet.Numerics.Distributions
    open MathNet.Numerics.Statistics
    open Spectre.Console
    open FileHelpers
    open Types


    module Condition =

        let private conditions = 
            [
                0, Sunny
                1, Cloudy
                2, Rainy
            ] |> Map

        let sample (rng: System.Random) =
            conditions.[rng.Next(0, 2)]


    module Temperature =

        let sample (rng: Random) (Temperature minTemperature) (Temperature maxTemperature) =
            rng.NextDouble() * (maxTemperature - minTemperature) + minTemperature
            |> Temperature


    module Weather =

        let sample (rng: Random) (minTemperature: Temperature) (maxTemperature: Temperature) : Weather =
            let condition = Condition.sample rng
            let temperature = Temperature.sample rng minTemperature maxTemperature
            {
                Condition = condition
                Temperature = temperature
            }

    module Demand =

        let sample (rng: Random) (parameters: DemandModelParameters) (weather: Weather) =
            let (Temperature t) = weather.Temperature
            let lambda = parameters.ConditionOffsets.[weather.Condition] + 
                         parameters.TemperatureModel.Intercept + 
                         t * parameters.TemperatureModel.Coefficient
            let result = Poisson.Sample (rng, lambda)
            Demand (float result * 1.0<serving>)


module Training =

    open Microsoft.ML
    open Microsoft.ML.Data
    open Types

    let private reportMetrics (metrics: RegressionMetrics) =
        // show the metrics
        printfn "Model metrics:"
        printfn "  RMSE:%f" metrics.RootMeanSquaredError
        printfn "  MSE: %f" metrics.MeanSquaredError
        printfn "  MAE: %f" metrics.MeanAbsoluteError


    let train (OutputDirectory outputDir) (modelName: string) (inputFile) =
        let context = MLContext()
        let dataView = context.Data.LoadFromTextFile<DemandData> (inputFile, hasHeader = true, separatorChar = ',')
        let partitions = context.Data.TrainTestSplit(dataView, testFraction = 0.2)
        let pipeline = 
            EstimatorChain()
                .Append(context.Transforms.Categorical.OneHotEncoding("Condition"))
                .Append(context.Transforms.NormalizeMeanVariance("Temperature"))
                .Append(context.Transforms.Concatenate("Features", "Condition", "Temperature"))
                .Append(context.Regression.Trainers.LbfgsPoissonRegression())
        
        let model = partitions.TrainSet |> pipeline.Fit
        let metrics = partitions.TestSet |> model.Transform |> context.Regression.Evaluate
        reportMetrics metrics |> ignore

        System.IO.Directory.CreateDirectory outputDir |> ignore
        let outputFile = $"{outputDir}/%O{modelName}.zip"
        context.Model.Save (model, dataView.Schema, outputFile)

        ModelFile outputFile


module Scoring =

    open Microsoft.ML
    open Types

    let score (ModelFile modelFile) (weather: Weather) =
        let weatherInput = WeatherInput.ofWeather weather
        let context = MLContext()
        let model, _ = context.Model.Load(modelFile)
        let predictionEngine = context.Model.CreatePredictionEngine<WeatherInput, DemandPrediction>(model)
        let prediction = predictionEngine.Predict weatherInput
        DemandRate (float prediction.Demand)

    let createPredictor (ModelFile modelFile) : IDemandPredictor =
        let context = MLContext()
        let model, _ = context.Model.Load(modelFile)
        let predictionEngine = context.Model.CreatePredictionEngine<WeatherInput, DemandPrediction>(model)
        { new IDemandPredictor with
            member _.predict weather = 
                let weatherInput = WeatherInput.ofWeather weather
                let prediction = predictionEngine.Predict weatherInput
                DemandRate (float prediction.Demand)
        }


module Save =

    open FileHelpers
    open Types

    module DemandSimulationResult =

        let toCSV (filePath: string) (demands: seq<DemandSimulationResult>) =
            let records =
                demands
                |> Seq.map DemandSimulationRecord.ofDemandSimulationResult

            let engine = FileHelperEngine<DemandSimulationRecord>()
            engine.HeaderText <- engine.GetFileHeader()
            engine.WriteFile(filePath, records)

module PlanOptimizer =

    open MathNet.Numerics.Distributions
    open Flips
    open Flips.Types
    open Flips.SliceMap
    open Flips.UnitsOfMeasure
    open Types

    let private createIncrementProbability
        (foodDemands: Map<Food, DemandRate>)
        (maxItemCount: int) =

        seq {
            for KeyValue(food, DemandRate demandRate) in foodDemands do
                for i in 1..maxItemCount ->
                    let probability =  1.0 - (Poisson.CDF (demandRate - 1.0, (float i)))
                    (food, NthItem i), probability
        } |> SMap2


    let private createModel 
        (revenue: SMap<Food, float<USD/serving>>)
        (storage: SMap<Food, float<cm^3/serving>>)
        (fridgeSpace: SMap<Food, float<cm^3/serving>>)
        (weight: SMap<Food, float<gm/serving>>)
        (incrementProbability: SMap2<Food, NthItem, float>)
        (packDecision: SMap2<Food, NthItem, Decision<serving>>)
        (maxStorage: float<cm^3>)
        (maxWeight: float<gm>)
        (maxFridgeSpace: float<cm^3>) =

        let weightConstraint =
            Constraint.create "MaxWeight" (sum (weight .* packDecision) <== maxWeight)

        let storageConstraint =
            Constraint.create "MaxStorage" (sum (storage .* packDecision) <== maxStorage)

        let fridgeSpaceConstraint =
            Constraint.create "MaxFridgeSpace" (sum (fridgeSpace .* packDecision) <== maxFridgeSpace)

        let revenueExpectation =
            sum (revenue .* incrementProbability .* packDecision)

        let objective =
            Objective.create "MaxRevenueExpectation" Maximize revenueExpectation


        Model.create objective
        |> Model.addConstraint weightConstraint
        |> Model.addConstraint storageConstraint
        |> Model.addConstraint fridgeSpaceConstraint


    let private plan (config: InventoryOptimizerConfig) (demandRates: Map<Food, DemandRate>) =
        let maxItemCount = int config.MaxItemCount
        let incrementProbabilities = createIncrementProbability demandRates maxItemCount

        let packDecisions =
            DecisionBuilder<serving> "Pack" {
                for food in foods do
                    for increment in ([1..maxItemCount] |> List.map NthItem) ->
                        Boolean
            } |> SMap2

        let revenue = SMap config.Revenue
        let storage = SMap config.Storage
        let fridgeSpace = SMap config.FridgeSpace
        let weight = SMap config.Weight
        let planModel = createModel revenue storage fridgeSpace weight incrementProbabilities packDecisions config.MaxStorage config.MaxWeight config.MaxFridgeSpace

        let result = Solver.solve Settings.basic planModel

        match result with
        | Optimal solution ->
            let burgerQuantity = Solution.evaluate solution (sum packDecisions.[Burger, All])
            let pizzaQuantity = Solution.evaluate solution (sum packDecisions.[Pizza, All])
            let tacoQuantity = Solution.evaluate solution (sum packDecisions.[Taco, All])

            Map [
                Burger, Inventory burgerQuantity
                Pizza, Inventory pizzaQuantity
                Taco, Inventory tacoQuantity
            ]

        | _ -> failwith "Failed to solve"

    let create (config: InventoryOptimizerConfig) =
        { new IInventoryOptimizer with
            member _.plan demandRates = plan config demandRates
        }

module Examples =

    open Types

    let predictExample (salesModels: Map<Food, ModelFile>) =

        let weatherSample : Weather = {
            Condition = Cloudy
            Temperature = Temperature 80.0
        }

        let salesPredictions =
            salesModels
            |> Map.map (fun food modelFile -> Scoring.score modelFile weatherSample)

        printfn "%A" salesPredictions


open Types
open Spectre.Console


// Parameters for generating samples data
let burgerParameters = {
    ConditionOffsets = [ Sunny, -30.0; Cloudy, 0.0; Rainy, 30.0 ] |> Map
    TemperatureModel = {
         Intercept = 337.5
         Coefficient = 3.5
     }
}

let pizzaParameters = {
    ConditionOffsets = [ Sunny, 0.0; Cloudy, 80.0; Rainy, -80.0 ] |> Map
    TemperatureModel = {
         Intercept = 690.0
         Coefficient = 2.8
     }
}

let tacoParameters = {
    ConditionOffsets = [ Sunny, 40.0; Cloudy, -40.0; Rainy, 0.0 ] |> Map
    TemperatureModel = {
         Intercept = 400.0
         Coefficient = 4.0
     }
}

// Setting the random number seed to perform reliable re-runs
let rng = System.Random(123)
// Number of days for which to generate data
let numberOfDays = 100
// Where to save the data to
let outputDirectory = OutputDirectory "./PastSalesData"
let minTemp = Temperature 40.0
let maxTemp = Temperature 110.0
let pastDays =
    [1..numberOfDays]
    |> List.map Day

let pastWeather =
    pastDays
    |> List.map (fun day -> {| Day = day; Weather = Simulation.Weather.sample rng minTemp maxTemp |})
    
let burgerDemand =
    pastWeather
    |> List.map (fun d -> {| d with Demand = Simulation.Demand.sample rng burgerParameters d.Weather |})
    |> List.map (fun d -> DemandSimulationResult.create d.Day d.Weather d.Demand )

let burgerDemandDataFile = "burger_demands.csv"
Save.DemandSimulationResult.toCSV burgerDemandDataFile burgerDemand

let burgerDemandModelFile =
    Training.train outputDirectory "BurgerDemandModel" burgerDemandDataFile

let weatherSample : Weather = {
        Condition = Cloudy
        Temperature = Temperature 80.0
    }

let burgerPredictionExample =
    Scoring.score burgerDemandModelFile weatherSample

let pizzaDemand =
    pastWeather
    |> List.map (fun d -> {| d with Demand = Simulation.Demand.sample rng pizzaParameters d.Weather |})
    |> List.map (fun d -> DemandSimulationResult.create d.Day d.Weather d.Demand )

let pizzaDemandDataFile = "pizza_demands.csv"
Save.DemandSimulationResult.toCSV pizzaDemandDataFile pizzaDemand

let pizzaDemandModelFile =
    Training.train outputDirectory "PizzaDemandModel" pizzaDemandDataFile

let tacoDemand =
    pastWeather
    |> List.map (fun d -> {| d with Demand = Simulation.Demand.sample rng tacoParameters d.Weather |})
    |> List.map (fun d -> DemandSimulationResult.create d.Day d.Weather d.Demand )

let tacoDemandDataFile = "taco_demands.csv"
Save.DemandSimulationResult.toCSV tacoDemandDataFile tacoDemand

let tacoDemandModelFile =
    Training.train outputDirectory "TacoDemandModel" tacoDemandDataFile

let numberFutureDays = 30

let burgerPredictor = Scoring.createPredictor burgerDemandModelFile
let pizzaPredictor = Scoring.createPredictor pizzaDemandModelFile
let tacoPredictor = Scoring.createPredictor tacoDemandModelFile


// Let's create some future data that we will evaluate our different techniques against
let futureDays =
    [1..numberFutureDays]
    |> List.map (fun d -> {| Day = Day d; Weather = Simulation.Weather.sample rng minTemp maxTemp |})
    |> List.map (fun d -> {| d with FoodDemand =  Map [ Burger, Simulation.Demand.sample rng burgerParameters d.Weather
                                                        Pizza, Simulation.Demand.sample rng pizzaParameters d.Weather
                                                        Taco, Simulation.Demand.sample rng tacoParameters d.Weather
                                                      ] |})

let revenuePerServing =
    Map [
        Burger, RevenuePerServing 1.3<USD/serving>
        Pizza, RevenuePerServing 1.6<USD/serving>
        Taco, RevenuePerServing 1.4<USD/serving>
    ]

let simpleHueristicPlan =
    Map [
        Burger, Inventory 0.0<serving>
        Pizza, Inventory 900.0<serving>
        Taco, Inventory 466.0<serving>
    ]

let simpleHeuristicRevenue =
    futureDays
    |> List.sumBy (fun d -> RevenueModel.evaluate revenuePerServing simpleHueristicPlan d.FoodDemand)

let optimizedPlan =
    Map [
        Burger, Inventory 572.0<serving>
        Pizza, Inventory 355.0<serving>
        Taco, Inventory 669.0<serving>
    ]

let optimizedPlanRevenue =
    futureDays
    |> List.sumBy (fun d -> RevenueModel.evaluate revenuePerServing optimizedPlan d.FoodDemand)


let optimizerConfig = {
    Revenue = 
        Map [
            Burger, 1.3<USD/serving>
            Pizza,  1.6<USD/serving>
            Taco,   1.4<USD/serving>
        ]
    Storage =
        Map [
            Burger, 700.0<cm^3/serving>
            Pizza,  950.0<cm^3/serving>
            Taco,   800.0<cm^3/serving>
        ]
    FridgeSpace =
        Map [
            Burger, 900.0<cm^3/serving>
            Pizza,  940.0<cm^3/serving>
            Taco,   850.0<cm^3/serving>
        ]
    Weight =
        Map [
            Burger, 550.0<gm/serving>
            Pizza,  800.0<gm/serving>
            Taco,   600.0<gm/serving>
        ]
    MaxWeight = 1_000_000.0<gm>
    MaxStorage = 3_000_000.0<cm^3>
    MaxFridgeSpace = 2_000_000.0<cm^3>
    MaxItemCount = 1_000<serving>
}

let planOptimizer = PlanOptimizer.create optimizerConfig 

let predictorPlusOptimizerRevenue =
    futureDays
    |> List.map (fun d -> {| d with DemandRates = Map [ Burger, burgerPredictor.predict d.Weather
                                                        Pizza, pizzaPredictor.predict d.Weather
                                                        Taco, tacoPredictor.predict d.Weather
                                                      ] |})
    |> List.map (fun d -> {| d with Plan = planOptimizer.plan d.DemandRates |})
    |> List.sumBy (fun d -> RevenueModel.evaluate revenuePerServing d.Plan d.FoodDemand)


let table = Table()
table.AddColumn("Simple Heuristic")
table.AddColumn("Optimizaiton")
table.AddColumn("Optimizaiton + Prediction")

table.AddRow( $"%O{simpleHeuristicRevenue}", $"%O{optimizedPlanRevenue}", $"%O{predictorPlusOptimizerRevenue}")
AnsiConsole.Render(table)