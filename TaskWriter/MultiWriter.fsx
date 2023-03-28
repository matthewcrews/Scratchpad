open System
open System.Threading
open System.Collections.Generic
open System.Threading.Channels

module BackgroundWriter =

    let create (ct: CancellationToken) (reader: ChannelReader<ResizeArray<int>>) (writer: ChannelWriter<ResizeArray<int>>) (outputFile: string) =
        backgroundTask {
            let mutable notFinished = true

            while notFinished do
                let! hasNewRecord = reader.WaitToReadAsync ct
                if hasNewRecord then
                    let! buffer = reader.ReadAsync()
                    let contents =
                        buffer
                        |> Seq.map string
                    do! IO.File.AppendAllLinesAsync (outputFile, contents)
                    buffer.Clear()
                    do! writer.WriteAsync buffer

                else
                    notFinished <- false
        }


type MultiWriter () =

        [<Literal>]
        let MAX_DATA_BUFFER_SIZE = 10
        [<Literal>]
        let DATA_BUFFER_COUNT = 2
        [<Literal>]
        let WRITE_BUFFER_COUNT = 4

        let tokenSource = new CancellationTokenSource()
        let ct = tokenSource.Token

        // Channel for sending data to writer thread
        let sendBufferChannel = Channel.CreateBounded<ResizeArray<int>> DATA_BUFFER_COUNT
        // Channel for retrieving empty buffers
        let returnBufferChannel = Channel.CreateBounded DATA_BUFFER_COUNT
        // Channel to return Write Buffers
        let returnWriteBufferChannel = Channel.CreateBounded<ResizeArray<int>> WRITE_BUFFER_COUNT

        let dataBuffers = Stack<ResizeArray<int>>()

        do for _ in 1..DATA_BUFFER_COUNT do
                let b = ResizeArray MAX_DATA_BUFFER_SIZE
                dataBuffers.Push b

        let getDataBuffer () =
            if dataBuffers.Count > 0 then
                dataBuffers.Pop()
            else
                let getBufferTask = task {
                    return! returnBufferChannel.Reader.ReadAsync()
                }
                getBufferTask.Wait()
                getBufferTask.Result


        let writeBuffers = Stack()
        do for _ in 1 .. WRITE_BUFFER_COUNT do
            writeBuffers.Push (ResizeArray())

        let getWriteBuffer () =
            if writeBuffers.Count > 0 then
                writeBuffers.Pop()
            else
                let getWriteBufferTask = task {
                    return! returnWriteBufferChannel.Reader.ReadAsync()
                }
                getWriteBufferTask.Wait()
                getWriteBufferTask.Result

        let writerChannels =
            Map [
                0, Channel.CreateBounded<ResizeArray<int>> WRITE_BUFFER_COUNT
                1, Channel.CreateBounded<ResizeArray<int>> WRITE_BUFFER_COUNT
            ]

        let backgroundWriters =
            writerChannels
            |> Map.map (fun key channel ->
                let outputFile = $"Result_{key}.txt"
                BackgroundWriter.create ct channel.Reader returnWriteBufferChannel.Writer outputFile)

        let distributeTask = backgroundTask {
            let mutable notFinished = true

            while notFinished do
                let! hasNewRecord = sendBufferChannel.Reader.WaitToReadAsync ct
                if hasNewRecord then
                    let! buffer = sendBufferChannel.Reader.ReadAsync()
                    // Do work here
                    let outputs =
                        Map [
                            0, getWriteBuffer()
                            1, getWriteBuffer()
                        ]

                    for elem in buffer do
                        let partitionKey = elem % 2
                        outputs[partitionKey].Add elem

                    for KeyValue (partitionKey, output) in outputs do
                        (writerChannels[partitionKey].Writer.WriteAsync output)
                            .AsTask()
                            .Wait()

                    buffer.Clear()
                    do! returnBufferChannel.Writer.WriteAsync buffer

                else
                    notFinished <- false
        }

        let mutable curBuffer = dataBuffers.Pop()

        member _.Add (elem: int) =
            printfn $"Received: {elem}"
            curBuffer.Add elem
            if curBuffer.Count >= MAX_DATA_BUFFER_SIZE then
                (sendBufferChannel.Writer.WriteAsync curBuffer)
                    .AsTask()
                    .Wait()

                curBuffer <- getDataBuffer()

        member _.Flush () =
            (sendBufferChannel.Writer.WriteAsync curBuffer)
                .AsTask()
                .Wait()
            sendBufferChannel.Writer.Complete()
            distributeTask.Wait()

            // Let the File Writers know that no more work is coming
            for KeyValue (_, channel) in writerChannels do
                channel.Writer.Complete()

            // Wait for all of the file writers to indicate that they have completed
            for KeyValue (_, backgroundWriter) in backgroundWriters do
                backgroundWriter.Wait()

let multiWriter = MultiWriter()

for i in 0..105 do
    multiWriter.Add i

multiWriter.Flush()
printfn "Done"
