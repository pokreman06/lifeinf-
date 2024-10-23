open System.Collections.Immutable;
let startBoard = Set<int*int> []

let delta = Set.empty.Add(1,1).Add(0,1).Add(-1,1).Add(1,0).Add(0,0).Add(-1,0).Add(1,-1).Add(0,-1).Add(-1,-1)

let addtup ((x1, y1): int*int) ((x2, y2): int*int) = 
    (x1+x2, y1+y2)
 
let rec deltaTup ((x,y): int*int) ((set: Set<int*int>)) n:int =
    (deltaTup (x,y) set
assert((3,4)=(addtup (1,1) (2,3)))

    