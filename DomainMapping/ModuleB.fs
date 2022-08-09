module DomainMapping.ModuleB

module Trigger =
    
    type Trigger =
        | A of int
        | B of int
        
        
module Mapping =
    
    module Trigger =
        
        let ofDomainA (a: DomainMapping.ModuleA.Trigger.Trigger) =
            
            match a with
            | ModuleA.Trigger.Trigger.A s ->
                Trigger.Trigger.A (int s)
            | ModuleA.Trigger.Trigger.B s ->
                Trigger.Trigger.B (int s)