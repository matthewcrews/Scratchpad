open System


type MyRandom (seed: int64) =
    let mutable position = seed
    [<Literal>]
    let BIT_NOISE1 = 0xB52297A4DL
    [<Literal>]
    let BIT_NOISE2 = 0x68E31DA4L
    [<Literal>]
    let BIT_NOISE3 = 0x1B56C4E9L
    // [<Literal>]
    let DOUBLE_COEFFICIENT  = 1.0 / (float Int64.MaxValue)

    let squirrel3 (position: int64) =
        let mutable mangled = position

        mangled <- mangled * BIT_NOISE1
        mangled <- mangled ^^^ (mangled >>> 8)
        mangled <- mangled + BIT_NOISE2
        mangled <- mangled ^^^ (mangled <<< 8)
        mangled <- mangled * BIT_NOISE3
        mangled <- mangled ^^^ (mangled >>> 8)
        mangled

    member _.Next (maxValue: int) =
        position <- position + 1L
        let maxValueCoefficient = float maxValue
        let value = float (squirrel3 position)
        int (maxValueCoefficient * DOUBLE_COEFFICIENT * value)

    member _.NextDouble () =
        position <- position + 1L
        DOUBLE_COEFFICIENT * (float (squirrel3 position))

let rng = MyRandom 0L

for i in 1 .. 100 do

    printfn $"{rng.Next 10}"
