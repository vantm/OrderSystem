namespace SharedLib.Contracts;

public record OkRequestResult<T>(T Value)
    : IRequestResponse, IRequestResponse<T>;