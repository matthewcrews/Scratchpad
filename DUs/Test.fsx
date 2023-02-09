type Chicken =
    {
        Breed: string
    }

type Cow =
    {
        Breed: string
    }

type AnimalType =
    | Chicken of Chicken
    | Cow of Cow

type Animal =
    {
        Name: string
        Type: AnimalType
    }




let myAnimalFunc (a: Animal) =
    match a with
    | Chicken c -> $"Chicken{c.Name}"
    | Cow c -> $"Cow{c.Name}"


// Option Type with Some and None cases
let myOption = Some 1
let emptyOption = None

let myResult = Result.Ok "It worked!"
let myBadResult = Result.Error "It didn't work :("


let myResultHandler (r: Result<_,_>) =
    match r with
    | Ok okMessage -> printfn okMessage
    | Error errMessage -> printfn errMessage


type InvalidEmail =
    {
        Payload: string
    }

type ValidEmail =
    {
        Payload: string
    }

type ContactInfo =
    // It is an invalid email
    | InvalidEmail of InvalidEmail
    // OR it's a valid email
    | ValidEmail of ValidEmail
    | PhoneNumber of string
    | AwesomePigeons of pigeonName: string
    | Unknown

let emailHandler (email: ContactInfo) =
    match email with
    | ValidEmail validEmail -> printfn $"Payload: {validEmail.Payload}"
    | InvalidEmail invalidEmail -> printfn $"Payload: {invalidEmail.Payload}"
    | PhoneNumber phoneNumber -> printfn $"Phone Number: {phoneNumber}"
    | AwesomePigeons pigeonName -> printfn $"Pigeon Name: {pigeonName}"
    | Unknown -> $"Unknown?"

// Discriminated Unions are the tool for Interfaces/Polymorphism

let sadHandler (contactInfo: ContactInfo) =
    match contactInfo with
    | ValidEmail validEmail -> printfn $"Payload: {validEmail.Payload}"
    // No, don't do this 😂
    | _ -> printfn "Catch call. Don't do this 😡"

let lessSadHandler (contactInfo: ContactInfo) =
    match contactInfo with
    | ValidEmail validEmail -> printfn $"Payload: {validEmail.Payload}"
    // Better
    | InvalidEmail _
    | PhoneNumber _
    | AwesomePigeons _
    | Unknown -> 
        printfn "It will pass code review now"






