open System
open System.Collections.Generic
[<NoComparison>]
type MonoidDictionary<'key, 'value when 'key : comparison> = {
    Values : Dictionary<'key, 'value>
} with
    member inline this.add (other: MonoidDictionary<'key, ^value>) =
            for KeyValue(key, otherValue) in other.Values do
                match this.Values.TryGetValue key with
                | true, thisValue -> this.Values.[key] <- thisValue + otherValue
                | false, _ -> this.Values.[key] <- otherValue


let myFunc x =
    match x with
    | First           -> doA
    | Ack             -> doB
    | SomeOtherOption -> doC

let myFunc x =
    match x with
    | First -> doA
    | Ack -> doB
    | SomeOtherOption -> doC