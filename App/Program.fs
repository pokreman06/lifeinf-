open System.Collections.Immutable;
let living = ImmutableSortedSet<int*int>.Empty
living = living.Add (4,8)

printfn "%A" living