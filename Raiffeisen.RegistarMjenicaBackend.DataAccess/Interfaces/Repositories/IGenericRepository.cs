using System.Linq.Expressions;
using DataAccess.Common.Interfaces;

namespace DataAccess.Interfaces.Repositories;


public interface IGenericRepository<T, TSearch> where T : class, IEntity where TSearch : class
{
    IQueryable<T> Entities { get; }
    Task<T> GetByIdAsync(int id, bool useTracking = true, params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> AddBatchAsync(List<T> entities);
    Task<int> UpdateBatchAsync(List<T> entities);
    Task<IEnumerable<T>> Get(TSearch search = null);
    Task<T> GetSingleAsync(TSearch search = null);

    #region transactions

    Task<T> AddTransactionAsync(T entity);
    Task<T> UpdateTransactionAsync(T entity);

    #endregion
}