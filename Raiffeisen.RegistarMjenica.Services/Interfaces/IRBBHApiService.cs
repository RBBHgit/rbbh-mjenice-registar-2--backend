using Raiffeisen.RegistarMjenica.Api.Services.DataModels;

namespace Raiffeisen.RegistarMjenica.Api.Services.Interfaces;

public interface IRBBHApiService
{
    Task<Client> GetClientInfo(string customerId);
    Task<List<Agreement>> GetClientAgreements(string customerId);
}