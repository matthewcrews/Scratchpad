open System.Collections.Generic

let target =
    [1..10_000]
    |> Set

let rng = System.Random(123)

let generateRandomSimple (rng: System.Random) =

    [|1..10_000|]
    |> Array.map (fun v -> v, rng.Next())
    |> Array.sortBy snd
    |> Array.map fst
    |> List.ofArray

let bruteRandom = generateRandomSimple rng

// Should return true
(Set bruteRandom) = target

let generateRandomMinMemory (numberOfShuffles: int) (rng: System.Random) =

    let values = [|1..10_000|]

    for _ in 1..numberOfShuffles do
        let indexA = rng.Next(0, values.Length)
        let indexB = rng.Next(0, values.Length)
        let valueA = values.[indexA]
        values.[indexA] <- values.[indexB]
        values.[indexB] <- valueA

    List values

let minMemory = generateRandomMinMemory 10_000 rng

(Set minMemory) = target


let generateRandom (rng: System.Random) =

    let sourceNumbers = 
        [1..10_000]
        |> SortedSet

    let rec generate (output: int list) (source: SortedSet<int>) =
        match source.Count = 0 with
        | true -> output
        | false ->
            let nextNumber = source.[rng.Next(0, source.Count)]
            source.Remove nextNumber |> ignore
            let newOutput = nextNumber::output
            generate newOutput source

    ()