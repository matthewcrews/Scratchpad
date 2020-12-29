#r "nuget: Microsoft.ML"
#r "nuget: MathNet.Numerics.FSharp"
#r "nuget: Spectre.Console"
#r "nuget: FileHelpers"


module Types =

    open FileHelpers
    open Microsoft.ML.Data

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

    [<CLIMutable>]
    [<DelimitedRecord(",")>]
    type Output = {
        Day : int
        Temperature : float
        Condition : string
        Sales : float
    }

    [<CLIMutable>]
    type SalesData = {
        [<LoadColumn(1)>] Temperature : single
        [<LoadColumn(2)>] Condition : string
        [<LoadColumn(3); ColumnName("Label")>] Sales : single
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
    type SalesPrediction = {
      [<ColumnName("Score")>] Sales : single
    }


module DataGeneration =

    open System
    open MathNet.Numerics.Distributions
    open MathNet.Numerics.Statistics
    open Spectre.Console
    open FileHelpers
    open Types

    // let rng = Random(123)

    module Output =

        let create (Day day) (Temperature temperature) (condition: Condition) (sales: float) =

            {
                Day = day
                Temperature = temperature
                Condition = 
                    match condition with
                    | Sunny -> "Sunny"
                    | Cloudy -> "Cloudy"
                    | Rainy -> "Rainy"
                Sales = sales
            }

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
        |> float


    let generate 
        (rng: Random)
        (numberOfDays: int)
        (outputDirectory: string)
        (parameters: FoodParameters)
        (food: Food) =

        let days =
            seq {1 .. numberOfDays}
            |> Seq.map (fun d -> 
                let weather = 
                    {| Day = Day d
                       Temperature = Temperature (ContinuousUniform.Sample (rng, 60.0, 90.0) |> Math.Truncate)
                       Condition = conditions.[DiscreteUniform.Sample (rng, 0, 2)]
                    |} 
                {| weather with 
                    Sales = demandModel parameters.ConditionOffsets parameters.TemperatureModel rng weather.Condition weather.Temperature
                |}
              )

        let outputData =
            days
            |> Seq.map (fun d -> Output.create d.Day d.Temperature d.Condition d.Sales)

        let engine = FileHelperEngine<Output>()
        engine.HeaderText <- engine.GetFileHeader()
        let outputFile = outputDirectory + IO.Path.DirectorySeparatorChar.ToString() + "Data_" + food.ToString() + ".csv"
        engine.WriteFile(outputFile, outputData)

        let stats =
            days
            |> Seq.map (fun d -> d.Sales)
            |> DescriptiveStatistics


        let rule = Rule($"{food} Results")
        rule.Alignment <- Justify.Center
        AnsiConsole.Render(rule);

        let table = Table()
        table.AddColumn("Mean") |> ignore
        table.AddColumn("Variance") |> ignore
        table.AddColumn("StdDev") |> ignore

        table.AddRow ([|$"%.2f{stats.Mean}"; $"%.2f{stats.Variance}"; $"%.2f{stats.StandardDeviation}"|]) |> ignore

        AnsiConsole.Render(table)

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


    let train (inputFile) outputDir (food: Food) =

        trainModel<SalesData> inputFile outputDir food


module Prediction =

    open Microsoft.ML
    open Microsoft.ML.Data
    open Types


    let private score (ModelFile modelFile) weatherObservation =
      
      let context = MLContext()
      let model, _ = context.Model.Load(modelFile)
      let predictionEngine = context.Model.CreatePredictionEngine<WeatherInput, SalesPrediction>(model)
      predictionEngine.Predict weatherObservation


    let predict (modelFile: ModelFile) (weather: WeatherInput) =

      let salesPrediction = score modelFile weather
      printfn "%A" salesPrediction

      salesPrediction


open Types

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

let foodModelData = 
    [
        Burger, burgerModelData
        Pizza, pizzaModelData
        Taco, tacoModelData
    ] |> Map


let rng = System.Random(123)
let numberOfDays = 100
let outputDirectory = "."

let salesModels =
    foodModelData
    |> Map.map (fun food parameters -> DataGeneration.generate rng numberOfDays outputDirectory parameters food)
    |> Map.map (fun food dataFile -> Fitting.train dataFile outputDirectory food)

let weatherSample : WeatherInput = {
    Condition = "Cloudy"
    Temperature = 80.0f
}

let salesPredictions =
    salesModels
    |> Map.map (fun food modelFile -> Prediction.predict modelFile weatherSample)

printfn "%A" salesPredictions
