let size = 256
let valueCount = 100

let rng = System.Random()

let values =
    [| for _ in 1 .. valueCount do
        rng.Next size |]

let grps =
    values
    |> Array.groupBy id
    |> Array.map (fun (value, grp) ->
        value, grp.Length)

let maxGrpSize =
    grps
    |> Array.maxBy snd

maxGrpSize


let sample () =
    let values =
        [| for _ in 1 .. valueCount do
            rng.Next size |]

    let grps =
        values
        |> Array.groupBy id
        |> Array.map (fun (value, grp) ->
            value, grp.Length)

    let maxGrpSize =
        grps
        |> Array.maxBy snd
        |> snd

    maxGrpSize

sample ()

let sampleCount = 1_000_000

let samples =
    [| for _ in 1 .. sampleCount do
        sample ()|]

let x =
    samples
    |> Array.groupBy id
    |> Array.map (fun (value, grp) ->
            value, 100.0 * (float grp.Length / float sampleCount))
    |> Array.sortBy fst




let hasDistinctMask (hashes: uint[]) =

    let maxBitShift = 30

    let rec loop (bitShift: int) =
        if bitShift > maxBitShift then
            false
        else
            let distinctMaskedValues =
                hashes
                |> Array.map (fun h ->
                    (h >>> bitShift) &&& 0b11u)
                |> Array.distinct

            if distinctMaskedValues.Length = hashes.Length then
                true
            else
                loop (bitShift + 1)

    loop 0

hasDistinctMask [|0u; 0u; 2u; 3u|]


let hashes =
    [| for _ in 1 .. 4 do
        uint (rng.Next (System.Int32.MinValue, System.Int32.MaxValue))|]

hasDistinctMask hashes

let hashSample () =
    [| for _ in 1 .. 4 do
        uint (rng.Next (System.Int32.MinValue, System.Int32.MaxValue))|]

let hashSampleCount = 10_000_000

let hashExperiment =
    [|for _ in 1 .. hashSampleCount do
        let hashes = hashSample()
        hasDistinctMask hashes |]
    |> Array.groupBy id
    |> Array.map (fun (b, grp) ->
        b, 100.0 * (float grp.Length / float hashSampleCount))


let z = 100u
let b = 1 + (1u &&& (z >>> 1))