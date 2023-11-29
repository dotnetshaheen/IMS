using Application.Abstractions.Repositories.Base;
using Application.Abstractions.Services;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Repositries.Base;

public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
{
    private DbSet<TEntity> _table;
    private readonly ICurrentUserService _currentUser;
    public Repository(IspManagementApplicationDbContext context, ICurrentUserService currentUser)
    {
        _table = context.Set<TEntity>();
        _currentUser = currentUser;
    }
    public IQueryable<TEntity> Queryable => _table;

    public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _table.Where(predicate).ToListAsync();
    }

    public async Task<TEntity> FindAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await _table.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _table.FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> IsExsitAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _table.Where(predicate).AnyAsync();
    }

    public async Task AddAsync(TEntity entity, int isActive = 1)
    {
        PropertyInfo[] properties = entity.GetType().GetProperties();
        foreach (PropertyInfo property in properties)
        {
            if (property.Name.Equals("IsActive", StringComparison.OrdinalIgnoreCase))
            {
                property.SetValue(entity, isActive);
            }

            if (property.Name.Equals("IsDeleted", StringComparison.OrdinalIgnoreCase))
            {
                property.SetValue(entity, 0);
            }

            if (property.Name.Equals("CreationTime", StringComparison.OrdinalIgnoreCase))
            {
                property.SetValue(entity, DateTime.Now);
            }

            if (property.Name.Equals("CreatorUserId", StringComparison.OrdinalIgnoreCase))
            {
                property.SetValue(entity, _currentUser.UserId);
            }
        }

        await _table.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, int isActive = 1)
    {
        foreach (var entity in entities)
        {
            await AddAsync(entity, isActive);
        }
    }

    public void Update(TEntity entity)
    {
        _table.Update(entity);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            Update(entity);
        }
    }

    public void Delete(TEntity entity)
    {
        _table.Remove(entity);
    }

    public void SoftDelete(TEntity entity)
    {
        _table.Update(entity);
    }

    public void SoftDeleteRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            SoftDelete(entity);
        }
    }
}
