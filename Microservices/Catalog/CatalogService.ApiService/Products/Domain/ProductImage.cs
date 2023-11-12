namespace CatalogService.ApiService.Products.Domain;

public class ProductImage
{
    public Guid Id { get; private set; }
    public Guid ProductId { get; private set; }
    public byte[] Data { get; private set; } = Array.Empty<byte>();

    public static ProductImage Create(Guid productId, byte[] data)
    {
        var image = new ProductImage()
        {
            Id = Guid.NewGuid(), ProductId = productId, Data = data
        };

        return image;
    }
}