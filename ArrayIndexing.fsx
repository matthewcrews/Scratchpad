module Array =

    open System

    let private findExtremeWithComparison<'T when 'T : comparison> 
        (comparisonFunction:'T -> 'T -> bool) 
        (a:'T []) 
        : int =

        let length = a.GetLength(0)

        if length = 0 then
            invalidArg "a" "The input array cannot have a length of 0"
        else

            let mutable currentExtremeIndex = 0
            let mutable currentExtremeValue = a.[0]

            for i = 1 to length-1 do
                if comparisonFunction a.[i] currentExtremeValue then
                    currentExtremeIndex <- i
                    currentExtremeValue <- a.[i]

            currentExtremeIndex


    let indexOfMin<'T when 'T : comparison> (a:'T []) : int =
        findExtremeWithComparison (<) a


    let indexOfMax<'T when 'T : comparison> (a:'T []) : int =
        findExtremeWithComparison (>) a


    let private findExtremeByWithComparison<'T1, 'T2 when 'T2 : comparison>
        (byFunction:'T1 -> 'T2)
        (comparisonFunction:'T2 -> 'T2 -> bool) 
        (a:'T1 []) 
        : int =

        let length = a.GetLength(0)

        if length = 0 then
            invalidArg "a" "The input array cannot have a length of 0"
        else

            let mutable currentExtremeIndex = 0
            let mutable currentExtremeValue = byFunction a.[0]

            for i = 1 to length-1 do
                if comparisonFunction (byFunction a.[i]) currentExtremeValue then
                    currentExtremeIndex <- i
                    currentExtremeValue <- byFunction a.[i]

            currentExtremeIndex


    let indexOfMinBy<'T1, 'T2 when 'T2 : comparison> 
        (byFunction:'T1 -> 'T2) 
        (a:'T1 []) 
        : int =
        findExtremeByWithComparison byFunction (<) a


    let indexOfMaxBy<'T1, 'T2 when 'T2 : comparison> 
        (byFunction:'T1 -> 'T2) 
        (a:'T1 []) 
        : int =
        findExtremeByWithComparison byFunction (>) a


    let private findExtremeWhereWithComparison<'T when 'T : comparison>
        (whereFunction:'T -> bool)
        (comparisonFunction:'T -> 'T -> bool)
        (a:'T [])
        : int option =

        let length = a.GetLength(0)

        if length = 0 then
            invalidArg "a" "The input array cannot have a length of 0"
        else

            let mutable currentExtremeIndex = None
            let mutable currentExtremeValue = a.[0]

            for i = 0 to length-1 do
                if whereFunction a.[i] then
                    if comparisonFunction a.[i] currentExtremeValue || Option.isNone currentExtremeIndex then
                        currentExtremeIndex <- Some i
                        currentExtremeValue <- a.[i]

            currentExtremeIndex


    let indexOfMinWhere<'T when 'T : comparison>
        (whereFunction:'T -> bool)
        (a: 'T [])
        : int option =
        findExtremeWhereWithComparison whereFunction (<) a


    let indexOfMaxWhere<'T when 'T : comparison>
        (whereFunction:'T -> bool)
        (a: 'T [])
        : int option =
        findExtremeWhereWithComparison whereFunction (>) a


    let private findExtremeByWhereWithComparison<'T1, 'T2 when 'T2 : comparison>
        (byFunction:'T1 -> 'T2)
        (whereFunction:'T1 -> bool)
        (comparisonFunction:'T2 -> 'T2 -> bool)
        (a:'T1 [])
        : int option =

        let length = a.GetLength(0)

        if length = 0 then
            invalidArg "a" "The input array cannot have a length of 0"
        else

            let mutable currentExtremeIndex = None
            let mutable currentExtremeValue = byFunction a.[0]

            for i = 0 to length-1 do
                if whereFunction a.[i] then
                    let potentialValue = byFunction a.[i]

                    if comparisonFunction potentialValue currentExtremeValue || Option.isNone currentExtremeIndex then
                        currentExtremeIndex <- Some i
                        currentExtremeValue <- potentialValue

            currentExtremeIndex

    let indexOfMinByWhere<'T1, 'T2 when 'T2 : comparison>
        (byFunction:'T1 -> 'T2)
        (whereFunction:'T1 -> bool)
        (a:'T1 [])
        : int option =

        findExtremeByWhereWithComparison byFunction whereFunction (<) a

    let indexOfMaxByWhere<'T1, 'T2 when 'T2 : comparison>
        (byFunction:'T1 -> 'T2)
        (whereFunction:'T1 -> bool)
        (a:'T1 [])
        : int option =

        findExtremeByWhereWithComparison byFunction whereFunction (>) a

    let private findExtremeByiWithComparison<'T1, 'T2 when 'T2 : comparison>
        (byFunction:'int -> 'T1 -> 'T2)
        (comparisonFunction:'T2 -> 'T2 -> bool) 
        (a:'T1 []) 
        : int =

        let length = a.GetLength(0)

        if length = 0 then
            invalidArg "a" "The input array cannot have a length of 0"
        else

            let mutable currentExtremeIndex = 0
            let mutable currentExtremeValue = byFunction 0 a.[0]

            for i = 1 to length-1 do
                let potentialValue = byFunction i a.[i]

                if comparisonFunction potentialValue currentExtremeValue then
                    currentExtremeIndex <- i
                    currentExtremeValue <- potentialValue

            currentExtremeIndex


    let indexOfMinByi<'T1, 'T2 when 'T2 : comparison> 
        (byFunction:int -> 'T1 -> 'T2) 
        (a:'T1 []) 
        : int =
        findExtremeByiWithComparison byFunction (<) a


    let indexOfMaxByi<'T1, 'T2 when 'T2 : comparison> 
        (byFunction:int -> 'T1 -> 'T2) 
        (a:'T1 []) 
        : int =
        findExtremeByiWithComparison byFunction (>) a


    let private findExtremeByWhereiWithComparison<'T1, 'T2 when 'T2 : comparison>
        (byFunction:int -> 'T1 -> 'T2)
        (whereFunction:int -> 'T1 -> bool)
        (comparisonFunction:'T2 -> 'T2 -> bool)
        (a:'T1 [])
        : int option =

        let length = a.GetLength(0)

        if length = 0 then
            invalidArg "a" "The input array cannot have a length of 0"
        else

            let mutable currentExtremeIndex = None
            let mutable currentExtremeValue = byFunction 0 a.[0]

            for i = 0 to length-1 do
                if whereFunction i a.[i] then
                    let potentialValue = byFunction i a.[i]

                    if comparisonFunction potentialValue currentExtremeValue || Option.isNone currentExtremeIndex then
                        currentExtremeIndex <- Some i
                        currentExtremeValue <- potentialValue

            currentExtremeIndex

    let indexOfMinByWherei<'T1, 'T2 when 'T2 : comparison>
        (byFunction:int -> 'T1 -> 'T2)
        (whereFunction:int -> 'T1 -> bool)
        (a:'T1 [])
        : int option =

        findExtremeByWhereiWithComparison byFunction whereFunction (<) a

    let indexOfMaxByWherei<'T1, 'T2 when 'T2 : comparison>
        (byFunction:int -> 'T1 -> 'T2)
        (whereFunction:int -> 'T1 -> bool)
        (a:'T1 [])
        : int option =

        findExtremeByWhereiWithComparison byFunction whereFunction (>) a



let t = [|1..10|]
printfn "IndexOfMin: %A" (Array.indexOfMin t)
printfn "IndexOfMax: %A" (Array.indexOfMax t)

Array.indexOfMin [|0|]
Array.indexOfMax [|10; 11|]
Array.indexOfMax [|12; 11|]

Array.indexOfMinBy (fun x -> -1*x) [|1..10|]
Array.indexOfMaxBy (fun x -> -1*x) [|1..10|]

Array.indexOfMinWhere (fun x -> x >= 5) [|1..10|]
Array.indexOfMinWhere (fun x -> x < 8) [|1..10|]

Array.indexOfMaxWhere (fun x -> x >= 5) [|1..10|]
Array.indexOfMaxWhere (fun x -> x < 3) [|1..10|]

Array.indexOfMinByWhere (fun x -> -1*x) (fun x -> x > 5) [|1..10|]
Array.indexOfMinByWhere (fun x -> 1*x) (fun x -> x > 5) [|1..10|]

Array.indexOfMaxByWhere (fun x -> -1*x) (fun x -> x < 5) [|1..10|]
Array.indexOfMaxByWhere (fun x -> 1*x) (fun x -> x < 5) [|1..10|]

let testArray = [|1..10|]

let byFunc (i:int) x =
    testArray.[i]

Array.indexOfMinByi byFunc [|5; 4; 3; 2; 1|]
Array.indexOfMaxByi byFunc [|5; 4; 3; 2; 1|]


Array.indexOfMinByWherei byFunc (fun i x -> x > 2) [|5; 4; 3; 2; 1|]
Array.indexOfMaxByWherei byFunc (fun i x -> x > 2) [|5; 4; 3; 2; 1|]


