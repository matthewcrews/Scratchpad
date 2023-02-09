type SimulationResult =
    {
        TailsCount: int
        HeadsCount: int
    }

let nonRecusiveVersion (rng: System.Random) (flips: int) =
    let mutable tailsCount = 0
    let mutable headsCount = 0
    let mutable flipCount = 0

    while flipCount < flips do
        let next = rng.NextDouble()

        if next < 0.5 then
            tailsCount <- tailsCount + 1
        else
            headsCount <- headsCount + 1

        flipCount <- flipCount + 1

    {
        TailsCount = tailsCount
        HeadsCount = headsCount
    }

let rng = System.Random 123
let r = nonRecusiveVersion rng 100

let recursiveVersion (rng: System.Random) (flips: int) =

    let rec loop flipCount headsCount tailsCount =
        if flipCount < flips then
            let next = rng.NextDouble()

            if next < 0.5 then
                loop (flipCount + 1) headsCount (tailsCount + 1)
            else
                loop (flipCount + 1) (headsCount + 1) tailsCount

        else
            {
                TailsCount = tailsCount
                HeadsCount = headsCount
            }

    loop 0 0 0

let rng2 = System.Random 123
let r2 = recursiveVersion rng2 100



