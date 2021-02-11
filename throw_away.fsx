fsi.ShowDeclarationValues <- false


// The Domain
[<RequireQualifiedAccess>]
type JobType =
    | A
    | B

type Job = {
    Id : int
    JobType : JobType
    Size : float
} with
    override this.ToString () =
        $"Job_{this.Id}"

type Machine = {
    Id : int
    JobTypes : Set<JobType>
} with
    override this.ToString () =
        $"Machine_{this.Id}"


// Set of JobTypes for iterating over and sampling from
let jobTypes = 
    [|
        JobType.A
        JobType.B
    |]

// Some theoretical JobTypeSets to be used in generating
// random Machines
let jobTypeSets =
    [|
        Set jobTypes
        Set jobTypes.[..0]
        Set jobTypes.[1..]
    |]

// Setting up parameters for the example
let rng = System.Random(123)
let numberOfJobs = 5_000
let numberOfMachines = 70
let minJobSize = 1
let maxJobSize = 3
let possibleJobSizes = 
    [|
        15.0
        30.0
        60.0
    |]
let maxWorkDifference = 240.0

let randomJobSize (rng: System.Random) =
    // rng.Next(minJobSize, maxJobSize)
    // |> float
    possibleJobSizes.[rng.Next(0, 2)]

let randomJobType (rng: System.Random) =
    jobTypes.[rng.Next(0, jobTypes.Length - 1)]

let randomJobTypeSet (rng: System.Random) =
    jobTypeSets.[rng.Next(0, jobTypeSets.Length - 1)]

module Map =

    // Useful when you want to look up a key in a Map but you want it to provide
    // a default value if the key is missing
    let tryFindDefault (key: 'a) (defaultValue: 'v) (m: Map<'a, 'v>) =
        match Map.tryFind key m with
        | Some v -> v
        | None -> defaultValue

// Create some examples jobs
let jobs =
    [1..numberOfJobs]
    |> List.map (fun id -> { 
        Id = id
        JobType = randomJobType rng
        Size = randomJobSize rng 
    })

// Create some test machines
let machines =
    [1..numberOfMachines]
    |> List.map (fun id -> {
        Id = id
        JobTypes = randomJobTypeSet rng
    })



#r "nuget: Flips"

open Flips
open Flips.Types
open Flips.SliceMap

// A Map from JobType to the Jobs which are of that type
let jobsForJobType =
    jobs
    |> List.groupBy (fun job -> job.JobType)
    |> Map

// A SliceMap where the key is a Job and the value is the length of the Job
let jobSizes =
    jobs
    |> List.map (fun job -> job, job.Size)
    |> SMap

// The Decisions which represent assigning a Job to a Machine
// The JobType index allows us to slice along the job type
// which is useful in some of the constraints
let assignments =
    DecisionBuilder "Assignment" {
        for machine in machines do
        for jobType in jobTypes do
        for job in Map.tryFindDefault jobType [] jobsForJobType ->
            Boolean
    } |> SMap3

// Each job must be assigned
let jobsAssignmentConstraints =
    ConstraintBuilder "JobAssignment" {
        for job in jobs ->
            sum assignments.[All, All, job] == 1.0
    }

// A Decision which is meant to represent the MaxWork value across all Machines
let maxWork = Decision.createContinuous "MaxWork" 0.0 infinity
// A Decision which is meant to represent the MinWork value across all Machines
let minWork = Decision.createContinuous "MinWork" 0.0 infinity

// We constrain the difference between the most heavily loaded machine
// and the least loaded
let maxWorkDifferenceConstraint =
    Constraint.create "MaxWorkDifferent" (maxWork - minWork <== maxWorkDifference)


// The maxWork Decision must be greater or equal to all of the total work
// for each Machine
let maxWorkConstraints =
    ConstraintBuilder "MaxWork" {
        for machine in machines ->
            maxWork >== sum (assignments.[machine, All, All] .* jobSizes)
    }

// The minWork Decision must be less or equal to all of the total work
// for each Machine
let minWorkConstraints =
    ConstraintBuilder "MinWork" {
        for machine in machines ->
            minWork <== sum (assignments.[machine, All, All] .* jobSizes)
    }

// A Decision which indicates whether we setup a given Machine for a 
// JobType at any point
let setups =
    DecisionBuilder "Setups" {
        for machine in machines do
        for jobType in jobTypes ->
            Boolean
    } |> SMap2

// We must turn the setups value for a given Machine and JobType to 1
// if we assign a Job of the given JobType to the Machine
let setupConstraints =
    ConstraintBuilder "SetupRequired" {
        for machine in machines do
        for jobType in jobTypes ->
            sum (assignments.[machine, jobType, All]) <== (float numberOfJobs) * setups.[machine, jobType]
    }


// An expression which is the sum of the Setups that will need to be performed
let numberSetupsExpression = sum setups
// We want to minimize the number of setups
let minSetupsObjective = Objective.create "MinSetups" Minimize numberSetupsExpression

// Compose the model
let model =
    Model.create minSetupsObjective
    |> Model.addConstraints maxWorkConstraints
    |> Model.addConstraints minWorkConstraints
    |> Model.addConstraint maxWorkDifferenceConstraint
    |> Model.addConstraints setupConstraints
    |> Model.addConstraints jobsAssignmentConstraints

// Give the solver plenty of time to find a solution
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
