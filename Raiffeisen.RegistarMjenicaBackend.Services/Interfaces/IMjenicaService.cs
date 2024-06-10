using Raiffeisen.RegistarMjenicaBackend.Services.DataModels;
using Raiffeisen.RegistarMjenicaBackend.Services.DataModels.SearchObjects;

namespace Raiffeisen.RegistarMjenicaBackend.Services.Interfaces;

public interface IMjenicaService
{
    Task<MjenicaModel> CreateAsync(MjenicaModel model);
    Task<MjenicaModel> UpdateAsync(MjenicaModel model);
    Task<MjenicaModel> GetByIdAsync(int id, bool asNoTracking = default, bool includeHistory = false);
    Task<MjenicaModel> VerifyMjenicaAsync(MjenicaModel mjenicaModel);
    Task<List<MjenicaGridModel>> GetGridAsync(MjenicaSearchObject mjenicaSearch = default);
    Task<MjenicaModel> ExemptMjenicaTransactionAsync(MjenicaModel model);
    Task<MjenicaModel> VerifyExemptionMjenicaAsync(MjenicaModel mjenicaModel);
    Task<List<MjenicaCard>> GetClientMjeniceAsync(string customerId);
    Task<int> CreateBatchAsync(List<MjenicaModel> models);
}