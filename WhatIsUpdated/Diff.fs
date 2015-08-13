namespace WhatIsUpdated
open System
open System.Linq
open System.Collections
open System.Collections.Generic

    module Diff = 
        let toDic map list = 
            Enumerable.ToDictionary(list, System.Func<_,_>(map), System.Func<_,_>(id))
        let get (hash:IDictionary<_,_>) key =
            hash.[key]
        
        let changed map existing incoming = 
            let mapWith = List.map map
            let (setE, setI) = (existing |> mapWith |> Set.ofList, incoming |> mapWith |>  Set.ofList)
              
            let (hashE, hashI) = ( toDic map existing, toDic map incoming )
            let toAdd = Set.difference setI setE
            let toDel = Set.difference setE setI
            let toChange = Set.intersect setI setE

            {
                ToBeAdded = toAdd   |> Set.toList |>  List.map (get hashI) ;
                ToBeRemoved = toDel |> Set.toList |>  List.map (get hashE) ; 
                ToChange = toChange |> Set.toList |>  List.map (get hashI)
            }

        let Changed map existing incoming = 
            let m = FSharpFunc.FromConverter(map)
            changed m (existing |> List.ofSeq) (incoming |> List.ofSeq)