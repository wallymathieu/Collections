namespace WhatIsUpdated
open System
open System.Linq
open System.Collections
open System.Collections.Generic

    module Diff = 
        
        let changed map existing incoming = 
            let mapWith = List.map map
            let (setE, setI) = (existing |> mapWith |> Set.ofList, incoming |> mapWith |>  Set.ofList)
              
            let (hashE, hashI) = ( Dic.fromSeq map existing, Dic.fromSeq map incoming )
            let toAdd = Set.difference setI setE
            let toDel = Set.difference setE setI
            let toChange = Set.intersect setI setE

            {
                ToBeAdded = toAdd   |> Set.toList |>  List.map (Dic.get hashI) |> List.toArray ;
                ToBeRemoved = toDel |> Set.toList |>  List.map (Dic.get hashE) |> List.toArray ; 
                ToChange = toChange |> Set.toList |>  List.map (fun key -> (Dic.get hashI key, Dic.get hashE key)) |> List.toArray
            }

        let Changed map existing incoming = 
            let m = FSharpFunc.FromConverter(map)
            changed m (existing |> List.ofSeq) (incoming |> List.ofSeq)