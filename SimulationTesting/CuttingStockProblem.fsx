#r "nuget: Flips"

open Flips
open Flips.Types
open Flips.SliceMap

type Width = Width of float
type RollDemand = {
    Width : Width
    Quantity : float
}

let widths = [
    Width 1380.0
    Width 1520.0
    Width 1560.0
    Width 1710.0
    Width 1820.0
    Width 1880.0
    Width 1930.0
    Width 2000.0
    Width 2050.0
    Width 2100.0
    Width 2140.0
    Width 2150.0
    Width 2200.0
]

let rollDemands = [
    { Width = Width 1380.0; Quantity = 22.0 }
    { Width = Width 1520.0; Quantity = 25.0 }
    { Width = Width 1560.0; Quantity = 12.0 }
    { Width = Width 1710.0; Quantity = 14.0 }
    { Width = Width 1820.0; Quantity = 18.0 }
    { Width = Width 1880.0; Quantity = 18.0 }
    { Width = Width 1930.0; Quantity = 20.0 }
    { Width = Width 2000.0; Quantity = 10.0 }
    { Width = Width 2050.0; Quantity = 12.0 }
    { Width = Width 2100.0; Quantity = 14.0 }
    { Width = Width 2140.0; Quantity = 16.0 }
    { Width = Width 2150.0; Quantity = 18.0 }
    { Width = Width 2200.0; Quantity = 20.0 }
]

let combinations =

    let rec combos (minTotal: float) (maxTotal: float) (accumulatedWidths: Width list) (remainingWidths: Width list) =
        match remainingWidths with
        | [] ->
            if List.sumBy (fun (Width w) -> w) accumulatedWidths >= minTotal then
                Some accumulatedWidths
            else
                None
        | head::tail ->
            let newAccumulatedWidths = [head] @ accumulatedWidths
            if List.sumBy (fun (Width w) -> w) newAccumulatedWidths <= maxTotal then
                combos minTotal maxTotal newAccumulatedWidths remainingWidths
            else
                combos minTotal maxTotal accumulatedWidths tail

    combos 0.0 5_600.0 [Width 1380.0] widths.[6..]
