namespace WhatIsUpdated
open System.Collections.Generic
open System.Linq

    module Dic =
        let fromSeq map list = 
            Enumerable.ToDictionary(list, System.Func<_,_>(map), System.Func<_,_>(id))
        let get (hash:IDictionary<_,_>) key =
            hash.[key]
        let tryGet (hash:IDictionary<_,_>) key =
            if hash.ContainsKey key then Some(hash.[key]) else None



