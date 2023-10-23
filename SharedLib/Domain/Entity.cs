using System.Text.Json.Serialization;

using MediatR;

namespace SharedLib.Domain;

public abstract class Entity
{
    [JsonIgnore] private readonly List<INotification> _domainEvents = new();

    protected void AddDomainEvent(INotification domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public IEnumerable<INotification> GetDomainEvents()
    {
        return _domainEvents;
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}