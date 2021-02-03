[<RequireQualifiedAccess>]
type JobType =
    | A
    | B
    | C

type Job = {
    Id : int
    JobType : JobType
    Size : float
} with
    override this.ToString() =
        $"Job_{this.Id}"

type Machine = {
    Id : int
    JobTypes : Set<JobType>
} with
    override this.ToString() =
        $"Machine{this.Id}"

let jobTypes =
    [|
        JobType.A
        JobType.B
        JobType.C
    |]

let jobTypeSets =
    [|
        Set jobTypes
        Set jobTypes.[1..]
        Set jobTypes.[..1]
    |]

let rng = System.Random(123)
let numberOfJobs = 20
let numberOfMachines = 5
let minJobSize = 1
let maxJobSize = 3
let maxWorkDifference = 2.0

let randomJobSize (rng: System.Random) =
    rng.Next(minJobSize, maxJobSize)
    |> float

let randomJobType (rng: System.Random) =
    jobTypes.[rng.Next(0, jobTypes.Length)]

let randomJobTypeSet (rng: System.Random) =
    jobTypeSets.[rng.Next(0, jobTypeSets.Length)]

let jobs =
    [1..numberOfJobs]
    |> List.map (fun id -> {
        Id = id
        JobType = randomJobType rng
        Size = randomJobSize rng
    })

fsi.ShowDeclarationValues <- false

let machines =
    [1..numberOfMachines]
    |> List.map (fun id -> {
        Id = id
        JobTypes = randomJobTypeSet rng
    })

module Map =

    let tryFindDefault (key: 'a) (defaultValue: 'v) (m: Map<'a, 'v>) =
        match Map.tryFind key m with
        | Some v -> v
        | None -> defaultValue


#r "nuget: Flips"

open Flips
open Flips.Types
open Flips.SliceMap

let jobsForJobType =
    jobs
    |> List.groupBy (fun job -> job.JobType)
    |> Map

let jobSizes =
    jobs
    |> List.map (fun job -> job, job.Size)
    |> SMap

let assignments =
    DecisionBuilder "Assignment" {
        for machine in machines do
        for jobType in machine.JobTypes do
        for job in Map.tryFindDefault jobType [] jobsForJobType ->
            Boolean
    } |> SMap3

let jobAssignmentConstraints =
    ConstraintBuilder "JobAssignment" {
        for job in jobs ->
            sum assignments.[All, All, job] == 1.0
    }

let maxWork = Decision.createContinuous "MaxWork" 0.0 infinity
let minWork = Decision.createContinuous "MinWork" 0.0 infinity

let maxWorkDifferenceConstraint =
    Constraint.create "MaxWorkDifference" (maxWork - minWork <== maxWorkDifference)

let maxWorkConstraints =
    ConstraintBuilder "MaxWork" {
        for machine in machines ->
            maxWork >== sum (assignments.[machine, All, All] .* jobSizes)
    }

let minWorkConstraints =
    ConstraintBuilder "MinWork" {
        for machine in machines ->
            minWork <== sum (assignments.[machine, All, All] .* jobSizes)
    }

let setups =
    DecisionBuilder "Setups" {
        for machine in machines do
        for jobType in jobTypes ->
            Boolean
    } |> SMap2

let setupConstraints =
    ConstraintBuilder "SetupRequired" {
        for machine in machines do
        for jobType in jobTypes ->
            sum (assignments.[machine, jobType, All]) <== (float numberOfJobs) * setups.[machine, jobType]
    }

let numberOfSetupsExpr = sum setups
let minSetupsObjective =
    Objective.create "MinSetups" Minimize numberOfSetupsExpr

let model =
    Model.create minSetupsObjective
    |> Model.addConstraints jobAssignmentConstraints
    |> Model.addConstraints maxWorkConstraints
    |> Model.addConstraints minWorkConstraints
    |> Model.addConstraint maxWorkDifferenceConstraint
    |> Model.addConstraints setupConstraints

let settings = { Settings.basic with MaxDuration = 60_000L }

let result = Solver.solve settings model

match result with
| Optimal solution ->

    // Get the assignments the solver is suggesting
    let machineAssignments =
        Solution.getValues solution assignments
        |> Map.filter (fun _ v -> v = 1.0)
        |> Map.toList
        |> List.map (fun ((machine, _, job), _) -> machine, job)
        |> List.sortBy (fun (machine, job) -> machine.Id, job.Id)
        |> List.groupBy fst
        |> List.map (fun (machine, jobs) -> machine, jobs |> List.map snd)

    printfn "Assignments:"
    for (machine, jobs) in machineAssignments do
        printfn $"Machine: {machine.Id}"
        for job in jobs do
            printfn $"\tJob: {job.Id}"


    // Calculate how much each machine is loaded
    let machineLoads =
        machineAssignments
        |> List.map (fun (machine, jobs) -> machine, jobs |> List.sumBy (fun j -> j.Size))

    printfn ""
    printfn "Machine Loading:"
    for (machine, load) in machineLoads do
        printfn $"Machine: {machine.Id} | Total Load: {load}"

    // Find the min and max loads and calculate the difference
    let maxDifference =
        let loads = machineLoads |> List.map snd
        (List.max loads) - (List.min loads)

    printfn ""
    printfn $"Max Diffence In Loading: { maxDifference }"

| _ -> printfn "%A" result