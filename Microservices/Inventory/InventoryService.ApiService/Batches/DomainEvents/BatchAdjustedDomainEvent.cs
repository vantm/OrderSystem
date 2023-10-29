using MediatR;

namespace InventoryService.ApiService.Batches;

public record BatchAdjustedDomainEvent : INotification
{
    public required Guid Id { get; init; }
    public required Guid ProductId { get; init; }
    public required int PreviousQuantity { get; init; }
    public required int Quantity { get; init; }
    public DateTime UpdatedAt { get; init; }
}