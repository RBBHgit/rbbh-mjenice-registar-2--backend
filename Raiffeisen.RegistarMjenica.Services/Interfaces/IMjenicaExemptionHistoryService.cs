using Raiffeisen.RegistarMjenica.Api.Services.DataModels;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels.SearchObjects;

namespace Raiffeisen.RegistarMjenica.Api.Services.Interfaces;

public interface IMjenicaExemptionHistoryService
{
    Task<bool> HasAnyAsync(int mjenicaId);
    Task<List<MjenicaHistoryGridModel>> Get(MjenicaHistorySearchObject search);
}