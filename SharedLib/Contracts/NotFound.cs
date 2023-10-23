namespace SharedLib.Contracts;

public sealed class NotFound
{
    public static readonly NotFound Value = new();

    private NotFound()
    {
    }
}