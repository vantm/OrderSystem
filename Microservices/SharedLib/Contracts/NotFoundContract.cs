namespace SharedLib.Contracts;

public sealed class NotFoundContract
{
    public static readonly NotFoundContract Value = new();

    private NotFoundContract()
    {
    }
}