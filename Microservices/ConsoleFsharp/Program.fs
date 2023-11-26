type IOutput =
    interface
    end

type Succeed =
    inherit IOutput

type Succeed<'a>(value: 'a) =
    member s.Value = value

type NotFound =
    inherit IOutput

type ValidationFailed =
    inherit IOutput

let mapResult result =
    match result with
    | Succeed -> Typed
