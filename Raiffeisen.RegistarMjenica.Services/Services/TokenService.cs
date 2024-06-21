using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Raiffeisen.RegistarMjenica.Api.Services.AppConfigs;
using Raiffeisen.RegistarMjenica.Api.Services.DataModels;
using Raiffeisen.RegistarMjenica.Api.Services.Exceptions;
using Raiffeisen.RegistarMjenica.Api.Services.Interfaces;

namespace Raiffeisen.RegistarMjenica.Api.Services.Services;

public class TokenService : ITokenService
{
    private const string AccessTokenCacheKey = "AccessToken";
    private static readonly SemaphoreSlim Semaphore = new(1, 1);
    private readonly IMemoryCache _cache;
    private readonly IOptions<RBBHApiConfig> _config;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IRfLogger _logger;
    private readonly int expirationTimeOffsetInSeconds = 30;

    public TokenService(IOptions<RBBHApiConfig> config, IHttpClientFactory httpClientFactory, IMemoryCache memoryCache,
        IRfLogger logger)
    {
        _config = config;
        _httpClientFactory = httpClientFactory;
        _cache = memoryCache;
        _logger = logger;
    }

    public async Task<AccessToken> GetAccessToken()
    {
        if (_cache.TryGetValue(AccessTokenCacheKey, out AccessToken cachedToken))
            return cachedToken;

        var token = await FetchAccessToken();

        CacheToken(token);

        return token;
    }

    private async Task<AccessToken> FetchAccessToken()
    {
        var authendpoint = _config.Value.AuthURL;

        AccessToken token;

        try
        {
            var httpClient = _httpClientFactory.CreateClient("RBBHApiClient");

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", _config.Value.ClientId),
                new KeyValuePair<string, string>("client_secret", _config.Value.ClientSecret),
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            var response = await httpClient.PostAsync(authendpoint, content);

            if (response.IsSuccessStatusCode)
                token = JsonConvert.DeserializeObject<AccessToken>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(
                    $"Fetching access token from {authendpoint} resulted in {response.StatusCode} - {response.ReasonPhrase}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting access token from {authendpoint}");
            throw;
        }

        return token;
    }

    private void CacheToken(AccessToken token)
    {
        try
        {
            Semaphore.Wait();

            var expirationTime = token.expires_in - expirationTimeOffsetInSeconds;

            if (expirationTime > 0)
                _cache.Set(AccessTokenCacheKey, token, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expirationTime)
                });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error caching access token");
        }
        finally
        {
            Semaphore.Release();
        }
    }
}