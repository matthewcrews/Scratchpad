type Variable = Variable of string
type Coefficient = Coefficient of float
type Proportion = Proportion of float
type Label = Label of string
type Source = {
    Label : Label
}
type Sink = {
    Label : Label
}
type Conversion = {
    Label : Label
    Input : Variable
    Coefficient : Coefficient
    Output : Variable
}
type Merge = {
    Label : Label
    Inputs : Map<Variable, Proportion>
    Output : Variable   
}
type Split = {
    Label : Label
    Input : Variable
    Outputs : Map<Variable, Proportion>
}
type Tank = {
    Label : Label
    Inputss : Set<Variable>
    Accumulator : Variable
    Outputs : Set<Variable>
}
type Limit = {
    Label : Label
    Variable : Variable
    Value : float
}
type Model = {
    Conversions : Conversion list
    Merges : Merge list
    Splits : Split list
    Tanks : Tank list
    Limits : Limit list
}

module Variable =
    let create variable =
        if System.String.IsNullOrEmpty variable then
            invalidArg (nameof variable) $"Cannot create Variable from null or empty string"

        Variable variable

module Coefficient =
    let create coefficient =
        if coefficient <= 0.0 then
            invalidArg (nameof coefficient) $"Cannot have a Coefficient <= 0.0"

        Coefficient coefficient

module Proportion =
    let create proportion =
        if proportion <= 0.0 then
            invalidArg (nameof proportion) $"Cannot have a Proportion <= 0.0"

        Proportion proportion