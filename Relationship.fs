module FSharpAutomatonymousDemo.Relationship

open Automatonymous

type Relationship() =
    [<DefaultValue>] val mutable private _name : string
    abstract member Name : string with get, set
    default this.Name with get() = this._name and set v = this._name <- v
    
    
    [<DefaultValue>] val mutable private _currentState : State
    abstract member CurrentState : State with get, set
    default this.CurrentState with get() = this._currentState and set v = this._currentState <- v
