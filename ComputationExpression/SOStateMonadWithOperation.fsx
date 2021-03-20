
/// From: https://stackoverflow.com/questions/59508149/fsharp-computation-expression-cannot-reference-binding-value-in-custom-operatio
type [<Struct>] State<'T> = S of (Map<string, obj> -> 'T*Map<string, obj>)

module State =
  let value v         = S <| fun m -> v, m

  let bind  uf (S t)  = S <| fun m -> 
    let tv, tm  = t m
    let (S u)   = uf tv
    u tm

  let combine u (S t) = S <| fun m -> 
    let _, tm   = t m
    let (S u)   = u
    u tm

  let delay tf  = S <| fun m -> 
    let (S t) = tf ()
    t m

  let forEach s tf  = S <| fun m -> 
    let mutable a = m
    for v in s do
      let (S t)   = tf v
      let (), tm  = t m
      a <- tm
    (), a

  let get k : State<'T option> = S <| fun m ->
    match m |> Map.tryFind k with
    | Some (:? 'T as v) -> Some v, m
    | _                 -> None, m

  let set k v = S <| fun m ->
    let m = m |> Map.add k (box v)
    (), m

  let run (S t) m = t m

  type Builder() =
    class
      member x.Bind       (t, uf) = bind    uf t
      member x.Combine    (t, u)  = combine u  t
      member x.Delay      tf      = delay   tf
      member x.For        (s, tf) = forEach s  tf
      member x.Return     v       = value   v
      member x.ReturnFrom t       = t             : State<'T>
      member x.Yield      v       = value   v
      member x.Zero ()            = value   ()

      [<CustomOperation("set", MaintainsVariableSpaceUsingBind = true)>] 
      member x.Set (s, k, v)      = s |> combine (set k v)
    end
let state = State.Builder ()

let testUpdate () =
  state {
    // Works fine
    set "key" -1
    for v in 0..2 do
      // Won't work because: FS3086: A custom operation may not be used in conjunction with 'use', 'try/with', 'try/finally', 'if/then/else' or 'match' operators within this computation expression
      // set "hello" v
      // Workaround but kind of meh
      // do! state { set "key" v }
      // Better IMHO
      do! State.set "key" v
    return! State.get "key"
  }

[<EntryPoint>]
let main argv =
  let tv, tm = State.run (testUpdate ()) Map.empty
  printfn "v:%A" tv
  printfn "m:%A" tm
  0