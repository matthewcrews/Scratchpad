
let myFunction (a: float) (b: float) =
    a + b

type MyTupleAlias = float * float

let myOtherFunction (a: float, b: float) =
    a + b
myOtherFunction (1.0, 2.0)

let myInput = 2.0, 3.0

let anotherFunction (myInput: MyTupleAlias) =
    1.0

anotherFunction myInput




myFunction 1.0 2.0

let myAdd1 = myFunction 1.0
myAdd1 10.0

