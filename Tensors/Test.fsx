// type Tensor<'a, [<Measure>] 'dims> =
//     {
//         Values: 'a[]
//         Lengths: int[]
//         Strides: int[]
//         Start: int
//     }

type Tensor<'a, [<Measure>] 'dims> =
    struct
    end

[<Measure>] type Dim

type TensorOps () =

    member _.Add (t1: Tensor<'a, Dim>, t2: Tensor<'a, Dim>) =
        ()
    member _.Add (t1: Tensor<'a, Dim>, t2: Tensor<'a, Dim^2>) =
        ()




let add (t1: Tensor<'a, Dim>) (t2: Tensor<'a, Dim>) =

    ()

