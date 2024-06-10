using Raiffeisen.RegistarMjenicaBackend.Services.DataModels;
using Raiffeisen.RegistarMjenicaBackend.Services.DataModels.SearchObjects;

namespace Raiffeisen.RegistarMjenicaBackend.Services.Interfaces;

public interface IMjenicaExemptionHistoryService
{
    Task<bool> HasAnyAsync(int mjenicaId);
    Task<List<MjenicaHistoryGridModel>> Get(MjenicaHistorySearchObject search);
}