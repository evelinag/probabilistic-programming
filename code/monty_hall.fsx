
type Door = Goat | Car
type Game = Door []
type Strategy = Stay | Switch

let rnd = System.Random()
let shuffle a = a |> Array.sortBy (fun _ -> rnd.Next())

let generateGame () = [| Car; Goat; Goat |] |> shuffle

let playGame strategy =
    let game = generateGame()

    let initialDoor = rnd.Next(0,3)
    let hostDoor = 
        [|0; 1; 2|] 
        |> Array.filter (fun i -> i <> initialDoor && game.[i] <> Car)
        |> shuffle 
        |> Array.head
    let switchedDoor = 
        [|0; 1; 2|] 
        |> Array.filter (fun i -> i <> initialDoor && i <> hostDoor)
        |> Array.exactlyOne

    match strategy with
    | Stay -> game.[initialDoor] = Car
    | Switch -> game.[switchedDoor] = Car

let nSamples = 100
let ifStay = 
    [| for _ in 1..nSamples -> if playGame Stay then 1.0 else 0.0 |]
    |> Array.average

let ifSwitch = 
    [| for i in 1..nSamples -> if playGame Switch then 1.0 else 0.0 |]
    |> Array.average

printfn "Probability of winning if stay: %f" ifStay
printfn "Probability of winning if switch: %f" ifSwitch


// Monte Carlo sampling
// more samples lead to more precise estimation