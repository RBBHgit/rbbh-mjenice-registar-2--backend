using Raiffeisen.RegistarMjenica.Api.Services.DataModels;

namespace Raiffeisen.RegistarMjenica.Api.Services.Interfaces;

public interface IMjenicaJobService
{
    public Task<MjenicaJobResult> UpdateContractStatus(Dictionary<string, string> statusBatch);
   
}