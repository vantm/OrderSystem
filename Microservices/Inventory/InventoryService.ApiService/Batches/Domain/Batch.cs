using SharedLib.Domain;

namespace InventoryService.ApiService.Batches;

public class Batch : Entity
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public decimal Price { get; private set; }
    public int Quantity { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public void Adjust(int adjustedValue)
    {
        var previousQuantity = Quantity;

        Quantity += adjustedValue;
        UpdatedAt = DateTime.UtcNow;

        var domainEvent = new BatchAdjustedDomainEvent()
        {
            Id = Id,
            ProductId = ProductId,
            PreviousQuantity = previousQuantity,
            Quantity = Quantity,
            UpdatedAt = UpdatedAt
        };

        AddDomainEvent(domainEvent);
    }

    public void Update(decimal price)
    {
        var previousPrice = Price;

        Price = price;
        UpdatedAt = DateTime.UtcNow;

        var domainEvent = new BatchUpdatedDomainEvent()
        {
            Id = Id,
            ProductId = ProductId,
            PreviousPrice = previousPrice,
            Price = Price,
            UpdatedAt = UpdatedAt
        };

        AddDomainEvent(domainEvent);
    }

    public static Batch CreateForProduct(
        Guid productId,
        decimal price,
        int quantity)
    {
        var item = new Batch()
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Price = price,
            Quantity = quantity,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var domainEvent = new BatchCreatedDomainEvent()
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