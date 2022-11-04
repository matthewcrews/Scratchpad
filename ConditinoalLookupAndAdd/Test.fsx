open System.Collections.Generic

let inline ( +? ) (a: 'a) (b: 'b option) =
    match b with
    | Some v -> a + v
    | None -> a

let inline ( *? ) (a: 'a) (b: 'b option) =
    match b with
    | Some v -> a * v
    | None -> LanguagePrimitives.GenericZero<_>

let inline ( ?* ) (a: 'a option) (b: 'b) =
    match a with
    | Some a -> a * b
    | None -> LanguagePrimitives.GenericZero<_>

// let inline ( ?+ ) (a: 'a option) (b: 'a option) =
//     match a, b with
//     | Some a, Some b -> a + b
//     | Some a, None -> a
//     | None, Some b -> b
//     | None, None -> LanguagePrimitives.GenericZero<'a>

// let inline ( ?*? ) (a: 'a option) (b: 'b option) =
//     match a, b with
//     | Some a, Some b -> a * b
//     | Some _, None
//     | None, Some _
//     | None, None -> LanguagePrimitives.GenericZero<_>

// let inline ( *? ) (a: 'a) (b: 'b option) : ^c =
//     match b with
//     | Some b -> a * b
//     | None -> LanguagePrimitives.GenericZero<_>

// let d =
//     [
//         1, 1.0
//         2, 2.0
//         4, 4.0
//     ]
//     |> List.map KeyValuePair
//     |> Dictionary


// let b =
//     [
//         1, 1.0
//         2, 3.0
//         4, 4.0
//     ]
//     |> List.map KeyValuePair
//     |> Dictionary



// let keys = [1 .. 10]

