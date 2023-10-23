using MediatR;

namespace ProductService.ApiService.Inventory;

public record ProductCreatedDomainEvent : INotification
{
    public required Guid Id { get; init; }
    public required Guid ProductId { get; init; }
    public required decimal Price { get; init; }
    public required int Quantity { get; init; }
    public DateTime CreatedAt { get; init; }
}