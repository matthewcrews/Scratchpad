open System.Collections.Generic

let minValue = 1
let maxValue = 10_000

let target =
    [minValue..maxValue]
    |> Set

// Always set the seed on a random number generator for reproducable results
// Using the built in System.Random. Whether this is the correct one would
// be dependent on the strength of randomness required by the use case.
// There are tradeoffs between how truly random the list is and the
// computational requirements of the random numbers. I would want to better
// understand the use case before recommending the "right" random number
// generator. I am using the built in System.Random to demonstrate the
// algorithms but this could easily be swapped out for another random
// number generator.
let rng = System.Random(123)


module Simple =

    // The simplest approach with no regard to performance
    // Best use case would be a one-off for a small analysis project
    // The assumption is that this would not be run often
    let generateList (minValue:int) (maxValue:int) (rng:System.Random) =

        if minValue > maxValue then
            invalidArg (nameof minValue) $"{nameof minValue} cannot be greater than {nameof maxValue}"

        [|minValue..maxValue|]
        |> Array.map (fun v -> v, rng.Next())
        |> Array.sortBy snd
        |> Array.map fst
        |> List.ofArray

// Create a sample
let simpleRandomList = Simple.generateList minValue maxValue rng

// Should return true since the random list should contain all of the
// values from 1 to listSize
(Set simpleRandomList) = target

// Create another sample for comparison
let otherSimpleRandomList = Simple.generateList minValue maxValue rng

// Should return false since consecutive samples should be different
otherSimpleRandomList = simpleRandomList


module MinMemory =

    // If the use case is to be run more frequently run and in a scenario where
    // memory usage should be minimized I suggest this approach. This has
    // better memory usage without much additional complexity. Since a List
    // is required the values must be materialized anyway so an IEnumerable
    // is not valid. More understanding of use case required to be certain.
    // The downside is the randomness guarantees are well below what a 
    // statisticain would require to be considered truly random.
    let generateList (minValue:int) (maxValue:int) (numberOfShuffles: int) (rng: System.Random) =
        if minValue > maxValue then
            invalidArg (nameof minValue) $"{nameof minValue} cannot be greater than {nameof maxValue}"

        // I am using a local array for shuffling the values. Localized mutation
        // is fine as long as it does not "leak out" of the context
        let values = [|minValue..maxValue|]

        // Perform a large number of shuffles to randomize the order of the values
        for _ in 1..numberOfShuffles do
            let indexA = rng.Next(0, values.Length)
            let indexB = rng.Next(0, values.Length)
            let valueA = values.[indexA]
            values.[indexA] <- values.[indexB]
            values.[indexB] <- valueA

        // Return the values as a List
        List values

// Generate a sample
let minMemoryRandomList = MinMemory.generateList minValue maxValue 10_000 rng

// Test that all values are present
(Set minMemoryRandomList) = target

// Create another sample
let otherMinMemoryRandomList = MinMemory.generateList minValue maxValue 10_000 rng

// Should return false since consecutive samples should be different
otherMinMemoryRandomList = minMemoryRandomList


module BestRandomness =

    // If the Client said that true randomness is required, I would use
    // this approach. It is slower, will require more memory, and may
    // be more complex to maintain. I would also swap for a different
    // random number generator with better randomness characteristics
    // but the idea remains the same. 
    let generateList (minValue:int) (maxValue:int) (rng: System.Random) =
        if minValue > maxValue then
            invalidArg (nameof minValue) $"{nameof minValue} cannot be greater than {nameof maxValue}"

        // We create the set of numbers that we will randomly sample from
        let sourceNumbers = ResizeArray [|minValue..maxValue|]

        let rec generate (output: int list) (source: ResizeArray<int>) =
            // Check if there are any remaining values to add to the output
            match source.Count = 0 with
            | true -> 
                // No values remain in the `source`. Return the resulting list
                output
            | false ->
                // Values remain in the source so we must continue to sample
                // Generate a random index in the range of remaining values
                let index = rng.Next(0, source.Count)
                // Retrieve the value from the source
                let nextNumber = source.[index]
                // Remove the value at the index so it cannot be sampled again
                source.RemoveAt index |> ignore
                // preprend the value to our output list we are building
                let newOutput = nextNumber::output
                // Continue to recurse until the source is empty
                generate newOutput source

        generate [] sourceNumbers

// Generate a sample
let bestRandomnessRandomList = BestRandomness.generateList minValue maxValue rng

// Test that all values are present
(Set bestRandomnessRandomList) = target

// Create another sample
let otherBestRandomnessRandomList = BestRandomness.generateList minValue maxValue rng

// Should return false since consecutive samples should be different
otherBestRandomnessRandomList = bestRandomnessRandomList