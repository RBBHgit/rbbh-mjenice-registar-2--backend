using DataAccess.Entities;
using DataAccess.Interfaces.Repositories;
using Raiffeisen.RegistarMjenica.Services.Contexts;
using Raiffeisen.RegistarMjenicaBackend.Services.DataModels.SearchObjects;
using Raiffeisen.RegistarMjenicaBackend.Services.Interfaces;

namespace Raiffeisen.RegistarMjenicaBackend.Services.Repositories;

public class MjenicaExemptionHistoryRepository : GenericRepository<MjenicaExemptionHistory, MjenicaHistorySearchObject>,
    IMjenicaExemptionHistoryRepository
{
    private readonly IGenericRepository<MjenicaExemptionHistory, MjenicaHistorySearchObject> _repository;

    public MjenicaExemptionHistoryRepository(
        IGenericRepository<MjenicaExemptionHistory, MjenicaHistorySearchObject> repository,
        ApplicationDbContext dbContext) : base(dbContext)
    {
        _repository = repository;
    }

    public override IQueryable<MjenicaExemptionHistory> AddFilter(IQueryable<MjenicaExemptionHistory> query,
        MjenicaHistorySearchObject search = null)
    {
        var filteredQuery = base.AddFilter(query, search);

        if (search.MjenicaId.HasValue)
            filteredQuery = filteredQuery.Where(x => x.MjenicaId == search.MjenicaId.Value).AsQueryable();

        return filteredQuery;
    }
}