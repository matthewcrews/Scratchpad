open System

// [<RequireQualifiedAccess>]
type MatchStatus =
    | Found
    | WhiteSpace
    | EligibleNext
    | NotEligible

let rec FindNextMatch codes statuses (searchTerm: string) = 
    let n = [1..(List.length statuses)]
    let ismatch = 
        let target = searchTerm.[0] |> Char.ToUpper |> int
        List.map2 (fun x y -> (x = target) && (y = EligibleNext)) codes statuses
    let lastmatch = List.filter (fun x -> ismatch.[x - 1]) n |> List.head
    let updatematches = List.mapi (fun i x -> if x = lastmatch then Found else statuses.[i]) n
    let newstatuses =
        List.scan (fun s x ->
            let oldstatus = updatematches.[x - 1]
            if (s = WhiteSpace || s = Found) && oldstatus <> WhiteSpace && oldstatus <> Found then
                if x > lastmatch then EligibleNext else NotEligible
            elif oldstatus = WhiteSpace then WhiteSpace 
            elif oldstatus = Found then Found 
            else NotEligible) NotEligible n
    let final = 
        if List.forall (fun x -> x = false) ismatch then 
            None 
        elif (String.length searchTerm) > 1 then
            FindNextMatch codes newstatuses.[1..] searchTerm.[1..]
        else Some (newstatuses.[1..])
    final

[<Literal>]
let SPACE_CHAR_AS_INT = 32

let (|IsWhiteSpace|IsNonWhiteSpace|) (c: int) =
    if c = SPACE_CHAR_AS_INT then
        IsWhiteSpace
    else
        IsNonWhiteSpace

let SearchCommandName (commandName: string) (searchTerm: string) =
    let codes = 
        commandName.ToUpper() 
        |> Seq.map (fun x -> int x)
        |> Seq.toList
    // let init =
    //     (WhiteSpace, codes)
    //     ||> List.scan (fun prevMatchStatus nextCharCode ->
    //         if (prevMatchStatus = WhiteSpace || nextCharCode <> SPACE_CHAR_AS_INT) then
    //             EligibleNext
    //         elif nextCharCode = SPACE_CHAR_AS_INT then 
    //             WhiteSpace
    //         else
    //             NotEligible)

    // let init =
    //     (WhiteSpace, codes)
    //     ||> List.scan (fun prevMathStatus nextCharCode ->
    //         match prevMathStatus, nextCharCode with
    //         | WhiteSpace, x
    //         | _, x when x <> SPACE_CHAR_AS_INT ->
    //             EligibleNext
    //         | _, SPACE_CHAR_AS_INT ->
    //             WhiteSpace
    //         | _, _ ->
    //             NotEligible)

    let init =
        (WhiteSpace, codes)
        ||> List.scan (fun prevMathStatus nextCharCode ->
            match prevMathStatus, nextCharCode with
            | WhiteSpace, IsNonWhiteSpace ->
                EligibleNext
            | _, IsWhiteSpace ->
                WhiteSpace
            | _, IsNonWhiteSpace ->
                NotEligible)
                
    let results = FindNextMatch codes init.[1..] searchTerm
    match results with
    | Some x -> String.concat "" (Seq.map string (List.map (fun s -> match s with | Found -> '^' | WhiteSpace -> '|' | EligibleNext -> '1' | NotEligible -> '0') x ))
    | None -> "No Match."
    
let commandName = "Auto Fit Column Width"
let searchTerm = "aficwid"

printfn $"{commandName}"
let searchResult = SearchCommandName commandName searchTerm
printfn $"{searchResult}"