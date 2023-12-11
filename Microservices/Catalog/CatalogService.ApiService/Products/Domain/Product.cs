using System.Text;

using SharedLib.Domain;

namespace CatalogService.ApiService.Products.Domain;

public class Product : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public void Delete()
    {
        IsDeleted = true;

        var domainEvent = new ProductDeletedDomainEvent()
        {
            Id = Id, Name = Name, DeletedAt = DateTime.UtcNow
        };

        AddDomainEvent(domainEvent);
    }

    public void Update(string name)
    {
        var previousName = Name;

        Name = name;

        UpdatedAt = DateTime.UtcNow;

        var domainEvent = new ProductUpdatedDomainEvent()
        {
            Id = Id,
            PreviousName = previousName,
            Name = Name,
            UpdatedAt = UpdatedAt
        };

        AddDomainEvent(domainEvent);
    }

    public static Product Create(string name)
    {
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            Name = name,
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

        sb.Append("Product { ");

        const string fmt = "{0} = {1}, ";

        sb.AppendFormat(fmt, nameof(Id), Id);
        sb.AppendFormat(fmt, nameof(Name), Name);
        sb.AppendFormat(fmt, nameof(CreatedAt), CreatedAt);
        sb.AppendFormat(fmt, nameof(UpdatedAt), UpdatedAt);

        sb.Append(" }");

        return sb.ToString();
    }
}