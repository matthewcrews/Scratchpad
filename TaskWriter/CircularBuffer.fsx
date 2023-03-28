open System
open System.Threading
open System.Collections.Generic

let fullBufferCount = 2

let channel = Channels.Channel.CreateUnbounded()
let writer = channel.Writer
let reader = channel.Reader
let buffers = Dictionary<int, ResizeArray<int>>()
let tokenSource = new CancellationTokenSource()
let ct = tokenSource.Token

let writingTask = task {
    let mutable notFinished = true
    while notFinished do
      let! _notFinished = reader.WaitToReadAsync(ct)
      notFinished <- _notFinished
      if _notFinished then
        let! nextItem = reader.ReadAsync()
        printfn $"receiving {nextItem}"
        // let key = nextItem % 10
        let key = 1
        let buffer =
            match buffers.TryGetValue key with
            | true, b -> b
            | false, _ ->
                let newBuffer = ResizeArray()
                buffers[key] <- newBuffer
                newBuffer

        buffer.Add nextItem

        if buffer.Count > fullBufferCount then
            let file = $"Output_{key}.txt"
            let outputString = buffer |> Seq.map string
            do! System.IO.File.AppendAllLinesAsync (file, outputString, ct)
            buffer.Clear()

    // Finalization loop
    printfn "Writing Final Values"
    for KeyValue (key, buffer) in buffers do
        let file = $"Output_{key}.txt"
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