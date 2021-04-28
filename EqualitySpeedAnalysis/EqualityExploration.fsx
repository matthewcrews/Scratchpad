type Chicken = {
    Name : string
    Size : decimal
}

type Model (values: Map<int, Chicken>) =

    let chickens = values
    member _.Chickens = chickens

    override this.GetHashCode () =
        hash chickens

    override this.Equals (b) =
        match b with
        | :? Model as other -> this.Chickens = other.Chickens
        | _ -> false

let chickens =
    [1..10]
    |> List.map (fun i -> i, { Name = $"{i}"; Size = decimal i })
    |> Map

let model1 = Model chickens
let model2 = Model chickens

model1 = model2
chickens.[1] = chickens.[1]
chickens = chickens