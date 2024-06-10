using System.Linq.Expressions;
using DataAccess.Common;
using DataAccess.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Raiffeisen.RegistarMjenica.Services.Contexts;
using Raiffeisen.RegistarMjenicaBackend.Services.DataModels.SearchObjects;

namespace Raiffeisen.RegistarMjenicaBackend.Services.Repositories;

public class GenericRepository<T, TSearch> : IGenericRepository<T, TSearch>
    where T : BaseAuditableEntity where TSearch : BaseSearchObject
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<T> Entities => _dbContext.Set<T>();

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var exist = _dbContext.Set<T>().Find(entity.Id);

        if (exist == null) return Activator.CreateInstance<T>();

        _dbContext.Entry(exist).CurrentValues.SetValues(entity);
        await _dbContext.SaveChangesAsync();
        return entity;

    }

    public Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbContext
            .Set<T>()
            .ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id, bool useTracking = true, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (!useTracking) query = query.AsNoTracking();

        query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<int> AddBatchAsync(List<T> entities)
    {
        foreach (var entity in entities) await _dbContext.Set<T>().AddAsync(entity);

        return await _dbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateBatchAsync(List<T> entities)
    {
        T exist = null;
        foreach (var entity in entities)
        {
            exist = _dbContext.Set<T>().Find(entity.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
        }

        return await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<IEnumerable<T>> Get(TSearch search = null)
    {
        var entity = _dbContext.Set<T>().AsQueryable();

        entity = AddFilter(entity, search);

        entity = ApplySort(entity, search);

        entity = AddInclude(entity, search);

        if (search?.IsDistinct.HasValue == true && search?.IsDistinct.Value == true) entity = entity.Distinct();

        if (search.Page == 0) search.TotalRecord = entity.Count();

        if (search?.Skip.HasValue == true && search?.Take.HasValue == true && search.PageSize != 0)
        {
            search.TotalRecord = entity.Count();

            entity = entity.Skip(search.Skip.Value).Take(search.Take.Value);
        }

        var list = await entity?.ToListAsync();

        if (list is null) return new List<T>();
        return list;
    }

    public virtual async Task<T> GetSingleAsync(TSearch search = null)
    {
        var entity = _dbContext.Set<T>().AsQueryable();

        entity = AddFilter(entity, search);

        entity = AddInclude(entity, search);

        if (search?.IsDistinct.HasValue == true && search?.IsDistinct.Value == true) entity = entity.Distinct();
        var entityResult = await entity.SingleOrDefaultAsync();
        return entityResult;
    }


    public async Task<T> AddTransactionAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);

        return entity;
    }

    public async Task<T> UpdateTransactionAsync(T entity)
    {
        var exist = _dbContext.Set<T>().Find(entity.Id);

        if (exist == null) return Activator.CreateInstance<T>();

        _dbContext.Entry(exist).CurrentValues.SetValues(entity);

        return entity;

    }

    public virtual IQueryable<T> AddInclude(IQueryable<T> query, TSearch search = null)
    {
        return query;
    }

    public virtual IQueryable<T> AddFilter(IQueryable<T> query, TSearch search = null)
    {
        return query;
    }

    public virtual IQueryable<T> ApplySort(IQueryable<T> query, TSearch search = null)
    {
        return query;
    }
}