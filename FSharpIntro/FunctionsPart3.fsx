
let myFunction a b c =
    a + b + c

let myOtherFunction =
    myFunction 1

let yetAnotherFunction =
    myOtherFunction 2

let myLastFunction =
    yetAnotherFunction 3

let logSomething (loggingParams: string) (myMessage: string) : unit =
    // Pretend there is cool logic here :)
    ()

let myLogger =
    logSomething "Parameters here :)"


let mutable myChickenName = "Clucky"

let myClojure (n: int) =
    $"{myChickenName} - {n}"

let notAClojure (name: string) (n: int) =   
    $"{name} - {n}"
myChickenName <- "Charlie"

myClojure 10
