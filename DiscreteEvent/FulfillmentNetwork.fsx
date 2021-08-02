module uom =
    [<Measure>] type Case

type ProductId = ProductId of string
type Product = {
    ProductId : ProductId
    CasePerPallet : int
    CasePerLayer : int
    LayerPerPallet : int
}
[<RequireQualifiedAccess>]
type InventoryIncrement =
    | Pallet
    | Layer
    | Case

type Case = {
    Product : Product
}
type Layer = {
    Cases : Case list
}
type Pallet = {
    Cases : Case list
}
type ResourceType = ResourceType of string
type Resource = Resource of string
type Location = Location of string

type ReplenishRequest = {
    Products : Map<Product, int<uom.Case>>
    Source : Location
    Destination : Location
}

type ReplenishFulfillment = {

}

type NodeId = NodeId of string
type Node = {
    NodeId : NodeId
    ReplenishSources : Map<InventoryIncrement, Node>
}


type State = {
    Inventory : Map<Location, Map<Product, int<uom.Case>>>
    ReplenishQueues : Map<
}