using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Abstractions.Repositories.Base;

public interface IUnitOfWork : IDisposable, ITransientService
{
    IRepository<Right, int> Right { get; }
    IRepository<RoleRight, int> RoleRight { get; }
    IRepository<Role, int> Role { get; }
    IRepository<User, int> User { get; }
    IRepository<AppFeature, int> AppFeature { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
