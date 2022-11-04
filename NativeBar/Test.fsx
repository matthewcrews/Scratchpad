module rec Collections =

    #nowarn "9"

    open System
    open System.Collections
    open System.Collections.Generic
    open System.Runtime.CompilerServices
    open System.Runtime.InteropServices
    open Microsoft.FSharp.NativeInterop

    [<Struct>]
    type NativeBar<[<Measure>] 'Measure, 'T when 'T : unmanaged> (ptr: nativeptr<'T>, len: int<'Measure>) =
        member x.Ptr = ptr
        
        member x.Item
            with inline get (n: int<'Measure>) = NativePtr.get x.Ptr (int n)

        member x.Length = len

        static member ofBar<'T when 'T: unmanaged> (b: Bar<_,'T>) =
            let arr = b._values
            use ptr = fixed arr
            NativeBar (ptr, b._values.Length)


    [<Struct>]
    type Bar<[<Measure>] 'Measure, 'T> internal (values: 'T[]) =
        
        new (count, value) =
            let newValues = Array.create count value
            Bar<_,_> newValues
        
        /// WARNING: This member is not intended for public consumption
        /// It is public to support inlining
        member _._values = values
        
        member inline b.Length = LanguagePrimitives.Int32WithMeasure<'Measure> b._values.Length
        
        member b.Item
            with inline get(i: int<'Measure>) =
                b._values[int i]
