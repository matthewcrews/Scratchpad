type Chicken =
    {
        Name : string
    }

let inline myFunction
    (a: ^T when ^T : (member Chicken : Chicken)) =

    let x = (^T: (member Chicken : Chicken) a)

    printfn "%A" x.Name

    