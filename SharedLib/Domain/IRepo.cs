namespace SharedLib.Domain;

public interface IRepo<T, TId> where T : Entity
{
    Task<T?> FindAsync(TId id, CancellationToken cancellationToken = default);
    Task InsertAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}