namespace WhatIsUpdated
open System
open System.Collections.Generic

    module Diff =
        val changed<'T, 'Key when 'Key : comparison> : ('T -> 'Key) -> 'T list -> 'T list -> Updated<'T>

        val Changed<'T, 'Key when 'Key : comparison> : Converter<'T,'Key> -> IEnumerable<'T> -> IEnumerable<'T> -> Updated<'T>