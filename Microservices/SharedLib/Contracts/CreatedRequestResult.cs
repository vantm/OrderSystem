namespace SharedLib.Contracts;

public record CreatedRequestResult<T>(T Value)
    : IRequestResponse, IRequestResponse<T>;