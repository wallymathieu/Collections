namespace System.Collections.Generic
type CollectionElementMaybeChanged<'TExisting,'TUpdated> = { 
      Existing:'TExisting
      Updated:'TUpdated
    }
with
    override __.ToString() = sprintf "%A" __

type CollectionChanges<'TExisting,'TUpdated> = { 
    ToBeAdded : 'TUpdated array 
    ToBeRemoved : 'TExisting array 
    ToChange : CollectionElementMaybeChanged<'TExisting,'TUpdated> array
    } with
    override __.ToString() = sprintf "%A" __


open System
open System.Linq
open System.Collections.Generic

module Diff = 

    module private Dic =
        let fromSeq (map: 'a->'b) (list: 'a seq) = 
            list.ToDictionary(map, id)
        let get (hash:IDictionary<'a,'b>) (key:'a)=
            hash.[key]
        let tryGet (hash:IDictionary<'a,'b>) (key:'a)  =
            match hash.TryGetValue key with
            | true, v-> Some(v) 
            | false, _ -> None

    let changed (mapE:'a->'b) (existing:'a seq) (mapI:'c->'b) (incoming:'c seq) = 
        let setE = existing |> Seq.map mapE |> HashSet
        let setI = incoming |> Seq.map mapI |> HashSet

        let hashE = Dic.fromSeq mapE existing
        let hashI = Dic.fromSeq mapI incoming 

        let toAdd = setI.Except setE // Set.difference setI setE
        let toDel = setE.Except setI
        let toChange =  setI.Intersect setE

        {
            ToBeAdded = toAdd   |> Seq.toList |>  List.map (Dic.get hashI) |> List.toArray 
            ToBeRemoved = toDel |> Seq.toList |>  List.map (Dic.get hashE) |> List.toArray  
            ToChange = toChange |> Seq.toList |>  List.map (fun key -> { Updated= Dic.get hashI key; Existing= Dic.get hashE key}) |> List.toArray
        }

