type Door = Goat | Car

type MontyHallValue = {
    Value: Door
    Probability : float  }

type Distribution = MontyHallValue seq

let uniformDistribution (values: Door seq) =
    let l = float (Seq.length values)
    values |> Seq.map (fun v -> {Value = v; Probability = 1.0/l})

let certainly value = seq [{Value = value; Probability = 1.0 }]

// ====================

type ProbabilisticComputation<'T> = 
  | Sample of Distribution * (Door -> ProbabilisticComputation<'T>)
  | Return of 'T   

type ProbabilisticComputationBuilder() = 
  member x.Return(v) = Return v
  member x.Bind(dist, f) = Sample(dist, f) 

let prob = ProbabilisticComputationBuilder()

// ====================
// Monty Hall models

let switch door = 
    match door with 
    | Car -> certainly Goat 
    | Goat -> certainly Car

let montyHallStay = prob {
    let! initialDoor = uniformDistribution [Car; Goat; Goat]
    return initialDoor
}

let montyHallSwitch = prob {
  let! initialDoor = uniformDistribution [Car; Goat; Goat]
  let! switchedDoor = switch initialDoor
  return switchedDoor
}

// ====================


let rec enumerate values comp = seq {
  match comp with
  | Sample(dist, f) ->
     for option in dist do
       yield! enumerate (option::values) (f option.Value)
  | Return v -> 
      // Print what's happening
      printfn "-----"
      values |> List.rev
      |> List.iteri (fun i mh -> printfn "%d:  %A (%f)" (i+1) mh.Value mh.Probability)

      yield v, 
        (1.0, values) 
        ||> List.fold (fun acc x -> acc * x.Probability)  }

let summarize dist =
  dist 
  |> Seq.groupBy fst
  |> Seq.map (fun (value, xs:('a * float) seq) -> 
      value, xs |> Seq.sumBy snd)
  |> List.ofSeq

let resultStay = montyHallStay |> enumerate [] |> summarize

let resultSwitch = montyHallSwitch |> enumerate [] |> summarize


