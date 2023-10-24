using MediatR;

namespace BasketService.ApiService.Baskets;

public record BaskItemAddedDomainEvent : INotification
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required BasketItem[] Items { get; init; }
    public required DateTime UpdatedAt { get; init; }
}