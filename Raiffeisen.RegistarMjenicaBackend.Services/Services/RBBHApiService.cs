using System.Net.Http.Headers;
using System.Web;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Raiffeisen.RegistarMjenicaBackend.Services.AppConfigs;
using Raiffeisen.RegistarMjenicaBackend.Services.DataModels;
using Raiffeisen.RegistarMjenicaBackend.Services.Exceptions;
using Raiffeisen.RegistarMjenicaBackend.Services.Interfaces;

namespace Raiffeisen.RegistarMjenicaBackend.Services.Services;

public class RBBHApiService : IRBBHApiService
{
     private readonly IOptions<RBBHApiConfig> _config;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IRfLogger _logger;
    private readonly ITokenService _tokenService;

    public RBBHApiService(IOptions<RBBHApiConfig> config, IHttpClientFactory httpClientFactory,
        ITokenService tokenService, IRfLogger logger)
    {
        _config = config;
        _httpClientFactory = httpClientFactory;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<Client> GetClientInfo(string customerId)
    {
        var clientsEndpoint = _config.Value.ClientsEndpoint;

        Client client;

        try
        {
            var httpClient = _httpClientFactory.CreateClient("RBBHApiClient");

            var url = $"{clientsEndpoint}?CusId={HttpUtility.UrlEncode(customerId)}";

            var token = await _tokenService.GetAccessToken();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(token.token_type, token.access_token);

            var response = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                client = JsonConvert.DeserializeObject<Client>(stringResponse);
            }
            else
            {
                throw new Exception(
                    $"Fetching client info from {clientsEndpoint} for client {customerId} resulted in {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting client info from {clientsEndpoint} for client {customerId}.");
            throw;
        }

        return client;
    }

    public async Task<List<Agreement>> GetClientAgreements(string customerId)
    {
        var agreementsEndpoint = _config.Value.AgreementsEndpoint;

        List<Agreement> agreements;

        try
        {
            var httpClient = _httpClientFactory.CreateClient("RBBHApiClient");

            var url = $"{agreementsEndpoint}?customerId={HttpUtility.UrlEncode(customerId)}";

            var token = await _tokenService.GetAccessToken();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(token.token_type, token.access_token);

            var response = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                agreements = JsonConvert.DeserializeObject<List<Agreement>>(stringResponse);
            }
            else
            {
                throw new Exception(
                    $"Fetching client agreements from {agreementsEndpoint} for client {customerId} resulted in {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting client agreements from {agreementsEndpoint} for client {customerId}.");
            throw;
        }

        return agreements;
    }
    
}