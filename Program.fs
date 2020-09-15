open System.Threading
open Automatonymous
open FSharpAutomatonymousDemo
open Person
open Relationship
open RelationshipStateMachine

[<EntryPoint>]
let main argv =
    let relationship = Relationship()
    let machine = RelationshipStateMachine()
    machine.Initialization()
    
    let person = Person()
    person.Name <- "Joe"

    use cts = new CancellationTokenSource()

    let _ = 
        machine.RaiseEvent(relationship, machine.Introduce, person, cts.Token)
        |> Async.AwaitTask |> Async.RunSynchronously

    printfn "relationship.CurrentState: %s" relationship.CurrentState.Name

    let _ = 
        machine.RaiseEvent(relationship, machine.PissOff, person, cts.Token)
        |> Async.AwaitTask |> Async.RunSynchronously

    printfn "relationship.CurrentState: %s" relationship.CurrentState.Name

    let _ = 
        machine.RaiseEvent(relationship, machine.Hello, person, cts.Token)
        |> Async.AwaitTask |> Async.RunSynchronously

    printfn "relationship.CurrentState: %s" relationship.CurrentState.Name
    
    0 // return an integer exit code
