
namespace rec NamespaceA

module Animal =

    type Animal =
        | Chicken of string
        | Duck of string



namespace NamespaceB

module Animal =

    type Animal =
        | Chicken of int
        | Duck of float



    module MappingCode =
        let animalA = NamespaceA.Animal.Animal.Chicken "1"

        let ofDomainA (x: NamespaceA.Animal.Animal) =

            match x with
            | NamespaceA.Animal.Animal.Chicken s ->
                let newValue = int s
                NamespaceB.Animal.Animal.Chicken newValue

            | NamespaceA.Animal.Animal.Duck s ->
                let newValue = float s
                NamespaceB.Animal.Animal.Duck newValue