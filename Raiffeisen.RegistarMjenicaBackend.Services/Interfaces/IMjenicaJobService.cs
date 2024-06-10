using Raiffeisen.RegistarMjenicaBackend.Services.DataModels;

namespace Raiffeisen.RegistarMjenicaBackend.Services.Interfaces;

public interface IMjenicaJobService
{
    public Task<MjenicaJobResult> UpdateContractStatus(Dictionary<string, string> statusBatch);
}