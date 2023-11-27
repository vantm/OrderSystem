using FluentValidation.Results;

namespace SharedLib.Contracts;

public record RequestResults<T1, T2>(T1? Result1, T2? Result2)
    where T1 : class, IRequestResponse
    where T2 : class, IRequestError
{
    public static implicit operator RequestResults<T1, T2>(T1 result)
        => new(result, default!);

    public static implicit operator RequestResults<T1, T2>(T2 result)
        => new(default!, result);
}

public record RequestResults<T1, T2, T3>(T1? Result1, T2? Result2, T3? Result3)
    where T1 : class, IRequestResponse
    where T2 : class, IRequestError
    where T3 : class, IRequestError
{
    public static implicit operator RequestResults<T1, T2, T3>(T1 result)
        => new(result, default!, default!);

    public static implicit operator RequestResults<T1, T2, T3>(T2 result)
        => new(default!, result, default!);

    public static implicit operator RequestResults<T1, T2, T3>(T3 result)
        => new(default!, default!, result);
}

public record RequestResults<T1, T2, T3, T4>(
    T1? Result1, T2? Result2, T3? Result3, T4? Result4)
    where T1 : class, IRequestResponse
    where T2 : class, IRequestError
    where T3 : class, IRequestError
    where T4 : class, IRequestError

{
    public static implicit operator RequestResults<T1, T2, T3, T4>(T1 result)
        => new(result, default!, default!, default!);

    public static implicit operator RequestResults<T1, T2, T3, T4>(T2 result)
        => new(default!, result, default!, default!);

    public static implicit operator RequestResults<T1, T2, T3, T4>(T3 result)
        => new(default!, default!, result, default!);

    public static implicit operator RequestResults<T1, T2, T3, T4>(T4 result)
        => new(default!, default!, default!, result);
}

public static class RequestResults
{
    public static OkRequestResult<T> Ok<T>(T value) => new(value);

    public static NoContentRequestResult NoContent() =>
        NoContentRequestResult.Instance;

    public static CreatedRequestResult<T> Created<T>(T value) => new(value);

    public static NotFoundRequestResult NotFound() =>
        NotFoundRequestResult.Instance;

    public static ValidationProblemRequestResult ValidationProblem(
        IEnumerable<ValidationFailure> errors) => new(errors);

    public static ProblemRequestResult Problem(
        string title,
        string? detail,
        string? type,
        string? instance) => new(title, detail, type, instance);
}