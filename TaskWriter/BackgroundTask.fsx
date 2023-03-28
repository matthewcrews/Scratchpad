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

let writingTask = backgroundTask {
    let mutable notFinished = true

    while notFinished do
        let! hasNewRecord = reader.WaitToReadAsync ct
        if hasNewRecord then
            let! nextItem = reader.ReadAsync ()
            printfn $"Read: {nextItem}"
            buffer.Add nextItem
            if buffer.Count > fullBufferCount then
                let outputString = buffer |> Seq.map string
                do! System.IO.File.AppendAllLinesAsync (file, outputString)
                buffer.Clear()

        else
            notFinished <- false

    // Finalization step
    printfn "Writing Final Values"
    let outputString = buffer |> Seq.map string
    do! System.IO.File.AppendAllLinesAsync (file, outputString)
    buffer.Clear()
}

for next in 1..10 do
    printfn $"Next Value: {next}"
    // Not sure how to write sync
    let _ = writer.WriteAsync (next, ct)
    System.Threading.Thread.Sleep 100

writer.Complete()
System.Threading.Thread.Sleep 2000
writingTask.Wait()