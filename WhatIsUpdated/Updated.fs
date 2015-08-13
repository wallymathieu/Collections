namespace WhatIsUpdated
    

    type Updated<'T> = { 
        ToBeAdded : 'T seq; 
        ToBeRemoved : 'T seq; 
        ToChange : ('T * 'T) seq 
        } with
        override __.ToString() = sprintf "%A" __

