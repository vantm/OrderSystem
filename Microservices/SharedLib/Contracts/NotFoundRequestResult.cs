namespace SharedLib.Contracts;

public record NotFoundRequestResult : IRequestError
{
    public static readonly NotFoundRequestResult Instance = new();
}