let rec kahan_sum_aux (xs : float list) (sum : float) (c : float) =
  match xs with
  | [] -> sum
  | x::xs ->
  let y = x - c in
  let t = sum + y in
  let c = (t - sum) - y in
  kahan_sum_aux xs t c

let kahan_sum (xs : float list) =
  match xs with
  | [] -> 0.0
  | _ -> kahan_sum_aux xs 0.0 0.0

let xs = List.replicate 10 0.1 in
let error_sum = List.fold (+) 0.0 xs in
let compensated_sum = kahan_sum xs in

printfn $"error_sum: {error_sum} compensated_sum: {compensated_sum}"