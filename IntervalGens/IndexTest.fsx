open System
open System.Collections.Generic

let data =
    [|for i in 1 .. 4 do
        for j in 1 .. 5 ->
            (string i, j), i + j
    |] |> Array.sortBy fst

type IndexRecord<'Key> = {
    Key : 'Key
    StartIdx : int
    EndIdx : int
    NextRecordIdx : int
}

type KeyInterval<'Key> = {
    Key : 'Key
    StartIdx : int
    EndIdx : int
}

let values =
    data
    |> Array.map snd

let keys =
    data
    |> Array.map fst

let generateKeyIntervals1D (keys: _[]) =

    keys
    |> Array.mapi (fun idx key -> { Key = key; StartIdx = idx; EndIdx = idx })


let generateKeyIntervals2D (keys: _[]) =

    if keys.Length = 0 then
        [], []
    else

        let mutable keys1 = []
        let mutable keys2 = []
        let mutable currKey1, _ = keys.[keys.Length - 1]
        let mutable key1IntervalEndIdx = keys.Length - 1

        for idx = keys.Length - 1 downto 0 do
            let nextKey1, nextKey2 = keys.[idx]

            keys2 <- { Key = nextKey2; StartIdx = idx; EndIdx = idx } :: keys2

            if nextKey1 <> currKey1 then
                keys1 <- { Key = currKey1; StartIdx = idx + 1; EndIdx = key1IntervalEndIdx } :: keys1
                currKey1 <- nextKey1
                key1IntervalEndIdx <- idx

            if idx = 0 then
                keys1 <- { Key = currKey1; StartIdx = idx; EndIdx = key1IntervalEndIdx } :: keys1

        keys1, keys2

let buildKeyRecords (intervals: KeyInterval<_> list) =

    if intervals.IsEmpty then
        Array.empty

    else
        let x = List.toArray intervals
        let result = Array.zeroCreate x.Length
        let lastRecordIndexForInterval = Dictionary ()

        for idx = x.Length - 1 downto 0 do
            let interval = x.[idx]
            let lastIdx =
                match lastRecordIndexForInterval.TryGetValue interval.Key with
                | true, lastIdx -> lastIdx
                | false, _ -> -1
            lastRecordIndexForInterval.[interval.Key] <- idx
            result.[idx] <- { Key = interval.Key; StartIdx = interval.StartIdx; EndIdx = interval.EndIdx; NextRecordIdx = lastIdx }

        result


let x, y = generateKeys keys
x
y
keys

let c = buildKeyRecords x
let d = buildKeyRecords y
d