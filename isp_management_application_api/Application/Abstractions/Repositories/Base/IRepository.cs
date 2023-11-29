using System.Linq.Expressions;

namespace Application.Abstractions.Repositories.Base;
public interface IRepository<TEntity, TId> where TEntity : class
{
    IQueryable<TEntity> Queryable { get; }

    Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity> FindAsync(TId id, CancellationToken cancellationToken = default);

    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    Task<bool> IsExsitAsync(Expression<Func<TEntity, bool>> predicate);

    Task AddAsync(TEntity entity, int isActive = 1);

    Task AddRangeAsync(IEnumerable<TEntity> entities, int isActive = 1);

    void Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entities);

    void Delete(TEntity entity);

    void SoftDelete(TEntity entity);

    void SoftDeleteRange(IEnumerable<TEntity> entities);
}
