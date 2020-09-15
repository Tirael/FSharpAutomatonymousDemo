module FSharpAutomatonymousDemo.RelationshipStateMachine

open System.Diagnostics
open Automatonymous
open Person
open Relationship

type RelationshipStateMachine()  =
    inherit AutomatonymousStateMachine<Relationship>()

    do
        base.InstanceState(fun x -> x.CurrentState)
    
    [<DefaultValue>] val mutable private _friend : State
    [<DefaultValue>] val mutable private _enemy : State
    [<DefaultValue>] val mutable private _hello : Event
    [<DefaultValue>] val mutable private _pissOff : Event
    [<DefaultValue>] val mutable private _introduce : Event<Person>
    
    abstract member Friend : State with get, set
    default this.Friend with get() = this._friend and set v = this._friend <- v
    
    abstract member Enemy : State with get, set
    default this.Enemy with get() = this._enemy and set v = this._enemy <- v
    
    abstract member Hello : Event with get, set
    default this.Hello with get() = this._hello and set v = this._hello <- v
    
    abstract member PissOff : Event with get, set
    default this.PissOff with get() = this._pissOff and set v = this._pissOff <- v
    
    abstract member Introduce : Event<Person> with get, set
    default this.Introduce with get() = this._introduce and set v = this._introduce <- v
        
    member this.Initialization() =
        this.Event(fun ctx -> this.Hello)
        this.Event(fun ctx -> this.PissOff)
        this.Event(fun ctx -> this.Introduce)
        
        this.State(fun ctx -> this.Friend)
        this.State(fun ctx -> this.Enemy)
        
        this.Initially(this.When(this.Introduce)
                           .Then(fun ctx -> ctx.Instance.Name <- ctx.Data.Name)
                           .Then(fun ctx -> printfn "%s" ctx.Instance.Name)
                           .TransitionTo(this.Friend))
        
        this.During(this.Friend, this.When(this.PissOff)
                                     .Then(fun ctx -> ctx.Instance.Name <- ctx.Event.Name)
                                     .Then(fun ctx -> printfn "%s" ctx.Instance.Name)
                                     .TransitionTo(this.Enemy))
        
        this.During(this.Enemy, this.When(this.Hello)
                                    .Then(fun ctx -> ctx.Instance.Name <- ctx.Event.Name)
                                    .Then(fun ctx -> printfn "%s" ctx.Instance.Name)
                                    .TransitionTo(this.Friend))
