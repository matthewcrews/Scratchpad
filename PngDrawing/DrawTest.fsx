open System

fsi.ShowDeclarationValues <- false
fsi.ShowProperties <- false
fsi.ShowIEnumerable <- false

let height = 10
let width = 15

let world = Array2D.create height width 0

for x = 0 to height - 1 do
    for y = 0 to width - 1 do
        Console.Write world.[x, y]

    Console.WriteLine()

Console.SetCursorPosition (0, 2)
Console.Write ("X")
Console.Clear()

