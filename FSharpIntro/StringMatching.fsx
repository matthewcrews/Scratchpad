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

               match nextSearchChar with
               | IsWhitespace ->
                  loop acc prevMatch cmdChars remainingSearchChars

               | IsChar ->
                  let nextMatchStatus, nextSearchTerms =
                     match prevMatch, nextCmdChar = nextSearchChar with
                     | MatchStatus.Found, true ->
                        MatchStatus.Found, remainingSearchChars
                     
                     | MatchStatus.Found, false ->
                        MatchStatus.NotEligible, searchChars

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

let commandName = "Auto Fit Column Width"
let searchTerm = "aficwid"
let resultStr =
   searchTerm
   |> searchCommandName commandName
   |> Array.map MatchStatus.asChar
   |> System.String


