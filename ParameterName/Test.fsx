open Microsoft.FSharp.Quotations
open Microsoft.FSharp.Quotations.Patterns

let velocity = 5

let fn (e:Expr) =
  match e with
    | PropertyGet (e, pi, li) -> pi.Name
    | _ -> failwith "not a let-bound value"

let name = fn <@velocity@> 

printfn "%s" name

let ChickenBuilder size =

type ChickenBuilder () =
    