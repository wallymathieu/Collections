namespace WhatIsUpdate
open System
open System.Collections.Generic

    type Updated<'T> = { ToBeAdded : List<'T>; ToBeRemoved : List<'T>; ToChange : List<'T> } 

    module Diff =
        val changed<'T, 'Key> : ('T -> 'Key, IEnumerable<'T>, IEnumerable<'T>) -> Updated<'T>
