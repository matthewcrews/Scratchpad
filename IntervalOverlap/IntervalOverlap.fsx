[<Struct>]
type Range =
    {
        Start : int
        Bound : int // Exclusive
    }

module BinaryTree =

    type private Node<'T> = 
        private {
            _value : 'T
            mutable _left : Node<'T> option
            mutable _right : Node<'T> option
        }
        member n.Value = n._value
        member n.Left = n._left
        member n.Right = n._right

    type BinaryTree<'T> =
        {
            Root : Node<'T>
        }

    let create (values: seq<'T>) =
        


let n = Node.create 1 None None