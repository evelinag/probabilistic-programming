#r "../packages/MathNet.Numerics/lib/net40/MathNet.Numerics.dll"
#r "../packages/FSharp.Data/lib/net45/FSharp.Data.dll"

open System
open MathNet.Numerics
open FSharp.Data

type Value = float

type Distribution = 
  | Gaussian of mean:float * variance:float
  | Bernoulli of probability:float

type ProbabilisticComputation<'T> = 
  | Sample of Distribution * (Value -> ProbabilisticComputation<'T>)
  | Return of 'T   

type ProbabilisticComputationBuilder() = 
  member x.Return(v) = Return v
  member x.Bind(dist, f) = Sample(dist, f) 

let prob = ProbabilisticComputationBuilder()

//===============================================================  

let model = 
  prob {
    let! yearlySalary = Gaussian(20000.0, 10000.0)
    let! mistake = Bernoulli(0.5)

    if mistake = 1.0 then 
      return yearlySalary / 12.0
    else 
      return yearlySalary
  }  

//===============================================================  

let binSize = 1000.0

let roundToBin x =
  let m = x % binSize
  if m >= binSize/2.0 then // round up
    x + binSize - m
  else // round down
    x - m

// List of distributions with different parameters to explore
let varyParameters dist =
  match dist with
  | Gaussian(m, v) ->
     let standardDeviation = (new Distributions.Normal(m, v)).StdDev
     let lower, upper = m - 3.0*standardDeviation, m + 3.0*standardDeviation
     let step = (upper - lower)/25.0
     [ for mean in lower .. step .. upper do
        for var in 5000.0 .. 5000.0 .. 50000.0 do 
          yield Gaussian(mean, var**2.0) ]
  | Bernoulli(_) ->
      [ for pt in 0.0 .. 0.05 .. 1.0 do
          yield Bernoulli(pt) ]

// Discrete versions of the distributions - bin the support region of the distribution
// Returns discretized value and it's probability of occuring
let discretize dist =
  match dist with 
  | Bernoulli(prob) -> 
      [1.0, prob; 0.0, (1.0 - prob) ]
  | Gaussian(mean, var) -> 
      [ for x in 0.0 .. binSize .. 110000.0 -> 
        // CDF (cummulative density function):   p(x <= N(mean,var)) 
        x, Distributions.Normal.CDF(mean, sqrt(var), x)]
      |> List.windowed 2
      |> List.map (fun w -> 
          fst w.[1], snd w.[1] - snd w.[0])

// Go through a distribution and obtain the probability for individual bins
// uses the computational expression to go through the distributions in our model
let rec enumerate dists choices model = seq {
  match model with
  | Sample(dist, f) ->
     for dist in varyParameters dist do
       for v, p in discretize dist do 
         yield! enumerate (dist::dists) ((v,p)::choices) (f v)
  | Return v -> 
      yield List.rev dists, (v, List.rev choices) }

// Compute probability for discrete bins based on sampled distribution
let histograms = 
  enumerate [] [] model
  |> Seq.map (fun (dist, (value, trace)) ->
      dist, (roundToBin value, trace))
  |> Seq.distinct
  |> Seq.map (fun (dist, (value, trace)) -> 
    let logp = 
      trace 
      |> List.map (fun (_, p) ->  
        if p = 0.0 then 1e-10 else p)
      |> List.map log
      |> List.fold (+) 0.0
    dist, value, logp)
  |> Seq.groupBy (fun (dist, _, _) -> dist)
  |> Seq.map (fun (dist, values) ->
      dist, 
      values |> Seq.map (fun (_, v, p) -> v, p)
      |> Array.ofSeq
      |> Array.sortBy fst)
  |> Array.ofSeq  

// Compute likelihood of the data given the simulated histogram
let compareDataHistogram (dist, values) data =
  let histLogProbability = 
    values
    |> Array.append [| (0.0, 0.0) |]
    |> Array.windowed 2
    |> Array.map (fun bin ->
        let p = bin.[1] |> snd
        p, 
        data 
        |> Array.filter (fun x -> 
            x > fst bin.[0] && x <= fst bin.[1])
        |> Array.length
        |> float)
    |> Array.fold (fun pAcc (logp, x) -> x * logp + pAcc) 0.0
  dist, histLogProbability

let mostLikelyDistribution hists data =
  hists
  |> Array.Parallel.map (fun distributionHist -> compareDataHistogram distributionHist data)
  |> Array.sortByDescending snd
  |> Array.take 10


// ===================================================
// Apply to Stack Overflow data

let [<Literal>] file = __SOURCE_DIRECTORY__ + "/../data/developer_survey_2017/survey_results_public.csv"
type SOData = CsvProvider<file>

let stackoverflowSurvey = 
  let data = SOData.GetSample()
  data.Rows
  |> Seq.filter (fun row -> row.Country = "Poland" && not (Double.IsNaN(row.Salary))) 
  |> Seq.map (fun row -> row.Salary )
  |> Seq.toArray

stackoverflowSurvey |> Array.length

let soResults =
  mostLikelyDistribution histograms stackoverflowSurvey
  |> Array.head |> fst
