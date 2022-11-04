[<Struct>]
type NativeArray<'T> =
    {
        Ptr : NativePtr<'T>
        Length : int
    }

module NativeArray =

    let create allocator size =

        () // NativeArray


module FrameLogic =

    let memoryAllocator = GetMemoryAllocator ()
    
    module private Logic =


        let logicFuncA () =
            
            let myNativeArr = NativeArray.create memoryAllocator length

            ()// Something funky happens

        let logicFuncB () =

            ()// Something funky happens

        let logicFuncC () =

            ()// Something funky happens


    let proces a b c =
        Logic.logicFuncA()
        Logic.logicFuncB()


        memoryAllocator.Clear()
        () // Result here?