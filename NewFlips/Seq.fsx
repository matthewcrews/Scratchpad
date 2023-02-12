open System.Collections.Generic

let inline productJoin (a: seq<KeyValuePair<'Key, 'LValue>>) (b: seq<KeyValuePair<'Key, 'b>>) =

    let comparer = LanguagePrimitives.FastGenericComparer
    let left = a.GetEnumerator()
    let right = b.GetEnumerator()

    let rec loop (leftHasValue: bool, rightHasValue: bool) =

        if leftHasValue && rightHasValue then
            let leftKey = left.Current.Key
            let rightKey = right.Current.Key
            let compareResult = comparer.Compare (leftKey, rightKey)

            if compareResult = 0 then
                let newValue = left.Current.Value * right.Current.Value
                let newKvp = KeyValuePair (leftKey, newValue)
                Some (newKvp, (left.MoveNext(), true))
            elif compareResult < 0 then
                loop (left.MoveNext(), true)
            else
                loop (true, right.MoveNext())

        else
            None

    (left.MoveNext(), right.MoveNext())
    |> Seq.unfold loop

let comparer1Dto1D (a: 'Key, b: 'Key) =
    let comparer = LanguagePrimitives.FastGenericComparer
    comparer.Compare (a, b)

let comparer2Dto1D (struct (a1: 'Key1, a2: 'Key2), b: 'Key2) =
    let comparer = LanguagePrimitives.FastGenericComparer
    comparer.Compare (a2, b)

let inline productJoin2
    (comparer: 'Key1 * 'Key2 -> int)
    (a: seq<KeyValuePair<'Key1, 'LValue>>)
    (b: seq<KeyValuePair<'Key2, 'RValue>>)
    =

    let left = a.GetEnumerator()
    let right = b.GetEnumerator()

    let rec loop (leftHasValue: bool, rightHasValue: bool) =

        if leftHasValue && rightHasValue then
            let leftKey = left.Current.Key
            let rightKey = right.Current.Key
            let compareResult = comparer (leftKey, rightKey)

            if compareResult = 0 then
                let newValue = left.Current.Value * right.Current.Value
                let newKvp = KeyValuePair (leftKey, newValue)
                Some (newKvp, (left.MoveNext(), true))
            elif compareResult < 0 then
                loop (left.MoveNext(), true)
            else
                loop (true, right.MoveNext())

        else
            None

    (left.MoveNext(), right.MoveNext())
    |> Seq.unfold loop



let a =
    [
        KeyValuePair (struct ("a", "b"), 1)
        KeyValuePair (struct ("b", "b"), 2)
        KeyValuePair (struct ("c", "b"), 3)
        KeyValuePair (struct ("d", "b"), 4)
    ]

let b =
    [
        KeyValuePair ("b", 2)
        KeyValuePair ("c", 2)
        KeyValuePair ("e", 2)
        KeyValuePair ("g", 2)
    ]

let c = productJoin2 comparer2Dto1D a b
for x in c do
    printfn $"{x}"