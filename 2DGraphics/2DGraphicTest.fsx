// #r "nuget: SixLabors.ImageSharp.Drawing, 1.0.0-beta11"

// open SixLabors.ImageSharp

// // let bm = BitMap()

// // let context = ImageContext()

// let image = new Image<SixLabors.ImageSharp.PixelFormats.Rg32>(600, 400)

// image.

#r "nuget: FSharp.Charting"
#r "nuget: System.Windows.Forms.DataVisualization, 1.0.0-prerelease.20110.1"

open System
open System.Windows.Forms
open FSharp.Charting

let rnd = new Random()
let rand() = rnd.NextDouble()
let randomPoints = [ for i in 0 .. 1000 -> rand(), rand() ]
Chart.Point randomPoints

open System

let block = "█"
Console.ForegroundColor <- ConsoleColor.Blue
printfn "%A" block