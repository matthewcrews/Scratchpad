open System.Threading.Tasks.Dataflow

let fullBufferCount = 10
let buffer = ResizeArray()
let file = "Output.txt"

let actionBlockSettings = ExecutionDataflowBlockOptions (MaxDegreeOfParallelism = 1)

let writer = ActionBlock (fun next ->
    printfn $"Got Value: {next}"
    buffer.Add next
    if buffer.Count > fullBufferCount then
        printfn "Writing values"
        let outputString = buffer |> Seq.map string
        System.IO.File.AppendAllLines (file, outputString)
        buffer.Clear()

, actionBlockSettings)

let wrapUp = writer.Completion.ContinueWith (fun _ ->
    System.Threading.Thread.Sleep 10000
    printfn "Writing Final Values"
    let outputString = buffer |> Seq.map string
    System.IO.File.AppendAllLines (file, outputString)
    buffer.Clear()
)

for next in 1..100 do
    writer.Post next
    |> ignore

writer.Complete()
wrapUp.Wait()