using Microsoft.EntityFrameworkCore.Storage;
using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using HRMS.Infrastructure.Persistence.Repositories;
namespace HRMS.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly HrmsDbContext _context;
    private readonly Dictionary<string, object> _repositories = new();
    private IDbContextTransaction? _transaction;
    private bool _disposed = false;

    public UnitOfWork(HrmsDbContext context) { _context = context; }

    public IRepository<T> Repository<T>() where T : BaseEntity
    {
        var key = typeof(T).Name;
        if (!_repositories.ContainsKey(key))
            _repositories[key] = new GenericRepository<T>(_context);
        return (IRepository<T>)_repositories[key];
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return await _context.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditFields()
    {
        foreach (var entry in _context.ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case Microsoft.EntityFrameworkCore.EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case Microsoft.EntityFrameworkCore.EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        => _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null) { await _transaction.CommitAsync(cancellationToken); await _transaction.DisposeAsync(); _transaction = null; }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null) { await _transaction.RollbackAsync(cancellationToken); await _transaction.DisposeAsync(); _transaction = null; }
    }

    public void Dispose() { if (!_disposed) { _context.Dispose(); _disposed = true; } GC.SuppressFinalize(this); }
}
