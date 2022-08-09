
type Test () =

    static member AddNumbers (x: int) (y: int) =
        x + y

    static member AddNumbers (x: int) (y: float) =
        x + (int y)
