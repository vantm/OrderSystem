using System.Collections.Frozen;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

using SharedLib.Contracts;

namespace SharedLib.Extensions;

public static class HttpResultExtensions
{
    public static CreatedAtRoute<T> ToHttpResult<T>(
        this CreatedRequestResult<T> result,
        string routeName,
        object routeValues) =>
        TypedResults.CreatedAtRoute(
            result.Value,
            routeName,
            routeValues);

    public static NoContent ToHttpResult(this NoContentRequestResult result) =>
        TypedResults.NoContent();

    public static NotFound ToHttpResult(this NotFoundRequestResult result) =>
        TypedResults.NotFound();

    public static Ok<T> ToHttpResult<T>(this OkRequestResult<T> result) =>
        TypedResults.Ok(result.Value);

    public static ProblemHttpResult ToHttpResult(
        this ProblemRequestResult result) =>
        TypedResults.Problem(
            detail: result.Detail,
            title: result.Title,
            type: result.Type,
            instance: result.Instance,
            statusCode: 400);

    public static ValidationProblem ToHttpResult(
        this ValidationProblemRequestResult result)
    {
        var errors = result.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                x => x.Key,
                g => g.Select(x => x.ErrorMessage).ToArray())
            .ToFrozenDictionary();

        return TypedResults.ValidationProblem(errors);
    }
}