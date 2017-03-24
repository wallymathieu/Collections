module Tests.``Given an existing list with one element``

open System
open System.Collections.Generic
open Xunit
let existing = [1]

let id = fun x -> x

[<Fact>] 
let ``When there is an incoming element.`` ()=
    Assert.Equal({ ToBeAdded = [|2|]; ToBeRemoved = [||]; ToChange = [|{ Updated=1;Existing=1 }|]},
        (Diff.changed id (existing|>List.toSeq) ((2::existing)|>List.toSeq) ))

[<Fact>] 
let ``When there an empty incoming list`` ()=
    Assert.Equal({ ToBeAdded = [||]; ToBeRemoved = [|1|]; ToChange = [||]},
        Diff.changed id existing [] ) 

[<Fact>]
let ``When incoming is the same as existing`` ()=
    Assert.Equal({ ToBeAdded = [||]; ToBeRemoved = [||]; ToChange = [|{Updated=1;Existing=1}|]},
        Diff.changed id existing existing )
        
