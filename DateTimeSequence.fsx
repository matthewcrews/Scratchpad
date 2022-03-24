open System

let startDate = DateTime (2020, 1, 1)
let endDate = DateTime (2020, 1, 7)
let period = TimeSpan (1, 0, 0, 0)
// let x =
//     { startDate .. p .. endDate}

let seriesOfDateTime (period: TimeSpan) (startDate: DateTime) (endDate: DateTime) =
    startDate
    |> Seq.unfold (fun date ->
        if date <= endDate then
            Some (date, date + period)
        else
            None
    )

let days = 
    seriesOfDateTime period startDate endDate
    |> List.ofSeq