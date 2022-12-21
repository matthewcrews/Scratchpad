[<Struct>]
type Range<[<Measure>] 'Measure> =
    {
        Start : int<'Measure>
        // This is the EXCLUSIVE upper bound
        Bound : int<'Measure>
    }
     
type RangeSeries<[<Measure>] 'Measure> = Range<'Measure>[]
type PointSeries<[<Measure>] 'Measure> = int<'Measure>[]

[<Struct>]
type Series<[<Measure>] 'Measure> =
    | Range of ranges: Range<'Measure>[]
    | Point of points: int<'Measure>[]


[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Series =
    
    let all<[<Measure>] 'Measure> (length: int) =
        Series.Range [| { Start = LanguagePrimitives.Int32WithMeasure<'Measure> 0; Bound = LanguagePrimitives.Int32WithMeasure<'Measure> length } |]
    
    let empty<[<Measure>] 'Measure> : Series<'Measure> =
        Series.Point Array.empty