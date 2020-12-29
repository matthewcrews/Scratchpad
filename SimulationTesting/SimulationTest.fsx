#r "nuget: Microsoft.ML"
#r "nuget: MathNet.Numerics.FSharp"
#r "nuget: Spectre.Console"
#r "nuget: FileHelpers"


module Types =

    open FileHelpers
    open Microsoft.ML.Data

    [<Measure>] type USD
    [<Measure>] type cm
    [<Measure>] type gm
    [<Measure>] type serving

    type Demand = Demand of int
    type Sales = Sales of int
    type Inventory = Inventory of int
    type Revenue = Revenue of float
        with
            static member Zero = Revenue 0.0
            static member (+) (Revenue a, Revenue b) = Revenue (a + b)
    type RevenuePerItem = RevenuePerItem of float
        with
            static member (*) (Sales sales, RevenuePerItem revenuePerItem) =
                (float sales) * revenuePerItem
                |> Revenue

            static member (*) (revenuePerItem: RevenuePerItem, sales: Sales) =
                sales * revenuePerItem
    type NthItem = NthItem of int
    type DemandRate = DemandRate of float
    type Day = Day of int
    type Temperature = Temperature of float
    type ModelFile = ModelFile of string
    type DataFile = DataFile of string

    type Condition =
        | Sunny
        | Cloudy
        | Rainy

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
        Demand : int
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

    type FoodParameters = {
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


module Condition =

    open Types

    let ofString (s: string) =
        match s.ToUpper() with
        | "Sunny" -> Sunny
        | "Rainy"-> Rainy
        | "Cloudy" -> Cloudy
        | _ -> invalidArg (nameof s) "Invalid string for Condition"

    let toString (c: Condition) =
        match c with
        | Sunny -> "Sunny"
        | Rainy -> "Rainy"
        | Cloudy -> "Cloudy"

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
                d
        }

    let importFile (filePath: string) =
        let engine = FileHelpers.FileHelperEngine<DemandSimulationRecord>()
        engine.ReadFile filePath


module DemandSimulationResult =

    open Types

    let create (day: Day) (temperature: Temperature) (condition: Condition) (demand: Demand) : DemandSimulationResult =
        {
            Day = day
            Temperature = temperature
            Condition = condition
            Demand = demand
        }

    let ofDemandSimulationRecord (d: DemandSimulationRecord) : DemandSimulationResult =
        {
            Day = Day d.Day
            Temperature = Temperature d.Temperature
            Condition = Condition.ofString d.Condition
            Demand = Demand d.Demand
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

    let calculate (Demand demand) (Inventory inventory) =
        System.Math.Min (demand, inventory) |> Sales


module RevenueModel =

    open Types

    let evaluate (revenuePerItem: Map<Food, RevenuePerItem>) (sales: Map<Food, Sales>) =
        sales
        |> Map.toSeq
        |> Seq.sumBy (fun (food, sales) -> sales * revenuePerItem.[food] )


module DemandSimulation =

    open System
    open MathNet.Numerics.Distributions
    open MathNet.Numerics.Statistics
    open Spectre.Console
    open FileHelpers
    open Types


    let private conditions = 
        [
            0, Sunny
            1, Cloudy
            2, Rainy
        ] |> Map


    let private demandModel 
        (conditionOffsets: Map<Condition, float>) 
        (temperatureModel: TemperatureModel)
        (rng: Random) 
        (condition: Condition) 
        (Temperature temperature) =
        let lambda = 
            conditionOffsets.[condition] + 
            temperatureModel.Intercept + 
            temperature * temperatureModel.Coefficient
        Poisson.Sample (rng, lambda)
        |> Demand


    let private reportResults (food: Food) (stats: DescriptiveStatistics) =
        let rule = Rule($"{food} Results")
        rule.Alignment <- Justify.Center
        AnsiConsole.Render(rule);

        let table = Table()
        table.AddColumn("Mean") |> ignore
        table.AddColumn("Variance") |> ignore
        table.AddColumn("StdDev") |> ignore

        table.AddRow ([|$"%.2f{stats.Mean}"; $"%.2f{stats.Variance}"; $"%.2f{stats.StandardDeviation}"|]) |> ignore

        AnsiConsole.Render(table)


    let private writeResults (outputDirectory: string) (food: Food) (outputData: seq<DemandSimulationResult>) =
        System.IO.Directory.CreateDirectory outputDirectory |> ignore

        let engine = FileHelperEngine<DemandSimulationResult>()
        engine.HeaderText <- engine.GetFileHeader()
        let outputFile = outputDirectory + IO.Path.DirectorySeparatorChar.ToString() + "Data_" + food.ToString() + ".csv"
        engine.WriteFile(outputFile, outputData)
        outputFile


    let generateDayDemand (rng: Random) (parameters: FoodParameters) (day: Day) =
        let temperature = Temperature (ContinuousUniform.Sample (rng, 60.0, 90.0) |> Math.Truncate)
        let condition = conditions.[DiscreteUniform.Sample (rng, 0, 2)]
        let demand = demandModel parameters.ConditionOffsets parameters.TemperatureModel rng condition temperature
        DemandSimulationResult.create day temperature condition demand


    let generateForNDays 
        (rng: Random)
        (outputDirectory: string)
        (numberOfDays: int)
        (food: Food)
        (parameters: FoodParameters) =

        let demandSimulations =
            seq {1 .. numberOfDays}
            |> Seq.map (Day >> generateDayDemand rng parameters)

        let outputFile = writeResults outputDirectory food demandSimulations

        let stats =
            demandSimulations
            |> Seq.map (fun x -> 
                let (Demand d) = x.Demand
                float d)
            |> DescriptiveStatistics

        reportResults food stats |> ignore

        DataFile outputFile


module Fitting =

    open Microsoft.ML
    open Microsoft.ML.Data
    open Types

    let private trainModel<'Input> (DataFile inputFile) (outputDir: string) (food: Food)  =

        let context = MLContext()
        let dataView = context.Data.LoadFromTextFile<'Input> (inputFile, hasHeader = true, separatorChar = ',')
        let partitions = context.Data.TrainTestSplit(dataView, testFraction = 0.2)
        let pipeline = 
            EstimatorChain()
                .Append(context.Transforms.Categorical.OneHotEncoding("Condition"))
                .Append(context.Transforms.NormalizeMeanVariance("Temperature"))
                .Append(context.Transforms.Concatenate("Features", "Condition", "Temperature"))
                .Append(context.Regression.Trainers.LbfgsPoissonRegression())
        let model = partitions.TrainSet |> pipeline.Fit
        let metrics = partitions.TestSet |> model.Transform |> context.Regression.Evaluate

        // show the metrics
        printfn "Model metrics:"
        printfn "  RMSE:%f" metrics.RootMeanSquaredError
        printfn "  MSE: %f" metrics.MeanSquaredError
        printfn "  MAE: %f" metrics.MeanAbsoluteError

        let outputFile = $"{outputDir}/Model_%O{food}.zip"
        context.Model.Save (model, dataView.Schema, outputFile)

        ModelFile outputFile


    let train outputDir (food: Food) (inputFile) =

        trainModel<DemandData> inputFile outputDir food


module Prediction =

    open Microsoft.ML
    open Types


    let private score (ModelFile modelFile) weatherObservation =
      
      let context = MLContext()
      let model, _ = context.Model.Load(modelFile)
      let predictionEngine = context.Model.CreatePredictionEngine<WeatherInput, DemandPrediction>(model)
      predictionEngine.Predict weatherObservation


    let predict (modelFile: ModelFile) (weather: WeatherInput) =

      let salesPrediction = score modelFile weather
      printfn "%A" salesPrediction

      salesPrediction


module Examples =

    open Types

    let predictExample (salesModels: Map<Food, ModelFile>) =

        let weatherSample : WeatherInput = {
            Condition = "Cloudy"
            Temperature = 80.0f
        }

        let salesPredictions =
            salesModels
            |> Map.map (fun food modelFile -> Prediction.predict modelFile weatherSample)

        printfn "%A" salesPredictions


open Types


// Parameters for generating samples data
let burgerModelData = {
    ConditionOffsets = [ Sunny, -30.0; Cloudy, 0.0; Rainy, 30.0 ] |> Map
    TemperatureModel = {
         Intercept = 337.5
         Coefficient = 3.5
     }
}

let pizzaModelData = {
    ConditionOffsets = [ Sunny, 0.0; Cloudy, 80.0; Rainy, -80.0 ] |> Map
    TemperatureModel = {
         Intercept = 690.0
         Coefficient = 2.8
     }
}

let tacoModelData = {
    ConditionOffsets = [ Sunny, 40.0; Cloudy, -40.0; Rainy, 0.0 ] |> Map
    TemperatureModel = {
         Intercept = 400.0
         Coefficient = 4.0
     }
}

// Map of parameter data for generation and training
let foodModelParameterData = 
    [
        Burger, burgerModelData
        Pizza, pizzaModelData
        Taco, tacoModelData
    ] |> Map

// Setting the random number seed to perform reliable re-runs
let rng = System.Random(123)
// Number of days for which to generate data
let numberOfDays = 100
// Where to save the data to
let outputDirectory = "./PastSalesData"

// Take the parameters for each Food, generate sample data
// and save it to a .csv
let salesData =
    foodModelParameterData
    |> Map.map (DemandSimulation.generateForNDays rng outputDirectory numberOfDays)

// Take the sales data for each Food and train a model
let salesModels =
    salesData
    |> Map.map (Fitting.train outputDirectory)


// An example of scoring using a trained model
Examples.predictExample salesModels

let futureDataDirectory = "./FutureSales"
let numberOfFutureDays = 30
// Generate some sample data for the future to compare the performance
// of the different ordering methods
let futureSalesData =
    foodModelParameterData
    |> Map.map (DemandSimulation.generateForNDays rng futureDataDirectory numberOfFutureDays)


let foodDemandData =
    futureSalesData
    |> Map.map (fun food dataFile -> 
        let d = DemandSimulationResult.fromFile dataFile
        d |> Array.map (fun x -> x.)
        )