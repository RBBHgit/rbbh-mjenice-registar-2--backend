using System.Linq.Dynamic.Core;
using DataAccess.Responses;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels.SearchObjects;

namespace Raiffeisen.RegistarMjenica.Api.Services.Interfaces;

public interface IMjenicaService
{
    Task<MjenicaModel> CreateAsync(MjenicaModel model, string loggedInUser);
    Task<MjenicaModel> UpdateAsync(MjenicaModel model, string username);
    Task<MjenicaModel> GetByIdAsync(int id, bool asNoTracking, bool includeHistory = false);
    Task<MjenicaModel> VerifyMjenicaAsync(MjenicaModel mjenicaModel, string loggedInUser);
    Task<PagedResponse<MjenicaGridModel>> GetGridAsync(MjenicaSearchObject mjenicaSearch = default);
    Task<MjenicaModel> ExemptMjenicaTransactionAsync(MjenicaModel model, string username);
    Task<MjenicaModel> VerifyExemptionMjenicaAsync(MjenicaModel mjenicaModel, string username);
    Task<List<MjenicaCard>> GetClientMjeniceAsync(string customerId);
    Task<int> CreateBatchAsync(List<MjenicaModel> models);
}