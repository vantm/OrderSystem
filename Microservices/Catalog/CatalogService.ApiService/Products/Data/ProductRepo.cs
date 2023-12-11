using CatalogService.ApiService.Products.Domain;
using CatalogService.ApiService.Products.Models;

using Dapper;

using SharedLib.Data;

namespace CatalogService.ApiService.Products.Data;

public class ProductRepo(
        CreateDbConnection createConnection,
        IPublisher publisher,
        ILogger<ProductRepo> logger)
    : IProductRepo
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
        using var conn = createConnection();

        const string sql = """
                           SELECT *
                           FROM [Catalog].[Product]
                           WHERE Id = @id
                           """;

        var param = new { id };

        logger.LogSql(sql, param);

        var product = await conn.QueryFirstOrDefaultAsync<Product>(sql, param);

        return product;
    }

    public async Task InsertAsync(Product entity,
        CancellationToken cancellationToken = default)
    {
        using var conn = createConnection();

        const string sql = """
                           INSERT INTO [Catalog].[Product] (Id, Name, CreatedAt, UpdatedAt)
                           VALUES (@Id, @Name, @CreatedAt, @UpdatedAt)
                           """;

        logger.LogSql(sql, entity);

        await conn.ExecuteAsync(sql, entity);

        await PublishDomainEvents(entity, cancellationToken);
    }

    public async Task UpdateAsync(Product entity,
        CancellationToken cancellationToken = default)
    {
        using var conn = createConnection();

        const string sql = """
                           UPDATE [Catalog].[Product]
                           SET Name = @Name,
                               UpdatedAt = @UpdatedAt
                           WHERE Id = @Id
                           """;

        var param = new { entity.Id, entity.Name, entity.UpdatedAt };

        logger.LogSql(sql, param);

        await conn.ExecuteAsync(sql, entity);

        await PublishDomainEvents(entity, cancellationToken);
    }

    public async Task DeleteAsync(Product entity,
        CancellationToken cancellationToken = default)
    {
        using var conn = createConnection();

        const string sql = """
                           UPDATE [Catalog].[Product]
                           SET [IsDeleted] = 1
                           WHERE [Id] = @id
                           """;

        var param = new { id = entity.Id };

        logger.LogSql(sql, param);

        await conn.ExecuteAsync(sql, entity);

        await PublishDomainEvents(entity, cancellationToken);
    }

    public async Task<IEnumerable<ProductDto>> QueryAsync(int offset, int limit,
        CancellationToken ct = default)
    {
        const string sql = """
                           SELECT *
                           FROM [Catalog].[Product]
                           WHERE [IsDeleted] = 0
                           ORDER BY [Id]
                           OFFSET @offset ROWS
                           FETCH NEXT @limit ROWS ONLY
                           """;
        using var conn = createConnection();

        var param = new { offset = offset, limit = limit };

        logger.LogSql(sql, param);

        var products = await conn
            .QueryAsync<ProductDto>(sql, param);

        return products;
    }

    public async Task<ProductDto?> QueryFindAsync(Guid id,
        CancellationToken ct = default)
    {
        const string sql = """
                           SELECT *
                           FROM [Catalog].[Product]
                           WHERE [Id] = @id AND [IsDeleted] = 0
                           """;

        using var conn = createConnection();

        var param = new { id = id };

        logger.LogSql(sql, param);

        var product =
            await conn.QueryFirstOrDefaultAsync<ProductDto>(sql, param);

        return product;
    }
}