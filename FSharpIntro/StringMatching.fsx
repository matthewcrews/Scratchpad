
(* Possible Matches
a
f
c
afcw
aufc
afic
ficw
cowi
*)
let commandName = "Auto Fit Column Width"
//                 012345678901234567890
let searchTerm = "afcw"
// 1234567890


// type Match =
//    {
//       Type: MatchType
//    }

type NextPossible =
   {
      Char: char
      Index: int
   }

type PossibleNextResults =
   {
      Str: string
      MatchStatuses: MatchStatus[]
   }

module MatchStatuses =


   let compute (str: string) : MatchStatus[] =
      let result = Array.create m

      let _, _, results =
         ((' ', 0, []), str)
         ||> Seq.fold (fun (prevChar, index, accNextPossibles) nextChar ->
            if prevChar = ' ' then
               let newPossibleNext = {
                  Char = nextChar
                  Index = index
               }
               let accNextPossibles = newPossibleNext :: accNextPossibles
               (nextChar, index + 1, accNextPossibles)
            else
               (nextChar, index + 1, accNextPossibles)
            )

   


// let x = PossibleNextResults.init commandName
//
//
// let stringToCodes (s:string) =
//     s.ToUpper()
//     |> Seq.map (fun x -> int x)
//     |> Seq.toArray
//
// // let NumberOfCharacters (s:string) = s
// // let CharIndex = Seq.init (NumberOfCharacters(CommandName)) (fun x -> x + 1)
// (*
// MarkFirsts=LAMBDA(codes,LET(
//    n, SEQUENCE(ROWS(codes)),
//    lastmatch, MAX(FILTER(n,codes=-1,0)),
//    SCAN(0, n, LAMBDA(s,x, LET(
//       code, INDEX(codes,x),
//       IF(AND(OR(s = 32, s = 0, s = -1), code <> 32, code <> 0, code <> -1),
//          IF(x>lastmatch,1,2), // 1 is a first, 2 is eligible to match
//          IF(OR(code = 32, code = 0), 0, IF(code = -1, -1, 2))
//       )
//    )))
// ))
// *)
//
//
//
//
let MarkFirsts (codes:int[]) =
    let lastIndexOf = Array.tryFindIndexBack (fun x -> x = -1) codes

    codes
    |> Seq.scan (fun s x ->
        let code = codes.[x]
        if (s = 32 || s = 0 || s = -1) && code <> 32 && code <> 0 && code <> -1 then
            let isMatch =
               lastIndexOf
               |> Option.map (fun index -> x > index)
               |> Option.defaultValue true
            if isMatch then
               1 
            else
               2
        elif
         (code = 32 || code = 0) then
            0
        elif code = -1 then
         -1
        else
         2
        ) 0
// //
// (*
// PrepCommandNameForSearch=LAMBDA(CommandName,LET(
//    n, SEQUENCE(LEN(CommandName)), codes, CODE(UPPER(MID(CommandName, n, 1))), Result, MarkFirsts(codes), Result))(CommandName)
// *)
//
// (*
// FindNextMatch=LAMBDA(codes,before,SearchTerm,LET(
//    n, SEQUENCE(ROWS(before)),
//    target, CODE(UPPER(LEFT(SearchTerm, 1))),
//    match, IF((codes = target) * (before = 1), 1, 0),
//    matches, TAKE(FILTER(n, (codes = target) * (match = 1)),1),
//    after, IF(n = matches, -1, before),
//    firsts, MarkFirsts(after),
//    final, IF(SUM(match) = 0, NA(), IF(LEN(SearchTerm) = 1, firsts, FindNextMatch(codes, firsts, MID(SearchTerm, 2, LEN(SearchTerm))))),
//    final
// ))
// *)
//
// (*
// SearchCommandName=LAMBDA(CommandName,SearchTerm,LET(
//    n, SEQUENCE(LEN(CommandName)),
//    before, PrepCommandNameForSearch(CommandName),
//    char, UPPER(MID(CommandName, n, 1)),
//    code, CODE(char),
//    after, FindNextMatch(code, before, SearchTerm),
//    Result, IF(SearchTerm = "", "", IFNA(REDUCE("", after, LAMBDA(s,x, s & SWITCH(x, 1, 1, -1, "^", 0, "|", 0))), "No Match")),
//    Result
// ))
// *)

let (|IsWhitespace|IsChar|) (c: char) =
   if c = ' ' then
      IsWhitespace
   else
      IsChar


[<Struct; RequireQualifiedAccess>]
type MatchStatus =
    | Found
    | WhiteSpace
    | Eligible
    | NotEligible
    
module MatchStatus =

   let asChar (m: MatchStatus) =
      match m with
      | MatchStatus.Found -> '^'
      | MatchStatus.WhiteSpace -> '|'
      | MatchStatus.Eligible -> '?'
      | MatchStatus.NotEligible -> '0'

   //  | Char of int

let commandName = "Auto Fit Column Width"
let searchTerm = "afcw"

let searchCommandName (cmdName: string) (searchTerm: string) =

   let rec loop (acc: list<MatchStatus>) (prevMatch: MatchStatus) (cmdChars: list<char>) (searchChars: list<char>) =
      match cmdChars with
      | [] ->
         acc
         |> Array.ofList
         |> Array.rev

      | nextCmdChar::remainingCmdChars ->

         match nextCmdChar with
         | IsWhitespace ->
            let nextMatchStatus = MatchStatus.WhiteSpace
            let newAcc = nextMatchStatus :: acc
            loop newAcc nextMatchStatus remainingCmdChars searchChars

         | IsChar ->
            match searchChars with
            | [] ->
               let nextMatchStatus =
                  match prevMatch with
                  | MatchStatus.Found -> MatchStatus.Eligible
                  | MatchStatus.WhiteSpace -> MatchStatus.Eligible
                  | MatchStatus.Eligible -> MatchStatus.NotEligible
                  | MatchStatus.NotEligible -> MatchStatus.NotEligible
               let newAcc = nextMatchStatus :: acc
               loop newAcc nextMatchStatus remainingCmdChars searchChars

            | nextSearchChar::remainingSearchChars ->

               let nextMatchStatus, nextSearchTerms =
                  match prevMatch, nextCmdChar = nextSearchChar with
                  | MatchStatus.Found, true ->
                     MatchStatus.Found, remainingSearchChars
                  
                  | MatchStatus.Found, false ->
                     MatchStatus.Eligible, searchChars

                  | MatchStatus.WhiteSpace, true ->
                     MatchStatus.Found, remainingSearchChars

                  | MatchStatus.WhiteSpace, false ->
                     MatchStatus.NotEligible, searchChars

                  | MatchStatus.Eligible, _ ->
                     MatchStatus.NotEligible, searchChars

                  | MatchStatus.NotEligible, _ ->
                     MatchStatus.NotEligible, searchChars

               let newAcc = nextMatchStatus :: acc
               loop newAcc nextMatchStatus remainingCmdChars nextSearchTerms

   let cmdChars = List.ofSeq (cmdName.ToUpper())
   let searchTermChars = List.ofSeq (searchTerm.ToUpper())

   loop [] MatchStatus.WhiteSpace cmdChars searchTermChars




let result = searchCommandName commandName searchTerm

let resultStr =
   result
   |> Array.map MatchStatus.asChar
   |> System.String