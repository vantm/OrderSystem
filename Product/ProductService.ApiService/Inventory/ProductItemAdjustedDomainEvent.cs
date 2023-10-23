using MediatR;

namespace ProductService.ApiService.Inventory;

public record ProductItemAdjustedDomainEvent : INotification
{
    public required Guid Id { get; init; }
    public required Guid ProductId { get; init; }
    public required int PreviousQuantity { get; init; }
    public required int Quantity { get; init; }
    public DateTime UpdatedAt { get; init; }
}