#r "nuget: Flips,2.4.5"

open Flips
open Flips.Types
open Flips.UnitsOfMeasure

fsi.AddPrinter<LinearExpression<_>> (fun x -> "Expression")
fsi.AddPrinter<Objective<_>> (fun x -> "Objective")
fsi.AddPrinter<Constraint> (fun x -> "Constraint")
fsi.AddPrinter<model> (fun x -> "Model")

type Truck = Truck of string
type City = City of string
[<Measure>] type Dose
[<Measure>] type Crate
[<Measure>] type USD
[<Measure>] type Load

let dosePerCrate = 100.0<Dose/Crate>

let trucks = [
  Truck "TruckA"
  Truck "TruckB"
  Truck "TruckC"
  Truck "TruckD"
  Truck "TruckE"
  Truck "TruckF"
  Truck "TruckG"
]

let cities = [
  City "Portland"
  City "Seattle"
  City "Los Angeles"
]

let demand = 
  Map [
    City "Portland",    10_000.0<Dose>
    City "Seattle",     15_000.0<Dose>
    City "Los Angeles", 18_000.0<Dose>
  ]

let capacity = 
  Map [
    Truck "TruckA", 7_000.0<Crate/Load>
    Truck "TruckB", 9_000.0<Crate/Load>
    Truck "TruckC", 7_000.0<Crate/Load>
    Truck "TruckD", 5_000.0<Crate/Load>
    Truck "TruckE", 6_000.0<Crate/Load>
    Truck "TruckF", 8_000.0<Crate/Load>
    Truck "TruckG", 6_000.0<Crate/Load>
  ]

let costs = 
  dict [
    (City "Portland"    , Truck "TruckA"), 12_000.0<USD/Load>
    (City "Portland"    , Truck "TruckB"), 13_000.0<USD/Load>
    (City "Portland"    , Truck "TruckC"), 13_000.0<USD/Load>
    (City "Portland"    , Truck "TruckD"), 10_000.0<USD/Load>
    (City "Portland"    , Truck "TruckE"), 18_000.0<USD/Load>
    (City "Portland"    , Truck "TruckF"), 20_000.0<USD/Load>
    (City "Portland"    , Truck "TruckG"), 19_000.0<USD/Load>
    (City "Seattle"     , Truck "TruckA"), 20_000.0<USD/Load>
    (City "Seattle"     , Truck "TruckB"), 18_000.0<USD/Load>
    (City "Seattle"     , Truck "TruckC"), 18_000.0<USD/Load>
    (City "Seattle"     , Truck "TruckD"), 20_000.0<USD/Load>
    (City "Seattle"     , Truck "TruckE"), 18_000.0<USD/Load>
    (City "Seattle"     , Truck "TruckF"), 15_000.0<USD/Load>
    (City "Seattle"     , Truck "TruckG"), 12_000.0<USD/Load>
    (City "Los Angeles" , Truck "TruckA"), 11_000.0<USD/Load>
    (City "Los Angeles" , Truck "TruckB"), 20_000.0<USD/Load>
    (City "Los Angeles" , Truck "TruckC"), 18_000.0<USD/Load>
    (City "Los Angeles" , Truck "TruckD"), 10_000.0<USD/Load>
    (City "Los Angeles" , Truck "TruckE"), 12_000.0<USD/Load>
    (City "Los Angeles" , Truck "TruckF"), 20_000.0<USD/Load>
    (City "Los Angeles" , Truck "TruckG"), 17_000.0<USD/Load>
  ]

let assignment =
  DecisionBuilder<Load> "Assign" {
    for t in trucks do
    for c in cities ->
      Boolean
  } |> dict

let singleAssignmentConstraints =
  ConstraintBuilder "SingleAssignment" {
    for t in trucks ->
      let totalAssignments = 
        List.sum [for c in cities -> 1.0 * assignment.[t, c]]
      totalAssignments <== 1.0<Load>
  }

let cityDemandConstraints =
  ConstraintBuilder "CityDemand" {
    for c in cities ->
      let totalSupply = 
        List.sum [for t in trucks -> dosePerCrate * capacity.[t] * assignment.[t, c]]
      totalSupply >== demand.[c]
  }

let objectiveFunction =
  List.sum [
    for c in cities do
      for t in trucks ->
        costs.[c, t] * assignment.[t, c]
  ]

let objective =
  Objective.create "MinimizeCost" Minimize objectiveFunction

let model =
  Model.create objective
  |> Model.addConstraints singleAssignmentConstraints
  |> Model.addConstraints cityDemandConstraints

let result =
  Solver.solve Settings.basic model