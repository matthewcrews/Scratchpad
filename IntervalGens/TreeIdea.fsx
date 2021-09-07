type ExprTree<'a> =
    | Product of ExprTree<'a> * ExprTree<'a>
    | Leaf of ExprTree<'a>