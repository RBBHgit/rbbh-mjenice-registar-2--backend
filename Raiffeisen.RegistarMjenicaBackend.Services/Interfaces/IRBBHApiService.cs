using Raiffeisen.RegistarMjenicaBackend.Services.DataModels;

namespace Raiffeisen.RegistarMjenicaBackend.Services.Interfaces;

public interface IRBBHApiService
{
    Task<Client> GetClientInfo(string customerId);
    Task<List<Agreement>> GetClientAgreements(string customerId);
}