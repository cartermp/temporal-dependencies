module Temporal.Core.Input.GitTransformer

open System
open Temporal.Core.Domain.Helpers

let newCommit (x:string) = x.StartsWith "new commit"

let ignoredString ignoredExtensions x =
    (String.IsNullOrWhiteSpace x || hasExtensions ignoredExtensions x)

let groupByCommit ignoredExtensions =
    List.filter (not << ignoredString ignoredExtensions)
    >> List.map (fun (s:string) -> s.Trim())
    >> split newCommit
    >> List.filter (not << List.isEmpty)
