#r "nuget: Flips,2.4.5"

open Flips
open Flips.Types
// open Flips.UnitsOfMeasure

let trucks = [
  "TruckA"
  "TruckB"
  "TruckC"
  "TruckD"
  "TruckE"
  "TruckF"
  "TruckG"
]

let cities = [
  "Portland"
  "Seattle"
  "Los Angeles"
]

let demand = 
  Map [
    "Portland",    10_000.0
    "Seattle",     15_000.0
    "Los Angeles", 18_000.0
  ]

let capacity = 
  Map [
    "TruckA", 7_000.0
    "TruckB", 9_000.0
    "TruckC", 7_000.0
    "TruckD", 5_000.0
    "TruckE", 6_000.0
    "TruckF", 8_000.0
    "TruckG", 6_000.0
  ]

let costs = 
  Map [
    ("Portland"  , "TruckA"),   12_000.0
    ("Portland"  , "TruckB"),   13_000.0
    ("Portland"  , "TruckC"),   13_000.0
    ("Portland"  , "TruckD"),   10_000.0
    ("Portland"  , "TruckE"),   18_000.0
    ("Portland"  , "TruckF"),   20_000.0
    ("Portland"  , "TruckG"),   19_000.0
    ("Seattle"   , "TruckA"),   20_000.0
    ("Seattle"   , "TruckB"),   18_000.0
    ("Seattle"   , "TruckC"),   18_000.0
    ("Seattle"   , "TruckD"),   20_000.0
    ("Seattle"   , "TruckE"),   18_000.0
    ("Seattle"   , "TruckF"),   15_000.0
    ("Seattle"   , "TruckG"),   12_000.0
    ("Los Angeles" , "TruckA"), 11_000.0
    ("Los Angeles" , "TruckB"), 20_000.0
    ("Los Angeles" , "TruckC"), 18_000.0
    ("Los Angeles" , "TruckD"), 10_000.0
    ("Los Angeles" , "TruckE"), 12_000.0
    ("Los Angeles" , "TruckF"), 20_000.0
    ("Los Angeles" , "TruckG"), 17_000.0
  ]

let assignment =
  DecisionBuilder "Assign" {
    for t in trucks do
    for c in cities ->
      Boolean
  } |> Map

let truckAssignedOnceConstraints =
  ConstraintBuilder "SingleAssignment" {
    for t in trucks ->
      let totalAssignments = 
        List.sum 
          [for c in cities -> 
            1.0 * assignment.[t, c]]
      
      totalAssignments <== 1.0
  }

let cityDemandMetConstraints =
  ConstraintBuilder "CityDemand" {
    for c in cities ->
      let totalSupply = 
        List.sum 
          [for t in trucks -> 
            capacity.[t] * assignment.[c, t]]

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
  |> Model.addConstraints truckAssignedOnceConstraints
  |> Model.addConstraints cityDemandMetConstraints

let result =
  Solver.solve Settings.basic model