type Buffer =
    {
        Name : string
        Capacity : float
    }

type Constraint =
    {
        Name : string
        Limit : float
    }

[<RequireQualifiedAccess>]
type Setting =
    | Buffer of Buffer
    | Constraint of Constraint


type BufferBuilder (name: string) =
    member _.Yield _ : Buffer = { Name = name; Capacity = 0.0 }

    [<CustomOperation("Capacity")>]
    member _.Capacity (b: Buffer, newCapacity) =
        { b with Capacity = newCapacity }

let Buffer = BufferBuilder

type ConstraintBuilder (name: string) =
    member _.Yield _ : Constraint = { Name = name; Limit = 0.0 }

    [<CustomOperation("Limit")>]
    member _.Limit (b: Constraint, newLimit) =
        { b with Limit = newLimit }

let Constraint = ConstraintBuilder

// This is effectively a list builder
type SettingsBuilder() =
    member _.Yield(x:Buffer) = [Setting.Buffer x]
    member _.Yield(x:Constraint) = [Setting.Constraint x]
    member _.Combine(x:Setting list, y:Setting list) = x @ y
    member _.Delay(f: unit -> Setting list) = f()
    member _.Run(x: Setting list) = x

let Settings = SettingsBuilder()
// The Computation Expression now works
let mySettings =
    Settings {
        Buffer "b1" {
            Capacity 100.0
        }
        Constraint "c1" {
            Limit 10.0
        }
        Constraint "c2" {
            Limit 10.0
        }
    }