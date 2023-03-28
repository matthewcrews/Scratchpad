open System
open System.Threading
open System.Collections.Generic

// Clear output file
let file = "Output.txt"
if IO.File.Exists file then
    IO.File.Delete file

[<Literal>]
let MAX_BUFFER_SIZE = 10
[<Literal>]
let BUFFER_COUNT = 4

let sendChannel = Channels.Channel.CreateBounded<ResizeArray<int>> BUFFER_COUNT
let sendWriter = sendChannel.Writer
let sendReader = sendChannel.Reader

let returnChannel = Channels.Channel.CreateBounded BUFFER_COUNT
let returnWriter = returnChannel.Writer
let returnReader = returnChannel.Reader
let buffers = Stack<ResizeArray<int>>()

for _ in 1..BUFFER_COUNT do
    let b = ResizeArray MAX_BUFFER_SIZE
    buffers.Push b

let tokenSource = new CancellationTokenSource()
let ct = tokenSource.Token

let writingTask = backgroundTask {
    let mutable notFinished = true

    while notFinished do
        let! hasNewRecord = sendReader.WaitToReadAsync ct
        if hasNewRecord then
            printfn "Writing out buffer"
            let! nextBuffer = sendReader.ReadAsync ()
            let outputString = nextBuffer |> Seq.map string
            do! System.IO.File.AppendAllLinesAsync (file, outputString)
            nextBuffer.Clear()
            do! returnWriter.WriteAsync nextBuffer

        else
            notFinished <- false
}

let mutable curBuffer = buffers.Pop()

for next in 1..100 do
    printfn $"Next Value: {next}"

    curBuffer.Add next

    if curBuffer.Count >= MAX_BUFFER_SIZE then
        let sendBufferTask = (sendWriter.WriteAsync curBuffer).AsTask()
        sendBufferTask.Wait()

        if buffers.Count > 0 then
            curBuffer <- buffers.Pop()
        else
            let getBufferTask = task {
                let! nextBuffer = returnReader.ReadAsync()
                curBuffer <- nextBuffer
            }
            getBufferTask.Wait()

if curBuffer.Count > 0 then
    let sendBufferTask = (sendWriter.WriteAsync curBuffer).AsTask()
    sendBufferTask.Wait()

sendWriter.Complete()
System.Threading.Thread.Sleep 2000
writingTask.Wait()