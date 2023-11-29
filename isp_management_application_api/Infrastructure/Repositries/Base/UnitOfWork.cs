using Application.Abstractions.Repositories.Base;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositries.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IspManagementApplicationDbContext _dbContext;
        public IRepository<User, int> User { get; }
        public IRepository<Right, int> Right { get; }
        public IRepository<RoleRight, int> RoleRight { get; }
        public IRepository<Role, int> Role { get; }
        public IRepository<AppFeature, int> AppFeature { get; }

        public UnitOfWork(IspManagementApplicationDbContext dbContext, IRepository<User, int> user, IRepository<Right, int> right, IRepository<RoleRight, int> roleRight, IRepository<Role, int> role, IRepository<AppFeature, int> appFeature)
        {
            _dbContext = dbContext;
            User = user;
            Right = right;
            RoleRight = roleRight;
            Role = role;
            AppFeature = appFeature;
        }

        private bool disposed;

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }
    }
}
