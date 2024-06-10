using DataAccess.Common;

namespace DataAccess.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T, TSearch> Repository<T, TSearch>() where T : BaseAuditableEntity where TSearch : class;
    Task<int> SaveAsync(CancellationToken cancellationToken);
    Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);
    Task Rollback();
}