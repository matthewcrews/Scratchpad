/// From this blogpost: http://tomasp.net/blog/2015/query-translation/

open Microsoft.FSharp.Quotations

/// Represents a query (but it is never executed!)
type Query<'T> = NA

/// Sample type that would be ideally imported from database
type Person = 
  { Name : string
    Age : int }

/// Models a database with one 'People' table
type DB = 
  static member People : Query<Person> = NA

/// Defines a query builder which lets us write queries containing
/// 'for', 'where', 'selectAttr' and 'selectCount' operations.
type SimpleQueryBuilder() = 
  /// 'For' and 'Yield' enables the 'for x in xs do ..' syntax
  member x.For(tz:Query<'T>, f:'T -> Query<'R>) : Query<'R> = NA
  member x.Yield(v:'T) : Query<'T> = NA

  /// Instructs the compiler to capture quotation of the query
  member x.Quote(e:Expr<_>) = e

  /// Represents filtering of the source using specified condition
  [<CustomOperation("where", MaintainsVariableSpace=true)>]
  member x.Where
    ( source:Query<'T>, 
      [<ProjectionParameter>] f:'T -> bool ) : Query<'T> = NA

  /// Represents projection where we select specified properties
  [<CustomOperation("selectAttrs")>]
  member x.SelectAttrs
    ( source:Query<'T>, 
      [<ProjectionParameter>] f:'T -> 'R) : Query<'R> = NA

  /// Represents projection where we get the count of values
  [<CustomOperation("selectCount")>]
  member x.SelectCount(source:Query<'T>) : int = 
    failwith "Never executed"

/// Global instance of the computation builder
let simpleq = SimpleQueryBuilder()

/// Defines a single condition of the form
/// p.<Property> <op> <Constant>
type QueryCondition = 
  { Property : string
    Operator : string 
    Constant : obj }

/// Specifies what kind of projection happens at the 
/// end of query (count or list of projected attributes)
type QueryProjection =
  | SelectAttributes of string list
  | SelectCount

/// A query consits of source (table) name, a list of
/// filters specified using `where` and an optional 
/// projection at the end.
type Query = 
  { Source : string
    Where : QueryCondition list
    Select : QueryProjection option }

open Microsoft.FSharp.Quotations.Patterns
open Microsoft.FSharp.Quotations.DerivedPatterns

/// Translate property access (may be in a tuple or not)
let translatePropGet varName = function
  | PropertyGet(Some(Var v), prop, []) 
      // The body is simple projection
      when v.Name = varName -> prop.Name
  | e -> 
    // Too complex expression in projection
    failwithf 
      "%s\nGot: %A" 
      ( "Only expressions of the form " + 
        "'p.Prop' are supported!" ) e

/// Translate projection - this handles both of the forms 
/// (with or without tuple) and calls `translatePropGet`
let translateProjection e =
  match e with
  | Lambda(var1, NewTuple args) -> 
      // Translate all tuple items
      List.map (translatePropGet var1.Name) args
  | Lambda(var1, arg) -> 
      // There is just one body expression
      [translatePropGet var1.Name arg]
  | _ -> failwith "Expected lambda expression"

let translateWhere = function
  | Lambda(var1, Call (None, op, [left; right])) ->
    match left, right with
    | PropertyGet(Some (Var var2), prop, []), Value(value, _) when 
        var1.Name = var2.Name && op.Name.StartsWith("op_") ->
        // We got 'where' that we understand. Build QueryCondition!
        { Property = prop.Name
          Operator = op.Name.Substring(3)
          Constant = value }
    | e -> 
      // 'where' with not supported format
      // (this can happen so report more useful error)
      failwithf 
        "%s\nGot: %A" 
        ( "Only expressions of the form " +
          "'p.Prop <op> <value>' are supported!") e

  // This should not happen - the parameter is always lambda!
  | _ -> failwith "Expected lambda expression"

let rec translateQuery e = 
  match e with
  // simpleq.SelectAttrs(<source>, fun p -> p.Name, p.Age)
  | SpecificCall <@@ simpleq.SelectAttrs @@> 
        (builder, [tTyp; rTyp], [source; proj]) -> 
      let q = translateQuery source
      let s = translateProjection proj
      { q with Select = Some(SelectAttributes s) }
  
  // simpleq.SelectCount(<source>)
  | SpecificCall <@@ simpleq.SelectCount @@> 
        (builder, [tTyp], [source]) -> 
      let q = translateQuery source
      { q with Select = Some SelectCount }

  // simpleq.Where(<source>, fun p -> p.Age > 10)
  | SpecificCall <@@ simpleq.Where @@> 
        (builder, [tTyp], [source; cond]) -> 
      let q = translateQuery source
      let w = translateWhere cond
      { q with Where = w :: q.Where }

  // simpleq.For(DB.People, <...>)
  | SpecificCall <@@ simpleq.For @@> 
        (builder, [tTyp; rTyp], [source; body]) -> 
      let source = 
        // Extract the table name from 'DB.People'
        match source with
        | PropertyGet(None, prop, []) when 
            prop.DeclaringType = typeof<DB> -> prop.Name
        | _ -> failwith "Only sources of the form 'DB.<Prop>' are supported!"
      { Source = source; Where = []; Select = None }

  // This should never happen
  | e -> failwithf "Unsupported query operation: %A" e


let q = 
  simpleq { 
    for p in DB.People do
    where (p.Age > 10)
    where (p.Name = "Tomas")
    selectCount }

q |> translateQuery