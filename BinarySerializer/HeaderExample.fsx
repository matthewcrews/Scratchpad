#r "nuget: MessagePack"

open System.IO
open System.Runtime.InteropServices
open MessagePack
open System

[<Struct; MessagePackObject>]
type Chicken =
    {
        [<Key 0>]
        Name: string
        [<Key 1>]
        Age: int
        [<Key 2>]
        Size: float
    }

let outputFile = "output.bin"

if File.Exists outputFile then
    File.Delete outputFile

let mutable flock1 =
    [| for i in 1..2 do
        {
            Name = $"Chicken{i}"
            Age = i
            Size = float i
        }|]

do
    let bytes = MessagePackSerializer.Serialize flock1
    let bytesLength = BitConverter.GetBytes bytes.Length
    let fs = new FileStream (outputFile, FileMode.Append, FileAccess.Write)
    fs.Write bytesLength
    fs.Write bytes
    fs.Close()


let mutable flock2 =
    [| for i in 3..5 do
        {
            Name = $"Chicken{i}"
            Age = i
            Size = float i
        }|]

do
    let bytes = MessagePackSerializer.Serialize flock2
    let bytesLength = BitConverter.GetBytes bytes.Length
    let fs = new FileStream (outputFile, FileMode.Append, FileAccess.Write)
    fs.Write bytesLength
    fs.Write bytes
    fs.Close()


let readBytes = File.ReadAllBytes outputFile
let readMem = ReadOnlyMemory readBytes

let mutable readHead = 0
let acc = ResizeArray()

while readHead < readMem.Length do

    let dataLength = BitConverter.ToInt32 (readMem.Span.Slice(readHead, sizeof<int>))
    printfn $"{dataLength}"

    let dataStart = readHead + sizeof<int>
    let data = readMem.Slice(dataStart, dataLength)
    let flock = MessagePackSerializer.Deserialize<Chicken[]> data
    for chicken in flock do
        acc.Add chicken

    // Move the readHead forward past Data
    readHead <- readHead + sizeof<int> + dataLength

for chicken in acc do
    printfn $"{chicken}"
