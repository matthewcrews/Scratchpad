type MyThing<'a> = MyThing of 'a

let testFunc str =
  MyThing (Seq.length str)

type AsyncBuilder with
  member x.Bind(value : MyThing<'a>, f : 'a -> Async<'b>) =
    let (MyThing inner) = value
    f inner

  [<CustomOperation("log", MaintainsVariableSpaceUsingBind = true)>]
  member x.Log(boundValues : Async<'a>, [<ProjectionParameter>] messageFunc) =
    async {
      let! b = boundValues
      printfn "Log message: %s" <| messageFunc b
      printfn "Currently let bound things: %A" b
      return b
    }

let workflow =
  async {
    log "a string"
    let! c = testFunc "Count the letters"
    let! result = async { return (c * 10) }
    do! Async.Sleep 100
    log "more string"
    let! a = MyThing "A prefix here: "
    log "a different string"
    return sprintf "%s %d" a result
  }

printfn "%A" <| Async.RunSynchronously workflow

// Program outputs:
// Log message: a string
// Currently let bound things: <null>
// Log message: more string
// Currently let bound things: (17, 170)
// Log message: a different string
// Currently let bound things: (17, 170, "A prefix here: ")
// "A prefix here:  170"