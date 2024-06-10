using Raiffeisen.RegistarMjenicaBackend.Services.DataModels;
namespace Raiffeisen.RegistarMjenicaBackend.Services.Interfaces;

public interface ITokenService
{
    Task<AccessToken> GetAccessToken();
}