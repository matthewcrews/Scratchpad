type Lowercase = 
    private
    | Lowercase of string
    static member Create (s: string) =
        match s with
        | null -> invalidArg (nameof s) "Cannot create Lowercase of null string"
        | value ->
            let x = s.ToLower()
            Lowercase x

// This works
let t1 = Lowercase.Create "Chicken"

// This won't
let t2 = Lowercase "Chicken"