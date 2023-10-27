
[<Measure>] type Chicken
[<Measure>] type Turkey
[<Measure>] type Goose

let (|Chicken|Turkey|Goose|) (x: int) =
    let k = x >>> 30
    let v = x &&& 0x3FFF_FFFF

    if k = 0 then
        Chicken v
    elif k = 1 then
        Turkey v
    else
        Goose v


buffers
    b1 "Source"
    b2 "Sink"

constraints
    c1 "Operation"

structure
    b1 --> c1
    c1 --> b2