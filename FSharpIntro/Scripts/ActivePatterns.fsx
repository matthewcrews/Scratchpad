let (|FileExtension|) (a: string) : string =
    System.IO.Path.GetExtension a

let myFilePath = "Chicken.xlsx"

let (FileExtension extension) = myFilePath
printfn $"{extension}"

let files =
    [
        "Chicken.xlsx"
        "Turkey.csv"
        "Monkey.txt"
    ]

for FileExtension ext in files do
    printfn $"{ext}"


let (|Even|Odd|Negative|) (v: int) =
    if v < 0 then
        Negative
    elif v % 2 = 0 then
        Even
    else
        Odd

let myFunction (v: int) =
    match v with
    | Even -> printfn "Is Even"
    | Odd -> printfn "Is Odd"
    | Negative -> printfn "It's negative"


type ExcelFile = ExcelFile of string

let (|IsExcel|_|) (v: string) =
    let (FileExtension ext) = v
    if ext = ".xlsx" then
        Some (ExcelFile v)
    else
        None

type TextFile = TextFile of string

let (|IsTxt|_|) (v: string) =
    let (FileExtension ext) = v
    if ext = ".txt" then
        Some (TextFile v)
    else
        None

let myOtherFunction (v: string) =
    match v with
    | IsExcel excelFile -> printfn $"It's Excel: {excelFile}"
    | IsTxt txtFile -> printfn $"It's Text: {txtFile}"
    | _ -> printfn "Unknown case"

let x = "MyText.txt"
myOtherFunction x