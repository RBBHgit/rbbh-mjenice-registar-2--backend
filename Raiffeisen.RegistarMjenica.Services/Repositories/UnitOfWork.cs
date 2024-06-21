using System.Collections;
using DataAccess.Common;
using DataAccess.Interfaces.Repositories;
using Raiffeisen.RegistarMjenica.Services.Contexts;

namespace Raiffeisen.RegistarMjenica.Api.Services.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private Hashtable _repositories;
    private bool disposed;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IGenericRepository<T, TSearch> Repository<T, TSearch>() where T : BaseAuditableEntity where TSearch : class
    {
        if (_repositories == null)
            _repositories = new Hashtable();

        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<,>);

            var repositoryInstance =
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TSearch)), _dbContext) as
                    IGenericRepository<T, TSearch>;
            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T, TSearch>)_repositories[type];
    }

    public Task Rollback()
    {
        _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        return Task.CompletedTask;
    }

    public async Task<int> SaveAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
            if (disposing)
                _dbContext.Dispose();
        disposed = true;
    }
}