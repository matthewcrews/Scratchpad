(*
# Challenge

Generate the Intersect of two sorted sequences.
Each of the sequences contain unique values

Example:
let a = [1; 2; 4; 5; 8]
let b = [2; 3; 4; 8; 9]
// Desired output
let x = seq [2; 4; 8]

*)



let intersect (a: seq<'T>) (b: seq<'T>) : seq<'T> =
    let comparer = LanguagePrimitives.FastGenericComparer
    let aEnumerator = a.GetEnumerator()
    let bEnumerator = b.GetEnumerator()

    let rec loop (aHasValue: bool, bHasValue: bool) =

        if aHasValue && bHasValue then
            let compareResult = comparer.Compare (aEnumerator.Current, bEnumerator.Current)

            if compareResult = 0 then
                Some (aEnumerator.Current, (aEnumerator.MoveNext(), bEnumerator.MoveNext()))

            elif compareResult < 0 then
                loop (aEnumerator.MoveNext(), true)
            else
                loop (true, bEnumerator.MoveNext())

        else
            None


    (aEnumerator.MoveNext(), bEnumerator.MoveNext())
    |> Seq.unfold loop


let intersect2 (a: seq<'T>) (b: seq<'T>) : seq<'T> =
    let comparer = LanguagePrimitives.FastGenericComparer

    seq {
        let aEnumerator = a.GetEnumerator()
        let bEnumerator = b.GetEnumerator()
        let mutable hasValues = aEnumerator.MoveNext() && bEnumerator.MoveNext()

        while hasValues do
            let compareResult = comparer.Compare (aEnumerator.Current, bEnumerator.Current)

            if compareResult = 0 then
                yield aEnumerator.Current
                hasValues <- aEnumerator.MoveNext() && bEnumerator.MoveNext()

            elif compareResult < 0 then
                hasValues <- aEnumerator.MoveNext()

            else
                hasValues <- bEnumerator.MoveNext()
    }


let a = Seq.ofList [1; 2; 4; 5; 8]
let b = Seq.ofList [2; 3; 4; 8; 9]
let x = intersect2 a b
x