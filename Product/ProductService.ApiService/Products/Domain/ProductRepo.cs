using Dapper;

using MediatR;

using SharedLib.Data;
using SharedLib.Domain;

namespace ProductService.ApiService.Products;

public class ProductRepo(
        OpenDbConnection openConnection,
        IPublisher publisher,
        ILogger<ProductRepo> logger)
    : IRepo<Product, Guid>
{
    private async Task PublishDomainEvents(Product entity,
        CancellationToken cancellationToken)
    {
        var domainEvents = entity.GetDomainEvents().ToArray();

        if (domainEvents.Length == 0)
        {
            return;
        }

        var tasks = domainEvents
            .Select(domainEvent =>
                publisher.Publish(domainEvent, cancellationToken))
            .ToArray();

        await Task.WhenAll(tasks);

        entity.ClearDomainEvents();
    }

    public async Task<Product?> FindAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        using var conn = openConnection();

        const string sql = """
                           SELECT *
                           FROM product
                           WHERE id = @id
                           """;

        var param = new { id };

        logger.LogSql(sql, param);

        var product = await conn.QueryFirstOrDefaultAsync<Product>(sql, param);

        return product;
    }

    public async Task InsertAsync(Product entity,
        CancellationToken cancellationToken = default)
    {
        using var conn = openConnection();

        const string sql = """
                           INSERT INTO product (Id, Name, Image, CreatedAt, UpdatedAt)
                           VALUES (@Id, @Name, @Image, @CreatedAt, @UpdatedAt)
                           """;

        logger.LogSql(sql, entity);

        await conn.ExecuteAsync(sql, entity);

        await PublishDomainEvents(entity, cancellationToken);
    }

    public async Task UpdateAsync(Product entity,
        CancellationToken cancellationToken = default)
    {
        using var conn = openConnection();

        const string sql = """
                           UPDATE product
                           SET Name = @Name,
                               Image = @Image,
                               UpdatedAt = @UpdatedAt
                           WHERE Id = @Id
                           """;

        var param = new
        {
            entity.Id, entity.Name, entity.Image, entity.UpdatedAt
        };

        logger.LogSql(sql, param);

        await conn.ExecuteAsync(sql, entity);

        await PublishDomainEvents(entity, cancellationToken);
    }

    public async Task DeleteAsync(Product entity,
        CancellationToken cancellationToken = default)
    {
        using var conn = openConnection();

        const string sql = """
                           DELETE FROM product
                           WHERE Id = @id
                           """;

        var param = new { id = entity.Id };

        logger.LogSql(sql, param);

        await conn.ExecuteAsync(sql, entity);

        await PublishDomainEvents(entity, cancellationToken);
    }
}