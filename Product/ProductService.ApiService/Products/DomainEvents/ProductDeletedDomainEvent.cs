using MediatR;

namespace ProductService.ApiService.Products.DomainEvents;

public record ProductDeletedDomainEvent : INotification
{
    public required Guid Id { get; init; }
    public required string Name { get; init; } = string.Empty;
    public required DateTime DeletedAt { get; init; }
}