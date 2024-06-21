using Raiffeisen.RegistarMjenica.Api.Services.DataModels;

namespace Raiffeisen.RegistarMjenica.Api.Services.Interfaces;

public interface ITokenService
{
    Task<AccessToken> GetAccessToken();
}