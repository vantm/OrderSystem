using Dapper;

using InventoryService.ApiService.Batches.Domain;

using MediatR;

using SharedLib.Data;
using SharedLib.Domain;
using SharedLib.Logging;

namespace InventoryService.ApiService.Batches.Data;

public class BatchRepo(
    CreateDbConnection createConnection,
    IPublisher publisher,
    ILogger<BatchRepo> logger) : IRepo<Batch, Guid>
{
    private async Task PublishDomainEvents(Batch entity,
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

    public async Task<Batch?> FindAsync(Guid id,
        CancellationToken cancellationToken = default)
    {
        using var conn = createConnection();

        const string sql = """
                           SELECT *
                           FROM [Inventory].[Batch]
                           WHERE Id = @id
                           """;

        var param = new { id };

        logger.LogSql(sql, param);

        var batch = await conn.QueryFirstOrDefaultAsync<Batch>(sql, param);

        return batch;
    }

    public async Task InsertAsync(Batch entity,
        CancellationToken cancellationToken = default)
    {
        using var conn = createConnection();

        const string sql = """
                           INSERT INTO [Inventory].[Batch] 
                           (
                               Id,
                               ProductId, 
                               Price, 
                               Quantity, 
                               IsActive,
                               CreatedAt,
                               UpdatedAt
                           )
                           VALUES 
                           (
                               @Id,
                               @ProductId, 
                               @Price, 
                               @Quantity, 
                               @IsActive, 
                               @CreatedAt, 
                               @CreatedAt
                           )
                           """;

        logger.LogSql(sql, entity);

        await conn.ExecuteAsync(sql, entity);

        await PublishDomainEvents(entity, cancellationToken);
    }

    public async Task UpdateAsync(Batch entity,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Batch entity,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}