open System
open System.IO

let part1 (numbers: int list): int =
    let sorted = List.sort numbers
    let set = Set.ofSeq numbers
    let n = List.find (fun x -> set.Contains (2020 - x)) sorted
    n * (2020 - n)

let part2 (numbers: int list): int =
    let set = Set.ofSeq numbers
    let indexed = List.indexed numbers
    let pairs =
        Seq.allPairs indexed indexed
        |> Seq.filter (fun (ia, ib) -> (fst ia) < (fst ib))
        |> Seq.map (fun (ia, ib) -> (snd ia), (snd ib))
    let (a, b) = Seq.find (fun (a, b) -> set.Contains (2020 - a - b)) pairs
    a * b * (2020 - a - b)

[<EntryPoint>]
let main argv =
    let numbers = File.ReadAllLines "day01.txt" |> Seq.map Int32.Parse |> List.ofSeq
    printfn "Part 1: %d" (part1 numbers)
    printfn "Part 2: %d" (part2 numbers)
    0
