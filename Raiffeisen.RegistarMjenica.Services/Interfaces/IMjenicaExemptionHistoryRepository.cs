

using DataAccess.Entities;
using DataAccess.Interfaces.Repositories;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels.SearchObjects;

namespace Raiffeisen.RegistarMjenica.Api.Services.Interfaces;

public interface IMjenicaExemptionHistoryRepository : IGenericRepository<MjenicaExemptionHistory, MjenicaHistorySearchObject>
{
}