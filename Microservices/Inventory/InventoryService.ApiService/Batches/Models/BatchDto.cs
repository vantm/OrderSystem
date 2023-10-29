namespace InventoryService.ApiService.Batches;

public record BatchDto
{
    public required Guid Id { get; init; }
    public required Guid ProductId { get; init; }
    public required decimal Price { get; init; }
    public required int Quantity { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime UpdatedAt { get; init; }

    public static BatchDto FromEntity(Batch entity) =>
        new()
        {
            Id = entity.Id,
            ProductId = entity.ProductId,
            Price = entity.Price,
            Quantity = entity.Quantity,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
}