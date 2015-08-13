namespace Tests
open WhatIsUpdated
open NUnit.Framework
open FsUnit

[<TestFixture>] 
type ``Given an existing list with one element`` ()=
    let existing = [1]
    
    let id = fun x -> x

    [<Test>] member test.
     ``When there is an incoming element.`` ()=
            Diff.changed id existing (2::existing) |> should equal { ToBeAdded = [|2|]; ToBeRemoved = [||]; ToChange = [|(1,1)|]}

    [<Test>] member test.
     ``When there an empty incoming list`` ()=
            Diff.changed id existing [] |> should equal { ToBeAdded = [||]; ToBeRemoved = [|1|]; ToChange = [||]}

    [<Test>] member test.
     ``When incoming is the same as existing`` ()=
            Diff.changed id existing existing |> should equal { ToBeAdded = [||]; ToBeRemoved = [||]; ToChange = [|(1,1)|]}
        