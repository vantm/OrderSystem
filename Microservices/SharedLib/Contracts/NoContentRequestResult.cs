namespace SharedLib.Contracts;

public record NoContentRequestResult : IRequestResponse
{
    public static readonly NoContentRequestResult Instance = new();
}