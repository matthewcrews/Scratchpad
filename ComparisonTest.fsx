type UpdateType =
    | A
    | B

type NextTime =
    | Update of updateType: UpdateType * time: int
    | None

let x = [
    None
    None
    Update (A, 3)
    Update (A, 2)
    Update (A, 4)
    Update (A, 5)
    Update (B, 1)
    Update (A, 6)
    // Update (1, A)
]

let minX = List.min x