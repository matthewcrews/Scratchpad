let a = Map [ 1, "A"; 2,"B"; 3,"C" ]

let b =
    Map [
        1, "A"
        2, "B"
        3, "C"
    ]

// Structural Equality
a = b

// Adding an element returns a new Map
let c = a.Add (4, "C")

// They are immutable so C <> A

// Key Collisions over-write
let d = a.Add (1, "D")

// You can access an element by key
a[1]

match a.TryGetValue 1 with
| true, v -> v
| false, _ -> "No string"

// You can also use TryGet
match a.TryFind 1 with
| Some v -> v
| None -> "No string"

// You cannot insert values
a[1] <- "Chicken"

// Map module
let e =
    a
    |> Map.filter (fun key value -> value > "A")

