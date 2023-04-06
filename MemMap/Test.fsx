#r "nuget: DotNext.Unsafe"
#r "nuget: DotNext.IO"
#r "nuget: MessagePack"

open System
open System.IO
open System.Buffers
open System.IO.MemoryMappedFiles
open System.Collections.Generic
open DotNext.IO.MemoryMappedFiles
open MessagePack

[<Struct; MessagePackObject>]
type Chicken =
    {
        [<Key 0>]
        Name: string
    }

let flock1 =
    [| for i in 1 ..4 do
           {
               Name = $"Cluck{i}"
           } |]

let flock2 =
    [| for i in 7 ..9 do
           {
               Name = $"Cluck{i}"
           } |]

let flocks =
    [|
        flock1
        flock2
    |]

let outputFilePath = "test.bin"

let writeToSpan (source: Span<byte>) (target: Span<byte>) (index: int) =
    let outSpan = target.Slice (index, source.Length)
    source.CopyTo outSpan
    index + source.Length

let writeTest () =

    use outputFile = MemoryMappedFile.CreateFromFile (outputFilePath, FileMode.Create, "result", 1_000_000)
    use writer = outputFile.CreateDirectAccessor()
    let resultBytes = writer.Bytes

    let mutable writeIndex = 0

    for flock in flocks do
        let flockBytes = (MessagePackSerializer.Serialize flock).AsSpan()
        let lengthBytes = (BitConverter.GetBytes flockBytes.Length).AsSpan()
        writeIndex <- writeToSpan lengthBytes resultBytes writeIndex
        writeIndex <- writeToSpan flockBytes resultBytes writeIndex

    let zeroBytes = (BitConverter.GetBytes 0).AsSpan()
    let _ = writeToSpan zeroBytes resultBytes writeIndex
    ()

writeTest ()

let readTest() =
    let data =
        File.ReadAllBytes outputFilePath
        |> ReadOnlyMemory
    printfn $"Data Length: {data.Length}"
    let arrayAcc = Queue()
    let mutable reading = true
    let mutable readIndex = 0

    while reading do
        printfn $"ReadIndex: {readIndex}"
        let dataByteLength = BitConverter.ToInt32 (data.Span.Slice (readIndex, sizeof<int>))
        if dataByteLength > 0 then
            printfn $"ReadData: {dataByteLength}"
            let dataBytes = data.Slice (readIndex + sizeof<int>, dataByteLength)
            let records = MessagePackSerializer.Deserialize<Chicken[]> dataBytes
            arrayAcc.Enqueue records

            // Need to move forward the length of the size indicator and the data
            readIndex <- readIndex + sizeof<int> + dataByteLength
        else
            reading <- false

    let readResult = arrayAcc.ToArray()
    readResult

let readResult = readTest()
flocks = readResult