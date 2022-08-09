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
    | Constriant of Constraint


type BufferBuilder (name: string) =
    member _.Yield _ : Buffer = { Name = name; Capacity = 0.0 }
    member _.Run x : Buffer = x

    [<CustomOperation("Capacity")>]
    member _.Capacity (b: Buffer, newCapacity) =
        { b with Capacity = newCapacity }

let Buffer = BufferBuilder

type ConstraintBuilder (name: string) =
    member _.Yield _ : Constraint = { Name = name; Limit = 0.0 }
    member _.Run x : Constraint = x

    [<CustomOperation("Limit")>]
    member _.Limit (b: Constraint, newLimit) =
        { b with Limit = newLimit }

let Constraint = ConstraintBuilder

type SettingsBuilder () =

    member _.Yield _ : Setting list = []
    member _.Run x : Setting list = x

    [<CustomOperation("Buffer", MaintainsVariableSpaceUsingBind = true)>]
    member _.Buffer (settings, name: string, expr) =
        // This does not work
        let x = BufferBuilder name
        let newSetting = (x.Run expr) |> Setting.Buffer
        newSetting :: settings

    [<CustomOperation("Constriant", MaintainsVariableSpaceUsingBind = true)>]
    member _.Constraint (settings, name: string, expr) =
        // This does not work
        let x = ConstraintBuilder name
        let newSetting = x.Run expr |> Setting.Constriant
        newSetting :: settings

let Settings = SettingsBuilder ()

// The Computation Expression does not work
let mySettings =
    Settings {
        Buffer "b1" [
            Capacity 100.0
        ]
        Constraint "c1" [
            Limit 10.0
        ]
    }

// Below shows that the desired outcome of `mySettings` would be
let b1 = { Name = "b1"; Capacity = 100.0 }
let c1 = { Name = "c1"; Limit = 10.0 }

let desiredSettings = [
    Setting.Buffer b1
    Setting.Constriant c1
]