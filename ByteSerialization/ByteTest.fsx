open System;
open System.IO;
open System.Collections;
open System.Runtime.Serialization.Formatters.Binary;
open System.Runtime.Serialization;

#nowarn "44"

[<Struct>]
type Chicken =
    {
        Name: string
        Size: float
        Id: int
    }

let flock = [|
    { Name = "Clucky"; Size = 10.0; Id = 1 }
    { Name = "Lucky1"; Size = 12.0; Id = 10 }
    { Name = "Clucky"; Size = 10.1; Id = 100000 }
|]

let fsWrite = new FileStream("DataFile.dat", FileMode.Create)
let formatter = System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
formatter.Serialize (fsWrite, flock)
fsWrite.Close()

let fsRead = new FileStream("DataFile.dat", FileMode.Open);
let readFlock = formatter.Deserialize fsRead :?> array<Chicken>