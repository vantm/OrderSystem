using System.Text;

using ProductService.ApiService.Products.DomainEvents;

using SharedLib.Domain;

namespace ProductService.ApiService.Products;

public class Product : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public byte[] Image { get; private set; } = Array.Empty<byte>();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public void Delete()
    {
        var domainEvent = new ProductDeletedDomainEvent()
        {
            Id = Id, Name = Name, DeletedAt = DateTime.UtcNow
        };

        AddDomainEvent(domainEvent);
    }

    public void Update(string name, byte[] image)
    {
        var previousName = Name;
        var isImageUpdated = image.SequenceEqual(Image);

        Name = name;
        if (isImageUpdated)
        {
            Image = image;
        }

        UpdatedAt = DateTime.UtcNow;

        var domainEvent = new ProductUpdatedDomainEvent()
        {
            Id = Id,
            PreviousName = previousName,
            IsImageUpdated = isImageUpdated,
            Name = Name,
            UpdatedAt = UpdatedAt
        };

        AddDomainEvent(domainEvent);
    }

    public static Product Create(string name, byte[] image)
    {
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Image = image,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var domainEvent = new ProductCreatedDomainEvent()
        {
            Id = product.Id,
            Name = product.Name,
            CreatedAt = product.CreatedAt,
        };

        product.AddDomainEvent(domainEvent);

        return product;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append("Product(");

        const string fmt = "{0}='{1}';";

        sb.AppendFormat(fmt, nameof(Id), Id);
        sb.AppendFormat(fmt, nameof(Name), Name);
        sb.AppendFormat(fmt, nameof(Image),
            $"bytearray(len:{Image.Length})");
        sb.AppendFormat(fmt, nameof(CreatedAt), CreatedAt);
        sb.AppendFormat(fmt, nameof(UpdatedAt), UpdatedAt);

        sb.Append(')');

        return sb.ToString();
    }
}