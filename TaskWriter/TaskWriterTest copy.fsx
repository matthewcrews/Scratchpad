open System
open System.Threading
open System.Collections.Generic

let fullBufferCount = 100
let channel = Channels.Channel.CreateUnbounded()
let writer = channel.Writer
let reader = channel.Reader
let buffer = ResizeArray()
let tokenSource = CancellationTokenSource()
let ct = tokenSource.Token
let file = "Output.txt"

let writingTask = task {
    while not ct.IsCancellationRequested do
        let! nextItem = reader.ReadAsync ct
        buffer.Add nextItem

        if buffer.Count > fullBufferCount then
            let outputString = buffer |> Seq.map string
            do! System.IO.File.AppendAllLinesAsync (file, outputString)
            buffer.Clear()

    // Finalization step
    printfn "Writing Final Values"
    let outputString = buffer |> Seq.map string
    do! System.IO.File.AppendAllLinesAsync (file, outputString)
    buffer.Clear()
}

for next in 1..10 do
    printfn $"Next Value: {next}"
    // Not sure how to write sync
    (writer.WriteAsync (next, ct)).AsTask().Wait()
    System.Threading.Thread.Sleep 100

tokenSource.Cancel()