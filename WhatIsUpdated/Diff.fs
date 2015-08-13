namespace WhatIsUpdated
open System
open System.Linq
    module Diff = 
        
        (*
        let Changed<'t,'key> (map:('t->'key), existing:IEnumerable<'t>, incoming:IEnumerable<'t>): Updated<'t> = 
            failwith("!")
        *)
        let changed map existing incoming = 
            let mapWith = List.map map
            let (setE, setI) = (existing |> mapWith |> Set.ofList, incoming |> mapWith |>  Set.ofList)
            let toAdd = Set.difference setI setE
            let toDel = Set.difference setE setI
            let toChange = Set.intersect setI setE

            let get list key =
                list |> List.find (fun i -> map(i) = key)

            {ToBeAdded= toAdd |> Set.toList |>  List.map (get incoming) ;
             ToBeRemoved=toDel|> Set.toList |>  List.map (get existing) ; 
             ToChange=toChange |> Set.toList |>  List.map (get incoming)
             }


