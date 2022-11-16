
type Tree<'a> =
    | Node of 'a Tree * 'a * 'a Tree
    | Nil



let rec create (acc: Tree<'A>) (values: 'T[]) (minIndex: int) (maxIndex: int) =

    