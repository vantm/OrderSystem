using MediatR;

namespace InventoryService.ApiService.Batches.DomainEvents;

public record BatchUpdatedDomainEvent : INotification
{
    public required Guid Id { get; init; }
    public required Guid ProductId { get; init; }
    public required decimal PreviousPrice { get; init; }
    public required decimal Price { get; init; }
    public DateTime UpdatedAt { get; init; }
}