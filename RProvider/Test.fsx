#r "nuget: RProvider"

open RProvider
open RProvider.stats
open RDotNet

fsi.AddPrinter (fun (synexpr:RDotNet.SymbolicExpression) -> synexpr.Print())

let x = 40.0

let summary = R.binom_test(x, 100.0, 0.5, alternative="two.sided")
summary.AsList().Names
let pValue = summary.AsList().["p.value"].GetValue<float>()