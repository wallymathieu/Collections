module Tests.``Given an existing list with one element``

open System
open System.Collections.Generic
open Xunit

[<Fact>] 
let ``When there is an incoming element.`` ()=
    let existing = [1]
    Assert.Equal({ ToBeAdded = [|2|]; ToBeRemoved = [||]; ToChange = [|{ Updated=1;Existing=1 }|]},
        (Diff.changed id (existing|>List.toSeq) id ((2::existing)|>List.toSeq) ))

[<Fact>] 
let ``When there an empty incoming list`` ()=
    let existing = [1]
    Assert.Equal({ ToBeAdded = [||]; ToBeRemoved = [|1|]; ToChange = [||]},
        Diff.changed id existing id [] ) 

[<Fact>]
let ``When incoming is the same as existing`` ()=
    let existing = [1]
    Assert.Equal({ ToBeAdded = [||]; ToBeRemoved = [||]; ToChange = [|{Updated=1;Existing=1}|]},
        Diff.changed id existing id existing )
        
