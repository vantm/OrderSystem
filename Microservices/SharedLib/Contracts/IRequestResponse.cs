namespace SharedLib.Contracts;

public interface IRequestResponse
{
}

public interface IRequestResponse<out T> : IRequestResponse
{
    T Value { get; }
}