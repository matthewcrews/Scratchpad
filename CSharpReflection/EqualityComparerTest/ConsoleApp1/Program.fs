open System
open System.Collections.Generic
open EqualityComparerTest

[<EntryPoint>]
let main (args: string[]) =

    // For more information see https://aka.ms/fsharp-console-apps
    printfn "Hello from F#"

    let cType = typeof<GarbageComparer>
    let t = typeof<obj>.Assembly.GetType("EqualityComparerTest.GarbageComparer")
    let x = Activator.CreateInstance(cType, "chicken") :?> IEqualityComparer<string>
    let strTest = x.Equals ("a", "a")

    let newComparerType = typeof<obj>.Assembly.GetType("System.Collections.Generic.NonRandomizedStringEqualityComparer+OrdinalComparer")
    let inputComparer : EqualityComparer<string> = EqualityComparer.Default
    let xComparer = Activator.CreateInstance (newComparerType, inputComparer) :?> IEqualityComparer<string>

    // let oldComparer = EqualityComparer<string>.Default
    //
    // let x = Activator.CreateInstance("System.Collections.Generic", "NonRandomizedStringEqualityComparer+OrdinalComparer")

    // let t2 = x.Equals ("a", "b")
    let b = 10 + 10
    ()
    1
