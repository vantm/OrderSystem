using FluentValidation.Results;

namespace SharedLib.Contracts;

public record ValidationProblemRequestResult(
    IEnumerable<ValidationFailure> Errors) : IRequestError;