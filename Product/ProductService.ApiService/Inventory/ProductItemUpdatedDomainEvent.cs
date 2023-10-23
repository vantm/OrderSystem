using MediatR;

namespace ProductService.ApiService.Inventory;

public record ProductItemUpdatedDomainEvent : INotification
{
    public required Guid Id { get; init; }
    public required Guid ProductId { get; init; }
    public required decimal PreviousPrice { get; init; }
    public required decimal Price { get; init; }
    public DateTime UpdatedAt { get; init; }
}