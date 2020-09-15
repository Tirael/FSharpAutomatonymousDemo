module FSharpAutomatonymousDemo.Person

type Person() =
    [<DefaultValue>] val mutable private _name : string
    abstract member Name : string with get, set
    default this.Name with get() = this._name and set v = this._name <- v
