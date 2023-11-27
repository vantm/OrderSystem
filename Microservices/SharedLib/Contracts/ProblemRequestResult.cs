namespace SharedLib.Contracts;

public record ProblemRequestResult(
    string Title,
    string? Detail,
    string? Type,
    string? Instance) : IRequestError;