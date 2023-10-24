namespace BasketService.ApiService.Baskets;

public record BasketDto(Guid Id, Guid UserId, IEnumerable<BasketItem> Items);