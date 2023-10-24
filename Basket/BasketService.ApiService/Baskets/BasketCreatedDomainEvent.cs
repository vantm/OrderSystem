using MediatR;

namespace BasketService.ApiService.Baskets;

public record BasketCreatedDomainEvent : INotification
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required DateTime CreatedAt { get; init; }
}