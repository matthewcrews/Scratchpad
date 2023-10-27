#r "nuget: Dagre.NET, 1.0.0.6"

open Dagre

let dg = DagreInputGraph()
dg.VerticalLayout <- false
let n1 = dg.AddNode("Chicken", 100.0f, 50.0f)
let n2 = dg.AddNode("Turkey", 100.0f, 50.0f)
let n3 = dg.AddNode("Goose", 100.0f, 50.0f)
let e1 = dg.AddEdge(n1, n2)
let e2 = dg.AddEdge(n1, n3)
dg.Layout()

n1
n2
n3
e1

for pt in e1.Points do
    printfn $"{pt.X}, {pt.Y}"

for pt in e2.Points do
    printfn $"{pt.X}, {pt.Y}"


e2