type Asin = Asin of string
type Upc = Upc of int
type Ean = Ean of string

let myFunc (x: Asin|Upc|Ean) y z =
    match x with
    | Asin a -> asinWork a y z
    | Upc u -> upcWork u y z
    | Ean e -> eanWork e z


let x = 
    [
        Some 2
        Some 1
        None
    ]

x
|> List.sort

[<Struct>]
type Maybe<'a> =
    | Just of 'a
    | Nothing

let y =
    [
        Nothing
        Just 2
        Just 1
        Nothing
    ]

y |> List.sort