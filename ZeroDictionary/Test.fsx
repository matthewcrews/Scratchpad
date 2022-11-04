open System.Collections.Generic

type ZeroDictionary<'Key, 'Value> (zero: 'Value, d: Dictionary<'Key, 'Value>) =
    member _.Zero = zero
    /// WARNING: Public for inlining only
    member _._values : Dictionary<'Key, 'Value> = d
    
    member zd.Item
        with inline get k =
            let values = zd._values
            match values.TryGetValue k with
            | true, v -> v
            | false, _ -> zd.Zero
            
        and inline set k v =
            let values = zd._values
            values[k] <- v
            
    member zd.GetEnumerator () =
        zd._values.GetEnumerator()
            
    interface IEnumerable<KeyValuePair<'Key, 'Value>> with
        
        member zd.GetEnumerator() =
            zd._values.GetEnumerator()
        
    interface System.Collections.IEnumerable with
        
        member zd.GetEnumerator() =
            zd._values.GetEnumerator() :> System.Collections.IEnumerator


module ZeroDictionary =
    
    let inline create (values: ('a * 'b) seq) =
        let zero = LanguagePrimitives.GenericZero<'b>
        let values =
            values
            |> Seq.map KeyValuePair
            |> Dictionary
        ZeroDictionary (zero, values)


let test = ZeroDictionary.create [1, 0.0; 2, 1.2; 3, 2.1]

for KeyValue (k, v) in test do
    printfn $"Key: {k} | Value: {v}"