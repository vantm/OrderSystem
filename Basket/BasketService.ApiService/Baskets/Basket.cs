using SharedLib.Domain;

namespace BasketService.ApiService.Baskets;

public class Basket : Entity
{
    private readonly List<BasketItem> _items = new();

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IEnumerable<BasketItem> Items => _items;

    public void SetItems(BasketItem[] items)
    {
        if (items.Length == 0)
        {
            return;
        }

        foreach (var item in items)
        {
            _items.RemoveAll(x => x.ProductId == item.ProductId);
            _items.Add(item);
        }

        UpdatedAt = DateTime.UtcNow;

        var domainEvent = new BaskItemAddedDomainEvent()
        {
            Id = Id, UserId = UserId, Items = items, UpdatedAt = UpdatedAt
        };

        AddDomainEvent(domainEvent);
    }

    public static Basket Create(Guid userId)
    {
        var basket = new Basket()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var domainEvent = new BasketCreatedDomainEvent()
        {
            Id = basket.Id,
            UserId = basket.UserId,
            CreatedAt = basket.CreatedAt
        };

        basket.AddDomainEvent(domainEvent);

        return basket;
    }
}