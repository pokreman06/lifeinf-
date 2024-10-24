open System.Collections.Immutable;
let startBoard = ImmutableList<int*int>.Empty.Add(0,0).Add(0,1).Add(0,2).Add(1,2).Add(2,1)
let gens = 100
let delta = ImmutableList<int*int>.Empty.Add(1,1).Add(0,1).Add(-1,1).Add(1,0).Add(-1,0).Add(1,-1).Add(0,-1).Add(-1,-1)

let addtup ((x1, y1): int*int) ((x2, y2): int*int) = 
    (x1+x2, y1+y2)
 
let rec deltaTup ((x,y): int*int) ((set: ImmutableList<int*int>)) (n:int)=
    if (n=set.Count) then (ImmutableList<int*int>.Empty)
    else ((deltaTup (x,y) set (n+1)).Add(addtup (x,y) delta[n]))

let rec nextNeighbors (n:int) (set: ImmutableList<int*int>) =
    if (n=set.Count) then (ImmutableList<int*int>.Empty)
    else ((nextNeighbors (n+1) set ).AddRange(deltaTup set[n] delta 0))

let rec neighborCounts (set: ImmutableList<int*int>) =
    if (set.Count=0) then ImmutableList<int>.Empty
    else (neighborCounts (set.RemoveAll(fun x -> x=set[0]))).Add(set.FindAll(fun x -> x=set[0]).Count) 

let rec neighborlist (set: ImmutableList<int*int>) =
    if (set.Count=0) then ImmutableList<int*int>.Empty
    else (neighborlist (set.RemoveAll(fun x -> x=set[0]))).Add(set.FindAll(fun x -> x=set[0])[0]) 

let validate (current: ImmutableList<int*int>) (point: int*int) (count: int) =
    (count=3||(current.Contains(point)&&count=2))

let rec validateAll (n: int) (count: ImmutableList<int>) (neib: ImmutableList<int*int>) (current: ImmutableList<int*int>) =
    if (n=count.Count) then (ImmutableList<int*int>.Empty)
    else 
    let rest = (validateAll (n+1) count neib current)
    if (validate current neib[n] count[n]) then (rest.Add(neib[n])) else rest

   
let rec excecute (n: int) (current: ImmutableList<int*int>) =
    if (n=0) then current
    else 
    let neibors = nextNeighbors 0 current
    excecute (n-1) (validateAll 0 (neighborCounts neibors) (neighborlist neibors) current)
(excecute gens startBoard) |> Seq.iter (fun x -> printf "%A " x)
assert((neighborCounts startBoard).Count = (neighborlist startBoard).Count)
printfn "%A" "success"