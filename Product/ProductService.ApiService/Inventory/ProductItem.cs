using SharedLib.Domain;

namespace ProductService.ApiService.Inventory;

public class ProductItem : Entity
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public static ProductItem CreateForProduct(Guid productId, decimal price, int quantity)
    {
        var item = new ProductItem()
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Price = price,
            Quantity = quantity,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var domainEvent = new ProductCreatedDomainEvent()
        {
            Id = item.Id,
            ProductId = item.ProductId,
            Price = item.Price,
            Quantity = item.Quantity,
            CreatedAt = item.CreatedAt
        };

        item.AddDomainEvent(domainEvent);

        return item;
    }
}