namespace TypedResponses

open System.Collections.Generic
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Http.HttpResults

type Completed() =
    static let instance = Completed
    static member Instance = instance

type Succeeded<'a>(value: 'a) =
    member s.Value = value

type ErrorOccurred(errorMessage: string) =
    member s.ErrorMessage = errorMessage

type ValidationFailed(errors: IDictionary<string, string[]>) =
    member s.Errors = errors

type EntityNotFound =
    class
    end

type ResponseValue =
    | NoContentOnly of Completed
    | BadRequestOnly of ErrorOccurred
    | ValidationProblemOnly of ValidationFailed
    | NotFoundOnly of EntityNotFound

module MapHelper =
    let constructMapResult
        response
        : Results<NoContent, BadRequest<string>, NotFound, ValidationProblem> =
        match response with
        | NoContentOnly _ -> TypedResults.NoContent()
        | BadRequestOnly err ->
            TypedResults.BadRequest<string>(err.ErrorMessage)
        | NotFoundOnly _ -> TypedResults.NotFound()
        | ValidationProblemOnly err ->
            TypedResults.ValidationProblem(err.Errors)
