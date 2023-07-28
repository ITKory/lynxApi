namespace Application.Abstractions;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<ICollection<TEntity>> GetAllAsync();
    IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
    TEntity? FindById(int id);
    Task<TEntity?> CreateAsync(TEntity item);
    Task<TEntity> UpdateAsync(TEntity item);
    Task<TEntity> Remove(int id);
}
