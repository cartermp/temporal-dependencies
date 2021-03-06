module Temporal.Core.Domain.Helpers

type DependencyList = ((string * string) * int) list

let split (predicate:string -> bool) =
    List.fold (fun acc x ->
        match x with
            | _ when predicate x -> [] :: acc
            | x' -> (x' :: List.head acc) :: List.tail acc 
    ) [[]]
    >> List.rev

let forAny = List.fold (||) false

let hasExtensions xs (s:string) =
    match xs with
    | [] -> false
    | _  -> forAny (List.map (s.EndsWith) xs)

let rec pair xs =
    match xs with
    | [] -> []
    | x :: xs' -> 
        List.fold (fun acc y -> (x,y) :: acc) [] xs'
        |> (fun r -> List.append r (pair xs'))
